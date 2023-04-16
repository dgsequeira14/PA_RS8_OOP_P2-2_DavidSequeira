using D00_Utility;
using RSGymPT_DAL.Model;
using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace RSGymPT_Client.Class
{
    public class Validation     // ToDo: Esta classe contém um conjunto de métodos aos quais eu recorro para processos de validação e pesquisa de dados.
    {
        private static string loggedInUser;

        #region User

        public static (string, string) ReadUserCredentials()        // ToDo: Este método devolve um Tupple cque vai ser posteriomente utilizado no método ValidateUserCredentials().
        {
            Console.Clear();
            Utility.WriteTitle("Login Menu");

            Console.Write("Please insert your Code: ");
            string userName = Console.ReadLine();

            Console.Write("Please insert your Password: ");
            string password = Console.ReadLine();

            return (userName, password);
        }      

        public static User ValidateUserCredentials((string, string) credentials)        // ToDo: Este método devolve um object do tipo User para poder ser utilizado no MenuLogin.
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

        public static string ValidateCode()             // ToDo: Neste método recorro à utilização de RegEx para colocar restrições, neste caso, ao Code.
        {
            Regex regex = new Regex("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{4,6}$");   // ToDo: Neste Regex valida-se as restrições: 4 a 6 caracteres, tem de conter pelo menos uma letra e um núemro e não pode conter caracteres especiais.
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

        #region Client

        public static int ValidateClient()
        {
            Console.Write("Please insert the First Name of the client: ");
            string name = Console.ReadLine();

            using (var db = new RSGymContext())
            {
                var queryClientCount = db.Client
                    .Where(u => u.FirstName == name).ToList();

                if (queryClientCount.Count == 1)
                {
                    var queryClientFirst = db.Client
                            .Select(x => x)
                            .FirstOrDefault(x => x.FirstName == name);

                    if (queryClientFirst != null)
                    {
                        return queryClientFirst.ClientID;
                    }
                }
                else if (queryClientCount.Count > 1)
                {
                    Console.Write("Please insert the Last Name of the client: ");
                    string lastName = Console.ReadLine();

                    using (var db01 = new RSGymContext())
                    {
                        var queryClientLast = db01.Client
                            .Select(x => x)
                            .FirstOrDefault(x => x.LastName == lastName && x.FirstName == name);

                        if (queryClientLast != null)
                        {
                            return queryClientLast.ClientID;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Client not found! Please try again.\n");
                }

                return 0;
            }
        }
       
        #endregion

        #region Location

        public static int ValidateLocation()        // ToDo: Para obter LocationID, solicito o nome da cidade e depois procuro para chegar ao ID.
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

        public static string FindPostCode(string postCode)
        {
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

        public static string ValidatePostCode(string prompt)
        {
            Regex regex = new Regex("^\\d{4}-\\d{3}$");     // ToDo: Neste Regex valida-se as restrições necessárias a um código-postal.
            string postCode;

            Console.Write($"{prompt}: ");

            do
            {
                postCode = Console.ReadLine();

                if (regex.IsMatch(postCode))
                {
                    return postCode;
                }
                else
                {
                    Console.WriteLine($"{prompt} in the wrong format. Please try again.\n");
                    Console.Write($"{prompt}: ");
                }
            } while (true);
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

        public static int ValidatePT()                  // ToDo: Para obter PersonalTrainerID, solicito o nome do PT e depois procuro para chegar ao ID.
        {
            Console.Write("\nPersonal Trainer First Name: ");         // ToDo: Tentei que este método permitisse procurar sem caratéres especiais, mas não consegui chegar a uma solução.
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

        public static string ValidateName(string prompt)
        {
            Regex regex = new Regex(@"^(?=.{1,100}$)[A-Za-z ]+$");      // ToDo: Neste Regex valida-se as restrições: máximo 100 caracteres, não pode conter números e pode conter espaços e caracteres especiais.
            string name;

            Console.Write($"{prompt}: ");

            do
            {
                name = Console.ReadLine();

                if (regex.IsMatch(name))
                {
                    return name;
                }
                else
                {
                    Console.WriteLine("Name can only have letters. Please try again.\n");
                    Console.Write($"{prompt}: ");
                }

            } while (!regex.IsMatch(name));

            return name;
        }

        public static string ValidateAddress(string prompt)
        {
            Console.Write($"{prompt}: ");
            string address;

            do
            {
                address = Console.ReadLine();

                if (address.Length > 100)
                {
                    Console.WriteLine($"{prompt} must have less than 100 characters. Please try again.\n");
                    Console.Write($"{prompt}: ");
                }
            }
            while (address.Length > 100);

            return address;
        }

        public static string ValidatePhoneAndNIF(string prompt)     
        {
            Console.Write($"{prompt}: ");
            string text;

            Regex regex = new Regex(@"^\d{9}$");            // ToDo: Neste Regex valida-se as restrições: exatamente 9 caracteres e só pode conter números

            do
            {
                text = Console.ReadLine();

                if (regex.IsMatch(text))
                {
                    return text;
                }
                else
                {
                    Console.WriteLine($"{prompt} must have 9 numbers, no spaces and no special characters. Please try again.\n ");
                    Console.Write($"{prompt}: ");
                }
            }
            while (!regex.IsMatch(text));

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

                if (birthDate > DateTime.Today)     // ToDo: Neste método calculo a idade para poder restringir a idade do cliente
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

            Regex regex = new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");         // ToDo: Neste Regex valida-se as restrições associadas a um endereço de email.
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
            DateTime dateTimeNow = DateTime.Now;
            DateTime date;

            string requestDate;         // ToDo: Neste processo utilizo o método TryParseExact para validar corretamente uma data: o primeiro parâmetro corresponde à string obtida da consola, o segundo corresponde ao formato,
                                                // o terceiro parâmetro corresponde à class System.Globalization e tem como função a remoção de qualquer dependência de uma CultureInfo, vai depender da CultureInfo da consola de onde vem o input,
                                                // o quarto parâmetro corresponde ao estilo do input, por exemplo se permite que o utilizador escreva espaços em branco,
                                                // o último parâmetro é o valor que será devolvido caso o TryParseExact tenha um resultado positivo.

            do
            {
                Console.Write($"{prompt} (dd/mm/yyyy): ");
                requestDate = Console.ReadLine();

                if (DateTime.TryParseExact(requestDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))     
                {
                    if (date >= dateTimeNow.Date)
                    {
                        return date;
                    }
                    else
                    {
                        Console.WriteLine("I am sorry, you cannot book appointments in the past!\nPlease try again.\n");
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
            DateTime hour;
            TimeSpan start = new TimeSpan(6, 0, 0);
            TimeSpan end = new TimeSpan(21, 0, 0);

            string requestHour;

            do
            {
                Console.Write($"{prompt} (hh:mm): ");
                requestHour = Console.ReadLine();

                if (DateTime.TryParseExact(requestHour, "HH:mm", CultureInfo.CurrentCulture, DateTimeStyles.None, out hour))    // ToDo: Nesta situação usei o mesmo processo do ValidateDate.
                {
                    if (hour.TimeOfDay < start || hour.TimeOfDay > end)         // ToDo: Criei duas variàveis com TimeSpan para restringir as marcações de aula entre as 06:00 e as 21:00. 
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



