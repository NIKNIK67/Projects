using System.Data;
namespace calcurator
{
    public static class LineStringBuilder
    {
        public static Form1 MainForm { get; set; }
        public static void AddSymbol(string action)
        {
            if (action != "=" && action != "c")
                MainForm.box.Text += action;
            else if (action == "c")
                MainForm.box.Text = "";
            else if (action == "=")
            {
                string line = MainForm.box.Text;
                List<int> actionPos = new List<int>();
                if (line.ToLower() == line && line.ToUpper() == line && line.Length != 0)
                {
                    try
                    {
                        DataTable dt = new DataTable();
                        MainForm.box.Text = dt.Compute(MainForm.box.Text, "").ToString();
                    }
                    catch
                    {
                        MessageBox.Show("Enter right value in field"); }
                    }
                else
                {
                    MainForm.box.Text = "Wrong input";
                }
            }
        
        }
    }
}

