using System.Collections.Generic;
using ToursWeb.Repositories;
using ToursWeb.ModelsDB;
using ToursWeb.ModelsBL;

namespace ToursWeb.Controllers
{
    public class FoodController
    {
        private readonly IFoodRepository _foodRepository;

        public FoodController(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }
        
        public List<FoodBL> GetAllFood()
        {
            return _foodRepository.FindAll();
        }

        public FoodBL GetFoodByID(int foodID)
        {
            return _foodRepository.FindByID(foodID);
        }

        public List<FoodBL> GetFoodByCategory(string cat)
        {
            return _foodRepository.FindFoodByCategory(cat);
        }

        public List<FoodBL> GetFoodByMenu(string menu)
        {
            return _foodRepository.FindFoodByMenu(menu);
        }

        public List<FoodBL> GetFoodByBar(bool bar)
        {
            return _foodRepository.FindFoodByBar(bar);
        }
        
        public ExitCode AddFood(FoodBL food)
        {
            return _foodRepository.Add(food);
        }
        
        public ExitCode UpdateFood(FoodBL food)
        {
            return _foodRepository.Update(food);
        }
        
        public ExitCode DeleteFoodByID(int foodID)
        {
            return _foodRepository.DeleteByID(foodID);
        }
    }
}