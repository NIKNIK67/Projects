namespace mines
{
    internal static class Game
    {
        internal static Form1? MainForm;
        internal static List<FieldButton>? Buttons;
        internal static List<FieldButton>? BombButtons;
        internal static List<FieldButton>? FlagedButtons;
        internal static int FieldWidth;
        internal static int FieldHeight;
        internal static int BombsCount;
        private static int _flagsLeft;
        private static Label FlagCountLable;
        
        internal static int FlagsLeft 
        { 
            get 
            { 
                return _flagsLeft; 
            } 
            set 
            {
                
                _flagsLeft = value;
                ReloadLable();
                if (FlagsLeft == 0)
                {
                    CheckEnd();
                } 
            }
        }
        private static void CheckEnd()
        {
            List<bool> count = new List<bool>();
            foreach (FieldButton button in BombButtons)
            {
               
                foreach (FieldButton button1 in FlagedButtons)
                {
                    if (button == button1)
                    {
                       count.Add(true);
                    }
                }
                
               
            }
            if (count.Count == BombsCount)
            {
                RestartGame("Won");
            }
        }
        public static void RestartGame(string result)
        {
            MainForm.Controls.Clear();
            Button button = new Button();
            button.Location = new Point(Convert.ToInt32(MainForm.Size.Width * 0.45), Convert.ToInt32(MainForm.Size.Height * 0.65));
            button.Size = new Size(Convert.ToInt32(MainForm.Size.Width * 0.10), Convert.ToInt32(MainForm.Size.Height * 0.10));
            MainForm.Controls.Add(button);
            button.Text = result;
            button.Click += ToMenu;

        }
        private static void ReloadLable()
        { 
            FlagCountLable.Text = $"Flags Left {_flagsLeft}";
        }
        private static void ToMenu(object? sender, EventArgs e)
        {
            
            MainForm.Controls.Clear();
            MainForm.InitializeComponent();
        }

        internal static void Initialze(int bombs,int fieldWidth, int fieldHeight,Form1 form)
        {
            
            BombsCount= bombs;
            _flagsLeft = BombsCount;
            MainForm = form;
            FieldWidth = fieldWidth;
            FieldHeight = fieldHeight;
            MainForm.Controls.Clear();
            Buttons = new List<FieldButton>();
            FlagedButtons = new List<FieldButton>();
            for (int i = 0; i < fieldWidth; i++)
            {
                for (int j = 0; j < fieldHeight; j++)
                { 
                    Buttons.Add(new FieldButton(i,j) );

                }
            }
            foreach (FieldButton button in Buttons)
            { 
                button.InitializeConnections();
                button.Text = "X";
            }
            foreach (FieldButton button in Buttons)
            {
                //Console.Write($"Buttons connected with {button.PosX} {button.PosY} are  ");
                foreach (FieldButton button1 in button.Connections)
                {
                    //Console.Write($"{button1.PosX} {button1.PosY}, ");

                }
                //Console.WriteLine();

            }
            Random random = new Random(DateTime.UtcNow.Millisecond);
            int f = 0;
            BombButtons = new List<FieldButton>();

            while (f<bombs)
            {
                int a = random.Next(0, fieldHeight);
                int b = random.Next(0, fieldWidth);
                FieldButton button = Buttons?.Find(x => a == x.PosY && b == x.PosX);
                if (button.IsBomb == false)
                { 
                    button.IsBomb = true;
                    f++;
                    //Console.WriteLine($"Bomb is button{button.PosX} {button.PosY}");
                    BombButtons.Add(button);
                }
            }
            FlagCountLable = new Label();
            FlagCountLable.Location = new Point(Convert.ToInt32(MainForm.Size.Width * 0.45), Convert.ToInt32(MainForm.Size.Height * 0));
            FlagCountLable.AutoSize = true;
            FlagCountLable.Text = $"Flags Left {FlagsLeft}";
            MainForm.Controls.Add(FlagCountLable);
        }
    }

}
    
