using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using ToursWeb.ModelsDB;
using ToursWeb.ModelsBL;
using ToursWeb.Repositories;

namespace ToursWeb.ImpRepositories
{
    public class FoodRepository : IFoodRepository, IDisposable
    {
        private readonly ToursContext _db;
        private readonly ILogger<FoodRepository> _logger;

        public FoodRepository(ToursContext createDB, ILogger<FoodRepository> logDB)
        {
            _db = createDB;
            _logger = logDB;
        }

        List<FoodBL> ListFoodBL(List<Food> foods)
        {
            if (foods == null)
            {
                return null;
            }
            
            List<FoodBL> foodsBL = new List<FoodBL>();
            foreach (var food in foods)
            {
                FoodBL foodBL = food.ToBL();
                foodsBL.Add(foodBL);
            }

            return foodsBL;
        }
        
        public List<FoodBL> FindAll()
        {
            List<Food> foods = _db.Foods.ToList();
            List<FoodBL> foodsBL = ListFoodBL(foods);
            return foodsBL;
        }

        public FoodBL FindByID(int id)
        {
            Food food = _db.Foods.Find(id);
            if (food is null)
            {
                return null;
            }
            
            FoodBL foodBL = food.ToBL();
            return foodBL;
        }

        public ExitCode Add(FoodBL obj)
        {
            try
            {
                Food food = new Food(obj);
                obj.Foodid = _db.Foods.Count() + 1;
                
                food.Foodid = _db.Foods.Count() + 1;
                _db.Foods.Add(food);
                _db.SaveChanges();
                _logger.LogInformation("+FoodRep : Food {Number} was added to Food", obj.Foodid);
                return ExitCode.Success;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException err)
            {
                _logger.LogError(err, "+FoodRep : Constraint violation when trying to add food to Food");
                return ExitCode.Constraint;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+FoodRep : Error trying to add food to Food");
                return ExitCode.Error;
            }
        }

        public ExitCode Update(FoodBL obj)
        {
            try
            {
                Food uFood = new Food(obj);
                _db.Foods.Update(uFood);
                _db.SaveChanges();
                _logger.LogInformation("+FoodRep : Food {Number} was updated at Food", obj.Foodid);
                return ExitCode.Success;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException err)
            {
                _logger.LogError(err, "+FoodRep : Constraint violation when trying to update food to Food");
                return ExitCode.Constraint;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+FoodRep : Error trying to update food to Food");
                return ExitCode.Error;
            }
        }

        public ExitCode DeleteByID(int id)
        {
            try
            {
                Food food = _db.Foods.Find(id);
                _db.Foods.Remove(food);
                _db.SaveChanges();
                _logger.LogInformation("+FoodRep : Food {Number} was deleted from Food", id);
                return ExitCode.Success;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+FoodRep : Error trying to delete food {Number} from Food", id);
                return ExitCode.Error;
            }
        }

        public List<FoodBL> FindFoodByCategory(string cat)
        {
            IQueryable<Food> foods = _db.Foods.Where(needed => needed.Category.Equals(cat));
            List<Food> lFoods = foods.ToList();
            List<FoodBL> lFoodsBL = ListFoodBL(lFoods);
            return lFoodsBL;
        }

        public List<FoodBL> FindFoodByMenu(string menu)
        {
            IQueryable<Food> foods = _db.Foods.Where(needed => needed.Menu.Equals(menu));
            List<Food> lFoods = foods.ToList();
            List<FoodBL> lFoodsBL = ListFoodBL(lFoods);
            return lFoodsBL;
        }

        public List<FoodBL> FindFoodByBar(bool bar)
        {
            IQueryable<Food> foods = _db.Foods.Where(needed => needed.Bar == bar);
            List<Food> lFoods = foods.ToList();
            List<FoodBL> lFoodsBL = ListFoodBL(lFoods);
            return lFoodsBL;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
