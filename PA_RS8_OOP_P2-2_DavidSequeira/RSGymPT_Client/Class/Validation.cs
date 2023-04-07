using D00_Utility;
using RSGymPT_Client.Repository;
using RSGymPT_DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

            return loggedInUser;
        }

        public static void ShowLoggedUser()
        {
            if (loggedInUser != null)
            {
                Console.WriteLine($"Logged in User: {loggedInUser}\n\n");

            }
        }

        public static string ValidateUserCode()
        {
            Console.Write("Code: ");
            string code = Console.ReadLine();

            using (var db = new RSGymContext())
            {
                var queryCode = db.User
                    .Select(x => x)
                    .FirstOrDefault(x => x.Code == code);

                    if (queryCode == null)
                    {
                        return code;
                    }
                    else
                    {
                        return "0";
                    }
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

        public static string ValidatePostCode()
        {
            Console.Write("Post-Code: ");
            string postCode = Console.ReadLine();

            using (var db = new RSGymContext())
            {
                var queryPost = db.Location
                    .Select(x => x)
                    .FirstOrDefault(x => x.PostCode == postCode);

                if (queryPost == null) 
                {
                    return postCode;
                }
                else
                {
                    return "0";
                }
            }


        }

        public static string ValidateCity()
        {
            Console.Write("City: ");
            string city = Console.ReadLine();

            using (var db = new RSGymContext())
            {
                var queryCity = db.Location
                    .Select(x => x)
                    .FirstOrDefault(x => x.City == city);

                if (queryCity == null)
                {
                    return city;
                }
                else
                {
                    return "0";
                }
            }
        }

        #endregion

        #region Personal Trainer

        public static int ValidatePT()
        {
            Console.Write("Personal Trainer First Name: ");
            string name = Console.ReadLine();

            using (var db = new RSGymContext())
            {
                var queryPT = db.PersonalTrainer
                    .Select(x => x)
                    .FirstOrDefault(x => x.FirstName == name);

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
