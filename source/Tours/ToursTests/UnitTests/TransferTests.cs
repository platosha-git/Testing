using System;
using System.Collections.Generic;
using ToursWeb.Repositories;
using ToursWeb.Controllers;
using ToursTests.Builders;
using ToursWeb.ModelsBL;
using Xunit;
using Moq;

namespace ToursTests.UnitTests
{
    public class TransferTests
    {
        [Fact]
        public void FindAll_NotNull()
        {
            var expTransfer = new TransferBuilder().Build();
            var expTransfers = new List<TransferBL>() {expTransfer};

            var mock = new Mock<ITransferRepository>();
            mock.Setup(x => x.FindAll())
                .Returns(expTransfers);
            var transferController = new TransferController(mock.Object);
            
            var actTransfers = transferController.GetAllTransfer();
            
            Assert.NotNull(expTransfers);
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
            
            Assert.NotNull(expTransfer);
            Assert.Equal(expTransfer, actTransfer);
        }
        
        [Fact]
        public void FindByType()
        {
            const string type = "Bus";
            
            var expTransfer = new TransferBuilder()
                .WhereType(type)
                .Build();
            
            var expTransfers = new List<TransferBL>() {expTransfer};

            var mock = new Mock<ITransferRepository>();
            mock.Setup(x => x.FindTransferByType(type))
                .Returns(expTransfers);
            var transferController = new TransferController(mock.Object);
            
            var actTransfers = transferController.GetTransferByType(type);
            
            Assert.NotNull(expTransfers);
            Assert.Equal(expTransfers, actTransfers);
        }
        
        [Fact]
        public void FindByCity()
        {
            const string city = "London";
            
            var expTransfer = new TransferBuilder()
                .WhereCity(city)
                .Build();
            
            var expTransfers = new List<TransferBL>() {expTransfer};

            var mock = new Mock<ITransferRepository>();
            mock.Setup(x => x.FindTransfersByCity(city))
                .Returns(expTransfers);
            var transferController = new TransferController(mock.Object);
            
            var actTransfers = transferController.GetTransfersByCity(city);
            
            Assert.NotNull(expTransfers);
            Assert.Equal(expTransfers, actTransfers);
        }
        
        [Fact]
        public void FindByDate()
        {
            var date = new DateTime(2022, 11, 09);
            
            var expTransfer = new TransferBuilder()
                .WhereDate(date)
                .Build();
            
            var expTransfers = new List<TransferBL>() {expTransfer};

            var mock = new Mock<ITransferRepository>();
            mock.Setup(x => x.FindTransfersByDate(date))
                .Returns(expTransfers);
            var transferController = new TransferController(mock.Object);
            
            var actTransfers = transferController.GetTransfersByDate(date);
            
            Assert.NotNull(expTransfers);
            Assert.Equal(expTransfers, actTransfers);
        }
    }
}