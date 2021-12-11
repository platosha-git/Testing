using System;
using System.Collections.Generic;
using Tours;
using Tours.ComponentsBL;

namespace TechnologicalUI
{
    public class ManagerRole : TechnologicalUI
    {
        protected ManagerController manager;
        protected Output outAll;
        public ManagerRole(ManagerController _manager, Output _outAll)
        {
            manager = _manager;
            outAll = _outAll;
        }

        public void Play()
        {
            while (true)
            {
                Console.WriteLine("0 - Конец роли менеджера\n" +
                    "1 - Показать всех туристов\n" +
                    "2 - Добавить тур\n" +
                    "3 - Изменить тур\n" +
                    "4 - Удалить тур\n");

                string testStr = Console.ReadLine();
                int test = Convert.ToInt32(testStr);

                if (test == 0)
                {
                    break;
                }

                switch (test)
                {
                    case 1:
                        GetAllUsers();
                        break;
                    case 2:
                        AddTour();
                        break;
                    case 3:
                        UpdateTour();
                        break;
                    case 4:
                        DeleteTour();
                        break;
                    default:
                        break;
                }
            }
        }

        void GetAllUsers()
        {
            List<User> users = manager.GetAllUsers();
            foreach (User user in users)
            {
                outAll.outputUser(user);
            }
        }

        void AddTour()
        {
            DateTime dateB = new DateTime(2022, 03, 10);
            DateTime dateE = new DateTime(2022, 05, 01);
            Tour ntour = new Tour { Tourid = 11, Food = 4, Hotel = 7, Transfer = 3, Cost = 72013, Datebegin = dateB, Dateend = dateE };
            manager.AddTour(ntour);
            
            List<Tour> tours = manager.GetAllTours();
            foreach(Tour tour in tours)
            {
                outAll.outputTour(tour);
            }
        }

        void UpdateTour()
        {
            DateTime dateB = new DateTime(2022, 03, 10);
            DateTime dateE = new DateTime(2022, 05, 01);
            Tour ntour = new Tour { Tourid = 11, Food = 5, Hotel = 7, Transfer = 3, Cost = 72013, Datebegin = dateB, Dateend = dateE };
            manager.UpdateTour(ntour);

            List<Tour> tours = manager.GetAllTours();
            foreach (Tour tour in tours)
            {
                outAll.outputTour(tour);
            }
        }

        void DeleteTour()
        {
            manager.DeleteTourByID(11);

            List<Tour> tours = manager.GetAllTours();
            foreach (Tour tour in tours)
            {
                outAll.outputTour(tour);
            }
        }
    }
}
