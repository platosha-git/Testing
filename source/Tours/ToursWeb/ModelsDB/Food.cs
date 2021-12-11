using System;
using System.Collections.Generic;
using ToursWeb.ModelsBL;

#nullable disable

namespace ToursWeb.ModelsDB
{
    public partial class Food
    {
        public Food()
        {
            Tours = new HashSet<Tour>();
        }

        public Food(FoodBL foodBL)
        {
            Foodid = foodBL.Foodid;
            Category = foodBL.Category;
            Menu = (foodBL.Menu is null) ? "" : Enum.GetName(typeof(FMenu), foodBL.Menu);
            Bar = foodBL.Bar;
            Cost = foodBL.Cost;
        }

        public int Foodid { get; set; }
        public string Category { get; set; }
        public string Menu { get; set; }
        public bool? Bar { get; set; }
        public int Cost { get; set; }

        public virtual ICollection<Tour> Tours { get; set; }

        public FoodBL ToBL()
        {
            FoodBL foodBL = new FoodBL()
            {
                Foodid = Foodid,
                Category = Category,
                Menu = (Menu == "") ? null : (FMenu) Enum.Parse(typeof(FMenu), Menu),
                Bar = Bar,
                Cost = Cost
            };

            return foodBL;
        }
    }
}
