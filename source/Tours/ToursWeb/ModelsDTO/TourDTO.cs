using System;
using ToursWeb.ModelsBL;

namespace ToursWeb.ModelsDTO
{
    public class TourUserDTO
    {
        public int Food { get; set; }
        public int Hotel { get; set; }
        public int Transfer { get; set; }
        public int Cost { get; set; }
        public DateTime Datebegin { get; set; }
        public DateTime Dateend { get; set; }

        public TourUserDTO() {}
        
        public TourBL GetTour(int tourID = 0)
        {
            TourBL tour = new TourBL ()
            {
                Tourid = tourID,
                Food = Food,
                Hotel = Hotel,
                Transfer = Transfer,
                Cost = Cost,
                Datebegin = Datebegin,
                Dateend = Dateend
            };
            
            return tour;
        }
    }

    public class TourDTO : TourUserDTO
    {
        public int Tourid { get; set; }

        public TourDTO() {}

        public TourDTO(TourBL tour)
        {
            Tourid = tour.Tourid;
            Food = tour.Food;
            Hotel = tour.Hotel;
            Transfer = tour.Transfer;
            Cost = tour.Cost;
            Datebegin = tour.Datebegin;
            Dateend = tour.Dateend;
        }
    }
}
