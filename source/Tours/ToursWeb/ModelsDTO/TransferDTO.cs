using System;
using ToursWeb.ModelsBL;

namespace ToursWeb.ModelsDTO
{
    public class TransferUserDTO
    {
        public TType Type { get; set; }
        public string Cityfrom { get; set; }
        public DateTime? Departuretime { get; set; }
        public int? Cost { get; set; }

        public TransferUserDTO() {}

        public TransferBL GetTransfer(int transferID = 0)
        {
            TransferBL transfer = new TransferBL ()
            {
                Transferid = transferID,
                Type = Type,
                Cityfrom = Cityfrom,
                Departuretime = Departuretime,
                Cost = Cost
            };
            
            return transfer;
        }
    }

    public class TransferDTO : TransferUserDTO
    {
        public int Transferid { get; set; }
        
        public TransferDTO() {}
        
        public TransferDTO(TransferBL transfer)
        {
            Transferid = transfer.Transferid;
            Type = transfer.Type;
            Cityfrom = transfer.Cityfrom;
            Departuretime = transfer.Departuretime;
            Cost = transfer.Cost;
        }
    }
}