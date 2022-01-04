using System;
using Xunit;
using ToursWeb.ModelsDB;
using ToursWeb.ModelsBL;
using ToursTests.Builders;
using System.Collections.Generic;

namespace ToursTests.IntegrationTests
{
    public class TourTests : IClassFixture<TourAccessObject>
    {
        private readonly TourAccessObject _accessObject;

        public TourTests() 
        {
            _accessObject = new TourAccessObject();
        }

        [Fact]
        public void FindAll_NotNull()
        {
            // Arrange
            var expTours = createTourList();
            addEntities(expTours);

            // Act
            var actToursBL = _accessObject.tourRepository.FindAll();
            var actTours = getTourList(actToursBL);

            // Assert
            Assert.NotNull(actTours);

            Cleanup();
        }

        [Fact]
        public void FindByID_FirstElement_NotNull()
        {
            const int tourID = 1;
            
            // Arrange
            var expTours = createTourList();
            addEntities(expTours);

            // Act
            var actTourBL = _accessObject.tourRepository.FindByID(tourID);
            var actTour = new Tour(actTourBL);

            // Assert
            Assert.NotNull(actTour);
            Assert.Equal(tourID, actTour.Tourid);

            Cleanup();
        }
        
        [Fact]
        public void FindByDate_NotNull()
        {
            var dateB = new DateTime(2022, 11, 09);
            var dateE = new DateTime(2022, 12, 31);
            
            // Arrange
            var expTours = new List<Tour>();
            for (var i = 5; i < 10; i++)
            {
                var curTourBL = new TourBuilder().WhereTourID(i).WhereDateBegin(dateB).WhereDateEnd(dateE).Build();
                var curTour = new Tour(curTourBL);
                expTours.Add(curTour);
            }
            addEntities(expTours);

            // Act
            var actToursBL = _accessObject.tourRepository.FindToursByDate(dateB, dateE);
            var actTours = getTourList(actToursBL);

            // Assert
            Assert.NotNull(actTours);

            Cleanup();
        }

        private List<Tour> createTourList()
        {
            var tours = new List<Tour>();
            for (var i = 1; i < 5; i++)
            {
                var curTourBL = new TourBuilder()
                    .WhereTourID(i)
                    .Build();
                var curTour = new Tour(curTourBL);
                tours.Add(curTour);
            }
            
            return tours;
        }

        void addEntities(List<Tour> tours)
        {
            _accessObject.toursContext.ChangeTracker.Clear();
            _accessObject.toursContext.Tours.AddRange(tours);
            _accessObject.toursContext.SaveChanges();
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

        bool areEqual(List<Tour> expTours, List<Tour> actTours)
        {
            bool equal = true;
            for (int i = 1; i < expTours.Count && equal; i++)
            {
                equal = areEqual(expTours[i], actTours[i]);
            }
            
            return equal;
        }
        
        bool areEqual(Tour expTours, Tour actTour)
        {
            return (expTours.Tourid == actTour.Tourid &&
                    expTours.Food == actTour.Food &&
                    expTours.Hotel == actTour.Hotel &&
                    expTours.Transfer == actTour.Transfer &&
                    expTours.Cost == actTour.Cost &&
                    expTours.Datebegin == actTour.Datebegin &&
                    expTours.Dateend == actTour.Dateend);
        }

        void Cleanup()
        {
            _accessObject.toursContext.ChangeTracker.Clear();
            _accessObject.toursContext.Tours.RemoveRange(_accessObject.toursContext.Tours);
            _accessObject.toursContext.SaveChanges();
        }
    }
}
