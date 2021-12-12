using ToursWeb.ModelsBL;

namespace ToursTests.Builders
{
    public class HotelBuilder
    {
        private int Hotelid;
        private string Name;
        private string Type;
        private int? Class;
        private bool? Swimpool;
        private string City;
        private int Cost;

        public HotelBuilder() { }

        public HotelBL Build()
        {
            var hotel = new HotelBL()
            {
                Hotelid = Hotelid,
                Name = Name,
                Type = Type,
                Class = Class,
                Swimpool = Swimpool,
                City = City,
                Cost = Cost
            };

            return hotel;
        }
        
        public HotelBuilder WhereHotelID(int hotelID)
        {
            Hotelid = hotelID;
            return this;
        }

        public HotelBuilder WhereCity(string city)
        {
            City = city;
            return this;
        }
        
        public HotelBuilder WhereType(string type)
        {
            Type = type;
            return this;
        }
        
        public HotelBuilder WhereClass(int cls)
        {
            Class = cls;
            return this;
        }
        
        public HotelBuilder WhereSwimPool(bool sp)
        {
            Swimpool = sp;
            return this;
        }
    }
}