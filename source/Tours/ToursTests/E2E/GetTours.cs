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
            var expFoods = createFoodList();
            var expHotels = createHotelList();
            var expTours = createTourList();
            
            addEntities(expFoods);
            addEntities(expHotels);
            addEntities(expTours);

            //Act : Get hotels from Moscow
            var hotels = _accessObject.hotelRepository.FindHotelsByCity("Moscow");
            
            //Assert
            Assert.NotNull(hotels);
            foreach (var hotel in hotels)
            {
                Assert.Equal("Moscow", hotel.City);
            }

            //Act : Get tours where hotel in Moscow
            var toursBL = _accessObject.tourRepository.FindAll();
            var tours = getTourList(toursBL);
            
            //Assert
            Assert.NotNull(tours);
            Assert.Equal(expTours.Count, tours.Count);

            //Act : update tours where hotel in moscow and food with bar
            List<Tour> toursInMosow = getToursByHotels(tours, hotels);
            var foodsWithBar = new List<FoodBL>();
            foreach (var tour in toursInMosow)
            {
                var curFoodBL = _accessObject.foodRepository.FindByID(tour.Food);
                if (curFoodBL is not {Bar: true}) continue;
                foodsWithBar.Add(curFoodBL);
                curFoodBL.Cost = 100;
                _accessObject.foodRepository.Update(curFoodBL);
            }
            
            //Assert
            foreach (var food in foodsWithBar)
            {
                Assert.Equal(100, food.Cost);
            }
            
            Cleanup();
        }
        
        private List<Food> createFoodList()
        {
            var foods = new List<Food>();
            for (var i = 10; i < 15; i++)
            {
                var curFoodB = new FoodBuilder()
                    .WhereFoodID(i)
                    .WhereBar(true)
                    .Build();
                var curFood = new Food(curFoodB);
                foods.Add(curFood);
            }
            return foods;
        }
        
        private List<Tour> createTourList()
        {
            var tours = new List<Tour>();
            for (var i = 10; i < 15; i++)
            {
                var curTourBL = new TourBuilder()
                    .WhereTourID(i)
                    .WhereFood(15 - i)
                    .WhereHotel(i + 2)
                    .Build();
                var curTour = new Tour(curTourBL);
                tours.Add(curTour);
            }
            return tours;
        }
        
        private List<Hotel> createHotelList()
        {
            var hotels = new List<Hotel>();
            for (var i = 10; i < 20; i++)
            {
                var curHotelBL = new HotelBuilder()
                    .WhereHotelID(i)
                    .WhereCity("Moscow")
                    .Build();
                var curHotel = new Hotel(curHotelBL);
                hotels.Add(curHotel);
            }
            return hotels;
        }
        
        private List<Tour> getTourList(List<TourBL> toursBL)
        {
            List<Tour> tours = new List<Tour>();
            foreach (var tourBL in toursBL)
            {
                Tour tour = new Tour(tourBL);
                tours.Add(tour);
            }
            return tours;
        }

        private List<Tour> getToursByHotels(List<Tour> tours, List<HotelBL> hotels)
        {
            List<int> ids = new List<int>();
            foreach (var hotel in hotels)
            {
                ids.Add(hotel.Hotelid);
            }

            List<Tour> resTours = new List<Tour>();
            foreach (var tour in tours)
            {
                if (ids.Contains(tour.Tourid))
                    resTours.Add(tour);
            }
            return resTours;
        }
        
        void addEntities(List<Food> foods)
        {
            _accessObject.toursContext.ChangeTracker.Clear();
            _accessObject.toursContext.Foods.AddRange(foods);
            _accessObject.toursContext.SaveChanges();
        }

        void addEntities(List<Tour> tours)
        {
            _accessObject.toursContext.ChangeTracker.Clear();
            _accessObject.toursContext.Tours.AddRange(tours);
            _accessObject.toursContext.SaveChanges();
        }
        
        void addEntities(List<Hotel> hotels)
        {
            _accessObject.toursContext.ChangeTracker.Clear();
            _accessObject.toursContext.Hotels.AddRange(hotels);
            _accessObject.toursContext.SaveChanges();
        }

        private void Cleanup()
        {
            _accessObject.toursContext.ChangeTracker.Clear();
            _accessObject.toursContext.Tours.RemoveRange(_accessObject.toursContext.Tours);
            _accessObject.toursContext.Foods.RemoveRange(_accessObject.toursContext.Foods);
            _accessObject.toursContext.Hotels.RemoveRange(_accessObject.toursContext.Hotels);
            _accessObject.toursContext.SaveChanges();
        }
    }
}        
