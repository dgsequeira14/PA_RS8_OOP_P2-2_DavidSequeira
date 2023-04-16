using D00_Utility;
using RSGymPT_Client.Class;
using RSGymPT_DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;

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
                    new User() {FirstName = "David", LastName = "Sequeira", Code = "user1", Password = "password1", Profile = User.EnumProfile.Administrator.ToString()},
                    new User() {FirstName = "Ana", LastName = "Lopes", Code = "user2", Password = "password2", Profile = User.EnumProfile.Collaborator.ToString()},
                    new User() {FirstName = "Salvador", LastName = "Sequeira", Code = "user3", Password = "password3", Profile = User.EnumProfile.Collaborator.ToString()}
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

        public static void UpdateUser()
        {
            Console.Clear();
            Utility.WriteTitle("Update Password");

            Validation.ShowLoggedUser();

            using (var db = new RSGymContext())
            {
                Console.Write("Code of the User your wish to update the Password: ");
                string code = Console.ReadLine();

                Console.Write("Password: ");
                string password = Console.ReadLine();

                bool loopUpdate = false;
                while (!loopUpdate)
                {
                    var queryUser = db.User
                        .Select(x => x)
                        .FirstOrDefault(x => x.Code == code && x.Password == password);

                    if (queryUser != null)
                    {
                        string newPassword = Validation.ValidatePassword("New Password");

                        if (newPassword == password)
                        {
                            Console.WriteLine("New password needs to be different from old password.\n");
                        }
                        else
                        {
                            queryUser.Password = newPassword;
                            db.SaveChanges();
                            loopUpdate = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please try again.\n");
                        loopUpdate = false;
                    }
                }

            }

        }

        public static void CreateNewUser()
        {
            Console.Clear();
            Utility.WriteTitle("New User");

            Validation.ShowLoggedUser();

            Console.WriteLine("Please fill the following fields with the new User's information: \n");

            string firstName = Validation.ValidateName("First Name");

            string lastName = Validation.ValidateName("Last Name");

            string code, validCode;
            do
            {
                validCode = Validation.ValidateCode();

                code = Validation.FindCode(validCode);

                if (code == "0")
                {
                    Console.WriteLine("Code already exists! Plesase choose other code.\n");
                }
            } while (code == "0");

            string password = Validation.ValidatePassword("Password");

            using (var db = new RSGymContext())
            {
                IList<User> users = new List<User>()
                {
                    new User
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Code = code,
                        Password = password,
                        Profile = User.EnumProfile.Collaborator.ToString()
                    }
                };

                db.User.AddRange(users);
                db.SaveChanges();
            }
        }

    }
}
