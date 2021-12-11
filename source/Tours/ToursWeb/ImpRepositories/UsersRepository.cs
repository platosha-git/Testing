using System;
using System.Collections.Generic;
using System.Linq;
using ToursWeb.Repositories;
using Microsoft.Extensions.Logging;
using ToursWeb.ModelsDB;
using ToursWeb.ModelsBL;

namespace ToursWeb.ImpRepositories
{
    public class UsersRepository : IUsersRepository, IDisposable
    {
        private readonly ToursContext _db;
        private readonly ILogger<UsersRepository> _logger;

        public UsersRepository(ToursContext createDB, ILogger<UsersRepository> logDB)
        {
            _db = createDB;
            _logger = logDB;
        }
        
        List<UserBL> ListUserBL(List<User> users)
        {
            if (users == null)
            {
                return null;
            }
            
            List<UserBL> usersBL = new List<UserBL>();
            foreach (var user in users)
            {
                UserBL userBL = user.ToBL();
                usersBL.Add(userBL);
            }

            return usersBL;
        }

        public List<UserBL> FindAll()
        {
            List<User> users = _db.Users.ToList();
            List<UserBL> usersBL = ListUserBL(users);
            return usersBL;
        }

        public UserBL FindByID(int id)
        {
            User user = _db.Users.Find(id);
            if (user == null)
            {
                return null;
            }

            UserBL userBL = user.ToBL();
            return userBL;
        }

        public ExitCode Add(UserBL obj)
        {
            try
            {
                User user = new User(obj);
                obj.Userid = _db.Users.Count() + 1;
                
                user.Userid = _db.Users.Count() + 1;
                _db.Users.Add(user);
                _db.SaveChanges();
                _logger.LogInformation("+UsersRep : User {Number} was added to Users", obj.Userid);
                return ExitCode.Success;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException err)
            {
                _logger.LogError(err, "+UsersRep : Constraint violation when trying to add user to Users");
                return ExitCode.Constraint;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+UsersRep : Error trying to add user to Users");
                return ExitCode.Error;
            }
        }

        public ExitCode Update(UserBL obj)
        {
            try
            {
                User uUser = new User(obj);
                _db.Users.Update(uUser);
                _db.SaveChanges();
                _logger.LogInformation("+UsersRep : User {Number} was updated at Users", obj.Userid);
                return ExitCode.Success;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException err)
            {
                _logger.LogError(err, "+UsersRep : Constraint violation when trying to update user to Users");
                return ExitCode.Constraint;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+UsersRep : Error trying to update user at Users");
                return ExitCode.Error;
            }
        }

        public ExitCode DeleteByID(int id)
        {
            try
            {
                User user = _db.Users.Find(id);
                _db.Users.Remove(user);
                _db.SaveChanges();
                _logger.LogInformation("+UsersRep : User {Number} was deleted from Users", id);
                return ExitCode.Success;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+UsersRep : Error trying to delete user {Number} from Users", id);
                return ExitCode.Error;
            }
        }

        public List<UserBL> FindUsersByLogin(string login)
        {
            IQueryable<User> users = _db.Users.Where(needed => needed.Login.Contains(login));
            List<User> lUsers = users.ToList();
            List<UserBL> lUsersBL = ListUserBL(lUsers);
            return lUsersBL;
        }

        public UserBL FindUserByLP(string login, string password)
        {
            IQueryable<User> users = _db.Users.Where(needed => needed.Login.Equals(login) &&
                                                                   needed.Password.Equals(password));
            if (!users.Any())
            {
                return null;
            }
            
            User user = users.First();
            UserBL userBL = user.ToBL();
            return userBL;
        }
        
        public List<int> FindBookedTours(int id)
        {
            UserBL user = FindByID(id);
            if (user is null)
            {
                return null;
            }
            
            return user.Toursid;
        }
        
        public ExitCode UpdateTours(UserBL obj, List<int> toursID)
        {
            try
            {
                User user = new User(obj);
                user.Toursid = toursID.ToArray();
                _db.Users.Update(user);
                _db.SaveChanges();
                _logger.LogInformation("+UsersRep : Tours user {Number} was updated at Users", obj.Userid);
                return ExitCode.Success;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException err)
            {
                _logger.LogError(err, "+UsersRep : Constraint violation when trying to update user tours to Users");
                return ExitCode.Constraint;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+UsersRep : Error trying to update tours at Users");
                return ExitCode.Error;
            }
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
