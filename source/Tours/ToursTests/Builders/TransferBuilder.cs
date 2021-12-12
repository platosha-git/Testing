using System;
using ToursWeb.ModelsBL;

namespace ToursTests.Builders
{
    public class TransferBuilder
    {
        private int Transferid;
        private TType Type;
        private string Cityfrom;
        private DateTime? Departuretime;
        private int? Cost;

        public TransferBuilder() { }

        public TransferBL Build()
        {
            var transfer = new TransferBL()
            {
                Transferid = Transferid,
                Type = Type,
                Cityfrom = Cityfrom,
                Departuretime = Departuretime,
                Cost = Cost
            };

            return transfer;
        }

        public TransferBuilder WhereTransferID(int transferID)
        {
            Transferid = transferID;
            return this;
        }
    }
}