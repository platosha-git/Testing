using System;
using Xunit;
using ToursWeb.ModelsDB;
using ToursWeb.ModelsBL;
using ToursTests.Builders;
using System.Collections.Generic;

namespace ToursTests.IntegrationTests
{
    [Collection("Integration")]
    public class TourTests : IClassFixture<TourAccessObject>
    {
        [Fact]
        public void FindAll_NotNull()
        {
            // Arrange
            var accessObject = new TourAccessObject();
            var expTours = createTourList();
            addEntities(accessObject, expTours);

            // Act
            var actToursBL = accessObject.tourRepository.FindAll();
            var actTours = getTourList(actToursBL);

            // Assert
            Assert.NotNull(actTours);

            Cleanup(accessObject);
        }

        [Fact]
        public void FindByID_FirstElement_NotNull()
        {
            const int tourID = 1;
            
            // Arrange
            var accessObject = new TourAccessObject();
            var expTours = createTourList();
            addEntities(accessObject, expTours);

            // Act
            var actTourBL = accessObject.tourRepository.FindByID(tourID);
            var actTour = new Tour(actTourBL);

            // Assert
            Assert.NotNull(actTour);
            Assert.Equal(tourID, actTour.Tourid);

            Cleanup(accessObject);
        }
        
        [Fact]
        public void FindByDate_NotNull()
        {
            var dateB = new DateTime(2022, 11, 09);
            var dateE = new DateTime(2022, 12, 31);
            
            // Arrange
            var accessObject = new TourAccessObject();
            var expTours = new List<Tour>();
            for (var i = 5; i < 10; i++)
            {
                var curTourBL = new TourBuilder().WhereTourID(i).WhereDateBegin(dateB).WhereDateEnd(dateE).Build();
                var curTour = new Tour(curTourBL);
                expTours.Add(curTour);
            }
            addEntities(accessObject, expTours);

            // Act
            var actToursBL = accessObject.tourRepository.FindToursByDate(dateB, dateE);
            var actTours = getTourList(actToursBL);

            // Assert
            Assert.NotNull(actTours);

            Cleanup(accessObject);
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

        void addEntities(TourAccessObject accessObject, List<Tour> tours)
        {
            accessObject.toursContext.ChangeTracker.Clear();
            accessObject.toursContext.Tours.AddRange(tours);
            accessObject.toursContext.SaveChanges();
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
            int size = actTours.Count;
            bool equal = true;
            for (int i = 0; i < size && equal; i++)
            {
                equal = areEqual(expTours[i], actTours[size - 1 - i]);
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

        void Cleanup(TourAccessObject accessObject)
        {
            accessObject.toursContext.ChangeTracker.Clear();
            accessObject.toursContext.Tours.RemoveRange(accessObject.toursContext.Tours);
            accessObject.toursContext.SaveChanges();
        }
    }
}
