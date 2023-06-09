﻿using D00_Utility;
using RSGymPT_Client.Class;
using RSGymPT_DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;

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
                    new PersonalTrainer() { LocationID = 2, FirstName = "Inácio", LastName = "Silva", NIF = "100000001", PhoneNumber = "910000000", Email = "is@email.com", Address = "Rua A, nº1" },
                    new PersonalTrainer() { LocationID = 1, FirstName = "José", LastName = "Pedro", NIF = "100000002", PhoneNumber = "910000001", Email = "jp@email.com", Address = "Rua B, nº2" }

                };

                db.PersonalTrainer.AddRange(personalTrainers);
                db.SaveChanges();
            }
        }

        public static void ReadPT()
        {
            Console.Clear();
            Utility.WriteTitle("List of Personal Trainers");

            Validation.ShowLoggedUser();

            using (var db = new RSGymContext())
            {
                var queryPT = db.PersonalTrainer
                    .Select(x => x)
                    .OrderBy(x => x.FirstName);

                queryPT.ToList().ForEach(x => Utility.WriteMessage(
                    $"ID: {x.PersonalTrainerID}\n" +
                    $"Location ID: {x.LocationID}\n" +
                    $"Code: {x.Code}\n" +
                    $"Name: {x.Name}\n" +
                    $"NIF: {x.NIF}\n" +
                    $"Phone Number: {x.PhoneNumber}\n" +
                    $"Email: {x.Email}\n" +
                    $"Address: {x.Address} \n", "", "\n\n"));
            }
        }

        public static void ReadPTName()
        {
            Console.WriteLine("PT's Names:");       // ToDo: Este método foi criado para poder ser listado no ecrã uma lista simplificada (apenas com os nomes) aquando da criação de um novo Client.

            using (var db = new RSGymContext())
            {
                var queryPT = db.PersonalTrainer
                    .Select(x => x)
                    .OrderBy(x => x.FirstName);

                queryPT.ToList().ForEach(x => Utility.WriteMessage($"{x.Name}\n", "", ""));
            }
        }

        public static void CreateNewPT()
        {
            Console.Clear();
            Utility.WriteTitle("New PT");

            Validation.ShowLoggedUser();

            Console.WriteLine("Please fill the following fields with the Personal Trainer's information: \n");


            int locationID;             
            do
            {
                locationID = Validation.ValidateLocation();     // ToDo: Este processo vai pesquisar o LocationID através do nome da City. Caso não exista na base de dados a cidade terá de ser criada.
                if (locationID == 0)
                {
                    Console.WriteLine("Location not found. You need to create a new Location first.\n");
                    Utility_Menu.MenuNewLocation();
                }
            } while (locationID == 0);


            string firstName = Validation.ValidateName("First Name");

            string lastName = Validation.ValidateName("Last Name");


            string nif, validNIF;
            do
            {
                validNIF = Validation.ValidatePhoneAndNIF("NIF");       // ToDo: Para a inserção do NIF, inicialmente valido se o formato é correcto e depois procuro na base de dados para verificar se já existe, para não haver vários PT's com o mesmo NIF.

                nif = Validation.FindNIF(validNIF);

                if (nif == "0")
                {
                    Console.WriteLine("\nNIF already in database! Please try again\n");
                }
            } while (nif == "0");


            string phone = Validation.ValidatePhoneAndNIF("Phone Number");

            string email = Validation.ValidateEmail("Email");

            string address = Validation.ValidateAddress("Address");


            using (var db = new RSGymContext())
            {
                IList<PersonalTrainer> personalTrainers = new List<PersonalTrainer>()
                {
                    new PersonalTrainer()
                    {
                        LocationID = locationID,
                        FirstName = firstName,
                        LastName = lastName,
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

