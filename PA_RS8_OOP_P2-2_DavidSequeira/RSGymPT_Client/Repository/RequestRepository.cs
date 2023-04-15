using D00_Utility;
using RSGymPT_Client.Class;
using RSGymPT_DAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
                    new Request { ClientID = 1, PersonalTrainerID = 1, Date = new DateTime(2023, 06, 10) , Hour = new DateTime( 2023, 06, 10, 10, 00, 00), Status = Request.EnumStatusRequest.Booked.ToString() },
                    new Request { ClientID = 2, PersonalTrainerID = 2, Date = new DateTime(2023, 06, 12), Hour = new DateTime(2023, 06, 12, 12, 00, 00), Status = Request.EnumStatusRequest.Booked.ToString() },
                    new Request { ClientID = 3, PersonalTrainerID = 1, Date = new DateTime(2023, 04, 25), Hour = new DateTime(2023, 04, 25, 16, 00, 00) , Status = Request.EnumStatusRequest.Booked.ToString() }
                };

                db.Request.AddRange(requests);
                db.SaveChanges();
            }
        }

        public static void ReadRequest()
        {
            Console.Clear();
            Utility.WriteTitle("List of Requests");

            using (var db = new RSGymContext())
            {

                var queryRequest = db.Request
                    .Select(x => x)
                    .OrderBy(x => x.Status)
                    .ThenBy(x => x.Date)
                    .ThenBy(x => x.Hour);

                queryRequest.ToList().ForEach(x => Utility.WriteMessage(
                    $"ID: {x.RequestID}\n" +
                    $"Client Name and (ID): {x.Client.Name} ({x.ClientID})\n" +
                    $"Personal Trainer Name and (ID): {x.PersonalTrainer.Name} ({x.PersonalTrainerID})\n" +
                    $"Date and Time: {x.Date.ToShortDateString()} - {x.Hour.ToShortTimeString()}\n" +
                    $"Status: {x.Status}\n" +
                    $"Observations: {x.Observation}", "", "\n\n"));
            }

        }

        public static void UpdateRequest()
        {
            Console.Clear();
            Utility.WriteTitle("Update Request");

            Validation.ShowLoggedUser();

            RequestRepository.ReadRequest();       // ToDo: Para facilitar a introdução do Request ID, mostro a lista de Requests.

            Console.Write("Please insert the Request ID to update: ");
            int.TryParse(Console.ReadLine(), out int requestID);

            using (var db = new RSGymContext())
            {
                var queryRequest = db.Request
                    .Select(x => x)
                    .FirstOrDefault(x => x.RequestID == requestID);

                if (queryRequest != null)
                {
                    Console.Write("PT ID: ");
                    int.TryParse(Console.ReadLine(), out int newPT);

                    DateTime newDate = Validation.ValidateDate("Date");

                    DateTime newHour = Validation.ValidateHour("Hour");

                    queryRequest.PersonalTrainerID = newPT;
                    queryRequest.Date = newDate;
                    queryRequest.Hour = newHour;

                    db.SaveChanges();
                }
            };
        }

        public static void CreateNewRequest()
        {
            Console.Clear();

            Validation.ShowLoggedUser();

            ClientRepository.ReadClient();          // ToDo: Para facilitar a introdução das FK, mostro a lista de Client e Personal Trainer
            PersonalTrainerRepository.ReadPT();

            Utility.WriteTitle("New Request");

            Console.WriteLine("Please fill the following fields with the Request's details: \n");

            Console.Write("Client ID: ");
            int.TryParse(Console.ReadLine(), out int clientID);

            Console.Write("PT ID: ");
            int.TryParse(Console.ReadLine(), out int ptID);

            DateTime date = Validation.ValidateDate("Date");

            DateTime hour = Validation.ValidateHour("Hour");

            string obs = Validation.ValidateObs("Observations");

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
                        Status = Request.EnumStatusRequest.Booked.ToString(),
                        Observation = obs,
                    }
                };

                db.Request.AddRange(requests);
                db.SaveChanges();
            }
        }
    }
}
