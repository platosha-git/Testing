using System.Collections.Generic;
using ToursWeb.ModelsBL;
using ToursWeb.ModelsDB;
using ToursWeb.Repositories;

namespace ToursWeb.Controllers
{
    public class HotelController
    {
        private readonly IHotelRepository _hotelRepository;
        
        public HotelController(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public List<HotelBL> GetAllHotels()
        {
            return _hotelRepository.FindAll();
        }

        public HotelBL GetHotelByID(int hotelID)
        {
            return _hotelRepository.FindByID(hotelID);
        }

        public List<HotelBL> GetHotelsByCity(string city)
        {
            return _hotelRepository.FindHotelsByCity(city);
        }

        public List<HotelBL> GetHotelsByType(string type)
        {
            return _hotelRepository.FindHotelByType(type);
        }

        public List<HotelBL> GetHotelsByClass(int cls)
        {
            return _hotelRepository.FindHotelByClass(cls);
        }

        public List<HotelBL> GetHotelsBySwimPool(bool sp)
        {
            return _hotelRepository.FindHotelBySwimPool(sp);
        }
        
        public ExitCode AddHotel(HotelBL hotel)
        {
            return _hotelRepository.Add(hotel);
        }
        
        public ExitCode UpdateHotel(HotelBL hotel)
        {
            return _hotelRepository.Update(hotel);
        }
        
        public ExitCode DeleteHotelByID(int hotelID)
        {
            return _hotelRepository.DeleteByID(hotelID);
        }
    }
}