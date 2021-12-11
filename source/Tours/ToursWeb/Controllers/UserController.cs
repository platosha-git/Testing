using System.Collections.Generic;
using ToursWeb.ModelsDB;
using ToursWeb.ModelsBL;
using ToursWeb.Repositories;

namespace ToursWeb.Controllers
{
    public class UserController
    {
        private readonly IUsersRepository _userRepository;

        public UserController(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public List<UserBL> GetAllUsers()
        {
            return _userRepository.FindAll();
        }
        
        public UserBL GetAllUserInfo(int userID)
        {
            return _userRepository.FindByID(userID);
        }

        public List<UserBL> GetUserByLogin(string login)
        {
            return _userRepository.FindUsersByLogin(login);
        }
        
        public UserBL GetUserByLP(string login, string password)
        {
            return _userRepository.FindUserByLP(login, password);
        }

        public List<int> GetBookedTours(int id)
        {
            return _userRepository.FindBookedTours(id);
        }

        public ExitCode BookTour(int userID, int tourID)
        {
            UserBL user = _userRepository.FindByID(userID);
            List<int> oldTours = user.Toursid;
            List<int> newTours = new List<int>();
            int size = oldTours.Count;
            
            if (size > 0)
            {
                foreach (int tour in oldTours)
                {
                    newTours.Add(tour);
                }
            }
            newTours.Add(tourID);

            return _userRepository.UpdateTours(user, newTours);
        }

        public ExitCode CancelTour(int userID, int tourID)
        {
            UserBL user = _userRepository.FindByID(userID);
            List<int> oldTours = user.Toursid;
            List<int> newTours = new List<int>(oldTours);
            newTours.Remove(tourID);

            return _userRepository.UpdateTours(user, newTours);
        }
    }
}