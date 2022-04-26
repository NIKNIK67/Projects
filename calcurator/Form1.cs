namespace calcurator
{
    public class Form1 : Form
    {
        public TextBox box;
        public Form1()
        {
            InitializeComponent();
        }
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            LineStringBuilder.MainForm = this;
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(Convert.ToInt32(Screen.PrimaryScreen.Bounds.Width*0.25),Convert.ToInt32(Screen.PrimaryScreen.Bounds.Height*0.35) );
            this.Text = "Form1";
            box = new TextBox();
            box.Location = new Point(0, 0);
            box.Height = Convert.ToInt32(this.Size.Height * 0.2);
            box.Size = new Size(Size.Width, Convert.ToInt32(Size.Height * 0.2));
            box.MinimumSize = box.Size;
            Controls.Add(box);
            List<ActionButton> list = new List<ActionButton>()
            {
                new ActionButton("1",0,0,this),
                new ActionButton("2",1,0,this),
                new ActionButton("3",2,0,this),
                new ActionButton("+",3,0,this),
                new ActionButton("4",0,1,this),
                new ActionButton("5",1,1,this),
                new ActionButton("6",2,1,this),
                new ActionButton("-",3,1,this),
                new ActionButton("7",0,2,this),
                new ActionButton("8",1,2,this),
                new ActionButton("9",2,2,this),
                new ActionButton("*",3,2,this),
                new ActionButton("c",0,3,this),
                new ActionButton("0",1,3,this),
                new ActionButton(".",2,3,this),
                new ActionButton("=",0,4,4,this),
                new ActionButton("/",3,3,this),
            };
            Label label = new Label();
            label.Text = list.Count.ToString(); 
            Controls.Add(label);
            label.AutoSize = true;

        }
    }

}