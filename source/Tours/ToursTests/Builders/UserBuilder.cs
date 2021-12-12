using System;
using System.Collections.Generic;
using ToursWeb.ModelsBL;

namespace ToursTests.Builders
{
    public class UserBuilder
    {
        private int Userid;
        private List<int> Toursid;
        private string Login;
        private string Password;
        private int? Accesslevel;

        public UserBuilder() { }

        public UserBL Build()
        {
            var user = new UserBL()
            {
                Userid = Userid,
                Toursid = Toursid,
                Login = Login,
                Password = Password,
                Accesslevel = Accesslevel
            };

            return user;
        }
        
        public UserBuilder WhereLogin(string login)
        {
            Login = login;
            return this;
        }
    }
}