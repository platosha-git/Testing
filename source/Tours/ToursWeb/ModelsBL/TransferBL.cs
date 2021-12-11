using System;
using ToursWeb.ModelsDB;

namespace ToursWeb.ModelsBL
{
    public enum TType
    {
        Bus,
        Train,
        Plane
    }
    
    public class TransferBL
    {
        public int Transferid { get; set; }
        public TType Type { get; set; }
        public string Cityfrom { get; set; }
        public DateTime? Departuretime { get; set; }
        public int? Cost { get; set; }

        public TransferBL() { }

        private bool Equals(TransferBL transfer)
        {
            if (transfer is null)
            {
                return false;
            }

            return Transferid == transfer.Transferid &&
                   Type == transfer.Type &&
                   Cityfrom == transfer.Cityfrom &&
                   Departuretime == transfer.Departuretime &&
                   Cost == transfer.Cost;
        }

        public override bool Equals(object obj) => Equals(obj as TransferBL);
        public override int GetHashCode() => (Transferid, Type, Cityfrom, Departuretime, Cost).GetHashCode();
    }
}