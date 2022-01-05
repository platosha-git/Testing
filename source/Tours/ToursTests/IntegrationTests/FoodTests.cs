using Xunit;
using ToursWeb.ModelsDB;
using ToursWeb.ModelsBL;
using ToursTests.Builders;
using System.Collections.Generic;

namespace ToursTests.IntegrationTests
{
    public class FoodTests : IClassFixture<TourAccessObject>
    {
        [Fact]
        public void FindAll_NotNull()
        {
            // Arrange
            var accessObject = new TourAccessObject();
            var expFoods = createFoodList();
            addEntities(accessObject, expFoods);

            // Act
            var actFoodBL = accessObject.foodRepository.FindAll();
            var actFoods = getFoodList(actFoodBL);

            // Assert
            Assert.NotNull(actFoods);
            Assert.Equal(expFoods.Count, actFoods.Count);
            Assert.True(areEqual(expFoods, actFoods));

            Cleanup(accessObject);
        }

        [Fact]
        public void FindByID_FirstElement_NotNull()
        {
            const int foodID = 1;
            
            // Arrange
            var accessObject = new TourAccessObject();
            var expFoods = createFoodList();
            addEntities(accessObject, expFoods);

            // Act
            var actFoodBL = accessObject.foodRepository.FindByID(foodID);
            var actFood = new Food(actFoodBL);

            // Assert
            Assert.NotNull(actFood);
            Assert.Equal(foodID, actFood.Foodid);

            Cleanup(accessObject);
        }
        
        [Fact]
        public void FindByCategory_Breakfast_NotNull()
        {
            const string category = "Breakfast";
            
            // Arrange
            var accessObject = new TourAccessObject();
            var expFoods = new List<Food>();
            for (var i = 5; i < 10; i++)
            {
                var curFoodB = new FoodBuilder().WhereFoodID(i).WhereCategory(category).Build();
                var curFood = new Food(curFoodB);
                expFoods.Add(curFood);
            }
            addEntities(accessObject, expFoods);

            // Act
            var actFoodBL = accessObject.foodRepository.FindFoodByCategory(category);
            var actFoods = getFoodList(actFoodBL);

            // Assert
            Assert.NotNull(actFoods);

            Cleanup(accessObject);
        }

        void addEntities(TourAccessObject accessObject, List<Food> foods)
        {
            accessObject.toursContext.ChangeTracker.Clear();
            accessObject.toursContext.Foods.AddRange(foods);
            accessObject.toursContext.SaveChanges();
        }

        private List<Food> createFoodList()
        {
            var foods = new List<Food>();
            for (var i = 1; i < 5; i++)
            {
                var curFoodB = new FoodBuilder()
                    .WhereFoodID(i)
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
            int size = actFoods.Count;
            bool equal = true;
            for (int i = 0; i < size && equal; i++)
            {
                equal = areEqual(expFoods[i], actFoods[size - 1 - i]);
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

        private void Cleanup(TourAccessObject accessObject)
        {
            accessObject.toursContext.ChangeTracker.Clear();
            accessObject.toursContext.Foods.RemoveRange(accessObject.toursContext.Foods);
            accessObject.toursContext.SaveChanges();
        }
    }
}
