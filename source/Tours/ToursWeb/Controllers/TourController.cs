using System;
using System.Collections.Generic;
using System.Linq;
using ToursWeb.ModelsDB;
using ToursWeb.ModelsBL;
using ToursWeb.Repositories;

namespace ToursWeb.Controllers
{
    public class TourController
    {
        private readonly ITourRepository _tourRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IFoodRepository _foodRepository;
        private readonly ITransferRepository _transferRepository;

        public TourController(ITourRepository tourRepository, IHotelRepository hotelRepository,
            IFoodRepository foodRepository, ITransferRepository transferRepository)
        {
            _tourRepository = tourRepository;
            _hotelRepository = hotelRepository;
            _foodRepository = foodRepository;
            _transferRepository = transferRepository;
        }

        public List<UserTour> ToUserTour(List<TourBL> tours)
        {
            List<UserTour> userTours = new List<UserTour>();
            foreach (var tour in tours)
            {
                HotelBL hotel = _hotelRepository.FindByID(tour.Hotel); 
                FoodBL food = _foodRepository.FindByID(tour.Food);
                TransferBL transfer = _transferRepository.FindByID(tour.Transfer);
                
                UserTour userTourDTO = new UserTour(tour, hotel, food, transfer);
                userTours.Add(userTourDTO);
            }
            return userTours;
        }

        public List<TourBL> GetAllTours()
        {
            return _tourRepository.FindAll();
        }
        
        public TourBL GetTourByID(int tourID)
        {
            return _tourRepository.FindByID(tourID);
        }
        
        public List<TourBL> GetToursByDate(DateTime beg, DateTime end)
        {
            return _tourRepository.FindToursByDate(beg, end);
        }
        
        public List<TourBL> GetToursByCity(string city)
        {
            List<HotelBL> hotels = _hotelRepository.FindHotelsByCity(city);
            
            List<TourBL> tours = new List<TourBL>();
            for (int i = 0; i < hotels.Count; i++)
            {
                HotelBL curHotel = hotels[i];
                List<TourBL> curTour = _tourRepository.FindToursByHotel(curHotel.Hotelid);
                tours.AddRange(curTour);
            }

            return tours;
        }
        
        public List<TourBL> GetToursByCityName(string city, string name)
        {
            List<HotelBL> hotelsCity = _hotelRepository.FindHotelsByCity(city);
            List<HotelBL> hotelsName = _hotelRepository.FindHotelsByName(name);
            List<HotelBL> hotels = hotelsCity.Intersect(hotelsName).ToList();

            List<TourBL> tours = new List<TourBL>();
            for (int i = 0; i < hotels.Count; i++)
            {
                HotelBL curHotel = hotels[i];
                List<TourBL> curTour = _tourRepository.FindToursByHotel(curHotel.Hotelid);
                tours.AddRange(curTour);
            }

            return tours;
        }

        public ExitCode AddTour(TourBL tour)
        {
            return _tourRepository.Add(tour);
        }
        
        public ExitCode UpdateTour(TourBL tour)
        {
            return _tourRepository.Update(tour);
        }
        
        public ExitCode DeleteTourByID(int tourID)
        {
            return _tourRepository.DeleteByID(tourID);
        }
    }
}