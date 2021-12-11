using System.Collections.Generic;
using ToursWeb.ModelsBL;

namespace ToursWeb.ModelsDTO
{
    public class UserDTO
    {
        public int Userid { get; set; }
        public List<int> Toursid { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int? Accesslevel { get; set; }

        public UserDTO()
        {
        }
        
        public UserDTO(UserBL user)
        {
            Userid = user.Userid;
            Toursid = user.Toursid;
            Login = user.Login;
            Password = user.Password;
            Accesslevel = user.Accesslevel;
        }
    }
}