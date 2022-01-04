using System.Collections.Generic;
using ToursWeb.ModelsBL;
using ToursWeb.ModelsDB;
using ToursTests.Builders;
using ToursTests;
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
            GetFullTour();
        }

        public void GetFullTour()
        {
            //Arrange
            List<Tour> tours = new List<Tour>();
            for (var i = 1; i < 5; i++)
            {
                var curTourBL = new TourBuilder().WhereTourID(i).Build();
                var curTour = new Tour(curTourBL);
                tours.Add(curTour);
            }
            
            _accessObject.toursContext.ChangeTracker.Clear();
            _accessObject.toursContext.Tours.AddRange();
            _accessObject.toursContext.SaveChanges();
            
            //Act
            var allTours = _accessObject.tourRepository.FindAll();
            
            //Assert
            Assert.NotNull(allTours);
            
        }
    }
}