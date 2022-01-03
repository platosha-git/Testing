using System;
using System.Collections.Generic;
using ToursWeb.Repositories;
using ToursWeb.Controllers;
using ToursTests.Builders;
using ToursWeb.ModelsBL;
using Xunit;
using Moq;

namespace ToursTests.TestsBL
{
    public class TourTests
    {
        private IHotelRepository mockH = new Mock<IHotelRepository>().Object;
        private IFoodRepository mockF = new Mock<IFoodRepository>().Object;
        private ITransferRepository mockT = new Mock<ITransferRepository>().Object;
        
        [Fact]
        public void FindAll_NotNull()
        {
            var expTour = new TourBuilder().Build();
            var expTours = new List<TourBL>() {expTour};

            var mock = new Mock<ITourRepository>();
            mock.Setup(x => x.FindAll())
                .Returns(expTours);
            var tourController = new TourController(mock.Object, mockH, mockF, mockT);
            
            var actTours = tourController.GetAllTours();
            
            Assert.NotNull(expTours);
            Assert.Equal(expTours, actTours);
        }
        
        [Fact]
        public void FindByID_First_NotNull()
        {
            const int tourID = 1;
            
            var expTour = new TourBuilder()
                .WhereTourID(tourID)
                .Build();

            var mock = new Mock<ITourRepository>();
            mock.Setup(x => x.FindByID(tourID))
                .Returns(expTour);
            var tourController = new TourController(mock.Object, mockH, mockF, mockT);
            
            var actTours = tourController.GetTourByID(tourID);
            
            Assert.NotNull(expTour);
            Assert.Equal(expTour, actTours);
        }
        
        [Fact]
        public void FindByDate_NotNull()
        {
            var dateB = new DateTime(2022, 11, 09);
            var dateE = new DateTime(2022, 12, 31);
            
            var expTour = new TourBuilder()
                .WhereDateBegin(dateB)
                .WhereDateEnd(dateE)
                .Build();
            
            var expTours = new List<TourBL>() {expTour};

            var mock = new Mock<ITourRepository>();
            mock.Setup(x => x.FindToursByDate(dateB, dateE))
                .Returns(expTours);
            var tourController = new TourController(mock.Object, mockH, mockF, mockT);
            
            var actTours = tourController.GetToursByDate(dateB, dateE);
            
            Assert.NotNull(expTours);
            Assert.Equal(expTours, actTours);
        }
    }
}
