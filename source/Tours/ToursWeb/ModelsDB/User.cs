using System.Linq;
using ToursWeb.ModelsBL;

#nullable disable

namespace ToursWeb.ModelsDB
{
    public partial class User
    {
        public User() { }
        
        public User(UserBL userBL)
        {
            Userid = userBL.Userid;
            Toursid = userBL.Toursid.ToArray();
            Login = userBL.Login;
            Password = userBL.Password;
            Accesslevel = userBL.Accesslevel;
        }
        
        public int Userid { get; set; }
        public int[] Toursid { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int? Accesslevel { get; set; }

        public UserBL ToBL()
        {
            UserBL userBL = new UserBL()
            {
                Userid = Userid,
                Toursid = Toursid.ToList(),
                Login = Login,
                Password = Password,
                Accesslevel = Accesslevel
            };

            return userBL;
        }
    }
}
