
using Microsoft.EntityFrameworkCore.SqlServer;

namespace tictactoe
{
    internal static class Program
    {
       
        [STAThread]
        static void Main()
        {
           
                Game.DataProvider = new XMLDataProvider();
           
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
            
        }
    }
}