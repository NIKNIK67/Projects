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
            button.Location = new Point(Convert.ToInt32(Size.Width * 0.45), Convert.ToInt32(Size.Height * 0.80));
            Controls.Add(button);
            button.Show();
            button.Size = new(Convert.ToInt32(Size.Width * 0.10), Convert.ToInt32(Size.Height * 0.1));
            button.Text = "Start Game";
            button.Click += StartGame;
            List<Player> list;
            using (ApplicationContext db = new ApplicationContext())
            { list = db.Players.ToList(); }
            list.Sort();
            Label label = new Label();
            label.Text = "Enter your name";
            label.AutoSize = true;
            label.Location = new Point(Convert.ToInt32(Size.Width * 0.35), Convert.ToInt32(Size.Height * 0.65));
            ListView table = new ListView();

            table.Size = new Size(Convert.ToInt32(Size.Width * 0.8), Convert.ToInt32(Size.Height * 0.5));
            table.Location = new Point(Convert.ToInt32(Size.Width * 0.1), Convert.ToInt32(Size.Height * 0.1));
            Controls.Add(table);
            Controls.Add(label);
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
            
            //    Console.WriteLine($"{player.Id} {player.Name} score: {player.Score}");

        }

        private void StartGame(object? sender, EventArgs e)
        {
            string playerName = PlayerName.Text;
            Player CurrentPlayer;
            using (ApplicationContext db = new ApplicationContext())
            {
                CurrentPlayer = PlayerManager.FindPlayerByName(db,playerName);
                if (CurrentPlayer == null)
                {
                    CurrentPlayer = new Player(playerName, 0);
                    db.Players.Add(CurrentPlayer);
                }
                db.SaveChanges();
                
            }
            Controls.Clear();
            Game.CurrentPlayer = CurrentPlayer;
            Game.Initilize(this);


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