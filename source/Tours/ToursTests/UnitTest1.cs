using System.Collections.Generic;
using ToursWeb.Repositories;
using Moq;
using Xunit;

namespace ToursTests
{
    public class FoodTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            var foodRepository = new Mock<IFoodRepository>();
            var foods = new List<FoodBL>();
            foodRepository
                .Setup(x => x.GetAll())
                .Returns(foods);
            
            var foodController = new FoodController(foodRepository.Object);
            
            // Act
            var result = foodController.GetAll();
            
            // Assert
            Assert.Null(result);

        }
    }
}