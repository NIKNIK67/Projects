using System.Runtime.InteropServices;

namespace mines
{
    public class Form1 : Form
    {
        private TextBox? fieldWidth;
        private TextBox?fieldHeight;
        private TextBox? mines;
        public Form1()
        {
            InitializeComponent();
        }
        public void InitializeComponent()
        {
            //Win32.AllocConsole();
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width,Convert.ToInt32(Screen.PrimaryScreen.Bounds.Height*0.95));
            this.Text = "Mines";
            Label label = new Label();
            label.Text = "Enter field size";
            label.Location = new Point(Convert.ToInt32(this.Size.Width * 0.45), Convert.ToInt32(this.Size.Height * 0.3));
            label.AutoSize = true;
            this.Controls.Add(label);
            Label fieldWidthlabel = new Label();
            fieldWidthlabel.Text = "Enter field width";
            fieldWidthlabel.Location = new Point(Convert.ToInt32(this.Size.Width * 0.40), Convert.ToInt32(this.Size.Height * 0.35));
            fieldWidthlabel.AutoSize = true;
            this.Controls.Add(fieldWidthlabel);
            Label fieldHeightlabel = new Label();
            fieldHeightlabel.Text = "Enter field height";
            fieldHeightlabel.Location = new Point(Convert.ToInt32(this.Size.Width * 0.40), Convert.ToInt32(this.Size.Height * 0.45));
            fieldHeightlabel.AutoSize = true;
            this.Controls.Add(fieldHeightlabel);
            Label minesCountlabel = new Label();
            minesCountlabel.Text = "Enter mines count";
            minesCountlabel.Location = new Point(Convert.ToInt32(this.Size.Width * 0.40), Convert.ToInt32(this.Size.Height * 0.55));
            minesCountlabel.AutoSize = true;
            this.Controls.Add(minesCountlabel);
            fieldWidth = new TextBox();
            fieldWidth.Location = new Point(Convert.ToInt32(this.Size.Width * 0.40), Convert.ToInt32(this.Size.Height * 0.40));
            fieldWidth.Size = new Size(Convert.ToInt32(this.Size.Width * 0.20), Convert.ToInt32(this.Size.Height * 0.40));
            this.Controls.Add(fieldWidth);
            fieldHeight = new TextBox();
            fieldHeight.Location = new Point(Convert.ToInt32(this.Size.Width * 0.40), Convert.ToInt32(this.Size.Height * 0.50));
            fieldHeight.Size = new Size(Convert.ToInt32(this.Size.Width * 0.20), Convert.ToInt32(this.Size.Height * 0.40));
            this.Controls.Add(fieldHeight);
            mines = new TextBox();
            mines.Location = new Point(Convert.ToInt32(this.Size.Width * 0.40), Convert.ToInt32(this.Size.Height * 0.60));
            mines.Size = new Size(Convert.ToInt32(this.Size.Width * 0.20), Convert.ToInt32(this.Size.Height * 0.40));
            this.Controls.Add(mines);
            Button button = new Button();
            button.Location = new Point(Convert.ToInt32(this.Size.Width * 0.45), Convert.ToInt32(this.Size.Height * 0.65));
            button.Size = new Size(Convert.ToInt32(this.Size.Width * 0.10), Convert.ToInt32(this.Size.Height * 0.10));
            this.Controls.Add(button);
            button.Click += action;
        }

        private void action(object? sender, EventArgs e)
        {
            if (int.TryParse(mines?.Text, out int _mines) && int.TryParse(fieldWidth?.Text, out int _width) && int.TryParse(fieldHeight?.Text,out int _height) && _mines < _width*_height)
            {
                Game.Initialze(_mines, _width, _height, this);
            }
            else
            {
                MessageBox.Show("Enter right values in fields");
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private System.ComponentModel.IContainer components = null;
    }



}