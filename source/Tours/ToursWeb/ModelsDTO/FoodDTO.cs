using ToursWeb.ModelsBL;

namespace ToursWeb.ModelsDTO
{
    public class FoodUserDTO 
    {
        public string Category { get; set; }
        public FMenu? Menu { get; set; }
        public bool? Bar { get; set; }
        public int Cost { get; set; }

        public FoodUserDTO() { }
        
        public FoodBL GetFood(int foodID = 0)
        {
            FoodBL food = new FoodBL ()
            {
                Foodid = foodID,
                Category = Category,
                Menu = Menu,
                Bar = Bar,
                Cost = Cost
            };
            
            return food;
        }
    }
    
    public class FoodDTO : FoodUserDTO
    {
        public int Foodid { get; set; }

        public FoodDTO() {}
        
        public FoodDTO(FoodBL food)
        {
            Foodid = food.Foodid;
            Category = food.Category;
            Menu = food.Menu;
            Bar = food.Bar;
            Cost = food.Cost;
        }
    }
}