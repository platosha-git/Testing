using System;
using ToursWeb.ModelsBL;

namespace ToursTests.Builders
{
    public class FoodBuilder
    {
        private int Foodid;
        private string Category;
        private FMenu? Menu;
        private bool? Bar;
        private int Cost;

        public FoodBuilder() { }

        public FoodBL Build()
        {
            var food = new FoodBL()
            {
                Foodid = Foodid,
                Category = Category,
                Menu = Menu,
                Bar = Bar,
                Cost = Cost
            };

            return food;
        }
        
        public FoodBuilder WhereFoodID(int foodID)
        {
            Foodid = foodID;
            return this;
        }
        
        public FoodBuilder WhereCategory(string category)
        {
            Category = category;
            return this;
        }
        
        public FoodBuilder WhereMenu(string menu)
        {
            Menu = (menu == "") ? null : (FMenu) Enum.Parse(typeof(FMenu), menu);
            return this;
        }
        
        public FoodBuilder WhereBar(bool bar)
        {
            Bar = bar;
            return this;
        }
    }
}
