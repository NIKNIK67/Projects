
namespace mines
{
    public class FieldButton : Field
    {
        public override int PosX { get ; set ; }
        public override int PosY { get; set; }
        public override bool IsFlaged { get; set; }
        public override List<FieldButton> Connections { get; set; }
        public override bool IsBomb { get; set; }
        public override bool IsOpend { get; set; }
        public override string state { get; set; }

        public FieldButton(int posX,int posY)
        {
            IsOpend = false;
            IsBomb = false;
            IsFlaged = false;
            PosX = posX;
            PosY = posY;
            Connections = new List<FieldButton>();
            Size = new Size(Convert.ToInt32(Game.MainForm?.Width * (0.8/Game.FieldWidth)), Convert.ToInt32(Game.MainForm?.Height * (0.8/Game.FieldHeight)));
            Location = new Point(Convert.ToInt32(Game.MainForm?.Width*(0.8 / Game.FieldWidth)*posX + (0.10 * Game.MainForm.Width)), Convert.ToInt32((Game.MainForm?.Height * (0.8 / Game.FieldHeight)*posY)+0.1*Game.MainForm.Height));
            Text = $"";
            Game.MainForm.Controls.Add(this);
            MouseClick += OnClick;
            Show();
        }

        private void OnClick(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && Control.ModifierKeys != Keys.Shift)
                OpenField(new List<FieldButton>());
            else if (e.Button == MouseButtons.Right && Control.ModifierKeys == Keys.Shift)
                PlaceFlag();
                
        }
        private void PlaceFlag()
        {
            if (!IsFlaged)
            {
                Game.FlagedButtons.Add(this);
                Game.FlagsLeft--;
                IsFlaged = true;
                state = Text;
                Text = "F";
            }
            else if (IsFlaged)
            { 
                Game.FlagedButtons.Remove(this);
                Game.FlagsLeft++;
                IsFlaged= false;
                Text = state;
            }
            
            
        }
        public void OpenField(List<FieldButton> checkedButtons)
        {
            
            if (!IsBomb && !IsOpend)
            {
                int k = 0;
                foreach (FieldButton button in Connections)
                {
                    if (button.IsBomb)
                    {
                        k++;
                    }
                }
                if (k == 0)
                {
                    this.Text = "";
                    IsOpend = true;
                    BackColor = Color.White;
                    foreach (FieldButton button in Connections)
                    {
                        bool l = false;
                        foreach (FieldButton button1 in checkedButtons)
                        {
                            if (button == button1)
                            {
                                l = true;
                            }
                        }
                        if (!l)
                        {
                            checkedButtons.Add(this);
                            button.OpenField(checkedButtons);
                        }
                        
                    }
                }
                else if (k>0)
                {
                    IsOpend = true;
                    Text = k.ToString();
                    BackColor = Color.White;
                }

            }
            else if (IsBomb)
            {
                Game.RestartGame("You Lossed");
            }
            
        }
        public void InitializeConnections()
        {
            for (int i = PosX - 1; i < PosX + 1; i++)
            {
                for (int j = PosY - 1; j < PosY + 1; j++)
                {
                    foreach (FieldButton button in Game.Buttons)
                    {
                        if (((i > -1 && i < Game.FieldWidth) && (j > -1 && j < Game.FieldHeight)) && (this != button))
                        {

                            if ( ((Math.Sqrt(Math.Pow(button.PosX - PosX, 2) + Math.Pow(button.PosY - PosY, 2))) <= 1.5))
                            {
                                int k = 0;
                                foreach (FieldButton button1 in Connections)
                                {
                                    if (button == button1)
                                    {
                                        k++;
                                    }
                                }
                                if (k == 0)
                                    Connections.Add(button);
                            }
                        }

                    }
                }
            }

        }
    }
    
}