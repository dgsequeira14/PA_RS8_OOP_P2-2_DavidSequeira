using D00_Utility;
using RSGymPT_DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSGymPT_Client.Repository
{
    public class RequestRepository
    {
        public static void CreateRequest()
        {
            using (var db = new RSGymContext())
            {
                IList<Request> requests = new List<Request>()
                {
                    new Request { ClientID = 1, PersonalTrainerID = 1, Date = new DateTime(2023, 06, 10) , Hour = new DateTime( 2023, 06, 10, 10, 00, 00), StatusRequest = Request.EnumStatusRequest.Booked },
                    new Request { ClientID = 2, PersonalTrainerID = 2, Date = new DateTime(2023, 06, 12), Hour = new DateTime(2023, 06, 12, 12, 00, 00), StatusRequest = Request.EnumStatusRequest.Booked },
                    new Request { ClientID = 3, PersonalTrainerID = 1, Date = new DateTime(2023, 04, 25), Hour = new DateTime(2023, 04, 25, 16, 00, 00) , StatusRequest = Request.EnumStatusRequest.Booked }
                };

                db.Request.AddRange(requests);
                db.SaveChanges();
            }
        }

        public static void ReadRequest()
        {
            using (var db = new RSGymContext())
            {
                var queryRequest = db.Request
                    .Select(x => x)
                    .OrderBy(x => x.StatusRequest)
                    .ThenBy(x => x.Date)
                    .ThenBy(x => x.Hour);

                queryRequest.ToList().ForEach(x => Utility.WriteMessage($"ID: {x.RequestID} | Client ID: {x.ClientID} | Personal Trainer ID: {x.PersonalTrainerID}| Date: {x.Date.ToShortDateString()} - Hour: {x.Hour.ToShortTimeString()} | Status: {x.StatusRequest} | Observations: {x.Observation}"));
            }

        }

        public static void UpdateRequest()
        {
            Console.Write("Please insert the Request ID to update: ");
            int requestID = Convert.ToInt16(Console.ReadLine());

            using (var db = new RSGymContext())
            {
                var queryRequest = db.Request
                    .Select(x => x)
                    .FirstOrDefault(x => x.RequestID == requestID);
                
                if (queryRequest != null)
                {
                    Console.Write("PT ID: ");
                    int newPT = Convert.ToInt16(Console.ReadLine());

                    Console.Write("Date (dd/mm/yyyy): ");
                    DateTime newDate = Convert.ToDateTime(Console.ReadLine());

                    Console.Write("Hour (hh:mm): ");
                    DateTime newHour = Convert.ToDateTime(Console.ReadLine());
                    
                    queryRequest.PersonalTrainerID = newPT;
                    queryRequest.Date = newDate;
                    queryRequest.Hour = newHour;

                    db.SaveChanges();
                }
            }
            ;


        }

        public static void CreateNewRequest()       // ToDo: Rever inserção de FK's na criação pela consola!!! -> possivelmnente terei que validar existencia de client e pt id's na bd.
        {
            Console.WriteLine("Please fill the following fields with the Request's details: \n");

            Console.Write("Client ID: ");
            int clientID = Convert.ToInt16(Console.ReadLine());

            Console.Write("PT ID: ");
            int ptID = Convert.ToInt16(Console.ReadLine());

            Console.Write("Date (dd/mm/yyyy): ");
            DateTime date = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Hour (hh:mm): ");
            DateTime hour = Convert.ToDateTime(Console.ReadLine());

            using (var db = new RSGymContext())
            {
                IList<Request> requests = new List<Request>()
                {
                    new Request
                    {
                        ClientID = clientID,
                        PersonalTrainerID = ptID,
                        Date = date,
                        Hour = hour,
                        StatusRequest = Request.EnumStatusRequest.Booked
                    }
                };

                db.Request.AddRange(requests);
                db.SaveChanges();
            }
        }
    }
}
