using System.Collections.Generic;
using ToursWeb.ModelsDB;
using ToursWeb.ModelsBL;

namespace ToursWeb.Repositories
{
    public interface IFoodRepository : CrudRepository<FoodBL, int>
    {
        List<FoodBL> FindFoodByCategory(string cat);
        List<FoodBL> FindFoodByMenu(string menu);
        List<FoodBL> FindFoodByBar(bool bar);
    }
}
