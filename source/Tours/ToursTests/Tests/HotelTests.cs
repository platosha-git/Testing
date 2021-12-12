using System.Collections.Generic;
using Moq;
using ToursTests.Builders;
using ToursWeb.Controllers;
using ToursWeb.ModelsBL;
using ToursWeb.Repositories;
using Xunit;

namespace ToursTests.Tests
{
    public class HotelTests
    {
        [Fact]
        public void FindAll()
        {
            var hotelRepository = new Mock<IHotelRepository>();
            var expHotels = new List<HotelBL>()
            {
                new HotelBuilder().Build()
            };
            
            hotelRepository
                .Setup(x => x.FindAll())
                .Returns(expHotels);
            
            var hotelController = new HotelController(hotelRepository.Object);
            var actHotels = hotelController.GetAllHotels();
            
            Assert.Equal(expHotels, actHotels);
        }
        
        [Fact]
        public void FindByID()
        {
            const int hotelID = 1;
            var hotelRepository = new Mock<IHotelRepository>();
            var expHotel = new HotelBuilder()
                .WhereHotelID(hotelID)
                .Build();

            hotelRepository
                .Setup(x => x.FindByID(hotelID))
                .Returns(expHotel);
            
            var hotelController = new HotelController(hotelRepository.Object);
            var actHotel = hotelController.GetHotelByID(hotelID);
            
            Assert.Equal(expHotel, actHotel);
        }

        [Fact]
        public void FindByCity()
        {
            const string city = "London";
            
            var hotelRepository = new Mock<IHotelRepository>();
            var expHotels = new List<HotelBL>()
            {
                new HotelBuilder()
                    .WhereCity(city)
                    .Build()
            };
            
            hotelRepository
                .Setup(x => x.FindHotelsByCity(city))
                .Returns(expHotels);
            
            var hotelController = new HotelController(hotelRepository.Object);
            var actHotels = hotelController.GetHotelsByCity(city);
            
            Assert.Equal(expHotels, actHotels);
        }
        
        [Fact]
        public void FindByType()
        {
            const string type = "Hotel";
            
            var hotelRepository = new Mock<IHotelRepository>();
            var expHotels = new List<HotelBL>()
            {
                new HotelBuilder()
                    .WhereType(type)
                    .Build()
            };
            
            hotelRepository
                .Setup(x => x.FindHotelsByType(type))
                .Returns(expHotels);
            
            var hotelController = new HotelController(hotelRepository.Object);
            var actHotels = hotelController.GetHotelsByType(type);
            
            Assert.Equal(expHotels, actHotels);
        }
        
        [Fact]
        public void FindByClass()
        {
            const int cls = 5;
            
            var hotelRepository = new Mock<IHotelRepository>();
            var expHotels = new List<HotelBL>()
            {
                new HotelBuilder()
                    .WhereClass(cls)
                    .Build()
            };
            
            hotelRepository
                .Setup(x => x.FindHotelsByClass(cls))
                .Returns(expHotels);
            
            var hotelController = new HotelController(hotelRepository.Object);
            var actHotels = hotelController.GetHotelsByClass(cls);
            
            Assert.Equal(expHotels, actHotels);
        }
        
        [Fact]
        public void FindBySwimPool()
        {
            const bool sp = true;
            
            var hotelRepository = new Mock<IHotelRepository>();
            var expHotels = new List<HotelBL>()
            {
                new HotelBuilder()
                    .WhereSwimPool(sp)
                    .Build()
            };
            
            hotelRepository
                .Setup(x => x.FindHotelsBySwimPool(sp))
                .Returns(expHotels);
            
            var hotelController = new HotelController(hotelRepository.Object);
            var actHotels = hotelController.GetHotelsBySwimPool(sp);
            
            Assert.Equal(expHotels, actHotels);
        }
    }
}