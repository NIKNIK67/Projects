namespace tictactoe
{
    internal class Cordinate : ICordinate
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public FieldButton Button { get; set; }
        public Cordinate(int posX,int posY)
        { 
            PosX = posX;
            PosY = posY;
            try
            {
                foreach (FieldButton button in Game.buttons)
                {
                    if (button.PosX == posX && button.PosY == posY)
                    {
                        Button = button;
                    } 
                }
                if (Button != null)
                {
                    throw new Exception($"Button with position{posX} and {posY} not found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Button = null;
            }
           
        }
        public Cordinate (FieldButton button)
        {
            Button = button;
            PosX = button.PosX;
            PosY = button.PosY;

        }
    }
}





