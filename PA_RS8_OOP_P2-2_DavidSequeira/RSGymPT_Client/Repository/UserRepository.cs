using RSGymPT_DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D00_Utility;
using System.Collections.Specialized;
using System.Data.Common;
using System.ComponentModel.Design;
using RSGymPT_Client.Class;

namespace RSGymPT_Client.Repository
{
    public class UserRepository
    {
        public static void CreateUser()
        {
            using (var db = new RSGymContext())
            {
                IList<User> users = new List<User>()
                {
                    new User() {Name = "David Sequeira", Code = "user1", Password = "password1", Profile = User.EnumProfile.Administrator},
                    new User() {Name = "Ana Lopes", Code = "user2", Password = "password2", Profile = User.EnumProfile.Collaborator},
                    new User() {Name = "Salvador Sequeira", Code = "user3", Password = "password3", Profile = User.EnumProfile.Collaborator}
                };

                db.User.AddRange(users);
                db.SaveChanges();
            }
        }

        public static void ReadUser()
        {
            Console.Clear();
            Utility.WriteTitle("List of Users");

            Validation.ShowLoggedUser();

            using (var db = new RSGymContext())
            {
                var queryUser = db.User
                    .Select(x => x)
                    .OrderBy(x => x.Code);

                queryUser.ToList().ForEach(x => Utility.WriteMessage(
                    $"ID: {x.UserID}\n" +
                    $"Name: {x.Name}\n" +
                    $"Code: {x.Code}\n" +
                    $"Profile: {x.Profile}", "", "\n\n"));
            }
        }

        public static void UpdateUser()     // ToDo: Fazer loop
        {
            Console.Clear();
            Utility.WriteTitle("Update Password");

            Validation.ShowLoggedUser();

            using (var db = new RSGymContext())
            {
                Console.Write("Please insert the Code of the User your wish to update the Password: ");
                string code = Console.ReadLine();

                Console.Write("Please insert the Password: ");
                string password = Console.ReadLine();

                var queryUser = db.User
                    .Select(x => x)
                    .FirstOrDefault(x => x.Code == code && x.Password == password);

                if (queryUser != null)
                {
                    Console.Write("Please insert the new Password: ");
                    string newPassword = Console.ReadLine();

                    queryUser.Password = newPassword;
                    db.SaveChanges();

                }
                else
                {
                    Console.WriteLine("Please try again.");
                }
            }

        }

        public static void CreateNewUser()
        {
            Console.Clear();
            Utility.WriteTitle("Create new User");

            Validation.ShowLoggedUser();

            Console.WriteLine("Please fill the following fields with the new User's information: \n");

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Code: ");
            string code = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            using (var db = new RSGymContext())
            {
                IList<User> users = new List<User>()
                {
                    new User
                    {
                        Name = name,
                        Code = code,
                        Password = password,
                        Profile = User.EnumProfile.Collaborator
                    }
                };

                db.User.AddRange(users);
                db.SaveChanges();
            }
        }

    }
}
