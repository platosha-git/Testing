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
            var expFoods = createFoodList();
            addEntities(expFoods);

            // Act
            var actFoodBL = _accessObject.foodRepository.FindAll();
            var actFoods = getFoodList(actFoodBL);

            // Assert
            Assert.NotNull(actFoods);
            //Assert.Equal(expFoods.Count, actFoods.Count);
            //Assert.True(areEqual(expFoods, actFoods));

            Cleanup();
        }

        [Fact]
        public void FindByID_FirstElement_NotNull()
        {
            const int foodID = 1;
            
            // Arrange
            var expFoods = createFoodList();
            addEntities(expFoods);

            // Act
            var actFoodBL = _accessObject.foodRepository.FindByID(foodID);
            var actFood = new Food(actFoodBL);

            // Assert
            Assert.NotNull(actFood);
            Assert.Equal(foodID, actFood.Foodid);

            Cleanup();
        }

        void addEntities(List<Food> foods)
        {
            _accessObject.toursContext.ChangeTracker.Clear();
            _accessObject.toursContext.Foods.AddRange(foods);
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
            for (int i = 1; i < expFoods.Count && equal; i++)
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
        
        private void Cleanup()
        {
            _accessObject.toursContext.ChangeTracker.Clear();
            _accessObject.toursContext.Foods.RemoveRange(_accessObject.toursContext.Foods);
            _accessObject.toursContext.SaveChanges();
        }
    }
}
