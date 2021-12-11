using System;
using System.Collections.Generic;
using Tours;
using Tours.ComponentsBL;

namespace TechnologicalUI
{
    public class GuestRole : TechnologicalUI
    {
        protected GuestController guest;
        protected Output outAll;
        public GuestRole(GuestController _guest, Output _outAll)
        {
            guest = _guest;
            outAll = _outAll;
        }

        public void Play()
        {
            while (true)
            {
                Console.WriteLine("0 - Конец роли гостя\n" +
                    "1 - Показать все туры\n" +
                    "2 - Показать туры по датам\n" +
                    "3 - Показать все отели\n" +
                    "4 - Показать отлеь по имени\n" +
                    "5 - Показать отель по бассейну\n" +
                    "6 - Показать еду по меню\n" +
                    "7 - Показать автобус по цене\n");

                string testStr = Console.ReadLine();
                int test = Convert.ToInt32(testStr);

                if (test == 0)
                {
                    break;
                }

                switch (test)
                {
                    case 1:
                        GetAllTours();
                        break;
                    case 2:
                        GetToursByDate();
                        break;
                    case 3:
                        GetAllHotels();
                        break;
                    case 4:
                        GetHotelByName();
                        break;
                    case 5:
                        GetHotelBySwimPool();
                        break;
                    case 6:
                        GetFoodByVegMenu();
                        break;
                    case 7:
                        GetBusByLowCost();
                        break;
                    default:
                        break;
                }
            }
        }

        void GetAllTours()
        {
            List<Tour> tours = guest.GetAllTours();
            foreach (Tour tour in tours)
            {
                outAll.outputTour(tour);
            }
        }

        void GetToursByDate()
        {
            DateTime beg = new DateTime(2020, 12, 01);
            DateTime end = new DateTime(2021, 12, 01);
            List<Tour> tours = guest.GetToursByDate(beg, end);
            foreach (Tour tour in tours)
            {
                outAll.outputTour(tour);
            }
        }

        void GetAllHotels()
        {
            List<Hotel> hotels = guest.GetAllHotels();
            foreach (Hotel hotel in hotels)
            {
                outAll.outputHotel(hotel);
            }
        }

        void GetHotelByName()
        {
            Hotel hotel = guest.GetHotelByName("Profitpros");
            outAll.outputHotel(hotel);
        }

        void GetHotelBySwimPool()
        {
            List<Hotel> hotels = guest.GetHotelsBySwimPool(true);
            foreach (Hotel hotel in hotels)
            {
                outAll.outputHotel(hotel);
            }
        }

        void GetFoodByVegMenu()
        {
            List<Food> foods = guest.GetFoodByVegMenu(true);
            foreach (Food food in foods)
            {
                outAll.outputFood(food);
            }
        }

        void GetBusByLowCost()
        {
            List<Busticket> buses = guest.GetBusByLowCost(80000);
            foreach (Busticket bus in buses)
            {
                outAll.outputBus(bus);
            }
        }
    }
}
