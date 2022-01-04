using Xunit;
using ToursWeb.ModelsDB;
using ToursWeb.ModelsBL;
using ToursTests.Builders;
using System.Collections.Generic;

namespace ToursTests.IntegrationTests
{
    public class TransferTests : IClassFixture<TourAccessObject>
    {
        private readonly TourAccessObject _accessObject;

        public TransferTests() 
        {
            _accessObject = new TourAccessObject();
        }

        [Fact]
        public void FindAll_NotNull()
        {
            // Arrange
            var expTransf = createTransfList();
            addEntities(expTransf);

            // Act
            var actTransfBL = _accessObject.transferRepository.FindAll();
            var actTransf = getTransfList(actTransfBL);

            // Assert
            Assert.NotNull(actTransf);
            Assert.True(areEqual(expTransf, actTransf));

            Cleanup();
        }

        [Fact]
        public void FindByID_FirstElement_NotNull()
        {
            const int transferID = 1;
            
            // Arrange
            var expTransf = createTransfList();
            addEntities(expTransf);

            // Act
            var actTransfBL = _accessObject.transferRepository.FindByID(transferID);
            var actTransf = new Transfer(actTransfBL);

            // Assert
            Assert.NotNull(actTransf);
            Assert.Equal(transferID, actTransf.Transferid);

            Cleanup();
        }
        
        [Fact]
        public void FindByType()
        {
            const string type = "Bus";
            
            // Arrange
            var expTransf = new List<Transfer>();
            for (var i = 5; i < 10; i++)
            {
                var curTransferBL = new TransferBuilder().WhereTransferID(i).WhereType(type).Build();
                var curTransfer = new Transfer(curTransferBL);
                expTransf.Add(curTransfer);
            }
            addEntities(expTransf);

            // Act
            var actTransfBL = _accessObject.transferRepository.FindTransferByType(type);
            var actTransf = getTransfList(actTransfBL);

            // Assert
            Assert.NotNull(actTransf);

            Cleanup();
        }

        private List<Transfer> createTransfList()
        {
            var transfers = new List<Transfer>();
            for (var i = 1; i < 5; i++)
            {
                var curTransferBL = new TransferBuilder()
                    .WhereTransferID(i)
                    .Build();
                var curTransfer = new Transfer(curTransferBL);
                transfers.Add(curTransfer);
            }
            
            return transfers;
        }

        void addEntities(List<Transfer> transfers)
        {
            _accessObject.toursContext.ChangeTracker.Clear();
            _accessObject.toursContext.Transfers.AddRange(transfers);
            _accessObject.toursContext.SaveChanges();
        }

        private List<Transfer> getTransfList(List<TransferBL> transfBL)
        {
            List<Transfer> transfers = new List<Transfer>();
            foreach (var transBL in transfBL)
            {
                Transfer transfer = new Transfer(transBL);
                transfers.Add(transfer);
            }
            return transfers;
        }

        bool areEqual(List<Transfer> expTransf, List<Transfer> actTransf)
        {
            int size = actTransf.Count;
            bool equal = true;
            for (int i = 0; i < size && equal; i++)
            {
                equal = areEqual(expTransf[i], actTransf[size - 1 - i]);
            }
            
            return equal;
        }
        
        bool areEqual(Transfer expTransf, Transfer actTransf)
        {
            return (expTransf.Transferid == actTransf.Transferid &&
                    expTransf.Type == actTransf.Type &&
                    expTransf.Cityfrom == actTransf.Cityfrom &&
                    expTransf.Departuretime == actTransf.Departuretime &&
                    expTransf.Cost == actTransf.Cost);
        }

        void Cleanup()
        {
            _accessObject.toursContext.ChangeTracker.Clear();
            _accessObject.toursContext.Transfers.RemoveRange(_accessObject.toursContext.Transfers);
            _accessObject.toursContext.SaveChanges();
        }
    }
}
