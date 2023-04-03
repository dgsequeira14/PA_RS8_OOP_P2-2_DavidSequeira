using D00_Utility;
using RSGymPT_DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSGymPT_Client.Repository
{
    public class PersonalTrainerRepository
    {
        public static void CreatePT()
        {
            using (var db = new RSGymContext())
            {
                IList<PersonalTrainer> personalTrainers = new List<PersonalTrainer>()
                {
                    new PersonalTrainer() { LocationID = 2, Code = "PT01", Name = "Inácio Silva", NIF = "100000001", PhoneNumber = "910000000", Email = "is@email.com", Address = "Rua A, nº1" },
                    new PersonalTrainer() { LocationID = 1, Code = "PT02", Name = "José Pedro", NIF = "100000002", PhoneNumber = "910000001", Email = "jp@email.com", Address = "Rua B, nº2" }

                };

                db.PersonalTrainer.AddRange(personalTrainers);
                db.SaveChanges();
            }
        }

        public static void ReadPT()
        {
            using (var db = new RSGymContext())
            {
                var queryPT = db.PersonalTrainer
                    .Select(x => x)
                    .OrderBy(x => x.Name);

                queryPT.ToList().ForEach(x => Utility.WriteMessage($"ID: {x.PersonalTrainerID} | Location ID: {x.LocationID} | Code: {x.Code} | Name: {x.Name} | NIF: {x.NIF} | Phone Number: {x.PhoneNumber} | Email: {x.Email} | Address: {x.Address}", "", "\n"));
            }
        }

        public static void CreateNewPT(int locationID)
        {
            Console.WriteLine("Please fill the following fields with the Personal Trainer's information: \n");

            Console.Write("Code: ");
            string code = Console.ReadLine();

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("NIF: ");
            string nif = Console.ReadLine();

            Console.Write("Phone Number: ");
            string phone = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Address: ");
            string address = Console.ReadLine();

            using (var db = new RSGymContext())
            {
                IList<PersonalTrainer> personalTrainers = new List<PersonalTrainer>()
                {
                    new PersonalTrainer()
                    {
                        LocationID = locationID,
                        Code = code,
                        Name = name,
                        NIF = nif,
                        PhoneNumber = phone,
                        Email = email,
                        Address = address
                    }
                };

                db.PersonalTrainer.AddRange(personalTrainers);
                db.SaveChanges();
            };
        }

    }
}

