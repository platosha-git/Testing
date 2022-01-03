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

        private void Cleanup()
        {
            _accessObject.toursContext.Foods.RemoveRange(_accessObject.toursContext.Foods);
            _accessObject.toursContext.SaveChanges();
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
            bool equal = true;
            for (int i = 0; i < expFoods.Count && equal; i++)
            {
                if (expFoods[i].Foodid != actFoods[i].Foodid ||
                    expFoods[i].Category != actFoods[i].Category ||
                    expFoods[i].Menu != actFoods[i].Menu ||
                    expFoods[i].Bar != actFoods[i].Bar ||
                    expFoods[i].Cost != actFoods[i].Cost)
                    equal = false;
            }
            
            return equal;
        }
    }
}