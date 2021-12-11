using System;
using System.Collections.Generic;
using Tours;
using Tours.ComponentsBL;

namespace TechnologicalUI
{
    public class TouristRole : TechnologicalUI
    {
        protected TouristController tourist;
        protected Output outAll;
        public TouristRole(TouristController _tourist, Output _outAll)
        {
            tourist = _tourist;
            outAll = _outAll;
        }

        public void Play()
        {
            while (true)
            {
                Console.WriteLine("0 - Конец роли туриста\n" +
                    "1 - Показать забронированные туры\n" +
                    "2 - Забронировать тур\n" +
                    "3 - Удалить тур\n" +
                    "4 - Показать информацию о пользователе\n" +
                    "5 - Изменить фамилию\n" +
                    "6 - Изменить год\n");

                string testStr = Console.ReadLine();
                int test = Convert.ToInt32(testStr);

                if (test == 0)
                {
                    break;
                }

                switch (test)
                {
                    case 1:
                        GetAllBookings();
                        break;
                    case 2:
                        BookTour();
                        GetAllBookings();
                        break;
                    case 3:
                        RemoveTour();
                        GetAllBookings();
                        break;
                    case 4:
                        GetAllUserInfo(); 
                        break;
                    case 5:
                        UpdateSurname();
                        GetAllUserInfo();
                        break;
                    case 6:
                        UpdateYear();
                        GetAllUserInfo();
                        break;
                    default:
                        break;
                }
            }
        }

        void GetAllBookings()
        {
            List<Tour> tours = tourist.GetAllBookings(2);
            foreach (Tour tour in tours)
            {
                outAll.outputTour(tour);
            }
        }

        void BookTour()
        {
            tourist.BookTour(2, 2);
        }

        void RemoveTour()
        {
            tourist.RemoveTour(2, 2);
        }

        void GetAllUserInfo()
        {
            User user = tourist.GetAllUserInfo(2);
            outAll.outputUser(user);
        }

        void UpdateSurname()
        {
            tourist.UpdateSurname("Nikitin", 2);
        }

        void UpdateYear()
        {
            tourist.UpdateYear(1999, 2);
        }
    }
}
