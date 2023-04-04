using D00_Utility;
using RSGymPT_DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    new Client() {PersonalTrainerID = 1, LocationID = 2, Name = "Eurico Ferreira", BirthDate = new DateTime(1985, 03, 09), NIF = "100000003", PhoneNumber = "910000002", Email = "ef@email.com", Address = "Rua C, nº3", Status = Client.EnumStatusClient.Active.ToString()},
                    new Client() {PersonalTrainerID = 2, LocationID = 3, Name = "Luís Ferreira", BirthDate = new DateTime(1985, 03, 09), NIF = "100000004", PhoneNumber = "910000003", Email = "lf@email.com", Address = "Rua D, nº4", Status = Client.EnumStatusClient.Active.ToString()},
                    new Client() {PersonalTrainerID = 1, LocationID = 1, Name = "Joaquim Alves", BirthDate = new DateTime(1970, 09, 02), NIF = "100000005", PhoneNumber = "910000004", Email = "ja@email.com", Address = "Rua E, nº5", Status = Client.EnumStatusClient.Active.ToString()}
                };

                db.Client.AddRange(clients);
                db.SaveChanges();
            }
        }

        public static void ReadClient()
        {
            Console.Clear();
            Utility.WriteTitle("List of Clients");

            using (var db = new RSGymContext())
            {
                var queryClient = db.Client
                    .Select(x => x)
                    .Where(x => x.Status == Client.EnumStatusClient.Active.ToString())
                    .OrderBy(x => x.Name);


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
                        $"Observations: {x.Observation}", "", "\n\n\n"));
            }
        }

        public static void UpdateClient()   // ToDo: Fazer loops
        {
            Console.Write("Please insert the name of the client: ");
            string name = Console.ReadLine();

            using (var db = new RSGymContext())
            {
                var queryClient = db.Client
                    .Select(x => x)
                    .FirstOrDefault(x => x.Name.Contains(name));

                if (queryClient != null)        // ToDo: Decidi permitir alterar o Nome para abranger situações, por exemplo, aquando da alteração do nome após casamento.
                {
                    Console.Write("New Name: ");
                    string newName = Console.ReadLine();

                    Console.Write("New Phone Number: ");
                    string newPhone = Console.ReadLine();

                    Console.Write("New Email: ");
                    string newEmail = Console.ReadLine();

                    Console.Write("New Address: ");
                    string newAdress = Console.ReadLine();

                    queryClient.Name = newName;
                    queryClient.PhoneNumber = newPhone;
                    queryClient.Email = newEmail;
                    queryClient.Address = newAdress;

                    db.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Client not found, please try again!");
                }
            }
        }

        public static void CreateNewClient(int personalTrainerID, int locationID)
        {
            Console.WriteLine("Please fill the following fields with the Client's information: \n");

            Console.Write("Name: ");
            string name = Console.ReadLine();

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
                        Name = name,
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

        public static void ChangeStatusClient()   // ToDo: Decidi dar o nome de ChangeStatusClient (em vez de DeleteClient, como seria lógico em CRUD), por não estarmos a apagar um cliente, mas sim a alterar o status.
        {
            Console.Write("Please insert the Client ID ");
            int id = Convert.ToInt16(Console.ReadLine());       // ToDo: rever validação de int.

            using (var db = new RSGymContext())
            {
                var queryClient = db.Client
                    .Select(x => x)
                    .FirstOrDefault(x => x.ClientID == id);

                if (queryClient != null)
                {
                    queryClient.Status = Client.EnumStatusClient.Inactive.ToString();
                    db.SaveChanges();
                }
            }
        }

        public static void ActivateClient()     // ToDo: Caso um cliente retorne ao ginário poderá ser reativado.
        {
            Console.Write("Please insert the Client ID ");
            int id = Convert.ToInt16(Console.ReadLine());       // ToDo: rever validação de int.

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
