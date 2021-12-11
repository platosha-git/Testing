using System.Collections.Generic;
using ToursWeb.ModelsBL;

#nullable disable

namespace ToursWeb.ModelsDB
{
    public partial class Hotel
    {
        public Hotel()
        {
            Tours = new HashSet<Tour>();
        }

        public Hotel(HotelBL hotelBL)
        {
            Hotelid = hotelBL.Hotelid;
            Name = hotelBL.Name;
            Type = hotelBL.Type;
            Class = hotelBL.Class;
            Swimpool = hotelBL.Swimpool;
            City = hotelBL.City;
            Cost = hotelBL.Cost;
        }

        public int Hotelid { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int? Class { get; set; }
        public bool? Swimpool { get; set; }
        public string City { get; set; }
        public int Cost { get; set; }

        public virtual ICollection<Tour> Tours { get; set; }

        public HotelBL ToBL()
        {
            HotelBL hotelBL = new HotelBL()
            {
                Hotelid = Hotelid,
                Name = Name,
                Type = Type,
                Class = Class,
                Swimpool = Swimpool,
                City = City,
                Cost = Cost
            };

            return hotelBL;
        }
    }
}
