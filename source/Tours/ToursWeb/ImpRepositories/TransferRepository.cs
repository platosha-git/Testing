using System;
using System.Collections.Generic;
using System.Linq;
using ToursWeb.Repositories;
using Microsoft.Extensions.Logging;
using ToursWeb.ModelsDB;
using ToursWeb.ModelsBL;

namespace ToursWeb.ImpRepositories
{
    public class TransferRepository : ITransferRepository, IDisposable
    {
        private readonly ToursContext _db;
        private readonly ILogger<TransferRepository> _logger;

        public TransferRepository(ToursContext createDB, ILogger<TransferRepository> logDB)
        {
            _db = createDB;
            _logger = logDB;
        }

        List<TransferBL> ListTransferBL(List<Transfer> transfers)
        {
            if (transfers == null)
            {
                return null;
            }
            
            List<TransferBL> transfersBL = new List<TransferBL>();
            foreach (var transfer in transfers)
            {
                TransferBL transferBL = transfer.ToBL();
                transfersBL.Add(transferBL);
            }

            return transfersBL;
        }

        public List<TransferBL> FindAll()
        {
            List<Transfer> transfers = _db.Transfers.ToList();
            List<TransferBL> transfersBL = ListTransferBL(transfers);
            return transfersBL;
        }

        public TransferBL FindByID(int id)
        {
            Transfer transfer = _db.Transfers.Find(id);
            if (transfer is null)
            {
                return null;
            }
            
            TransferBL transferBL = transfer.ToBL();
            return transferBL;
        }

        public ExitCode Add(TransferBL obj)
        {
            try
            {
                Transfer transfer = new Transfer(obj);
                obj.Transferid = _db.Transfers.Count() + 1;
                
                transfer.Transferid = _db.Transfers.Count() + 1;
                _db.Transfers.Add(transfer);
                _db.SaveChanges();
                _logger.LogInformation("+TransferRep : Transfer {Number} was added to Transfers", obj.Transferid);
                return ExitCode.Success;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException err)
            {
                _logger.LogError(err, "+TransferRep : Constraint violation when trying to add transfer to Transfers");
                return ExitCode.Constraint;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+TransferRep : Error trying to add transfer to Transfers");
                return ExitCode.Error;
            }
        }

        public ExitCode Update(TransferBL obj)
        {
            try
            {
                Transfer transfer = new Transfer(obj);
                _db.Transfers.Update(transfer);
                _db.SaveChanges();
                _logger.LogInformation("+TransferRep : Transfer {Number} was updated at Transfers", obj.Transferid);
                return ExitCode.Success;

            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException err)
            {
                _logger.LogError(err, "+TransferRep : Constraint violation when trying to update transfer at Transfers");
                return ExitCode.Constraint;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+TransferRep : Error trying to update transfer at Transfers");
                return ExitCode.Error;
            }
        }

        public ExitCode DeleteByID(int id)
        {
            try
            {
                Transfer transfer = _db.Transfers.Find(id);
                _db.Transfers.Remove(transfer);
                _db.SaveChanges();
                _logger.LogInformation("+TransferRep : Transfer {Number} was deleted from Transfers", transfer.Transferid);
                return ExitCode.Success;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+TransferRep : Error trying to delete transfer from Transfer");
                return ExitCode.Error;
            }
        }

        public List<TransferBL> FindTransferByType(string type)
        {
            IQueryable<Transfer> transfers = _db.Transfers.Where(needed => needed.Type.Equals(type));
            List<Transfer> lTransfers = transfers.ToList();
            List<TransferBL> lTransfersBL = ListTransferBL(lTransfers);
            return lTransfersBL;
        }

        public List<TransferBL> FindTransfersByCity(string cityFrom)
        {
            IQueryable<Transfer> transfers = _db.Transfers.Where(needed => needed.Cityfrom.Equals(cityFrom));
            List<Transfer> lTransfers = transfers.ToList();
            List<TransferBL> lTransfersBL = ListTransferBL(lTransfers);
            return lTransfersBL;
        }

        public List<TransferBL> FindTransfersByDate(DateTime date)
        {
            DateTime dateBegin = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            
            IQueryable<Transfer> transfers = _db.Transfers.Where(needed => needed.Departuretime >= dateBegin &&
                                                                           needed.Departuretime <= dateEnd);
            List<Transfer> lTransfers = transfers.ToList();
            List<TransferBL> lTransfersBL = ListTransferBL(lTransfers);
            return lTransfersBL;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}