using System.Collections.Generic;
using ToursWeb.Repositories;
using ToursWeb.Controllers;
using ToursTests.Builders;
using ToursWeb.ModelsBL;
using Xunit;
using Moq;

namespace ToursTests.Tests
{
    public class TourTests
    {
        /*[Fact]
        public void FindAll()
        {
            var expTour = new TourBuilder().Build();
            var expTours = new List<TourBL>() {expTour};

            var mock = new Mock<ITourRepository>();
            mock.Setup(x => x.FindAll())
                .Returns(expTours);
            var tourController = new TourController(mock.Object);
            
            var actTours = tourController.GetAllTours();
            
            Assert.Equal(expTours, actTours);
        }
        */
    }
}