using System;
using ToursWeb.ModelsBL;

#nullable disable

namespace ToursWeb.ModelsDB
{
    public partial class Tour
    {
        public Tour() { }

        public Tour(TourBL tourBL)
        {
            Tourid = tourBL.Tourid;
            Food = tourBL.Food;
            Hotel = tourBL.Hotel;
            Transfer = tourBL.Transfer;
            Cost = tourBL.Cost;
            Datebegin = tourBL.Datebegin;
            Dateend = tourBL.Dateend;
        }
        
        public int Tourid { get; set; }
        public int Food { get; set; }
        public int Hotel { get; set; }
        public int Transfer { get; set; }
        public int Cost { get; set; }
        public DateTime Datebegin { get; set; }
        public DateTime Dateend { get; set; }

        public virtual Food FoodNavigation { get; set; }
        public virtual Hotel HotelNavigation { get; set; }
        public virtual Transfer TransferNavigation { get; set; }

        public TourBL ToBl()
        {
            TourBL tourBL = new TourBL()
            {
                Tourid = Tourid,
                Food = Food,
                Hotel = Hotel,
                Transfer = Transfer,
                Cost = Cost,
                Datebegin = Datebegin,
                Dateend = Dateend
            };

            return tourBL;
        }
    }
}
