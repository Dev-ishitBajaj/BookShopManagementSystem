namespace BOOKSHOP
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }

        private Label Title;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Splash));
            this.Title = new System.Windows.Forms.Label();
            this.MyProgress = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.PercentLbl = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.BackColor = System.Drawing.Color.Transparent;
            this.Title.Font = new System.Drawing.Font("Modern No. 20", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Title.ForeColor = System.Drawing.Color.Azure;
            this.Title.Location = new System.Drawing.Point(47, 184);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(653, 48);
            this.Title.TabIndex = 0;
            this.Title.Text = "Book Shop Management System";
            this.Title.Click += new System.EventHandler(this.label1_Click);
            // 
            // MyProgress
            // 
            this.MyProgress.Location = new System.Drawing.Point(12, 499);
            this.MyProgress.Name = "MyProgress";
            this.MyProgress.Size = new System.Drawing.Size(738, 10);
            this.MyProgress.TabIndex = 1;
            
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Modern No. 20", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.MintCream;
            this.label2.Location = new System.Drawing.Point(12, 446);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(201, 41);
            this.label2.TabIndex = 2;
            this.label2.Text = "Loading ...";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // PercentLbl
            // 
            this.PercentLbl.AutoSize = true;
            this.PercentLbl.BackColor = System.Drawing.Color.Transparent;
            this.PercentLbl.Font = new System.Drawing.Font("Modern No. 20", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.PercentLbl.ForeColor = System.Drawing.Color.MintCream;
            this.PercentLbl.Location = new System.Drawing.Point(219, 446);
            this.PercentLbl.Name = "PercentLbl";
            this.PercentLbl.Size = new System.Drawing.Size(37, 41);
            this.PercentLbl.TabIndex = 3;
            this.PercentLbl.Text = "%";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Splash
            // 
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(762, 530);
            this.Controls.Add(this.PercentLbl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MyProgress);
            this.Controls.Add(this.Title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Splash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Splash_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private ProgressBar MyProgress;
        private Label label2;
        private Label PercentLbl;
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.IContainer components;

        int startpos = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startpos += 1;
            MyProgress.Value = startpos;
            PercentLbl.Text = startpos + "%";
            if(MyProgress.Value == 100)
            {
                MyProgress.Value = 0;
                timer1.Stop();
                Login log = new Login();
                log.Show();
                this.Hide();
            }
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        
    }
}