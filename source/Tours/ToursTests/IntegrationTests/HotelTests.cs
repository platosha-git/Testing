using Xunit;
using ToursWeb.ModelsDB;
using ToursWeb.ModelsBL;
using ToursTests.Builders;
using System.Collections.Generic;

namespace ToursTests.IntegrationTests
{
    [Collection("Integration")]
    public class HotelTests : IClassFixture<TourAccessObject>
    {
        [Fact]
        public void FindAll_NotNull()
        {
            // Arrange
            var accessObject = new TourAccessObject();
            var expHotels = createHotelList();
            addEntities(accessObject, expHotels);

            // Act
            var actHotelsBL = accessObject.hotelRepository.FindAll();
            var actHotels = getHotelList(actHotelsBL);

            // Assert
            Assert.NotNull(actHotels);
            Assert.Equal(expHotels.Count, actHotels.Count);
            Assert.True(areEqual(expHotels, actHotels));

            Cleanup(accessObject);
        }

        [Fact]
        public void FindByID_FirstElement_NotNull()
        {
            const int hotelID = 1;
            
            // Arrange
            var accessObject = new TourAccessObject();
            var expHotels = createHotelList();
            addEntities(accessObject, expHotels);

            // Act
            var actHotelBL = accessObject.hotelRepository.FindByID(hotelID);
            var actHotel = new Hotel(actHotelBL);

            // Assert
            Assert.NotNull(actHotel);
            Assert.Equal(hotelID, actHotel.Hotelid);

            Cleanup(accessObject);
        }
        
        [Fact]
        public void FindByCity_London_NotNull()
        {
            const string city = "London";
            
            // Arrange
            var accessObject = new TourAccessObject();
            var expHotels = new List<Hotel>();
            for (var i = 5; i < 10; i++)
            {
                var curHotelB = new HotelBuilder().WhereHotelID(i).WhereCity(city).Build();
                var curHotel = new Hotel(curHotelB);
                expHotels.Add(curHotel);
            }
            addEntities(accessObject, expHotels);

            // Act
            var actHotelsBL = accessObject.hotelRepository.FindHotelsByCity(city);
            var actHotels = getHotelList(actHotelsBL);

            // Assert
            Assert.NotNull(actHotels);

            Cleanup(accessObject);
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

        void addEntities(TourAccessObject accessObject, List<Hotel> hotels)
        {
            accessObject.toursContext.ChangeTracker.Clear();
            accessObject.toursContext.Hotels.AddRange(hotels);
            accessObject.toursContext.SaveChanges();
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
            int size = actHotels.Count;
            bool equal = true;
            for (int i = 0; i < size && equal; i++)
            {
                equal = areEqual(expHotels[i], actHotels[size - 1 - i]);
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

        void Cleanup(TourAccessObject accessObject)
        {
            accessObject.toursContext.ChangeTracker.Clear();
            accessObject.toursContext.Hotels.RemoveRange(accessObject.toursContext.Hotels);
            accessObject.toursContext.SaveChanges();
        }
    }
}
