
using Microsoft.EntityFrameworkCore.SqlServer;

namespace tictactoe
{
    internal static class Program
    {
       
        [STAThread]
        static void Main()
        {
            
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
            try
            {
                using (EFContext db = new EFContext())
                {
                    Game.DataProvider = new EFDataProvider();
                }
            }
            catch (Exception ex)
            { 
                Game.DataProvider = new XMLDataProvider();
            }
        }
    }
}