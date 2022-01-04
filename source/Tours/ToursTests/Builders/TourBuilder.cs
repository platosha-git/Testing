using System;
using ToursWeb.ModelsBL;

namespace ToursTests.Builders
{
    public class TourBuilder
    {
        private int Tourid;
        private int Food;
        private int Hotel;
        private int Transfer;
        private int Cost;
        private DateTime Datebegin;
        private DateTime Dateend;

        public TourBuilder()
        {
            Food = 0;
            Hotel = 0;
            Transfer = 0;
            Cost = 0;
        }

        public TourBL Build()
        {
            var tour = new TourBL()
            {
                Tourid = Tourid,
                Food = Food,
                Hotel = Hotel,
                Transfer = Transfer,
                Cost = Cost,
                Datebegin = Datebegin,
                Dateend = Dateend
            };

            return tour;
        }
        
        public TourBuilder WhereTourID(int tourID)
        {
            Tourid = tourID;
            return this;
        }
        
        public TourBuilder WhereFood(int foodID)
        {
            Food = foodID;
            return this;
        }
        
        public TourBuilder WhereDateBegin(DateTime dateB)
        {
            Datebegin = dateB;
            return this;
        }
        
        public TourBuilder WhereDateEnd(DateTime dateE)
        {
            Dateend = dateE;
            return this;
        }
    }
}