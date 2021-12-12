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

        public FoodBuilder()
        {
            Foodid = 0;
            Category = "";
            Menu = null;
            Bar = false;
            Cost = 0;
        }

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
    }
}