using System;
using Tours;
using Tours.ComponentsBL;
using Tours.Repositories;
using Tours.ImpRepositories;
using Serilog;

namespace TechnologicalUI
{
    class Program
    {
        static void Main(string[] args)
        {
            TechnologicalUI ui = new TechnologicalUI();
            ui.Run();
        }
    }

    public class TechnologicalUI
    {        
        private readonly GuestController guest;
        private readonly TouristController tourist;
        private readonly ManagerController manager;
        private readonly TransferManagerController transferManager;

        private readonly ToursContext db;

        Output outAll = new Output();

        public TechnologicalUI()
        {
            var log = new LoggerConfiguration()
                .WriteTo.File(ConfigManager.GetLogFile())
                .CreateLogger();

            db = new ToursContext(ConfigManager.GetConnectionString(2));

            ITourRepository tourRep = new TourRepository(db, log);
            IHotelRepository hotelRep = new HotelRepository(db, log);
            IFoodRepository foodRep = new FoodRepository(db, log);

            ITransferRepository transferRep = new TransferRepository(db, log);
            IBusRepository busRep = new BusRepository(db, log);
            IPlaneRepository planeRep = new PlaneRepository(db, log);
            ITrainRepository trainRep = new TrainRepository(db, log);

            IUsersRepository usersRep = new UsersRepository(db, log);
            IBookingRepository bookingRep = new BookingRepository(db, log);
            
            guest = new GuestController(tourRep, hotelRep, foodRep, transferRep, busRep, planeRep, trainRep);
            tourist = new TouristController(tourRep, hotelRep, foodRep, transferRep, busRep, planeRep, trainRep, bookingRep, usersRep);
            manager = new ManagerController(tourRep, hotelRep, foodRep, transferRep, busRep, planeRep, trainRep, bookingRep, usersRep);
            transferManager = new TransferManagerController(tourRep, hotelRep, foodRep, transferRep, busRep, planeRep, trainRep);
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("0 - Выход\n" +
                    "1 - Роль гостя\n" +
                    "2 - Роль туриста\n" +
                    "3 - Роль менеджера транфера\n" +
                    "4 - Роль менеджера\n");

                string testStr = Console.ReadLine();
                int test = Convert.ToInt32(testStr);

                if (test == 0)
                {
                    break;
                }

                switch(test)
                {
                    case 1:
                        GuestRole tGuest = new GuestRole(guest, outAll);
                        tGuest.Play();
                        break;
                    case 2:
                        TouristRole tTourist = new TouristRole(tourist, outAll);
                        tTourist.Play();
                        break;
                    case 3:
                        TransferManagerRole tTransferManager = new TransferManagerRole(transferManager, outAll);
                        tTransferManager.Play();
                        break;
                    case 4:
                        ManagerRole tManager = new ManagerRole(manager, outAll);
                        tManager.Play();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
