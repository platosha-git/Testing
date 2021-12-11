using System.Collections.Generic;
using System.Linq;
using ToursWeb.ModelsDB;

namespace ToursWeb.ModelsBL
{
    public class UserBL
    {
        public int Userid { get; set; }
        public List<int> Toursid { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int? Accesslevel { get; set; }

        public UserBL() { }
    }
}