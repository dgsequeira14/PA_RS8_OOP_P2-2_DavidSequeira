using RSGymPT_Client.Repository;

namespace RSGymPT_Client.Class
{
    public class InitialData
    {
        public static void CreateInitialData()
        {
            LocationRepository.CreateLocation();
            
            UserRepository.CreateUser();
            
            PersonalTrainerRepository.CreatePT();
            
            ClientRepository.CreateClient();
            
            RequestRepository.CreateRequest();

        }
    }
}
