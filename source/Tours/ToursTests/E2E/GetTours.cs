using System.Collections.Generic;
using ToursWeb.ModelsBL;
using ToursWeb.ModelsDB;
using ToursTests.Builders;
using Xunit;

namespace ToursTests.E2E
{
    public class GetTours : IClassFixture<TourAccessObject>
    {
        private readonly TourAccessObject _accessObject;

        public GetTours()
        {
            _accessObject = new TourAccessObject();
        }

        [Fact]
        public void GetToursRepeat()
        {
            for (int i = 0; i < 100; i++)
            {
                GetFullTour();
            }
        }

        public void GetFullTour()
        {
            //Arrange
            List<Tour> tours = new List<Tour>();
            for (var i = 5; i < 10; i++)
            {
                var curTourBL = new TourBuilder().WhereTourID(i).WhereFood(i).Build();
                var curTour = new Tour(curTourBL);
                tours.Add(curTour);
            }
            _accessObject.toursContext.ChangeTracker.Clear();
            _accessObject.toursContext.Tours.AddRange(tours);
            _accessObject.toursContext.SaveChanges();
            
            //Act
            var allTours = _accessObject.tourRepository.FindAll();
            
            //Assert
            Assert.NotNull(allTours);

            //Arrange
            List<Food> foods = new List<Food>();
            for (var i = 5; i < 10; i++)
            {
                var curFoodBL = new FoodBuilder().WhereFoodID(i).Build();
                var curFood = new Food(curFoodBL);
                foods.Add(curFood);
            }
            _accessObject.toursContext.ChangeTracker.Clear();
            _accessObject.toursContext.Foods.AddRange(foods);
            _accessObject.toursContext.SaveChanges();
            
            //Act
            var allFoodsWithBar = new List<FoodBL>();
            foreach (var tour in allTours)
            {
                var curFoodBL = _accessObject.foodRepository.FindByID(tour.Food);
                if (curFoodBL.Bar == true)
                {
                    allFoodsWithBar.Add(curFoodBL);
                    curFoodBL.Cost = 100;
                    _accessObject.foodRepository.Update(curFoodBL);
                }
            }
            
            //Assert
            foreach (var food in allFoodsWithBar)
            {
                Assert.NotEqual(0, food.Cost);
            }
            
            Cleanup();
        }
        
        private void Cleanup()
        {
            _accessObject.toursContext.ChangeTracker.Clear();
            _accessObject.toursContext.Tours.RemoveRange(_accessObject.toursContext.Tours);
            _accessObject.toursContext.Foods.RemoveRange(_accessObject.toursContext.Foods);
            _accessObject.toursContext.SaveChanges();
        }
    }
}        
