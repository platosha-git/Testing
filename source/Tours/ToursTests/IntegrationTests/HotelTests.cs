using Xunit;
using ToursWeb.ModelsDB;
using ToursWeb.ModelsBL;
using ToursTests.Builders;
using System.Collections.Generic;

namespace ToursTests.IntegrationTests
{
    public class HotelTests : IClassFixture<TourAccessObject>
    {
        private readonly TourAccessObject _accessObject;

        public HotelTests(TourAccessObject tourAccessObject) 
        {
            _accessObject = tourAccessObject;
        }

        [Fact]
        public void FindAll_NotNull()
        {
            // Arrange
            var expHotels = createHotelList();
            addEntities(expHotels);

            // Act
            var actHotelsBL = _accessObject.hotelRepository.FindAll();
            var actHotels = getHotelList(actHotelsBL);

            // Assert
            Assert.NotNull(actHotels);
            Assert.Equal(expHotels.Count, actHotels.Count);
            Assert.True(areEqual(expHotels, actHotels));

            Cleanup();
        }

        [Fact]
        public void FindByID_FirstElement_NotNull()
        {
            const int hotelID = 1;
            
            // Arrange
            var expHotels = createHotelList();
            addEntities(expHotels);

            // Act
            var actHotelBL = _accessObject.hotelRepository.FindByID(hotelID);
            var actHotel = new Hotel(actHotelBL);

            // Assert
            Assert.NotNull(actHotel);
            Assert.Equal(hotelID, actHotel.Hotelid);

            Cleanup();
        }

        private List<Hotel> createHotelList()
        {
            var hotels = new List<Hotel>();
            for (var i = 1; i < 5; i++)
            {
                var curHotelB = new HotelBuilder()
                    .WhereHotelID(i)
                    .Build();
                var curHotel = new Hotel(curHotelB);
                hotels.Add(curHotel);
            }
            
            return hotels;
        }

        void addEntities(List<Hotel> hotels)
        {
            _accessObject.toursContext.ChangeTracker.Clear();
            _accessObject.toursContext.Hotels.AddRange(hotels);
            _accessObject.toursContext.SaveChanges();
        }

        private List<Hotel> getHotelList(List<HotelBL> hotelsBL)
        {
            List<Hotel> hotels = new List<Hotel>();
            foreach (var hotelBL in hotelsBL)
            {
                Hotel hotel = new Hotel(hotelBL);
                hotels.Add(hotel);
            }
            return hotels;
        }

        bool areEqual(List<Hotel> expHotels, List<Hotel> actHotels)
        {
            bool equal = true;
            for (int i = 0; i < expHotels.Count && equal; i++)
            {
                equal = areEqual(expHotels[i], actHotels[i]);
            }
            
            return equal;
        }
        
        bool areEqual(Hotel expHotel, Hotel actHotel)
        {
            return (expHotel.City == actHotel.City &&
                    expHotel.Class == actHotel.Class &&
                    expHotel.Cost == actHotel.Cost &&
                    expHotel.Hotelid == actHotel.Hotelid &&
                    expHotel.Name == actHotel.Name &&
                    expHotel.Swimpool == actHotel.Swimpool &&
                    expHotel.Type == actHotel.Type);
        }

        void Cleanup()
        {
            _accessObject.toursContext.ChangeTracker.Clear();
            _accessObject.toursContext.Hotels.RemoveRange(_accessObject.toursContext.Hotels);
            _accessObject.toursContext.SaveChanges();
        }
    }
}
