namespace JustNull
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class KageControl : Form
    {
        private IContainer components = null;
        private Jigglypuff jigglypuff = new Jigglypuff();
        private Button loadButton;
        private Button newDittoButton;
        private OpenFileDialog openFileDialog1;
        private Button saveButton;
        private SaveFileDialog saveFileDialog1;
        private WebBrowser webBrowser1;
        private Label label1;
        private Timer xyTimer;

        public KageControl()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void KageControl_Load(object sender, EventArgs e)
        {
            //this.xyDisplay.Enabled = false;
            //this.xyColor.Enabled = false;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KageControl));
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.newDittoButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.xyTimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(199, 135);
            this.webBrowser1.TabIndex = 0;
            // 
            // newDittoButton
            // 
            this.newDittoButton.Location = new System.Drawing.Point(8, 8);
            this.newDittoButton.Name = "newDittoButton";
            this.newDittoButton.Size = new System.Drawing.Size(184, 62);
            this.newDittoButton.TabIndex = 1;
            this.newDittoButton.Text = "New JustNull";
            this.newDittoButton.UseVisualStyleBackColor = true;
            this.newDittoButton.Click += new System.EventHandler(this.newDittoButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(8, 77);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(91, 23);
            this.loadButton.TabIndex = 2;
            this.loadButton.Text = "Load JustNull";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(105, 76);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(87, 23);
            this.saveButton.TabIndex = 8;
            this.saveButton.Text = "Save JustNull";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk_1);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Happy new year my friends and leakers";
            // 
            // KageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(199, 135);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.newDittoButton);
            this.Controls.Add(this.webBrowser1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "KageControl";
            this.Text = "JustNull";
            this.Load += new System.EventHandler(this.KageControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = this.openFileDialog1.FileName;
                try
                {
                    string str2 = File.ReadAllText(fileName);
                    string[] separator = new string[] { Environment.NewLine + Environment.NewLine };
                    int num = 0;
                    foreach (string str3 in str2.Split(separator, (StringSplitOptions)num))
                    {
                        Program.newDitto().SetInstructions(str3);
                    }
                }
                catch (IOException)
                {
                }
            }
        }

        private void newDittoButton_Click(object sender, EventArgs e)
        {
            Program.newDitto();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            this.saveFileDialog1.DefaultExt = "JustNull";
            int num = (int)this.saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk_1(object sender, CancelEventArgs e)
        {
            string fileName = this.saveFileDialog1.FileName;
            string str2 = "";
            foreach (KageForm form in Program.dittos)
            {
                str2 = str2 + form.GetInstructions();
                str2 = str2 + Environment.NewLine + Environment.NewLine;
            }
            File.WriteAllText(fileName, str2.TrimEnd(new char[] { '\r', '\n', ' ' }));
        }

        /*private void xyBtn_Click(object sender, EventArgs e)
        {
            if (this.xyTimer.Enabled)
            {
                this.xyDisplay.Text = "";
                this.xyColor.BackColor = Color.White;
            }
            this.xyTimer.Enabled = !this.xyTimer.Enabled;
            this.xyDisplay.Enabled = !this.xyDisplay.Enabled;
            this.xyColor.Enabled = !this.xyDisplay.Enabled;
        }

        private void xyDisplay_TextChanged(object sender, EventArgs e)
        {
        }

        private void xyTimer_Tick(object sender, EventArgs e)
        {
            Color colorFromScreen = this.jigglypuff.GetColorFromScreen(Cursor.Position);
            this.xyColor.BackColor = colorFromScreen;
            this.xyDisplay.Text = Cursor.Position.X.ToString() + " " + Cursor.Position.Y.ToString() + " " + colorFromScreen.R.ToString() + "." + colorFromScreen.G.ToString() + "." + colorFromScreen.B.ToString();
        }*/
    }
}