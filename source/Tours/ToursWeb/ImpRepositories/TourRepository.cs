using System;
using System.Collections.Generic;
using System.Linq;
using ToursWeb.Repositories;
using Microsoft.Extensions.Logging;
using ToursWeb.ModelsDB;
using ToursWeb.ModelsBL;

namespace ToursWeb.ImpRepositories
{
    public class TourRepository : ITourRepository, IDisposable
    {
        private readonly ToursContext _db;
        private readonly ILogger<TourRepository> _logger;

        public TourRepository(ToursContext createDB, ILogger<TourRepository> logDB)
        {
            _db = createDB;
            _logger = logDB;
        }

        List<TourBL> ListTourBL(List<Tour> tours)
        {
            if (tours == null)
            {
                return null;
            }
            
            List<TourBL> toursBL = new List<TourBL>();
            foreach (var tour in tours)
            {
                TourBL tourBL = tour.ToBl();
                toursBL.Add(tourBL);
            }

            return toursBL;
        }
        
        public List<TourBL> FindAll()
        {
            List<Tour> tours = _db.Tours.ToList();
            List<TourBL> toursBL = ListTourBL(tours);
            return toursBL;
        }

        public TourBL FindByID(int id)
        {
            Tour tour = _db.Tours.Find(id);
            if (tour is null)
            {
                return null;
            }
            
            TourBL tourBL = tour.ToBl();
            return tourBL;
        }

        public ExitCode Add(TourBL obj)
        {
            try
            {
                Tour tour = new Tour(obj);
                obj.Tourid = _db.Tours.Count() + 1;
                
                tour.Tourid = _db.Tours.Count() + 1;
                _db.Tours.Add(tour);
                _db.SaveChanges();
                _logger.LogInformation("+TourRep : Tour {Number} was added to Tours", obj.Tourid);
                return ExitCode.Success;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException err)
            {
                _logger.LogError(err, "+TourRep : Constraint violation when trying to add tour to Tours");
                return ExitCode.Constraint;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+TourRep : Error trying to add tour to Tours");
                return ExitCode.Error;
            }
        }

        public ExitCode Update(TourBL obj)
        {
            try
            {
                Tour uTour = new Tour(obj);
                _db.Tours.Update(uTour);
                _db.SaveChanges();
                _logger.LogInformation("+TourRep : Tours {Number} was updated at Tours", obj.Tourid);
                return ExitCode.Success;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException err)
            {
                _logger.LogError(err, "+TourRep : Constraint violation when trying to update tour at Tours");
                return ExitCode.Constraint;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+TourRep : Error trying to update tour at Tours");
                return ExitCode.Error;
            }
        }

        public ExitCode DeleteByID(int id)
        {
            try
            {
                Tour tour = _db.Tours.Find(id);
                _db.Tours.Remove(tour);
                _db.SaveChanges();
                _logger.LogInformation("+TourRep : Tours {Number} was deleted from Tours", tour.Tourid);
                return ExitCode.Success;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+TourRep : Error trying to delete tour from Tours");
                return ExitCode.Error;
            }
        }

        public List<TourBL> FindToursByDate(DateTime b, DateTime e)
        {
            IQueryable<Tour> tours = _db.Tours.Where(needed => needed.Datebegin >= b && needed.Dateend <= e);
            List<Tour> lTours = tours.ToList();
            List<TourBL> lToursBL = ListTourBL(lTours);
            return lToursBL;
        }

        public List<TourBL> FindToursByHotel(int hotelID)
        {
            IQueryable<Tour> tours = _db.Tours.Where(needed => needed.Hotel == hotelID);
            List<Tour> lTours = tours.ToList();
            List<TourBL> lToursBL = ListTourBL(lTours);
            return lToursBL;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
