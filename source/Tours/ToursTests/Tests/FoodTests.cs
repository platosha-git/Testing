using System.Collections.Generic;
using ToursWeb.Repositories;
using ToursWeb.Controllers;
using ToursTests.Builders;
using ToursWeb.ModelsBL;
using Xunit;
using Moq;

namespace ToursTests.Tests
{
    public class FoodTests
    {
        [Fact]
        public void FindAll()
        {
            var foodRepository = new Mock<IFoodRepository>();
            var expFoods = new List<FoodBL>()
            {
                new FoodBuilder().Build()
            };
            
            foodRepository
                .Setup(x => x.FindAll())
                .Returns(expFoods);
            
            var foodController = new FoodController(foodRepository.Object);
            var actFoods = foodController.GetAllFood();
            
            Assert.Equal(expFoods, actFoods);
        }
        
        [Fact]
        public void FindByID()
        {
            const int foodID = 1;
            var foodRepository = new Mock<IFoodRepository>();
            var expFood = new FoodBuilder()
                .WhereFoodID(foodID)
                .Build();

            foodRepository
                .Setup(x => x.FindByID(foodID))
                .Returns(expFood);
            
            var foodController = new FoodController(foodRepository.Object);
            var actFood = foodController.GetFoodByID(foodID);
            
            Assert.Equal(expFood, actFood);
        }
        
        [Fact]
        public void FindByCategory()
        {
            const string category = "Breakfast";
            
            var foodRepository = new Mock<IFoodRepository>();
            var expFoods = new List<FoodBL>()
            {
                new FoodBuilder()
                    .WhereCategory(category)
                    .Build()
            };
            
            foodRepository
                .Setup(x => x.FindFoodByCategory(category))
                .Returns(expFoods);
            
            var foodController = new FoodController(foodRepository.Object);
            var actFoods = foodController.GetFoodByCategory(category);
            
            Assert.Equal(expFoods, actFoods);
        }
        
        [Fact]
        public void FindByMenu()
        {
            const string menu = "Vegeterian";
            
            var foodRepository = new Mock<IFoodRepository>();
            var expFoods = new List<FoodBL>()
            {
                new FoodBuilder()
                    .WhereMenu(menu)
                    .Build()
            };
            
            foodRepository
                .Setup(x => x.FindFoodByMenu(menu))
                .Returns(expFoods);
            
            var foodController = new FoodController(foodRepository.Object);
            var actFoods = foodController.GetFoodByMenu(menu);
            
            Assert.Equal(expFoods, actFoods);
        }
        
        [Fact]
        public void FindByBar()
        {
            const bool bar = true;
            
            var foodRepository = new Mock<IFoodRepository>();
            var expFoods = new List<FoodBL>()
            {
                new FoodBuilder()
                    .WhereBar(bar)
                    .Build()
            };
            
            foodRepository
                .Setup(x => x.FindFoodByBar(bar))
                .Returns(expFoods);
            
            var foodController = new FoodController(foodRepository.Object);
            var actFoods = foodController.GetFoodByBar(bar);
            
            Assert.Equal(expFoods, actFoods);
        }
    }
}