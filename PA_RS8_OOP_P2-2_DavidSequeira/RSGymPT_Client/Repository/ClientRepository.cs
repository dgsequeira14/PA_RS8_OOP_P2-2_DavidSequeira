using D00_Utility;
using RSGymPT_Client.Class;
using RSGymPT_DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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
                        $"Date of Birth: {x.BirthDate}\n" +
                        $"NIF: {x.NIF}\n" +
                        $"Phone Number: {x.PhoneNumber}\n" +
                        $"Email: {x.Email}\n" +
                        $"Address: {x.Address}\n" +
                        $"Status: {x.Status}\n" +
                        $"Observations: {x.Observation}", "", "\n\n"));
            }
        }

        public static void UpdateClient()
        {
            Console.Clear();
            Utility.WriteTitle("Update Client Information");

            Validation.ShowLoggedUser();

            bool loopName = false;
            while (!loopName)
            {
                Console.Write("Please insert the name of the client: ");
                string name = Console.ReadLine();

                using (var db = new RSGymContext())
                {
                    var queryClient = db.Client
                        .Select(x => x)
                        .FirstOrDefault(x => x.Name.Contains(name));

                    if (queryClient != null)        // ToDo: Decidi permitir alterar o Nome para abranger situações, por exemplo, de alterações no registo civil.
                    {
                        Console.Write("First Name: ");    // ToDo: Rever como pedir o nome, terá de ser necessário criar campo LastName??
                        string newFirstName = Console.ReadLine();

                        Console.Write("Last Name: ");
                        string newLastName = Console.ReadLine();

                        Console.Write("New Phone Number: ");
                        string newPhone = Console.ReadLine();

                        Console.Write("New Email: ");
                        string newEmail = Console.ReadLine();

                        Console.Write("New Address: ");
                        string newAdress = Console.ReadLine();

                        queryClient.FirstName = newFirstName;
                        queryClient.LastName = newLastName;
                        queryClient.PhoneNumber = newPhone;
                        queryClient.Email = newEmail;
                        queryClient.Address = newAdress;

                        db.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("Client not found! Please try again.\n");
                        loopName = true;
                    }
                }
                loopName = false;
            }

        }

        public static void CreateNewClient()
        {
            Console.Clear();
            Utility.WriteTitle("Create new Client");

            Validation.ShowLoggedUser();

            Console.WriteLine("Please fill the following fields with the Client's information: \n");

            int personalTrainerID = Validation.ValidatePT();

            bool loopPT = false;
            while (!loopPT)
            {
                if (personalTrainerID == 0)
                {
                    Console.WriteLine("Personal Trainer not found! Please try again.\n");
                    Validation.ValidatePT();
                    loopPT = false;
                }

                loopPT = true;
            }

            int locationID = Validation.ValidateLocation();

            bool loopLocal = false;
            while (!loopLocal)
            {
                if (locationID == 0)
                {
                    Console.WriteLine("Location not found! Please try again.\n");
                    Validation.ValidatePT();
                    loopLocal = false;
                }

                loopLocal = true;
            }


            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string LastName = Console.ReadLine();

            Console.Write("Date of Birth (dd/mm/yyy): ");
            DateTime birthDate = Convert.ToDateTime(Console.ReadLine());

            Console.Write("NIF: ");
            string nif = Console.ReadLine();

            Console.Write("Phone Number: ");
            string phone = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Address: ");
            string address = Console.ReadLine();

            Console.Write("Observations: ");
            string obs = Console.ReadLine();

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
