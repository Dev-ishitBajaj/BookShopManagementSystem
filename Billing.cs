using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BOOKSHOP
{
    public partial class Billing : Form
    {
        public Billing()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\91789\Documents\BookShopDB.mdf;Integrated Security=True;Connect Timeout=30");

        private void populate()
        {
            Con.Open();
            string query = "select * from BookTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookDGV.DataSource = ds.Tables[0];

            Con.Close();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void updateQuantity(int qty)
        {
            Con.Open();
            
            int left = stock - qty;
            string query = "update BookTbl set BQty='"+left+"' where BId='"+key+"';";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Order placed in Bill");
            Con.Close();
        }

        

        int n = 0,GrdTotal=0;
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            
            if(QtyTb.Text == "" || Convert.ToInt32(QtyTb.Text)>stock)
            {
                MessageBox.Show("Not Enough Stock");
            }
            else
            {
                int total = Convert.ToInt32(QtyTb.Text)*Convert.ToInt32(PriceTb.Text);
                GrdTotal += total;
                DataGridViewRow rowNew = new DataGridViewRow();
                rowNew.CreateCells(BillDGV);
                rowNew.Cells[0].Value = ++n;
                rowNew.Cells[1].Value = BTitle.Text;
                rowNew.Cells[2].Value = PriceTb.Text;
                rowNew.Cells[3].Value = QtyTb.Text;
                rowNew.Cells[4].Value = total;
                BillDGV.Rows.Add(rowNew);
                updateQuantity(Convert.ToInt32(QtyTb.Text));
                TotalLbl.Text = "Rs. "+GrdTotal.ToString();
                populate();
                ResetWithoutClient();
            }

            
        }
        
        private void Reset()
        {
            BTitle.Text = "";
            PriceTb.Text = "";
            QtyTb.Text = "";
            ClientNameTb.Text = "";
        }
        private void ResetWithoutClient()
        {
            BTitle.Text = "";
            PriceTb.Text = "";
            QtyTb.Text = "";
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application .Exit();
        }
        int key = 0,stock = 0;
        int pos = 100;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Book Shop",new Font("Century Gothic",12,FontStyle.Bold),Brushes.Red,new Point(60,20));
            e.Graphics.DrawString("ID PRODUCT PRICE QUANTITY TOTAL", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Red, new Point(26,40));
            foreach(DataGridViewRow row in BillDGV.Rows)
            {
                int prodId = Convert.ToInt32(row.Cells["Column1"].Value);
                String prodName = "" + row.Cells["Column2"].Value;
                int prodPrice = Convert.ToInt32(row.Cells["Column3"].Value);
                int prodQty = Convert.ToInt32(row.Cells["Column4"].Value);
                int tottal = Convert.ToInt32(row.Cells["Column5"].Value);
                e.Graphics.DrawString(""+prodId, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Blue, new Point(26,pos));
                e.Graphics.DrawString("" + prodName, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                e.Graphics.DrawString("" + prodPrice, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
                e.Graphics.DrawString("" + prodQty, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                e.Graphics.DrawString("" + tottal, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Blue, new Point(235, pos));
                pos = pos + 20;

            }
            e.Graphics.DrawString("Grand Total: Rs. "+GrdTotal, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Crimson, new Point(60,pos+50));
            e.Graphics.DrawString("************** Book Store **************" + GrdTotal, new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Crimson, new Point(40, pos + 85));
            BillDGV.Rows.Clear();
            BillDGV.Refresh();
            pos = 100;
            GrdTotal = 0;
        }

        

        private void PrintBtn_Click_1(object sender, EventArgs e)
        {
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
            if (ClientNameTb.Text == "")
            {
                MessageBox.Show("Select Client Name");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into BillTbl values('" + UsName.Text + "','" + ClientNameTb.Text + "','" + GrdTotal + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bill Saved Successfully");
                    Reset();
                    Con.Close();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
                    

                
            
            
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        
        private void Billing_Load(object sender, EventArgs e)
        {
            UsName.Text = Login.UserName;
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();

        }

        private void BookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BTitle.Text = BookDGV.SelectedRows[0].Cells[1].Value.ToString();
            //QtyTb.Text = BookDGV.SelectedRows[0].Cells[4].Value.ToString();
            PriceTb.Text = BookDGV.SelectedRows[0].Cells[5].Value.ToString();
            //            BAuth.Text = BookDGV.SelectedRows[0].Cells[2].Value.ToString();
            if (BTitle.Text == "")
            {
                key = 0;
                stock = 0;
            }
            else
            {
                key = Convert.ToInt32(BookDGV.SelectedRows[0].Cells[0].Value.ToString());
                stock = Convert.ToInt32(BookDGV.SelectedRows[0].Cells[4].Value.ToString());
            }
        }

        private void Resetbtn_Click(object sender, EventArgs e)
        {
            Reset();
        }
    }
}
