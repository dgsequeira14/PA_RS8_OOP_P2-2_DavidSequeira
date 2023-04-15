using D00_Utility;
using RSGymPT_Client.Repository;
using RSGymPT_DAL.Migrations;
using RSGymPT_DAL.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
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

        public static string ValidateCode()
        {
            Regex regex = new Regex("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{4,6}$");
            string code;

            do
            {
                Console.Write("Code: ");
                code = Console.ReadLine();

                if (regex.IsMatch(code))
                {
                    return code;
                }
                else
                {
                    Console.WriteLine("Code must be between 4 and 6 characters, and have at least one number and one letter.\nPlease try again.\n");
                }
            } while (!regex.IsMatch(code));

            return code;
        }

        public static string FindCode(string code)
        {
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

        public static string ValidatePassword(string prompt)
        {
            Console.Write($"{prompt}: ");

            Regex regex = new Regex("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,12}$");
            string pass;

            do
            {
                pass = Console.ReadLine();

                if (regex.IsMatch(pass))
                {
                    return pass;
                }
                else
                {
                    Console.WriteLine($"{prompt} must be between 8 and 12 characters, and have at least one number and one letter.\nPlease try again.\n");
                    Console.Write($"{prompt}: ");
                }
            } while (!regex.IsMatch(pass));

            return pass;
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

        #region Find and Validate Input Data

        public static string ValidateNameAndAddress(string prompt)
        {
            Console.Write($"{prompt}: ");
            string text;

            do
            {
                text = Console.ReadLine();

                if (text.Length > 100)
                {
                    Console.WriteLine($"{prompt} must have less than 100 characters. Please try again.\n");
                    Console.Write($"{prompt}: ");
                }
            }
            while (text.Length > 100);

            return text;
        }

        public static string ValidatePhoneAndNIF(string prompt)
        {
            Console.Write($"{prompt}: ");
            string text;

            do
            {
                text = Console.ReadLine();

                if (text.Length != 9)
                {
                    Console.WriteLine($"{prompt} must have 9 characters. Please try again.\n ");
                    Console.Write($"{prompt}: ");
                }
            }
            while (text.Length != 9);

            return text;
        }

        public static string ValidateObs(string prompt)
        {
            Console.Write($"{prompt}: ");
            string obs;

            do
            {
                obs = Console.ReadLine();

                if (obs.Length > 255)
                {
                    Console.WriteLine($"This field must have less than 255 characters. Please try again.\n ");
                    Console.Write($"{prompt}: ");
                }

            } while (obs.Length > 255);

            return obs;
        }

        public static DateTime ValidateBirthDate(string prompt)
        {
            Console.Write($"{prompt} (dd/mm/yyyy): ");

            DateTime birthDate;
            int age;

            do
            {
                birthDate = Convert.ToDateTime(Console.ReadLine());
                age = DateTime.Now.Year - birthDate.Year;

                if (birthDate > DateTime.Today)
                {
                    Console.WriteLine("Please wait until client is born to register!\n");
                    Console.Write($"{prompt} (dd/mm/yyyy): ");
                }
                else if (age < 18)
                {
                    Console.WriteLine("Client's must be over 16.\n");
                    Console.Write($"{prompt} (dd/mm/yyyy): ");
                }
            }
            while (age < 16);

            return birthDate;
        }

        public static string ValidateEmail(string prompt)
        {
            Console.Write($"{prompt}: ");

            Regex regex = new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");
            string email;

            do
            {
                email = Console.ReadLine();

                if (regex.IsMatch(email))
                {
                    return email;
                }
                else
                {
                    Console.WriteLine("Email provided in the wrong format. Please try again.\n");
                    Console.Write($"{prompt}: ");
                }

            } while (!regex.IsMatch(email));

            return email;
        }

        public static DateTime ValidateDate(string prompt)
        {
            Console.Write($"{prompt} (dd/mm/yyyy): ");

            DateTime dateTimeNow = DateTime.Now;
            DateTime date;

            string requestDate;

            do
            {
                Console.Write($"{prompt} (dd/mm/yyyy): ");
                requestDate = Console.ReadLine();

                if (DateTime.TryParseExact(requestDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    if (date < dateTimeNow)
                    {
                        Console.WriteLine("I am sorry, you cannot book appointments in the past!\nPlease try again.\n");
                    }
                    else
                    {
                        return date;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid date format! Please try again\n");
                }
            } while (DateTime.TryParseExact(requestDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date) == false || date < dateTimeNow);

            return date;
        }

        public static DateTime ValidateHour(string prompt)
        {
            Console.Write($"{prompt} (hh:mm): ");

            DateTime hour;
            TimeSpan start = new TimeSpan(6, 0, 0);
            TimeSpan end = new TimeSpan(21, 0, 0);

            string requestHour;

            do
            {
                Console.Write($"{prompt} (hh:mm): ");
                requestHour = Console.ReadLine();

                if (DateTime.TryParseExact(requestHour, "HH:mm", CultureInfo.CurrentCulture, DateTimeStyles.None, out hour))
                {
                    if (hour.TimeOfDay < start || hour.TimeOfDay > end)
                    {
                        Console.WriteLine("I am sorry, appointments can only be booked between 6 and 21!\nPlease try again.\n");
                    }
                    else
                    {
                        return hour;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid hour format! Please try again\n");
                }
            } while (true);

            // return hour;
        }

        public static string FindNIF(string nif)
        {
            using (var db = new RSGymContext())
            {
                var queryNIF = db.Client
                    .Select(x => x)
                    .FirstOrDefault(x => x.NIF == nif || x.PersonalTrainer.NIF == nif);

                if (queryNIF == null)
                {
                    return nif;
                }
                else
                {
                    return "0";
                }
            }
        }

        #endregion

    }


}
