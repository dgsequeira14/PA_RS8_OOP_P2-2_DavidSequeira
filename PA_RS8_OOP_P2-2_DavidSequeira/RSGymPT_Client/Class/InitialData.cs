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
    public class InitialData
    {
        public static void CreateInitialData()
        {
            #region Initial Data Creation

            LocationRepository.CreateLocation();
            
            UserRepository.CreateUser();
            
            PersonalTrainerRepository.CreatePT();
            
            ClientRepository.CreateClient();
            
            RequestRepository.CreateRequest();

            #endregion

        }

        public static void ReadInitialData()
        {
            #region Location

            Utility.WriteTitle("Locations");

            LocationRepository.ReadLocation();

            #endregion

            #region User

            Utility.BlockSeparator("\n");
            Utility.WriteTitle("Users");

            UserRepository.ReadUser();

            #endregion

            #region Personal Trainer

            Utility.BlockSeparator("\n");
            Utility.WriteTitle("Personal Trainers");

            PersonalTrainerRepository.ReadPT();

            #endregion

            #region Client

            Utility.BlockSeparator("\n");
            Utility.WriteTitle("Client");

            ClientRepository.ReadClient();

            #endregion

            #region Request

            Utility.BlockSeparator("\n");
            Utility.WriteTitle("Request");

            RequestRepository.ReadRequest();

            #endregion
        }



    }
}
