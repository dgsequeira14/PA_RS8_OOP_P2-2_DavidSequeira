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
                    new Client() {PersonalTrainerID = 1, LocationID = 2, Name = "Eurico Ferreira", BirthDate = new DateTime(1985, 03, 09), NIF = "100000003", PhoneNumber = "910000002", Email = "ef@email.com", Address = "Rua C, nº3", StatusClient = Client.EnumStatusClient.Active},
                    new Client() {PersonalTrainerID = 2, LocationID = 3, Name = "Luís Ferreira", BirthDate = new DateTime(1985, 03, 09), NIF = "100000004", PhoneNumber = "910000003", Email = "lf@email.com", Address = "Rua D, nº4", StatusClient = Client.EnumStatusClient.Active},
                    new Client() {PersonalTrainerID = 1, LocationID = 1, Name = "Joaquim Alves", BirthDate = new DateTime(1970, 09, 02), NIF = "100000005", PhoneNumber = "910000004", Email = "ja@email.com", Address = "Rua E, nº5", StatusClient = Client.EnumStatusClient.Active}
                };

                db.Client.AddRange(clients);
                db.SaveChanges();
            }
        }

        public static void ReadClient()
        {
            using (var db = new RSGymContext())
            {
                var queryClient = db.Client
                    .Select(x => x)
                    .Where(x => x.StatusClient == Client.EnumStatusClient.Active)
                    .OrderBy(x => x.Name);


                queryClient.ToList().ForEach(x => Utility.WriteMessage($"ID: {x.ClientID} | Personal Trainer ID: {x.PersonalTrainerID} | Location ID: {x.LocationID} | Name: {x.Name} | Date of Birth: {x.BirthDate} | NIF: {x.NIF} | Phone Number: {x.PhoneNumber} | Email: {x.Email} | Address: {x.Address} | Status: {x.StatusClient} | Observations: {x.Observation}", "", "\n"));
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
                        StatusClient = Client.EnumStatusClient.Active,
                        Observation = obs
                    }
                };

                db.Client.AddRange(clients);
                db.SaveChanges();
            }
        }

        public static void DeleteClient()   // ToDo: Rever nome deste método pois não vai fazer Delete, mas sim Alterar o Status do cliente -> InactivateClient
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
                    queryClient.StatusClient = Client.EnumStatusClient.Inactive;
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
                    queryClient.StatusClient = Client.EnumStatusClient.Active;
                    db.SaveChanges();
                }
            }
        }
    }
}
