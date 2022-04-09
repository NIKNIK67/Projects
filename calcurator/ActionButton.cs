namespace calcurator
{
    public class ActionButton : Button
    { 
        public int PosX { get; set; }
        public int PosY { get; set;}
        public string Action { get; set; }
        private double widthArg = 0.2;
        private double heightArg = 0.2;
        private double offsetBottom = 0.2;
        private double offsetBorders = 0.1;
        public ActionButton(string action,int posX, int posY,Form form)
        {
            Action = action;
            PosX = posX;
            PosY = posY;
            this.Location = new Point(Convert.ToInt32((form.Size.Width * widthArg * posX)+(form.Size.Width*offsetBorders)), Convert.ToInt32((form.Size.Height * (heightArg * PosY)) + (form.Size.Height * offsetBottom)));
            this.Size = new Size(Convert.ToInt32(form.Size.Width * widthArg), Convert.ToInt32(form.Size.Height * heightArg));
            form.Controls.Add(this);
            this.Show();
            Text = action;
            Click += OnClick;

            
        }

        private void OnClick(object? sender, EventArgs e) => LineStringBuilder.AddSymbol(Action);
        
    }

}