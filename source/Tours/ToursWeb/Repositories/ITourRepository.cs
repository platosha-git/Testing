using System;
using System.Collections.Generic;
using ToursWeb.ModelsDB;
using ToursWeb.ModelsBL;

namespace ToursWeb.Repositories
{
    public interface ITourRepository : CrudRepository<TourBL, int>
    {
        List<TourBL> FindToursByDate(DateTime b, DateTime e);
        List<TourBL> FindToursByHotel(int hotelID);
    }
}
