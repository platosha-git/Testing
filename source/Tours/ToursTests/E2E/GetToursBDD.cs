using System;
using System.Collections.Generic;
using System.Linq;
using ToursWeb.ModelsBL;
using ToursWeb.ModelsDB;
using ToursTests.Builders;
using Xunit;
using LightBDD.XUnit2;
using LightBDD.Framework;
using LightBDD.Framework.Parameters;
using LightBDD.Framework.Scenarios;



namespace ToursTests.E2E
{
    [Collection("GetTours")]
    [FeatureDescription(
    @"In order to make good offer
    As an user
    I want to change food cost")]
    public class GetToursBDD : FeatureFixture
    {
        private readonly TourAccessObject _accessObject;
        private List<Hotel> _hotels;
        private List<Tour> _tours;
        private List<Food> _foods;

        private string _city = "Moscow";
        private List<HotelBL> _hotelsBL;
        private List<TourBL> _toursBL;
        private List<FoodBL> _foodsBL;

        public GetToursBDD()
        {
            _accessObject = new TourAccessObject();
            _hotels = createHotelList();
            _tours = createTourList();
            _foods = createFoodList();
        }

        [Scenario]
        public void GetFullTour()
        {
            Runner.RunScenario(
           _ => Given_the_data(_hotels, _tours, _foods),
           _ => When_acquire_hotel_data(_city),
           _ => When_acquire_tour_data(),
           _ => When_filter_tour_data_by_hotel(),
           _ => When_change_food_data(),
           _ => Then_food_data_changed()
            );

            Cleanup();
        }


        private CompositeStep When_acquire_hotel_data(string city)
        {
            return CompositeStep.DefineNew().AddSteps(
                _ => When_make_hotel_request(city),
                _ => Then_acquired_hotel_data(_hotelsBL, _city)
                ).Build();
        }

        private CompositeStep When_acquire_tour_data()
        {
            return CompositeStep.DefineNew().AddSteps(
                _ => When_make_tour_request(),
                _ => Then_acquired_tour_data()
                ).Build();
        }

        private void Given_the_data(List<Hotel> hotels, List<Tour> tours, List<Food> foods)
        {
            addEntities(hotels);
            addEntities(tours);
            addEntities(foods);
        }

        private void When_make_hotel_request(string city)
        {
            _hotelsBL = _accessObject.hotelRepository.FindHotelsByCity(city);
        }
        private void Then_acquired_hotel_data(List<HotelBL> hotels, string city)
        {
            Assert.NotNull(hotels);
            foreach (var hotel in hotels)
            {
                Assert.Equal(city, hotel.City);
            }
        }
        private void When_make_tour_request()
        {
            _toursBL = _accessObject.tourRepository.FindAll();
        }
        private void Then_acquired_tour_data()
        {
            var tours = getTourList(_toursBL);

            Assert.NotNull(tours);
            Assert.Equal(_tours.Count, tours.Count);
        }

        private void When_filter_tour_data_by_hotel()
        {
            _tours = getToursByHotels(_tours, _hotelsBL);
        }
        private void When_change_food_data()
        {
            _foodsBL = new List<FoodBL>();
            foreach (var tour in _tours)
            {
                var curFoodBL = _accessObject.foodRepository.FindByID(tour.Food);
                if (curFoodBL is not { Bar: true }) continue;
                _foodsBL.Add(curFoodBL);
                curFoodBL.Cost = 100;
                _accessObject.foodRepository.Update(curFoodBL);
            }
        }
        private void Then_food_data_changed()
        {
            foreach (var food in _foodsBL)
            {
                Assert.Equal(100, food.Cost);
            }
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
