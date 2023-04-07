using D00_Utility;
using RSGymPT_Client.Class;
using RSGymPT_DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSGymPT_Client.Repository
{
    public class LocationRepository     // ToDo: Ponderei criar um método Update que permitisse atualizar os dados desta tabela, mas como o Código-Postal tem algumas especificações/regras decidi não criar.
    {
        public static void CreateLocation()
        {
            using (var db = new RSGymContext())
            {
                IList<Location> locations = new List<Location>()
                {
                    new Location {PostCode = "4815-178", City = "Lordelo" },
                    new Location {PostCode = "4000-014", City = "Porto" },
                    new Location {PostCode = "2005-002", City = "Santarém" }

                };

                db.Location.AddRange(locations);
                db.SaveChanges();
            }
        }

        public static void ReadLocation()
        {
            Console.Clear();
            Utility.WriteTitle("List of Locations");

            Validation.ShowLoggedUser();

            using (var db = new RSGymContext())
            {
                var queryLocation = db.Location
                    .Select(x => x)
                    .OrderBy(x => x.City);

                queryLocation.ToList().ForEach(x => Utility.WriteMessage($"ID: {x.LocationID} | Post-Code: {x.PostCode} | City: {x.City}", "", "\n"));
            }

        }

        public static void CreateNewLocation()
        {
            Console.Clear();
            Utility.WriteTitle("New Location");

            Validation.ShowLoggedUser();

            Console.WriteLine("Please fill the following fields with the Location's information: \n");

            string postCode;
            do
            {
                postCode = Validation.ValidatePostCode();
                if (postCode == "0")
                {
                    Console.WriteLine("\nPost-Code already in database! Please try again.\n");
                }
            } while (postCode == "0");

            string city;
            do
            {
                city = Validation.ValidateCity();
                if (city == "0")
                {
                    Console.WriteLine("\nCity already in database! Please try again.\n");
                }
            } while (city == "0");


            using (var db = new RSGymContext())
            {
                IList<Location> locations = new List<Location>()
                {
                    new Location
                    {
                        PostCode = postCode ,
                        City =  city
                    }
                };

                db.Location.AddRange(locations);
                db.SaveChanges();

            }
        }
    }
}
