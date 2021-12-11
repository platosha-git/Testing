namespace ToursWeb.ModelsBL
{
    public enum FMenu
    {
        Vegeterian,
        Children,
        Dietary
    };
    
    public class FoodBL
    {
        public int Foodid { get; set; }
        public string Category { get; set; }
        public FMenu? Menu { get; set; }
        public bool? Bar { get; set; }
        public int Cost { get; set; }

        public FoodBL() { }

        private bool Equals(FoodBL food)
        {
            if (food is null)
            {
                return false;
            }

            return Foodid == food.Foodid && 
                   Category == food.Category &&
                   Menu == food.Menu &&
                   Bar == food.Bar &&
                   Cost == food.Cost;
        }

        public override bool Equals(object obj) => Equals(obj as FoodBL);
        public override int GetHashCode() => (Foodid, Category, Menu, Bar, Cost).GetHashCode();
    }
}