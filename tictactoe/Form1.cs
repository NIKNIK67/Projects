namespace tictactoe
{
    public class Form1 : Form
    {
        private TextBox PlayerName;
        public Form1()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.Text = "TicTacToe";
            this.Size = new Size(Convert.ToInt32(Screen.PrimaryScreen.Bounds.Width * 0.75), Convert.ToInt32(Screen.PrimaryScreen.Bounds.Height * 0.75));
           
            Console.WriteLine(Convert.ToInt32(Screen.PrimaryScreen.Bounds.Width * 0.5));
            Console.WriteLine(Convert.ToInt32(Screen.PrimaryScreen.Bounds.Height * 0.5));
            Console.WriteLine(this.Size.Height);
            Console.WriteLine(this.Size.Width);
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode =AutoScaleMode.Font;
            PlayerName = new TextBox();
            PlayerName.Location = new Point(Convert.ToInt32(Size.Width * 0.35), Convert.ToInt32(Size.Height * 0.70));
            Controls.Add(PlayerName);
            PlayerName.Show();
            PlayerName.Name = "Field";
            PlayerName.Size = new Size(Convert.ToInt32(Size.Width * 0.30), Convert.ToInt32(Size.Height * 0.2));
            Button button = new Button();
            button.Location = new Point(Convert.ToInt32(Size.Width * 0.35), Convert.ToInt32(Size.Height * 0.80));
            Controls.Add(button);
            button.Show();
            button.Size = new(Convert.ToInt32(Size.Width * 0.10), Convert.ToInt32(Size.Height * 0.1));
            button.Text = "Start Solo Game";
            button.Click += StartGame;
            Button button1 = new Button();
            button1.Location = new Point(Convert.ToInt32(Size.Width * 0.55), Convert.ToInt32(Size.Height * 0.80));
            Controls.Add(button1);
            button1.Show();
            button1.Size = new(Convert.ToInt32(Size.Width * 0.10), Convert.ToInt32(Size.Height * 0.1));
            button1.Text = "Start Duo Game";
            button1.Click += StartGame;
            List<Player>? list;
            
            Label label = new Label();
            label.Text = "Enter your name";
            label.AutoSize = true;
            label.Location = new Point(Convert.ToInt32(Size.Width * 0.35), Convert.ToInt32(Size.Height * 0.65));
            ListView table = new ListView();

            table.Size = new Size(Convert.ToInt32(Size.Width * 0.8), Convert.ToInt32(Size.Height * 0.5));
            table.Location = new Point(Convert.ToInt32(Size.Width * 0.1), Convert.ToInt32(Size.Height * 0.1));
            Controls.Add(table);
            Controls.Add(label);
            list = Game.DataProvider.GetPlayers();
            list.Sort();
            List<int> Ids = new List<int>();
            List<string> Names = new List<string>();
            List<int> scores = new List<int>();
            table.View = View.Details;
            table.Columns.Clear();
            table.Columns.Add("id");
            table.Columns.Add("Name");
            table.Columns.Add("Score");

            foreach (Player player in list)
            { 
                Ids.Add(player.Id);
                Names.Add(player.Name);
                scores.Add(player.Score);
            }
            for (int i = 0; i < Ids.Count; i++)
            { 
                ListViewItem item = new ListViewItem(new string[] { Ids[i].ToString(), Names[i], scores[i].ToString() });
                table.Items.Add(item);
            }
         

        }

        private void StartGame(object? sender, EventArgs e)
        {
            Button senderButton = sender as Button;
            if (senderButton.Text == "Start Solo Game")
            {
                string playerName = PlayerName.Text;
                Player CurrentPlayer;
                CurrentPlayer = Game.DataProvider.FindPlayerByName(playerName);
                if (CurrentPlayer == null)
                {
                    CurrentPlayer = new Player(playerName, 0);
                    Game.DataProvider.AddPlayer(CurrentPlayer);
                }
                Controls.Clear();
                Game.CurrentPlayer = CurrentPlayer;
                Game.InitilizeSolo(this);
            }
            else if (senderButton.Text == "Start Duo Game")
            {
                Controls.Clear();
                Game.InitilizeDuo(this);
            }
            
        }

        public System.ComponentModel.IContainer components;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}