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

        [Fact]
        public void FindAll_NotNull()
        {
            // Arrange
            List<Food> expFoods = createFoodList();
            _accessObject.toursContext.Foods.AddRange(expFoods);
            _accessObject.toursContext.SaveChanges();
            
            // Act
            var actFoodsBL = _accessObject.foodRepository.FindAll();
            var actFoods = getFoodList(actFoodsBL);

            // Assert
            Assert.NotNull(actFoods);
            Assert.Equal(expFoods.Count, actFoods.Count);
            Assert.True(areEqual(expFoods, actFoods));

            Cleanup();
        }

        [Fact]
        public void FindByID_FirstElement_NotNull()
        {
            const int foodID = 1;
            
            // Arrange
            var foodB = new FoodBuilder()
                    .WhereFoodID(foodID)
                    .Build();
            var expFood = new Food(foodB);
            _accessObject.toursContext.Foods.Add(expFood);
            _accessObject.toursContext.SaveChanges();
            
            // Act
            var actFoodBL = _accessObject.foodRepository.FindByID(foodID);
            var actFood = new Food(actFoodBL);

            // Assert
            Assert.NotNull(actFood);
            Assert.True(areEqual(expFood, actFood));

            Cleanup();
        }
        
        /*[Fact]
        public void FindByCategory_Breakfast_NotNull()
        {
            const string category = "All inclusive";
            
            // Arrange
            List<Food> expFoods = createFoodList(category);
            _accessObject.toursContext.Foods.AddRange(expFoods);
            _accessObject.toursContext.SaveChanges();
            
            // Act
            var actFoodsBL = _accessObject.foodRepository.FindFoodByCategory(category);
            var actFoods = getFoodList(actFoodsBL);

            // Assert
            Assert.NotNull(actFoods);
            Assert.Equal(expFoods.Count, actFoods.Count);
            Assert.True(areEqual(expFoods, actFoods));

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
