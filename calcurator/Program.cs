using System.Threading;

namespace calcurator
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Thread.CurrentThread.CurrentCulture =new System.Globalization.CultureInfo("en-US");
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}