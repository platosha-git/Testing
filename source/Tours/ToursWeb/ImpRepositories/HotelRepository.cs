using System;
using System.Collections.Generic;
using System.Linq;
using ToursWeb.Repositories;
using Microsoft.Extensions.Logging;
using ToursWeb.ModelsDB;
using ToursWeb.ModelsBL;

namespace ToursWeb.ImpRepositories
{
    public class HotelRepository : IHotelRepository, IDisposable
    {
        private readonly ToursContext _db;
        private readonly ILogger<HotelRepository> _logger;

        public HotelRepository(ToursContext createDB, ILogger<HotelRepository> logDB)
        {
            _db = createDB;
            _logger = logDB;
        }

        List<HotelBL> ListHotelBL(List<Hotel> hotels)
        {
            if (hotels == null)
            {
                return null;
            }
            
            List<HotelBL> hotelsBL = new List<HotelBL>();
            foreach (var hotel in hotels)
            {
                HotelBL hotelBL = hotel.ToBL();
                hotelsBL.Add(hotelBL);
            }

            return hotelsBL;
        }

        public List<HotelBL> FindAll()
        {
            List<Hotel> hotels = _db.Hotels.ToList();
            List<HotelBL> hotelsBL = ListHotelBL(hotels);
            return hotelsBL;
        }

        public HotelBL FindByID(int id)
        {
            Hotel hotel = _db.Hotels.Find(id);
            if (hotel is null)
            {
                return null;
            }
            
            HotelBL hotelBL = hotel.ToBL();
            return hotelBL;
        }

        public ExitCode Add(HotelBL obj)
        {
            try
            {
                Hotel hotel = new Hotel(obj);
                obj.Hotelid = _db.Hotels.Count() + 1;
                hotel.Hotelid = _db.Hotels.Count() + 1;
                _db.Hotels.Add(hotel);
                _db.SaveChanges();
                _logger.LogInformation("+HotelRep : Hotel {Number} was added to Hotels", obj.Hotelid);
                return ExitCode.Success;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException err)
            {
                _logger.LogError(err, "+HotelRep : Constraint violation when trying to add hotel to Hotel");
                return ExitCode.Constraint;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+HotelRep : Error trying to add hotel to Hotels");
                return ExitCode.Error;
            }
        }

        public ExitCode Update(HotelBL obj)
        {
            try
            {
                Hotel uHotel = new Hotel(obj);
                _db.Hotels.Update(uHotel);
                _db.SaveChanges();
                _logger.LogInformation("+HotelRep : Hotel {Number} was updated in Hotels", obj.Hotelid);
                return ExitCode.Success;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException err)
            {
                _logger.LogError(err, "+HotelRep : Constraint violation when trying to update hotel in Hotel");
                return ExitCode.Constraint;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+HotelRep : Error trying to update hotel in Hotels");
                return ExitCode.Error;
            }
        }

        public ExitCode DeleteByID(int id)
        {
            try
            {
                Hotel hotel = _db.Hotels.Find(id);
                _db.Hotels.Remove(hotel);
                _db.SaveChanges();
                _logger.LogInformation("+HotelRep : Hotel {Number} was deleted from Hotels", id);
                return ExitCode.Success;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+HotelRep : Error trying to delete hotel {Number} from Hotels", id);
                return ExitCode.Error;
            }
        }
        
        public List<HotelBL> FindHotelsByCity(string city)
        {
            IQueryable<Hotel> hotels = _db.Hotels.Where(needed => needed.City.Equals(city));
            List<Hotel> lHotels = hotels.ToList();
            List<HotelBL> lHotelsBL = ListHotelBL(lHotels);
            return lHotelsBL;
        }

        public List<HotelBL> FindHotelsByName(string name)
        {
            IQueryable<Hotel> hotels = _db.Hotels.Where(needed => needed.Name == name);
            List<Hotel> lHotels = hotels.ToList();
            List<HotelBL> lHotelsBL = ListHotelBL(lHotels);
            return lHotelsBL;
        }

        public List<HotelBL> FindHotelByType(string type)
        {
            IQueryable<Hotel> hotels = _db.Hotels.Where(needed => needed.Type == type);
            List<Hotel> lHotels = hotels.ToList();
            List<HotelBL> lHotelsBL = ListHotelBL(lHotels);
            return lHotelsBL;
        }

        public List<HotelBL> FindHotelByClass(int cls)
        {
            IQueryable<Hotel> hotels = _db.Hotels.Where(needed => needed.Class == cls);
            List<Hotel> lHotels = hotels.ToList();
            List<HotelBL> lHotelsBL = ListHotelBL(lHotels);
            return lHotelsBL;
        }

        public List<HotelBL> FindHotelBySwimPool(bool sp)
        {
            IQueryable<Hotel> hotels = _db.Hotels.Where(needed => needed.Swimpool == sp);
            List<Hotel> lHotels = hotels.ToList();
            List<HotelBL> lHotelsBL = ListHotelBL(lHotels);
            return lHotelsBL;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
