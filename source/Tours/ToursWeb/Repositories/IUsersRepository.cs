using System.Collections.Generic;
using ToursWeb.ModelsDB;
using ToursWeb.ModelsBL;

namespace ToursWeb.Repositories
{
    public interface IUsersRepository : CrudRepository<UserBL, int>
    {
        List<UserBL> FindUsersByLogin(string login);
        UserBL FindUserByLP(string login, string password);
        List<int> FindBookedTours(int id);
        public ExitCode UpdateTours(UserBL obj, List<int> toursID);
    }
}
