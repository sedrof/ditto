namespace JustNull
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;

    public class KageForm : Form
    {
        private IContainer components = null;
        private Button f8button;
        private Gastly gastly = new Gastly();
        private TextBox handlerBox;
        private int index;
        private string instructions = "";
        private RichTextBox instructionsDisplay;
        private TextBox instructionsField;
        private Jigglypuff jigglypuff = new Jigglypuff();
        private Point point = new Point();
        private bool running = false;
        private Button startButton;
        private Thread worker;
        private bool choosing_key = false;
        private Button button1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private bool chosen_key = false;

        public KageForm()
        {
            this.InitializeComponent();
            this.worker = new Thread(new ThreadStart(this.PerformMacro));
            this.worker.IsBackground = true;
            this.worker.Start();
            this.index = Program.dittos.Count;
            this.Text = "JustNull - " + this.index;
            this.instructionsDisplay.Hide();
            this.instructionsDisplay.Enabled = false;
            JustNull.InterceptKeys.OnKeyDown += new KeyEventHandler(onGlobalKeypress);
            JustNull.InterceptKeys.Start();
        }

        void onGlobalKeypress(object sender, KeyEventArgs e)
        {
            if (!chosen_key && !choosing_key)
                return;
            if (chosen_key && !choosing_key)
            {
                if (e.KeyCode.ToString().Equals(this.button1.Text))
                {
                    PlayPause();
                }
            }
            else if (!chosen_key && choosing_key)
            {
                this.button1.Text = e.KeyCode.ToString();
                choosing_key = false;
                chosen_key = true;
            }
            else if (chosen_key && choosing_key)
            {
                if (e.KeyCode.ToString().Equals(this.button1.Text))
                    return;

                this.button1.Text = e.KeyCode.ToString();
                choosing_key = false;
                chosen_key = true;
            }
        }

        void myKeyDown(object sender, KeyEventArgs e)
        {
            choosing_key = true;
        }
            

        private void Debug(string text)
        {
            this.startButton.Invoke((Action)(() => this.startButton.Text = text));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void f8button_Click(object sender, EventArgs e)
        {
            RegisterHotKey(base.Handle, 1, 0, 0x77);
            this.f8button.Enabled = false;
        }

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        private static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);
        private IntPtr GetActiveWindow() =>
            GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        private void getHwnd()
        {
            string str = "false";
            IntPtr activeWindow = this.GetActiveWindow();
            this.point = Cursor.Position;
            UnregisterHotKey(base.Handle, 1);
            try
            {
                this.handlerBox.Text = "Loading Handler...";
                str = "o_O cracked :)";
            }
            catch (Exception)
            {
                str = "false";
            }
            finally
            {
                if (str != "false")
                {
                    this.handlerBox.Text = "h: " + activeWindow.ToString() + " x:" + this.point.X.ToString() + " y:" + this.point.Y.ToString();
                    this.gastly.setWindow(activeWindow);
                    this.jigglypuff.setWindow(activeWindow);
                }
                else
                {
                    this.handlerBox.Text = "You don't have enough badges";
                }
                this.f8button.Enabled = true;
            }
        }

        public string GetInstructions() =>
            this.instructionsField.Text;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KageForm));
            this.startButton = new System.Windows.Forms.Button();
            this.instructionsField = new System.Windows.Forms.TextBox();
            this.handlerBox = new System.Windows.Forms.TextBox();
            this.f8button = new System.Windows.Forms.Button();
            this.instructionsDisplay = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Enabled = false;
            this.startButton.Location = new System.Drawing.Point(12, 12);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(215, 75);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // instructionsField
            // 
            this.instructionsField.Location = new System.Drawing.Point(14, 120);
            this.instructionsField.Multiline = true;
            this.instructionsField.Name = "instructionsField";
            this.instructionsField.Size = new System.Drawing.Size(214, 95);
            this.instructionsField.TabIndex = 1;
            this.instructionsField.TextChanged += new System.EventHandler(this.instructionsField_TextChanged);
            // 
            // handlerBox
            // 
            this.handlerBox.Enabled = false;
            this.handlerBox.Location = new System.Drawing.Point(13, 234);
            this.handlerBox.Name = "handlerBox";
            this.handlerBox.Size = new System.Drawing.Size(169, 20);
            this.handlerBox.TabIndex = 2;
            this.handlerBox.TextChanged += new System.EventHandler(this.handlerBox_TextChanged);
            // 
            // f8button
            // 
            this.f8button.Location = new System.Drawing.Point(187, 234);
            this.f8button.Name = "f8button";
            this.f8button.Size = new System.Drawing.Size(40, 23);
            this.f8button.TabIndex = 3;
            this.f8button.Text = "F8";
            this.f8button.UseVisualStyleBackColor = true;
            this.f8button.Click += new System.EventHandler(this.f8button_Click);
            // 
            // instructionsDisplay
            // 
            this.instructionsDisplay.Location = new System.Drawing.Point(13, 120);
            this.instructionsDisplay.Name = "instructionsDisplay";
            this.instructionsDisplay.Size = new System.Drawing.Size(214, 95);
            this.instructionsDisplay.TabIndex = 4;
            this.instructionsDisplay.Text = "";
            this.instructionsDisplay.TextChanged += new System.EventHandler(this.instructionsDisplay_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(136, 270);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.myKeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 275);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Button to start program:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(93, 257);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Optional";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 218);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(225, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Smash that F8 button go to game and click F8";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(169, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Charge your rage to this macro tab";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // KageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(239, 302);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.instructionsDisplay);
            this.Controls.Add(this.f8button);
            this.Controls.Add(this.handlerBox);
            this.Controls.Add(this.instructionsField);
            this.Controls.Add(this.startButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "KageForm";
            this.Text = "JustNull";
            this.Load += new System.EventHandler(this.KageForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void KageForm_Load(object sender, EventArgs e)
        {
        
        }

        private void instructionsDisplay_TextChanged(object sender, EventArgs e)
        {
        }

        private void instructionsField_TextChanged(object sender, EventArgs e)
        {
            this.instructions = this.instructionsField.Text.ToString();
        }

        private void Mock()
        {
            this.Debug("LOOOL");
        }

        private void newBtn_Click(object sender, EventArgs e)
        {
            Program.newDitto();
        }

        private void Pause()
        {
            this.startButton.Text = "Start";
            this.running = false;
            this.instructionsField.Show();
            this.instructionsDisplay.Hide();
        }

        private void PerformMacro()
        {
            while (true)
            {
                if (this.running)
                {
                    Action method = null;
                    this.instructions = this.GetInstructions();
                    string[] lines = this.instructions.Trim().Split(new char[] { '\n' });
                    int linecount = 0;
                    foreach (string str in lines)
                    {
                        linecount++;
                        if (method == null)
                        {
                            method = delegate {
                                this.instructionsDisplay.Clear();
                                int num = 0;
                                foreach (string tekstas in lines)
                                {
                                    num++;
                                    if (num == linecount)
                                    {
                                        int start = this.instructionsDisplay.TextLength;
                                        this.instructionsDisplay.AppendText(tekstas);
                                        int textLength = this.instructionsDisplay.TextLength;
                                        this.instructionsDisplay.Select(start, textLength);
                                        this.instructionsDisplay.SelectionColor = Color.Blue;
                                        this.instructionsDisplay.ScrollToCaret();
                                        this.instructionsDisplay.Select(0, 0);
                                    }
                                    else
                                    {
                                        this.instructionsDisplay.AppendText(tekstas);
                                    }
                                }
                            };
                        }
                        this.instructionsDisplay.BeginInvoke(method);
                        if (this.running)
                        {
                            string[] strArray = str.Trim().Split(new char[] { ' ' });
                            if (strArray[0] == "keypress")
                            {
                                this.gastly.KeyPress(strArray[1]);
                            }
                            if (strArray[0] == "rightclick")
                            {
                                if (strArray.Length == 1)
                                {
                                    this.gastly.RightClick(this.point);
                                }
                                else
                                {
                                    this.gastly.RightClick(new Point(int.Parse(strArray[1]), int.Parse(strArray[2])));
                                }
                            }
                            if (strArray[0] == "leftclick")
                            {
                                if (strArray.Length == 1)
                                {
                                    this.gastly.LeftClick(this.point);
                                }
                                else
                                {
                                    this.gastly.LeftClick(new Point(int.Parse(strArray[1]), int.Parse(strArray[2])));
                                }
                            }
                            if (strArray[0] == "startditto")
                            {
                                KageForm ditto = Program.dittos[int.Parse(strArray[1])];
                                ditto.BeginInvoke((Action)(() => ditto.Play()));
                            }
                            if (strArray[0] == "stopditto")
                            {
                                KageForm ditto = Program.dittos[int.Parse(strArray[1])];
                                ditto.BeginInvoke((Action)(() => ditto.Pause()));
                            }
                            if (strArray[0] == "wait")
                            {
                                int num = 0;
                                if (strArray.Length > 2)
                                {
                                    int x = int.Parse(strArray[1]);
                                    int y = int.Parse(strArray[2]);
                                    if (strArray.Length == 5)
                                    {
                                        num = this.timeString(strArray[4]);
                                    }
                                    bool flag = true;
                                    while (flag)
                                    {
                                        flag = this.jigglypuff.ExpectColor(new Point(x, y), strArray[3]);
                                        Thread.Sleep(100);
                                        if (strArray.Length == 5)
                                        {
                                            num -= 100;
                                            if (num <= 0)
                                            {
                                                flag = false;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    int num4 = this.timeString(strArray[1]) / 100;
                                    for (int i = 0; (i < num4) && this.running; i++)
                                    {
                                        this.gastly.Wait(100);
                                    }
                                }
                            }
                            if (strArray[0] == "closeclient")
                            {
                                this.gastly.Kill();
                            }
                            Thread.Sleep(100);
                        }
                    }
                }
                else
                {
                    this.gastly.Wait(100);
                }
            }
        }

        private void Play()
        {
            string[] strArray = this.instructionsField.Text.Trim().Split(new char[] { '\n' });
            List<string> list = new List<string>();
            foreach (string str in strArray)
            {
                if (str.Contains("#"))
                {
                    this.Text = this.Text.Replace("JustNull", str.Replace("#", ""));
                }
                else
                {
                    list.Add(str);
                }
            }
            this.instructionsField.Text = string.Join('\n'.ToString(), list.ToArray());
            this.startButton.Text = "Stop";
            this.running = true;
            this.instructionsField.Hide();
            this.instructionsDisplay.Show();
        }

        private void PlayPause()
        {
            if (this.running)
            {
                this.Pause();
            }
            else
            {
                this.Play();
            }
        }

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private void setGastly()
        {
        }

        public void SetInstructions(string instruction)
        {
            this.instructionsField.Text = instruction;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            this.PlayPause();
        }

        private int timeString(string masked)
        {
            if (masked.Contains("y"))
            {
                masked = masked.Replace("y", "");
                return (int.Parse(masked));
            }
            if (masked.Contains("s"))
            {
                masked = masked.Replace("s", "");
                return (int.Parse(masked) * 0x3e8);
            }
            if (masked.Contains("m"))
            {
                masked = masked.Replace("m", "");
                return (int.Parse(masked) * 0xea60);
            }
            return int.Parse(masked);
        }

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        protected override void WndProc(ref Message m)
        {
            if ((m.Msg == 0x312) && (m.WParam.ToInt32() == 1))
            {
                this.getHwnd();
            }
            base.WndProc(ref m);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public delegate void Action();

        private void handlerBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.startButton.Enabled)
                this.startButton.Enabled = true;
        }
    }
}