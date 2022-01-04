using System.Collections.Generic;
using ToursWeb.Repositories;
using ToursWeb.Controllers;
using ToursTests.Builders;
using ToursWeb.ModelsBL;
using Xunit;
using Moq;

namespace ToursTests.UnitTests
{
    public class HotelTests
    {
        [Fact]
        public void FindAll_NotNull()
        {
            var expHotel = new HotelBuilder().Build();
            var expHotels = new List<HotelBL>() {expHotel};

            var mock = new Mock<IHotelRepository>();
            mock.Setup(x => x.FindAll())
                .Returns(expHotels);
            var hotelController = new HotelController(mock.Object);
            
            var actHotels = hotelController.GetAllHotels();
            
            Assert.NotNull(expHotels);
            Assert.Equal(expHotels, actHotels);
        }
        
        [Fact]
        public void FindByID_First_NotNull()
        {
            const int hotelID = 1;
            
            var expHotel = new HotelBuilder()
                .WhereHotelID(hotelID)
                .Build();
            
            var mock = new Mock<IHotelRepository>();
            mock.Setup(x => x.FindByID(hotelID))
                .Returns(expHotel);
            var hotelController = new HotelController(mock.Object);
            
            var actHotel = hotelController.GetHotelByID(hotelID);
            
            Assert.NotNull(expHotel);
            Assert.Equal(expHotel, actHotel);
        }

        [Fact]
        public void FindByCity_London_NotNull()
        {
            const string city = "London";
            
            var expHotel = new HotelBuilder()
                .WhereCity(city)
                .Build();
            
            var expHotels = new List<HotelBL>() {expHotel};
            
            var mock = new Mock<IHotelRepository>();
            mock.Setup(x => x.FindHotelsByCity(city))
                .Returns(expHotels);
            var hotelController = new HotelController(mock.Object);
            
            var actHotels = hotelController.GetHotelsByCity(city);
            
            Assert.NotNull(expHotels);
            Assert.Equal(expHotels, actHotels);
        }
        
        [Fact]
        public void FindByType_Hotel_NotNull()
        {
            const string type = "Hotel";
            
            var expHotel = new HotelBuilder()
                .WhereType(type)
                .Build();
            
            var expHotels = new List<HotelBL>() {expHotel};
            
            var mock = new Mock<IHotelRepository>();
            mock.Setup(x => x.FindHotelsByType(type))
                .Returns(expHotels);
            var hotelController = new HotelController(mock.Object);
            
            var actHotels = hotelController.GetHotelsByType(type);
            
            Assert.NotNull(expHotels);
            Assert.Equal(expHotels, actHotels);
        }
        
        [Fact]
        public void FindByClass_Five_NotNull()
        {
            const int cls = 5;
            
            var expHotel = new HotelBuilder()
                .WhereClass(cls)
                .Build();
            
            var expHotels = new List<HotelBL>() {expHotel};
            
            var mock = new Mock<IHotelRepository>();
            mock.Setup(x => x.FindHotelsByClass(cls))
                .Returns(expHotels);
            var hotelController = new HotelController(mock.Object);
            
            var actHotels = hotelController.GetHotelsByClass(cls);
            
            Assert.NotNull(expHotels);
            Assert.Equal(expHotels, actHotels);
        }
        
        [Fact]
        public void FindBySwimPool_True_NotNull()
        {
            const bool sp = true;
            
            var expHotel = new HotelBuilder()
                .WhereSwimPool(sp)
                .Build();
            
            var expHotels = new List<HotelBL>() {expHotel};
            
            var mock = new Mock<IHotelRepository>();
            mock.Setup(x => x.FindHotelsBySwimPool(sp))
                .Returns(expHotels);
            var hotelController = new HotelController(mock.Object);
            
            var actHotels = hotelController.GetHotelsBySwimPool(sp);
            
            Assert.NotNull(expHotels);
            Assert.Equal(expHotels, actHotels);
        }
    }
}
