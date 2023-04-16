using D00_Utility;
using RSGymPT_Client.Class;
using System;


namespace RSGymPT_Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Utility.SetUnicodeConsole();
            try
            {
                #region Initial Data Creation

                InitialData.CreateInitialData();

                #endregion

                #region App Initialization

                Utility_Menu.MenuInitial();
                
                #endregion

            }
            catch (Exception)
            {

                Console.WriteLine("\nAn error has occurred.\nPlease report to administrator via e-mail. Thank you.");
                Console.ReadKey();
                Utility_Menu.MenuLogin();
            }


            Utility.TerminateConsole();
        }
    }
}
