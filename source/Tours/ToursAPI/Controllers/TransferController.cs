using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToursWeb.Controllers;
using ToursWeb.ModelsDTO;
using ToursWeb.ModelsBL;

namespace ToursAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/Transfers")]

    public class ApiTransferController : ControllerBase
    {
        private readonly TransferController _transferController;

        public ApiTransferController(TransferController transferController)
        {
            _transferController = transferController;
        }
        
        bool isCorrectDate(string date)
        {
            try
            {
                Convert.ToDateTime(date);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private List<TransferDTO> ListTransferDTO(List<TransferBL> lTransfers)
        {
            List<TransferDTO> lTransfersDTO = new List<TransferDTO>();
            foreach (var transfer in lTransfers)
            {
                TransferDTO transferDTO = new TransferDTO(transfer);
                lTransfersDTO.Add(transferDTO);
            }
            return lTransfersDTO;
        }

        /// <summary>Transfers by parameters</summary>
        /// <returns>Transfers information</returns>
        /// <param name="type"></param>
        /// <param name="cityFrom"></param>
        /// <param name="date">Format: dd-mm-yyyy</param>
        /// <response code="200">Transfers found</response>
        /// <response code="204">No transfers</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TransferDTO>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllTransfer([FromQuery(Name = "Type")] TType? type = null,
            [FromQuery(Name = "CityFrom")] string cityFrom = null, [FromQuery(Name = "Date")] string date = null)
        {
            List<TransferBL> transfers = _transferController.GetAllTransfer();
            if (transfers.Count != 0)
            {
                if (type != null)
                {
                    transfers = _transferController.GetTransferByType(type.ToString());
                }

                if (cityFrom != null)
                {
                    List<TransferBL> transfersCities = _transferController.GetTransfersByCity(cityFrom);
                    List<TransferBL> res1 = transfers.Intersect(transfersCities).ToList();
                    transfers = res1;
                }

                if (date != null)
                {
                    if (!isCorrectDate(date))
                    {
                        return BadRequest();
                    }
                    DateTime dateTr = Convert.ToDateTime(date);
                    List<TransferBL> transfersDate = _transferController.GetTransfersByDate(dateTr);
                    List<TransferBL> res2 = transfers.Intersect(transfersDate).ToList();
                    transfers = res2;
                }
            }

            if (transfers.Count == 0)
            {
                return NoContent();
            }

            List<TransferDTO> lTtransfersDTO = ListTransferDTO(transfers);
            return Ok(lTtransfersDTO);
        }

        /// <summary>Transfer by ID</summary>
        /// <returns>Transfer information</returns>
        /// <response code="200">Transfer found</response>
        /// <response code="404">No transfer</response>
        [HttpGet]
        [Route("{TransferID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransferDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTransferByID([FromRoute(Name = "TransferID")] int transferID)
        {
            var transfer = _transferController.GetTransferByID(transferID);
            if (transfer == null)
            {
                return NotFound();
            }

            TransferDTO transferDTO = new TransferDTO(transfer);
            return Ok(transferDTO);
        }

        /// <summary>Adding transfer</summary>
        /// <param name="transferDTO">Transfer to add</param>
        /// <returns>Added transfer</returns>
        /// <response code="201">Transfer added</response>
        /// <response code="409">Constraint error</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TransferDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddTransfer([FromBody] TransferUserDTO transferDTO)
        {
            TransferBL aTransfer = transferDTO.GetTransfer();
            ExitCode result = _transferController.AddTransfer(aTransfer);
            
            if (result == ExitCode.Constraint) 
            {
                return Conflict();
            }

            if (result == ExitCode.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            TransferDTO addedTransfer = new TransferDTO(aTransfer);
            return StatusCode(StatusCodes.Status201Created, addedTransfer);
        }

        /// <summary>Updating transfer</summary>
        /// <param name="transferDTO">Transfer to update</param>
        /// <returns>Updated transfer</returns>
        /// <response code="200">Transfer updated</response>
        /// <response code="409">Constraint error</response>
        /// <response code="500">Internal server error</response>
        [HttpPut]
        [Route("{TransferID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransferDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTransfer([FromRoute(Name = "TransferID")] int transferID, [FromBody] TransferUserDTO transferDTO)
        {
            TransferBL uTransfer = transferDTO.GetTransfer(transferID);
            ExitCode result = _transferController.UpdateTransfer(uTransfer);
            
            if (result == ExitCode.Constraint) 
            {
                return Conflict();
            }

            if (result == ExitCode.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            TransferDTO updatedTransfer = new TransferDTO(uTransfer);
            return Ok(updatedTransfer);
        }

        /// <summary>Removing transfer by ID</summary>
        /// <returns>Removed transfer</returns>
        /// <response code="200">Transfer removed</response>
        /// <response code="404">No transfer</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete]
        [Route("{TransferID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransferDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTransfer([FromRoute(Name = "TransferID")] int transferID)
        {
            TransferBL delTransfer = _transferController.GetTransferByID(transferID);
            if (delTransfer == null)
            {
                return NotFound();
            }
            
            ExitCode result = _transferController.DeleteTransferByID(transferID);
            if (result == ExitCode.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
            TransferDTO transfer = new TransferDTO(delTransfer);
            return Ok(transfer);
        }
    }
}
