
using Microsoft.EntityFrameworkCore.SqlServer;

namespace tictactoe
{
    internal static class Program
    {
       
        [STAThread]
        static void Main()
        {
            try
            {
                EFContext context = new EFContext();
                Game.DataProvider = new EFDataProvider();
            }
            catch (Exception ex)
            {
                Game.DataProvider = new XmlDataProvider();
            }


            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
            
        }
    }
}