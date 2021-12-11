using ToursWeb.ModelsBL;

namespace ToursWeb.ModelsDTO
{
    public class HotelUserDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int? Class { get; set; }
        public bool? Swimpool { get; set; }
        public string City { get; set; }
        public int Cost { get; set; }

        public HotelUserDTO() {}

        public HotelBL GetHotel(int hotelID = 0)
        {
            HotelBL hotel = new HotelBL ()
            {
                Hotelid = hotelID,
                Name = Name,
                Type = Type,
                Class = Class,
                Swimpool = Swimpool,
                City = City,
                Cost = Cost
            };
            
            return hotel;
        }
    }

    public class HotelDTO : HotelUserDTO
    {
        public int Hotelid { get; set; }
        
        public HotelDTO() {}
        
        public HotelDTO(HotelBL hotel)
        {
            Hotelid = hotel.Hotelid;
            Name = hotel.Name;
            Type = hotel.Type;
            Class = hotel.Class;
            Swimpool = hotel.Swimpool;
            City = hotel.City;
            Cost = hotel.Cost;
        }
    }
}
