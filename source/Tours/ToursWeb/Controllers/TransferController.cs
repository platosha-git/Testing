using System;
using System.Collections.Generic;
using ToursWeb.ModelsDB;
using ToursWeb.ModelsBL;
using ToursWeb.Repositories;

namespace ToursWeb.Controllers
{
    public class TransferController
    {
        private readonly ITransferRepository _transferRepository;

        public TransferController(ITransferRepository transferRepository)
        {
            _transferRepository = transferRepository;
        }

        public List<TransferBL> GetAllTransfer()
        {
            return _transferRepository.FindAll();
        }
        
        public TransferBL GetTransferByID(int transfID)
        {
            return _transferRepository.FindByID(transfID);
        }
        
        public List<TransferBL> GetTransferByType(string type)
        {
            return _transferRepository.FindTransferByType(type);
        }
        
        public List<TransferBL> GetTransfersByCity(string cityFrom)
        {
            return _transferRepository.FindTransfersByCity(cityFrom);
        }

        public List<TransferBL> GetTransfersByDate(DateTime date)
        {
            return _transferRepository.FindTransfersByDate(date);
        }
        
        public ExitCode AddTransfer(TransferBL tran)
        {
            return _transferRepository.Add(tran);
        }
        
        public ExitCode UpdateTransfer(TransferBL tran)
        {
            return _transferRepository.Update(tran);
        }
        
        public ExitCode DeleteTransferByID(int tranID)
        {
            return _transferRepository.DeleteByID(tranID);
        }
    }
}