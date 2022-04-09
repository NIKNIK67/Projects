namespace tictactoe
{
    internal class FieldButton : Field
    {
        
        
        public override int PosY { get; set; }
        public override int PosX { get; set; }
        internal override int State { get; set; }
        internal override List<FieldButton> Connections { get; set; }

        internal FieldButton(int posX, int posY)
        { 
            PosX=posX;
            PosY=posY;
            State = -1;
            Connections = new List<FieldButton>();
            Size = new Size(Convert.ToInt32(Game.MainForm?.Width*0.3), Convert.ToInt32(Game.MainForm?.Height*0.3));
            Location = new Point(Convert.ToInt32(Game.MainForm.Width * 0.3*posX)+ Convert.ToInt32(Game.MainForm.Width*0.05)
                       , Convert.ToInt32(Game.MainForm.Height * 0.3 * posY) + Convert.ToInt32(Game.MainForm.Height * 0.0125));
            Show();
            Text = $"";
            Click += PlayerClick;
            Game.MainForm.Controls.Add(this);
            
            
        }
        internal void InitializeConnections()
        { 
            for (int i = PosX - 1; i < PosX+1; i++)
            {
                for (int j = PosY - 1; j < PosY+1; j++)
                {
                    foreach (FieldButton button in Game.buttons)
                    {
                        if (((i > -1 && i < 3) && (j > -1 && j < 3)) &&(this != button))
                        {

                            if ((PosX != PosY) && ((Math.Sqrt(Math.Pow(button.PosX - PosX, 2) + Math.Pow(button.PosY - PosY, 2))) <= 1))
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
                            else if ((PosX == PosY || (PosX == 0 && PosY ==2) || (PosX==2 && PosY == 0)) && ((Math.Sqrt(Math.Pow(button.PosX - PosX, 2) + Math.Pow(button.PosY - PosY, 2))) <= 1.5))
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
        private void PlayerClick(object? sender, EventArgs e)
        {
            if (Game.turn)
            {
                Game.ChangeState(this);
            }
        }
        
    }
}





