using Xunit;
using ToursWeb.ModelsDB;
using ToursTests.Builders;
using System.Collections.Generic;

namespace ToursTests.IntegrationTests
{
    [Collection("Integration")]
    public class FoodAPITests : IClassFixture<TourAccessObject>
    {
        private readonly TourAccessObject _accessObject;

        public FoodAPITests(TourAccessObject tourAccessObject) 
        {
            _accessObject = tourAccessObject;
        }

        [Fact]
        public void TestGetAll()
        {
            // Arrange
            List<Food> foods = new List<Food>();
            for (var i = 1; i < 4; i++)
            {
                var curFood = new FoodBuilder()
                    .WhereFoodID(i)
                    .Build();
                foods.Add(new Food(curFood));
            }
            
            _accessObject.toursContext.Foods.AddRange(foods);
            _accessObject.toursContext.SaveChanges();
            
            // Act
            var result = _accessObject.foodRepository.FindAll();
           
            // Assert
            Assert.Equal(foods.Count, result.Count);
            Assert.NotNull(result);
            Assert.Equal(foods[0].Foodid, result[0].Foodid);

            Cleanup();
        }

        private void Cleanup()
        {
            _accessObject.toursContext.Foods.RemoveRange(_accessObject.toursContext.Foods);
            _accessObject.toursContext.SaveChanges();
        }
    }
}