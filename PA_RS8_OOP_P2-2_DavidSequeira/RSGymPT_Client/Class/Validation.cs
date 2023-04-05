using D00_Utility;
using RSGymPT_Client.Repository;
using RSGymPT_DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RSGymPT_Client.Class
{
    public class Validation
    {
        private static string loggedInUser;
        
        #region User

        public static (string, string) ReadUserCredentials()
        {
            Console.Clear();
            Utility.WriteTitle("Login Menu");

            Console.Write("Please insert your Code: ");
            string userName = Console.ReadLine();

            Console.Write("Please insert your Password: ");
            string password = Console.ReadLine();

            return (userName, password);
        }

        public static User ValidateUserCredentials((string, string) credentials)
        {
            using (var db = new RSGymContext())
            {
                var queryCredentials = db.User
                    .FirstOrDefault(x => x.Code == credentials.Item1 && x.Password == credentials.Item2);

                return queryCredentials;
            }
        }

        public static string LogginMessage(string name)
        {
            loggedInUser = name;

            Console.Write($"\nHello {name}. Welcome to RSGym!\n");
            // Console.ReadKey();

            return loggedInUser;
        }

        public static void ShowLoggedUser()
        {
            // string name = Validation.LogginMessage(loggedInUser);

            if (loggedInUser != null)
            {
                Console.WriteLine($"Logged in User: {loggedInUser}\n\n");
                   
            }
        }

        #endregion

        #region Location

        public static int ValidateLocation()
        {
            Console.Write("City: ");
            string city = Console.ReadLine();

            using (var db = new RSGymContext())
            {
                var queryCity = db.Location
                    .Select(x => x)
                    .FirstOrDefault(x => x.City == city);

                if (queryCity != null)
                {
                    return queryCity.LocationID;
                }
                else
                {
                    return 0;
                }
            }
        }

        #endregion

        #region Personal Trainer

        public static int ValidatePT()
        {
            Console.Write("Personal Trainer Name: ");
            string name = Console.ReadLine();

            using (var db = new RSGymContext())
            {
                var queryPT = db.PersonalTrainer
                    .Select(x => x)
                    .FirstOrDefault(x => x.Name == name);

                if (queryPT != null)
                {
                    return queryPT.PersonalTrainerID;
                }
                else
                {
                    return 0;
                }
            }
        }


        #endregion

    }
}
