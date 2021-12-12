using System.Collections.Generic;
using ToursWeb.Repositories;
using ToursWeb.Controllers;
using ToursTests.Builders;
using ToursWeb.ModelsBL;
using Xunit;
using Moq;

namespace ToursTests.Tests
{
    public class UserTests
    {
        [Fact]
        public void FindAll()
        {
            var expUser = new UserBuilder().Build();
            var expUsers = new List<UserBL>() {expUser};

            var mock = new Mock<IUsersRepository>();
            mock.Setup(x => x.FindAll())
                .Returns(expUsers);
            var userController = new UserController(mock.Object);
            
            var actUsers = userController.GetAllUsers();
            
            Assert.Equal(expUsers, actUsers);
        }
        
        [Fact]
        public void FindByLogin()
        {
            const string login = "test";
            
            var expUser = new UserBuilder()
                .WhereLogin(login)
                .Build();
            var expUsers = new List<UserBL>() {expUser};

            var mock = new Mock<IUsersRepository>();
            mock.Setup(x => x.FindUsersByLogin(login))
                .Returns(expUsers);
            var userController = new UserController(mock.Object);
            
            var actUsers = userController.GetUserByLogin(login);
            
            Assert.Equal(expUsers, actUsers);
        }
        
        [Fact]
        public void FindBookedTours()
        {
            const int userID = 2;

            var expTours = new List<int>();

            var mock = new Mock<IUsersRepository>();
            mock.Setup(x => x.FindBookedTours(userID))
                .Returns(expTours);
            var userController = new UserController(mock.Object);
            
            var actTours = userController.GetBookedTours(userID);
            
            Assert.Equal(expTours, actTours);
        }
    }
}
