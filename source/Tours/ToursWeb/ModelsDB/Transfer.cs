using System;
using System.Collections.Generic;
using ToursWeb.ModelsBL;

#nullable disable

namespace ToursWeb.ModelsDB
{
    public partial class Transfer
    {
        public Transfer()
        {
            Tours = new HashSet<Tour>();
        }

        public Transfer(TransferBL transferBL)
        {
            Transferid = transferBL.Transferid;
            Type = Enum.GetName(typeof(TType), transferBL.Type);
            Cityfrom = transferBL.Cityfrom;
            Departuretime = transferBL.Departuretime;
            Cost = transferBL.Cost;
        }

        public int Transferid { get; set; }
        public string Type { get; set; }
        public string Cityfrom { get; set; }
        public DateTime? Departuretime { get; set; }
        public int? Cost { get; set; }

        public virtual ICollection<Tour> Tours { get; set; }

        public TransferBL ToBL()
        {
            TransferBL transferBl = new TransferBL()
            {
                Transferid = Transferid,
                Type = (TType) Enum.Parse(typeof(TType), Type),
                Cityfrom = Cityfrom,
                Departuretime = Departuretime,
                Cost = Cost
            };

            return transferBl;
        }
    }
}
