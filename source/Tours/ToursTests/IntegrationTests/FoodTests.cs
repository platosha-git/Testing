using Xunit;
using ToursWeb.ModelsDB;
using ToursWeb.ModelsBL;
using ToursTests.Builders;
using System.Collections.Generic;

namespace ToursTests.IntegrationTests
{
    public class FoodTests : IClassFixture<TourAccessObject>
    {
        private readonly TourAccessObject _accessObject;

        public FoodTests(TourAccessObject tourAccessObject) 
        {
            _accessObject = tourAccessObject;
        }

        /*[Fact]
        public void FindAll_NotNull()
        {
            // Arrange
            List<Food> teams = new List<Food>();
            for (var i = 1; i < 4; i++)
            {
                var curTeam = new FoodBuilder().WhereFoodID(i).Build();
                teams.Add(new Food(curTeam));
            }
            
            _accessObject.toursContext.Foods.AddRange(teams);
            _accessObject.toursContext.SaveChanges();
            
            // Act
            var result = _accessObject.foodRepository.FindAll();
            
            // Assert
            Assert.Equal(teams.Count, result.Count);
            for (var i = 0; i < teams.Count; i++)
            {
                Assert.Equal(teams[i].Foodid, result[i].Foodid);
            }
            
            Cleanup();

        }

        [Fact]
        public void FindByID_FirstElement_NotNull()
        {
            // Arrange
            const int id = 1; 
            
            List<Food> teams = new List<Food>();
            for (var i = 1; i < 4; i++)
            {
                var curTeam = new FoodBuilder().WhereFoodID(i).Build();
                teams.Add(new Food(curTeam));
            }
            
            _accessObject.toursContext.Foods.AddRange(teams);
            _accessObject.toursContext.SaveChanges();
            
            // Act
            var result = _accessObject.foodRepository.FindByID(id);
            
            // Assert
            Assert.Equal(id, result.Foodid);
            
            Cleanup();

        }
        
        [Fact]
        public void FindByCategory_Breakfast_NotNull()
        {
            // Arrange
            const string name = "aboba";

            List<Food> teams = new List<Food>();
            for (var i = 1; i < 4; i++)
            {
                var curTeam = new FoodBuilder().WhereFoodID(i).WhereCategory(name).Build();
                teams.Add(new Food(curTeam));
            }
            
            _accessObject.toursContext.Foods.AddRange(teams);
            _accessObject.toursContext.SaveChanges();
            
            // Act
            var result = _accessObject.foodRepository.FindFoodByCategory(name);
            
            // Assert
            Assert.Equal(name, result[0].Category);
            
            Cleanup();

        }
        */

        private void Cleanup()
        {
            _accessObject.toursContext.Foods.RemoveRange(_accessObject.toursContext.Foods);
            _accessObject.toursContext.SaveChanges();
        }

        private List<Food> createFoodList(string category = null)
        {
            var foods = new List<Food>();
            for (var i = 1; i < 5; i++)
            {
                var curFoodB = new FoodBuilder()
                    .WhereFoodID(i)
                    .WhereCategory(category)
                    .Build();
                var curFood = new Food(curFoodB);
                foods.Add(curFood);
            }

            return foods;
        }

        private List<Food> getFoodList(List<FoodBL> foodsBL)
        {
            List<Food> foods = new List<Food>();
            foreach (var foodBL in foodsBL)
            {
                Food food = new Food(foodBL);
                foods.Add(food);
            }
            return foods;
        }

        bool areEqual(List<Food> expFoods, List<Food> actFoods)
        {
            bool equal = true;
            for (int i = 0; i < expFoods.Count && equal; i++)
            {
                equal = areEqual(expFoods[i], actFoods[i]);
            }
            
            return equal;
        }
        
        bool areEqual(Food expFood, Food actFood)
        {
            return (expFood.Foodid == actFood.Foodid &&
                    expFood.Category == actFood.Category &&
                    expFood.Menu == actFood.Menu &&
                    expFood.Bar == actFood.Bar &&
                    expFood.Cost == actFood.Cost);
        }
    }
}
