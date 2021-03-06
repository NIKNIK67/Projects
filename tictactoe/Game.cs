using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tictactoe
{
    internal enum GameMode
    {
        Solo,
        Duo,
    }

    internal static class Game
    {
        private static event Handler? TurnChange;
        private delegate void Handler();
        internal static bool turn = true;
        internal static Form1? MainForm;
        internal static List<FieldButton>? buttons;
        private static Cordinate? nextStep;
        static bool found;
        public static Player FirstPlayer;
        public static Player SecondPlayer;
        public static IDataProvider DataProvider;
        public static GameMode gameMode;
        internal static void InitilizeDuo(Form1 form,Player firstPlayer,Player secondPlayer)
        {
            FirstPlayer = firstPlayer;
            SecondPlayer = secondPlayer;
            gameMode = GameMode.Duo;
            MainForm = form;
            turn = true;
            MainForm.Controls.Clear();
            TurnChange = null;
            TurnChange += NextTurn;
            TurnChange += Reload;
            TurnChange += CallCheckEnd;
            Button exitButton = new Button();
            exitButton.Location = new Point(Convert.ToInt32(MainForm.Size.Width * 0.4), Convert.ToInt32(MainForm.Size.Height * 0.80));
            exitButton.Size = new Size(Convert.ToInt32(MainForm.Size.Width * 0.2), Convert.ToInt32(MainForm.Size.Height * 0.15));
            MainForm.Controls.Add(exitButton);
            exitButton.Text = "Exit";
            exitButton.Click += OnExit;
            Label player1 = new Label();
            player1.Location = new Point(0, 0);
            player1.AutoSize = true;
            player1.Text = $"Player X: {FirstPlayer.Score}";
            MainForm.Controls.Add(player1);
            Label player2 = new Label();
            player2.Location = new Point(0, Convert.ToInt32(MainForm.Size.Height * 0.05));
            player2.AutoSize = true;
            player2.Text = $"Player O: {SecondPlayer.Score}";
            MainForm.Controls.Add(player2);
            
            buttons = new List<FieldButton>();
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    buttons.Add(new FieldButton(i, j));
                }
            foreach (FieldButton button in buttons)
                button.InitializeConnections();
        }

        private static void OnExit(object? sender, EventArgs e)
        {
            MainForm.Controls.Clear();
            MainForm.InitializeComponent();
            
            

        }

        internal static void InitilizeSolo(Form1 form)
        {
            gameMode = GameMode.Solo;
            MainForm = form;
            turn = true;
            found = false;
            MainForm.Controls.Clear();
            TurnChange = null;
            TurnChange += NextTurn;
            TurnChange += Reload;
            TurnChange += CallCheckEnd;
            TurnChange += StepAnalizer;
            TurnChange += AiTurn;
            TurnChange += ShowInConsole;
            Button exitButton = new Button();
            exitButton.Location = new Point(Convert.ToInt32(MainForm.Size.Width * 0.4), Convert.ToInt32(MainForm.Size.Height * 0.80));
            exitButton.Size = new Size(Convert.ToInt32(MainForm.Size.Width * 0.2), Convert.ToInt32(MainForm.Size.Height * 0.15));
            MainForm.Controls.Add(exitButton);
            exitButton.Text = "Exit";
            exitButton.Click += OnExit;
            FirstPlayer = DataProvider.FindPlayerByName(FirstPlayer.Name);
            Label label = new Label();
            label.Text = $"{FirstPlayer.Name}: {FirstPlayer.Score}";
            label.Location = new Point(0, 0);
            label.AutoSize = true;
            MainForm.Controls.Add(label);
            buttons = new List<FieldButton>();
            nextStep = new Cordinate(-1, -1);
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    buttons.Add(new FieldButton(i, j));
                }
            foreach (FieldButton button in buttons)
                button.InitializeConnections();
            foreach (FieldButton button in buttons)
            {
                Console.Write($"Buttons connected with {button.PosX} {button.PosY} are  ");
                foreach (FieldButton button1 in button.Connections)
                {
                    Console.Write($"{button1.PosX} {button1.PosY}, ");

                }
                Console.WriteLine();

            }


        }
        internal static void ShowInConsole()
        {
            int[,] mas = new int[3, 3];
            foreach (FieldButton button in buttons)
            {
                mas[button.PosX, button.PosY] = button.State;
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write($"{mas[j, i]} ");
                }
                Console.WriteLine();
            }

        }
        internal static void ClearForm(string text)
        {

            MainForm.Controls.Clear();
            Button button1 = new Button();
            button1.Text = text;
            button1.Size = new Size(Convert.ToInt32(Game.MainForm?.Width * 0.3), Convert.ToInt32(Game.MainForm?.Height * 0.3));
            button1.Location = new Point(Convert.ToInt32(Game.MainForm?.Width * 0.35), Convert.ToInt32(Game.MainForm?.Height * 0.35));
            button1.Click += Button1_Click;
            MainForm.Controls.Add(button1);
        }
        private static void Button1_Click(object? sender, EventArgs e)
        {
            if(gameMode == GameMode.Solo)
                Game.InitilizeSolo(MainForm);
            else if(gameMode == GameMode.Duo)
                Game.InitilizeDuo(MainForm,FirstPlayer,SecondPlayer);
        }
        internal static void CallCheckEnd() => CheckEnd();
        internal static int CheckEnd()
        {
            bool trigger = true;
            int k = 0;
            foreach (FieldButton button in buttons)
            {
                if (button.State == -1)
                    k++;
            }
            if (k == 0)
            { ClearForm("Reset"); }
            foreach (FieldButton button in buttons)
            {
                foreach (FieldButton button1 in button.Connections)
                {
                    if (button1.State == button.State)
                    {
                        int nextX = button1.PosX + button1.PosX - button.PosX;
                        int nextY = button1.PosY + button1.PosY - button.PosY;
                        if (nextX > -1 && nextY > -1 && nextX < 3 && nextY < 3)
                        {
                            foreach (FieldButton button2 in buttons)
                            {
                                if ((button2.PosX == nextX && button2.PosY == nextY) && button2.State == button.State)
                                {
                                    if (Game.gameMode == GameMode.Solo)
                                    {
                                        if (button.State == 1 && trigger)
                                        {
                                            trigger = false;
                                            DataProvider.AddScore(FirstPlayer, 1);
                                            ClearForm("You won");
                                            return 1;
                                        }
                                        if (button.State == 0 && trigger)
                                        {
                                            trigger = false;
                                            DataProvider.AddScore(FirstPlayer, -1);
                                            ClearForm("You Lossed");

                                            return -1;
                                        }
                                    }
                                    else if (Game.gameMode == GameMode.Duo)
                                    {
                                        if (button.State == 1 && trigger)
                                        {
                                            trigger = false;
                                            FirstPlayer.Score += 1;
                                            ClearForm("Player X Won");
                                            return 1;
                                        }
                                        if (button.State == 0 && trigger)
                                        {
                                            trigger = false;
                                            SecondPlayer.Score += 1;
                                            ClearForm("Player O Won");
                                            return -1;
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
            return 0;
        }
        internal static void Reload()
        {
            foreach (FieldButton button in buttons)
            {
                switch (button.State)
                {
                    case -1:
                        button.Text = "";
                        break;
                    case 0:
                        button.Text = "O";
                        break;
                    case 1:
                        button.Text = "X";
                        break;
                    default:
                        throw new Exception("Unexpected state");
                        break;
                }
            }
        }
        internal static void StepAnalizer()
        {
            foreach (FieldButton button in buttons)
            {   
                if (!found)
                {
                    foreach (FieldButton button1 in button.Connections)
                    {
                        if (button1.State == -1 && button.State == 1)
                        {
                            int nextX = button1.PosX + button1.PosX - button.PosX;
                            int nextY = button1.PosY + button1.PosY - button.PosY;
                            if (nextX > -1 && nextY > -1 && nextX < 3 && nextY < 3)
                            {
                                foreach (FieldButton button2 in buttons)
                                {
                                    if ((button2.PosX == nextX && button2.PosY == nextY) && button2.State == button.State)
                                    {
                                        nextStep = new Cordinate(button1.PosX,button1.PosY);
                                        found = true;
                                    }
                                }
                            }
                        }
                    }
                    foreach (FieldButton button1 in button.Connections)
                    {
                        if (button1.State == button.State && button.State == 1)
                        {
                            int nextX = button1.PosX + button1.PosX - button.PosX;
                            int nextY = button1.PosY + button1.PosY - button.PosY;
                            if (nextX > -1 && nextY > -1 && nextX < 3 && nextY < 3)
                            {
                                foreach (FieldButton button2 in buttons)
                                {
                                    if ((button2.PosX == nextX && button2.PosY == nextY) && button2.State == -1)
                                    {
                                        nextStep = new Cordinate(nextX, nextY);
            
                                        found = true;
                                    }
                                }
                            }
                        }
                    }
                } 
            }
        }
        internal static void NextTurn()
        {
            if (turn)
                turn = false;
            else
                turn = true;
        }
        private static void AiTurn()
        {
            if (!turn)
            {
                if (!found)
                {
                    Random rand = new Random(DateTime.Now.Millisecond);
                    List<int> unique = new List<int>();
                    while (true)
                    {   
                        int a = rand.Next()%9;
                        if (!unique.Contains(a) && buttons[a].State == -1)
                        {
                            Console.WriteLine($"Make step on{buttons[a].PosX} {buttons[a].PosY}");
                            ChangeState(buttons[a]);
                            break;
                        }
                        else if (!unique.Contains(a) && buttons[a].State != -1)
                        { 
                            unique.Add(a);
                        }
                        if (unique.Count >= 9)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    foreach (FieldButton button in buttons)
                    {
                        if (button.PosX == nextStep.PosX && button.PosY == nextStep.PosY)
                        {
                            found = false;
                            Console.WriteLine($"Make step on{button.PosX} {button.PosY}");
                            ChangeState(button);
                            break;
                        }
                    }
                }
            }
        }
        internal static void ChangeState(FieldButton button)
        {
            if (button.State == -1 && Game.turn)
            {
                button.State = 1;
                TurnChange.Invoke();
            }
            else if (button.State == -1)
            {
                button.State = 0;
                TurnChange.Invoke();
            }

        }
    }

}




