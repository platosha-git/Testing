using System.Collections.Generic;
using Moq;
using ToursWeb.Controllers;
using ToursWeb.ModelsBL;
using ToursWeb.Repositories;
using Xunit;

namespace ToursTests.Tests
{
    public class FoodTests
    {
        [Fact]
        public void GetAll()
        {
            // Arrange
            var foodRepository = new Mock<IFoodRepository>();
            var foods = new List<FoodBL>();
            foodRepository
                .Setup(x => x.FindAll())
                .Returns(foods);
            
            var foodController = new FoodController(foodRepository.Object);
            
            // Act
            var result = foodController.GetAllFood();
            
            // Assert
            Assert.Null(result);

        }
    }
}