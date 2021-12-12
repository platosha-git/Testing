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
            var expFood = new FoodBuilder().Build();
            var expFoods = new List<FoodBL>() {expFood};

            var mock = new Mock<IFoodRepository>();
            mock.Setup(x => x.FindAll())
                .Returns(expFoods);
            var foodController = new FoodController(mock.Object);
            
            var actFoods = foodController.GetAllFood();
            
            Assert.Equal(expFoods, actFoods);
        }
        
        [Fact]
        public void FindByID()
        {
            const int foodID = 1;
            
            var expFood = new FoodBuilder()
                .WhereFoodID(foodID)
                .Build();
            
            var mock = new Mock<IFoodRepository>();
            mock.Setup(x => x.FindByID(foodID))
                .Returns(expFood);
            var foodController = new FoodController(mock.Object);
            
            var actFood = foodController.GetFoodByID(foodID);
            
            Assert.Equal(expFood, actFood);
        }
        
        [Fact]
        public void FindByCategory()
        {
            const string category = "Breakfast";

            var expFood = new FoodBuilder()
                .WhereCategory(category)
                .Build();
            
            var expFoods = new List<FoodBL>() {expFood};
            
            var mock = new Mock<IFoodRepository>();
            mock.Setup(x => x.FindFoodByCategory(category))
                .Returns(expFoods);
            var foodController = new FoodController(mock.Object);
            
            var actFoods = foodController.GetFoodByCategory(category);
            
            Assert.Equal(expFoods, actFoods);
        }
        
        [Fact]
        public void FindByMenu()
        {
            const string menu = "Vegeterian";
            
            var expFood = new FoodBuilder()
                .WhereMenu(menu)
                .Build();
            var expFoods = new List<FoodBL>() {expFood};
            
            var mock = new Mock<IFoodRepository>();
            mock.Setup(x => x.FindFoodByMenu(menu))
                .Returns(expFoods);
            var foodController = new FoodController(mock.Object);
            
            var actFoods = foodController.GetFoodByMenu(menu);
            
            Assert.Equal(expFoods, actFoods);
        }
        
        [Fact]
        public void FindByBar()
        {
            const bool bar = true;
            
            var expFood = new FoodBuilder()
                .WhereBar(bar)
                .Build();
            var expFoods = new List<FoodBL>() {expFood};
            
            var mock = new Mock<IFoodRepository>();
            mock.Setup(x => x.FindFoodByBar(bar))
                .Returns(expFoods);
            var foodController = new FoodController(mock.Object);
            
            var actFoods = foodController.GetFoodByBar(bar);
            
            Assert.Equal(expFoods, actFoods);
        }
    }
}