using System;
using System.Collections.Generic;
using ToursWeb.ModelsDB;
using ToursWeb.ModelsBL;

namespace ToursWeb.Repositories
{
    public interface ITransferRepository : CrudRepository<TransferBL, int>
    {
        List<TransferBL> FindTransferByType(string type);
        List<TransferBL> FindTransfersByCity(string cityFrom);
        List<TransferBL> FindTransfersByDate(DateTime date);
    }

}
