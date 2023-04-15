using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D00_Utility;
using RSGymPT_Client.Class;
using RSGymPT_Client.Repository;
using RSGymPT_DAL.Model;


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

                // InitialData.CreateInitialData();

                #endregion

                #region List Initial Data 

                // InitialData.ReadInitialData();   // ToDo: Remover!!

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
