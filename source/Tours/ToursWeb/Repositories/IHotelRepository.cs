using System.Collections.Generic;
using ToursWeb.ModelsDB;
using ToursWeb.ModelsBL;

namespace ToursWeb.Repositories
{
    public interface IHotelRepository : CrudRepository<HotelBL, int>
    {
        List<HotelBL> FindHotelsByCity(string city);
        List<HotelBL> FindHotelsByName(string name);
        List<HotelBL> FindHotelsByType(string type);
        List<HotelBL> FindHotelsByClass(int cls);
        List<HotelBL> FindHotelsBySwimPool(bool sp);
    }
}
