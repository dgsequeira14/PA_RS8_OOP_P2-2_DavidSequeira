using D00_Utility;
using RSGymPT_Client.Class;
using RSGymPT_DAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RSGymPT_Client.Repository
{
    public class ClientRepository
    {
        public static void CreateClient()

        {
            using (var db = new RSGymContext())
            {
                IList<Client> clients = new List<Client>()
                {
                    new Client() {PersonalTrainerID = 1, LocationID = 2, FirstName = "Eurico", LastName = "Ferreira", BirthDate = new DateTime(1985, 03, 09), NIF = "100000003", PhoneNumber = "910000002", Email = "ef@email.com", Address = "Rua C, nº3", Status = Client.EnumStatusClient.Active.ToString()},
                    new Client() {PersonalTrainerID = 2, LocationID = 3, FirstName = "Luís", LastName = "Ferreira", BirthDate = new DateTime(1985, 03, 09), NIF = "100000004", PhoneNumber = "910000003", Email = "lf@email.com", Address = "Rua D, nº4", Status = Client.EnumStatusClient.Active.ToString()},
                    new Client() {PersonalTrainerID = 1, LocationID = 1, FirstName = "Joaquim", LastName = "Alves", BirthDate = new DateTime(1970, 09, 02), NIF = "100000005", PhoneNumber = "910000004", Email = "ja@email.com", Address = "Rua E, nº5", Status = Client.EnumStatusClient.Active.ToString()}
                };

                db.Client.AddRange(clients);
                db.SaveChanges();
            }
        }

        public static void ReadClient()
        {
            Console.Clear();
            Utility.WriteTitle("List of Clients");

            Validation.ShowLoggedUser();

            using (var db = new RSGymContext())
            {
                var queryClient = db.Client
                    .Select(x => x)
                    .Where(x => x.Status == Client.EnumStatusClient.Active.ToString())
                    .OrderBy(x => x.FirstName);


                queryClient.ToList().ForEach(x => Utility.WriteMessage(
                        $"ID: {x.ClientID}\n" +
                        $"Personal Trainer ID: {x.PersonalTrainerID}\n" +
                        $"Location ID: {x.LocationID}\n" +
                        $"Name: {x.Name}\n" +
                        $"Date of Birth: {x.BirthDate.ToShortDateString()}\n" +
                        $"NIF: {x.NIF}\n" +
                        $"Phone Number: {x.PhoneNumber}\n" +
                        $"Email: {x.Email}\n" +
                        $"Address: {x.Address}\n" +
                        $"Status: {x.Status}\n" +
                        $"Observations: {x.Observation}", "", "\n\n"));
            }
        }

        public static string UpdateClient()
        {
            Console.Clear();
            Utility.WriteTitle("Update Client Information");

            Validation.ShowLoggedUser();

            /*
            bool loopName = true;
            while (loopName)
            {
                Console.Write("Please insert the First Name of the client: ");
                string name = Console.ReadLine();

                using (var db = new RSGymContext())
                {
                    var queryClient = db.Client
                        .Select(x => x)
                        .FirstOrDefault(x => x.FirstName == name);

                    if (queryClient != null)
                    {
                        string newFirstName = Validation.ValidateNameAndAddress("First Name");

                        string newLastName = Validation.ValidateNameAndAddress("Last Name");

                        string newPhone = Validation.ValidatePhoneAndNIF("New Phone Number");

                        string newEmail = Validation.ValidateEmail("New Email");

                        string newAddress = Validation.ValidateNameAndAddress("New Address");

                        queryClient.FirstName = newFirstName;
                        queryClient.LastName = newLastName;
                        queryClient.PhoneNumber = newPhone;
                        queryClient.Email = newEmail;
                        queryClient.Address = newAddress;

                        db.SaveChanges();
                        loopName = false;
                    }
                    else
                    {
                        Console.WriteLine("Client not found! Please try again.\n");
                    }
                }
            } */


            bool loopName = true;
            while (loopName)
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
                            string choice = Utility_Menu.MenuUpdateUser();

                            switch (choice)
                            {
                                case "1":
                                    string newFirstName = Validation.ValidateNameAndAddress("New First Name");
                                    queryClientFirst.FirstName = newFirstName;
                                    db.SaveChanges();

                                    Console.WriteLine("Information updated!\n");
                                    Console.ReadKey();

                                    Utility_Menu.MenuClient();
                                    break;
                                case "2":
                                    string newLastName = Validation.ValidateNameAndAddress("New Last Name");
                                    queryClientFirst.LastName = newLastName;
                                    db.SaveChanges();

                                    Console.WriteLine("Information updated!\n");
                                    Console.ReadKey();

                                    Utility_Menu.MenuClient();
                                    break;
                                case "3":
                                    string newPhone = Validation.ValidatePhoneAndNIF("New Phone Number");
                                    queryClientFirst.PhoneNumber = newPhone;
                                    db.SaveChanges();

                                    Console.WriteLine("Information updated!\n");
                                    Console.ReadKey();

                                    Utility_Menu.MenuClient();
                                    break;
                                case "4":
                                    string newEmail = Validation.ValidateEmail("New Email");
                                    queryClientFirst.Email = newEmail;
                                    db.SaveChanges();

                                    Console.WriteLine("Information updated!\n");
                                    Console.ReadKey();

                                    Utility_Menu.MenuClient();
                                    break;
                                case "5":
                                    string newAddress = Validation.ValidateNameAndAddress("New Address");
                                    queryClientFirst.Address = newAddress;
                                    db.SaveChanges();

                                    Console.WriteLine("Information updated!\n");
                                    Console.ReadKey();

                                    Utility_Menu.MenuClient();
                                    break;
                                default:
                                    Console.Write("\nInvalid option! Please try again.\n");
                                    Console.ReadKey();

                                    Utility_Menu.MenuUpdateUser();
                                    loopName = true;
                                    break;
                            }
                            loopName = false;
                        }
                        else
                        {
                            Console.WriteLine("Client not found! Please try again.\n");
                            loopName = true;

                        }

                    }
                    else if(queryClientCount.Count > 1)
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
                                string choice = Utility_Menu.MenuUpdateUser();

                                switch (choice)
                                {
                                    case "1":
                                        string newFirstName = Validation.ValidateNameAndAddress("New First Name");
                                        queryClientLast.FirstName = newFirstName;
                                        db01.SaveChanges();

                                        Console.WriteLine("Information updated!\n");
                                        Console.ReadKey();

                                        Utility_Menu.MenuClient();
                                        break;
                                    case "2":
                                        string newLastName = Validation.ValidateNameAndAddress("New Last Name");
                                        queryClientLast.LastName = newLastName;
                                        db01.SaveChanges();
                                        break;
                                    case "3":
                                        string newPhone = Validation.ValidatePhoneAndNIF("New Phone Number");
                                        queryClientLast.PhoneNumber = newPhone;
                                        db01.SaveChanges();
                                        break;
                                    case "4":
                                        string newEmail = Validation.ValidateEmail("New Email");
                                        queryClientLast.Email = newEmail;
                                        db01.SaveChanges();
                                        break;
                                    case "5":
                                        string newAddress = Validation.ValidateNameAndAddress("New Address");
                                        queryClientLast.Address = newAddress;
                                        db01.SaveChanges();
                                        break;
                                    default:
                                        break;
                                }
                                loopName = false;
                            }
                            else
                            {
                                Console.WriteLine("Client not found! Please try again.\n");
                                loopName = true;

                            }

                        }
                    }
                    else
                    {
                        Console.WriteLine("Client not found! Please try again.\n");
                        loopName = true;
                    }

                }
            }
            return string.Empty;


        }

        public static void CreateNewClient()
        {
            Console.Clear();
            Utility.WriteTitle("Create new Client");

            Validation.ShowLoggedUser();

            Console.WriteLine("Please fill the following fields with the Client's information: \n");

            int personalTrainerID;

            do
            {
                personalTrainerID = Validation.ValidatePT();
                if (personalTrainerID == 0)
                {
                    Console.WriteLine("Personal Trainer not found! Please try again.\n");
                }
            } while (personalTrainerID == 0);


            int locationID;
            do
            {
                locationID = Validation.ValidateLocation();
                if (locationID == 0)
                {
                    Console.WriteLine("Location not found. You need to create a new Location first.\n");
                    Utility_Menu.MenuNewLocation();
                }
            } while (locationID == 0);


            Console.Write("First Name: ");
            string firstName = Validation.ValidateNameAndAddress("First Name");

            Console.Write("Last Name: ");
            string LastName = Validation.ValidateNameAndAddress("Last Name");

            DateTime birthDate = Validation.ValidateBirthDate("Date of Birth");

            string nif, validNIF;
            do
            {
                validNIF = Validation.ValidatePhoneAndNIF("NIF");

                nif = Validation.FindNIF(validNIF);

                if (nif == "0")
                {
                    Console.WriteLine("\nNIF already in database! Please try again\n");
                }
            } while (nif == "0");


            string phone = Validation.ValidatePhoneAndNIF("Phone Number");

            string email = Validation.ValidateEmail("Email");

            string address = Validation.ValidateNameAndAddress("Address");

            string obs = Validation.ValidateObs("Observations");

            using (var db = new RSGymContext())
            {
                IList<Client> clients = new List<Client>()
                {
                    new Client
                    {
                        PersonalTrainerID = personalTrainerID,
                        LocationID = locationID,
                        FirstName = firstName,
                        LastName = LastName,
                        BirthDate = birthDate,
                        NIF = nif,
                        PhoneNumber = phone,
                        Email = email,
                        Address = address,
                        Status = Client.EnumStatusClient.Active.ToString(),
                        Observation = obs
                    }
                };

                db.Client.AddRange(clients);
                db.SaveChanges();
            }
        }

        public static void InactivateClient()   // ToDo: Decidi dar o nome de InactivateClient (em vez de DeleteClient, como seria lógico em CRUD), por não estarmos a apagar um cliente, mas sim a alterar o status.
        {
            Console.Clear();
            Utility.WriteTitle("Change Client Status");

            Validation.ShowLoggedUser();

            Console.Write("Please insert the Client ID: ");
            int.TryParse(Console.ReadLine(), out int id);

            using (var db = new RSGymContext())
            {
                var queryClient = db.Client
                    .Select(x => x)
                    .FirstOrDefault(x => x.ClientID == id);

                if (queryClient != null)
                {
                    queryClient.Status = Client.EnumStatusClient.Inactive.ToString();
                    db.SaveChanges();

                    Console.WriteLine($"Client ID: {queryClient.ClientID} - Current Status: {queryClient.Status}\n");
                }
            }
        }

        public static void ActivateClient()     // ToDo: Caso um cliente retorne ao ginásio poderá ser reativado.
        {
            Console.Clear();
            Utility.WriteTitle("Change Client Status");

            Validation.ShowLoggedUser();

            Console.Write("Please insert the Client ID: ");
            int.TryParse(Console.ReadLine(), out int id);

            using (var db = new RSGymContext())
            {
                var queryClient = db.Client
                    .Select(x => x)
                    .FirstOrDefault(x => x.ClientID == id);

                if (queryClient != null)
                {
                    queryClient.Status = Client.EnumStatusClient.Active.ToString();
                    db.SaveChanges();
                }
            }
        }
    }
}
