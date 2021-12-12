using System.Collections.Generic;
using ToursWeb.Repositories;
using ToursWeb.Controllers;
using ToursTests.Builders;
using ToursWeb.ModelsBL;
using Xunit;
using Moq;

namespace ToursTests.Tests
{
    public class TransferTests
    {
        [Fact]
        public void FindAll()
        {
            var expTransfer = new TransferBuilder().Build();
            var expTransfers = new List<TransferBL>() {expTransfer};

            var mock = new Mock<ITransferRepository>();
            mock.Setup(x => x.FindAll())
                .Returns(expTransfers);
            var transferController = new TransferController(mock.Object);
            
            var actTransfers = transferController.GetAllTransfer();
            
            Assert.Equal(expTransfers, actTransfers);
        }
        
        [Fact]
        public void FindByID()
        {
            const int transferID = 1;
            
            var expTransfer = new TransferBuilder()
                .WhereTransferID(transferID)
                .Build();

            var mock = new Mock<ITransferRepository>();
            mock.Setup(x => x.FindByID(transferID))
                .Returns(expTransfer);
            var transferController = new TransferController(mock.Object);
            
            var actTransfer = transferController.GetTransferByID(transferID);
            
            Assert.Equal(expTransfer, actTransfer);
        }
    }
}