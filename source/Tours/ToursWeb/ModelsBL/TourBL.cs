using System;
using ToursWeb.ModelsDB;

namespace ToursWeb.ModelsBL
{
    public enum ExitCode
    {
        Success,
        Constraint,
        Error
    }
    
    public class UserTour
    {
        public int Tourid { get; set; }
        public string City { get; set; }
        public string HName { get; set; }
        public string HType { get; set; }
        public int HClass { get; set; }
        public string FCategory { get; set; }
        public TType TType { get; set; }
        public DateTime Datebegin { get; set; }
        public DateTime Dateend { get; set; }
        public int Cost { get; set; }
        
        public UserTour() { }
        
        public UserTour(TourBL tour, HotelBL hotel, FoodBL food, TransferBL transfer)
        {
            Tourid = tour.Tourid;
            City = hotel.City;
            HName = hotel.Name;
            HType = hotel.Type;
            HClass = (int) hotel.Class;
            FCategory = food.Category;
            TType = transfer.Type;
            Datebegin = tour.Datebegin;
            Dateend = tour.Dateend;
            Cost = tour.Cost;
        }
    }
    
    public class TourBL
    {
        public int Tourid { get; set; }
        public int Food { get; set; }
        public int Hotel { get; set; }
        public int Transfer { get; set; }
        public int Cost { get; set; }
        public DateTime Datebegin { get; set; }
        public DateTime Dateend { get; set; }

        public TourBL() { }

        private bool Equals(TourBL tour)
        {
            if (tour is null)
            {
                return false;
            }

            return Tourid == tour.Tourid &&
                   Food == tour.Food &&
                   Hotel == tour.Hotel &&
                   Transfer == tour.Transfer &&
                   Cost == tour.Cost &&
                   Datebegin == tour.Datebegin &&
                   Dateend == tour.Dateend;
        }

        public override bool Equals(object obj) => Equals(obj as TourBL);
        public override int GetHashCode() => (Tourid, Food, Hotel, Transfer, Cost, Datebegin, Dateend).GetHashCode();
    }
}