using System;
using ToursWeb.ImpRepositories;
using ToursWeb.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using ToursWeb.ModelsDB;

namespace ToursTests
{
    public class TourAccessObject : IDisposable
    {
        public ToursContext toursContext { get; }
        public IFoodRepository foodRepository { get; }
        public IHotelRepository hotelRepository { get; }
        public ITourRepository tourRepository { get; }
        public ITransferRepository transferRepository { get; }

        public TourAccessObject()
        {
            var builder = new DbContextOptionsBuilder<ToursContext>();
            builder.UseInMemoryDatabase("Tours");

            toursContext = new ToursContext(builder.Options);
            
            foodRepository = new FoodRepository(toursContext, NullLogger<FoodRepository>.Instance);
            hotelRepository = new HotelRepository(toursContext, NullLogger<HotelRepository>.Instance);
            tourRepository = new TourRepository(toursContext, NullLogger<TourRepository>.Instance);
            transferRepository = new TransferRepository(toursContext, NullLogger<TransferRepository>.Instance);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                toursContext.Database.EnsureDeleted();
                toursContext?.Dispose();
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}