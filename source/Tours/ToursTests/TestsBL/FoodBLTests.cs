using System.Collections.Generic;
using ToursWeb.Repositories;
using ToursWeb.Controllers;
using ToursTests.Builders;
using ToursWeb.ModelsBL;
using Xunit;
using Moq;

namespace ToursTests.TestsBL
{
    public class FoodBLTests
    {
        [Fact]
        public void FindAll_NotNull()
        {
            var expFood = new FoodBuilder().Build();
            var expFoods = new List<FoodBL>() {expFood};

            var mock = new Mock<IFoodRepository>();
            mock.Setup(x => x.FindAll())
                .Returns(expFoods);
            var foodController = new FoodController(mock.Object);
            
            var actFoods = foodController.GetAllFood();
            
            Assert.NotNull(expFoods);
            Assert.Equal(expFoods, actFoods);
        }
        
        [Fact]
        public void FindByID_FirstElement_NotNull()
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
            
            Assert.NotNull(expFood);
            Assert.Equal(expFood, actFood);
        }
        
        [Fact]
        public void FindByCategory_Breakfast_NotNull()
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
            
            Assert.NotNull(expFoods);
            Assert.Equal(expFoods, actFoods);
        }
        
        [Fact]
        public void FindByMenu_Vegeterian_NotNull()
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
            
            Assert.NotNull(expFoods);
            Assert.Equal(expFoods, actFoods);
        }
        
        [Fact]
        public void FindByBar_True_NotNull()
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
            
            Assert.NotNull(expFoods);
            Assert.Equal(expFoods, actFoods);
        }
    }
}