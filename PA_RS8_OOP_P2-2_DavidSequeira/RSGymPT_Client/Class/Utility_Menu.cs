using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D00_Utility;
using RSGymPT_Client.Repository;
using RSGymPT_DAL.Model;

namespace RSGymPT_Client.Class
{
    internal class Utility_Menu
    {
        internal static void MenuInitial()
        {
            Utility.WriteTitle("Hello! Welcome to the RS Gym App!");

            Console.Write("Choose from one of the following options: \n");

            string[,] menuInitial = new string[2, 2]
                {
                    {"1", " - Login" },
                    {"0", " - Exit" }
                };

            for (int row = 0; row < menuInitial.GetLength(0); row++)
            {
                for (int col = 0; col < menuInitial.GetLength(1); col++)
                {
                    Console.Write(menuInitial[row, col]);
                }
                Console.WriteLine();
            }

            bool loopInitial = false;
            while (!loopInitial)
            {
                string choiceInitial = Console.ReadLine();

                string[] optionInitial = new string[menuInitial.GetLength(0)];
                for (int i = 0; i < menuInitial.GetLength(0); i++)
                {
                    optionInitial[i] = menuInitial[i, 0];
                }


                string foundedOption = Array.Find(optionInitial, e => e == choiceInitial);
                switch (choiceInitial)
                {
                    case "0":
                        Console.WriteLine("\nWe're sorry to see you leaving!");
                        Console.WriteLine("Thank you for using the RSGym App!");
                        Utility.TerminateConsole();
                        Environment.Exit(0);
                        loopInitial = true;
                        break;
                    case "1":
                        Console.Clear();
                        MenuLogin();
                        loopInitial = true;
                        break;
                    default:
                        Console.Write("\nInvalid option. Please try again. ");
                        Console.ReadKey();

                        Console.Clear();
                        Utility.WriteTitle("Hello! Welcome to the RS Gym App!");
                        Console.Write("Choose from one of the following options: \n");
                        for (int row = 0; row < menuInitial.GetLength(0); row++)
                        {
                            for (int col = 0; col < menuInitial.GetLength(1); col++)
                            {
                                Console.Write(menuInitial[row, col]);
                            }
                            Console.WriteLine();
                        }
                        loopInitial = false;
                        break;
                }
            }


        }

        internal static void MenuLogin()        // ToDo: Rever este Menu, não está funcional!!
        {
            Utility.WriteTitle("Hello! Welcome to RSGym!");

            bool loopLogin = false;
            while (!loopLogin)
            {
                (string, string) credentials = UserRepository.ReadCredentials();

                try
                {
                    (string, string) existingUser = UserRepository.ValidateCredentials(credentials);
                    Console.WriteLine($"Hello {existingUser.Item1}. Welcome to RSGym!");
                    Console.ReadKey();

                    MenuApp();

                    loopLogin = true;
                }
                catch (Exception)
                {

                    Console.WriteLine($"Login failed! Please try again.");
                }
            }
        }
                  
        internal static void MenuApp()
        {
            Console.Clear();
            Utility.WriteTitle("RS GYM - Office");

            Console.WriteLine("Choose one of the following options: \n");

            string[,] menuApp = new string[5, 2]
                {
                    {"1", " - User" },
                    {"2", " - Client" },
                    {"3", " - Personal Trainer" },
                    {"4", " - Request" },
                    {"0", " - Logout" }
                };

            for (int row = 0; row < menuApp.GetLength(0); row++)
            {
                for (int col = 0; col < menuApp.GetLength(1); col++)
                {
                    Console.Write(menuApp[row, col]);
                }
                Console.WriteLine();
            }

            bool loopApp = false;
            while (!loopApp)
            {
                string choiceApp = Console.ReadLine();

                string[] optionApp = new string[menuApp.GetLength(0)];
                for (int i = 0; i < menuApp.GetLength(0); i++)
                {
                    optionApp[i] = menuApp[i, 0];
                }

                string foundedOption = Array.Find(optionApp, e => e == choiceApp);
                switch (choiceApp)
                {
                    case "1":
                        MenuUser();
                        loopApp = true;
                        break;
                    case "2":
                        MenuClient();
                        loopApp = true;
                        break;
                    case "3":
                        MenuPersonalTrainer();
                        loopApp = true;
                        break;
                    case "4":
                        MenuRequest();
                        loopApp = true;
                        break;
                    case "0":
                        Console.WriteLine("We're sorry to see you leaving!");
                        Console.WriteLine("Thank you for using the RSGym App!");

                        Utility.TerminateConsole();
                        Environment.Exit(0);
                        loopApp = true;
                        break;
                    default:
                        Console.Write("\nInvalid option! Please try again. ");
                        Console.ReadKey();

                        Console.Clear();
                        Utility.WriteTitle("RS GYM - Office");

                        Console.WriteLine("Choose one of the following options: \n");
                        for (int row = 0; row < menuApp.GetLength(0); row++)
                        {
                            for (int col = 0; col < menuApp.GetLength(1); col++)
                            {
                                Console.Write(menuApp[row, col]);
                            }
                            Console.WriteLine();
                        }
                        loopApp = false;
                        break;
                }
            }


        }
                  
        internal static void MenuUser()
        {
            Console.Clear();
            Utility.WriteTitle("User");

            Console.WriteLine("Choose one of the following options: \n");

            string[,] menuUser = new string[4, 2]
                {
                    {"1", " - Create new User" },
                    {"2", " - Update Password" },
                    {"3", " - View Users " },
                    {"0", " - Logout" },
                };

            for (int row = 0; row < menuUser.GetLength(0); row++)
            {
                for (int col = 0; col < menuUser.GetLength(1); col++)
                {
                    Console.Write(menuUser[row, col]);
                }
                Console.WriteLine();
            }

            bool loopUser = false;
            while (!loopUser)
            {
                string choiceUser = Console.ReadLine();

                string[] optionUser = new string[menuUser.GetLength(0)];
                for (int i = 0; i < menuUser.GetLength(0); i++)
                {
                    optionUser[i] = menuUser[i, 0];
                }

                string foundedOption = Array.Find(optionUser, e => e == choiceUser);
                switch (choiceUser)
                {
                    case "0":
                        Logout();
                        loopUser = true;
                        break;
                    case "1":
                        Console.Clear();

                        UserRepository.CreateNewUser();

                        ReturnMenu();
                        loopUser = true;
                        break;
                    case "2":
                        Console.Clear();

                        UserRepository.UpdateUser();

                        ReturnMenu();
                        loopUser = true;
                        break;
                    case "3":
                        Console.Clear();

                        UserRepository.ReadUser();

                        ReturnMenu();
                        loopUser = true;
                        break;

                    default:
                        Console.Write("\nInvalid option! Please try again. ");
                        Console.ReadKey();

                        Console.Clear();
                        Utility.WriteTitle("User");
                        Console.WriteLine("Choose one of the following options: \n");
                        for (int row = 0; row < menuUser.GetLength(0); row++)
                        {
                            for (int col = 0; col < menuUser.GetLength(1); col++)
                            {
                                Console.Write(menuUser[row, col]);
                            }
                            Console.WriteLine();
                        }

                        loopUser = false;
                        break;
                }
            }
        }
                  
        internal static void MenuClient()
        {
            Console.Clear();
            Utility.WriteTitle("Client");

            Console.WriteLine("Choose one of the following options: \n");

            string[,] menuClient = new string[5, 2]
                {
                    {"1", " - Create new Client" },
                    {"2", " - Update Information" },
                    {"3", " - View Clients" },
                    {"4", " - Change Status" },
                    {"0", " - Return to Main Menu" },
                };

            for (int row = 0; row < menuClient.GetLength(0); row++)
            {
                for (int col = 0; col < menuClient.GetLength(1); col++)
                {
                    Console.Write(menuClient[row, col]);
                }
                Console.WriteLine();
            }

            bool loopClient = false;
            while (!loopClient)
            {
                string choiceClient = Console.ReadLine();

                string[] optionClient = new string[menuClient.GetLength(0)];
                for (int i = 0; i < menuClient.GetLength(0); i++)
                {
                    optionClient[i] = menuClient[i, 0];
                }

                string foundedOption = Array.Find(optionClient, e => e == choiceClient);
                switch (choiceClient)
                {
                    case "0":
                        MenuApp();
                        loopClient = true;
                        break;
                    case "1":
                        Console.Clear();

                        // ClientRepository.CreateNewClient();  // ToDo: Rever como receber parametros necessários.

                        ReturnMenu();
                        loopClient = true;
                        break;
                    case "2":
                        Console.Clear();

                        ClientRepository.UpdateClient();

                        ReturnMenu();
                        loopClient = true;
                        break;
                    case "3":
                        Console.Clear();

                        ClientRepository.ReadClient();

                        ReturnMenu();
                        loopClient = true;
                        break;
                    case "4":
                        Console.Clear();
                        ClientRepository.DeleteClient();

                        ReturnMenu();
                        loopClient = true;
                        break;
                    default:
                        Console.Write("\nInvalid option! Please try again. ");
                        Console.ReadKey();

                        Console.Clear();
                        Utility.WriteTitle("Client");
                        Console.WriteLine("Choose one of the following options: \n");
                        for (int row = 0; row < menuClient.GetLength(0); row++)
                        {
                            for (int col = 0; col < menuClient.GetLength(1); col++)
                            {
                                Console.Write(menuClient[row, col]);
                            }
                            Console.WriteLine();
                        }

                        loopClient = false;
                        break;
                }
            }
        }
                  
        internal static void MenuPersonalTrainer()
        {
            Console.Clear();
            Utility.WriteTitle("Personal Trainer");

            Console.WriteLine("Choose one of the following options: \n");

            string[,] menuPT = new string[3, 2]
                {
                    {"1", " - Create new Personal Trainer" },
                    {"2", " - View Personal Trainers" },
                    {"0", " - Return to Main Menu" },
                };

            for (int row = 0; row < menuPT.GetLength(0); row++)
            {
                for (int col = 0; col < menuPT.GetLength(1); col++)
                {
                    Console.Write(menuPT[row, col]);
                }
                Console.WriteLine();
            }

            bool loopPT = false;
            while (!loopPT)
            {
                string choicePT = Console.ReadLine();

                string[] optionPT = new string[menuPT.GetLength(0)];
                for (int i = 0; i < menuPT.GetLength(0); i++)
                {
                    optionPT[i] = menuPT[i, 0];
                }

                string foundedOption = Array.Find(optionPT, e => e == choicePT);
                switch (choicePT)
                {
                    case "0":
                        Console.Clear();
                        MenuApp();
                        loopPT = true;
                        break;
                    case "1":
                        Console.Clear();

                        // PersonalTrainerRepository.CreateNewPT();     // ToDo: Rever como receber parametros necessários.

                        ReturnMenu();
                        loopPT = true;
                        break;
                    case "2":
                        Console.Clear();

                        PersonalTrainerRepository.ReadPT();

                        ReturnMenu();
                        loopPT = true;
                        break;

                    default:
                        Console.Write("\nInvalid option! Please try again. ");
                        Console.ReadKey();

                        Console.Clear();
                        Utility.WriteTitle("Personal Trainer");
                        Console.WriteLine("Choose one of the following options: \n");
                        for (int row = 0; row < menuPT.GetLength(0); row++)
                        {
                            for (int col = 0; col < menuPT.GetLength(1); col++)
                            {
                                Console.Write(menuPT[row, col]);
                            }
                            Console.WriteLine();
                        }
                        loopPT = false;
                        break;
                }
            }


        }
                  
        internal static void MenuRequest()
        {
            bool loopRequest = false;
            while (!loopRequest)
            {
                Console.Clear();
                Utility.WriteTitle("Request");

                Console.WriteLine("Choose one of the following options: \n");

                string[,] menuRequest = new string[3, 2]
                    {
                        {"1", " - Create new Request" },
                        {"2", " - View Requests" },
                        {"0", " - Return to Main Menu" }
                    };

                for (int row = 0; row < menuRequest.GetLength(0); row++)
                {
                    for (int col = 0; col < menuRequest.GetLength(1); col++)
                    {
                        Console.Write(menuRequest[row, col]);
                    }
                    Console.WriteLine();
                }

                string choiceRequest = Console.ReadLine();

                string[] optionRequest = new string[menuRequest.GetLength(0)];
                for (int i = 0; i < menuRequest.GetLength(0); i++)
                {
                    optionRequest[i] = menuRequest[i, 0];
                }

                Request request = new Request();
                string foundedOption = Array.Find(optionRequest, e => e == choiceRequest);
                if (foundedOption != null)
                {
                    foundedOption = Array.Find(optionRequest, e => e == choiceRequest);
                    switch (choiceRequest)
                    {
                        case "0":
                            // Return Menu;
                            loopRequest = true;
                            break;
                        case "1":
                            Console.Clear();

                            RequestRepository.CreateNewRequest();

                            ReturnMenuRequest();
                            loopRequest = false;
                            break;
                        case "2":
                            Console.Clear();

                            RequestRepository.ReadRequest();

                            ReturnMenuRequest();
                            loopRequest = false;
                            break;
                        default:
                            Console.WriteLine("\nThe option selected is not valid. Please try again.\n");
                            loopRequest = true;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\nThe option selected is not valid. Please try again.\n");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
                  
        internal static void ReturnMenu()
        {
            string selected;
            do
            {
                Console.Write("\nPress x to return no Main Menu: ");
                selected = Console.ReadLine();

                if (selected == "x")
                {
                    MenuApp();
                }
                else
                {
                    Console.Write("\nIf you want to return to Main Menu please press x: ");
                    selected = Console.ReadLine();
                }
            } while (selected != "x");
        }
                  
        internal static void ReturnMenuRequest()
        {
            string selected;
            do
            {
                Console.Write("\nPress x to return no Menu Request: ");
                selected = Console.ReadLine();

                if (selected == "x")
                {
                    MenuRequest();
                }
            } while (selected != "x");
        }
                  
        internal static void Logout()
        {
            Console.Write("\nPress x to Logout: ");
            string selected = Console.ReadLine();

            while (selected != "x")
            {
                Console.WriteLine("\nIf you want to Logout please press x.");
                Console.Write("\nPress x to Logout: ");
                selected = Console.ReadLine();
            }

            Console.WriteLine("\nWe're sorry to see you leaving!");
            Console.WriteLine("Thank you for using the RSGym App!");
            Console.ReadKey();

            Console.Clear();
            MenuLogin();
        }

    }
}
