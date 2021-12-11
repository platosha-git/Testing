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
    [Route("/api/v1/Tours")]

    public class ApiTourController : ControllerBase
    {
        private readonly TourController _tourController;
        private readonly UserController _userController;

        public ApiTourController(TourController tourController, UserController userController)
        {
            _tourController = tourController;
            _userController = userController;
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

        /// <summary>Tours by parameters</summary>
        /// <returns>Tours information</returns>
        /// <param name="city"></param>
        /// <param name="name"></param>
        /// <param name="dBegin">Format: dd-mm-yyyy</param>
        /// <param name="dEnd">Format: dd-mm-yyyy</param>
        /// <param name="userID"></param>
        /// <response code="200">Tours found</response>
        /// <response code="204">No tours</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserTour>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

            public IActionResult GetAllTours([FromQuery(Name = "City")] string city = null,
            [FromQuery(Name = "HotelName")] string name = null,
            [FromQuery(Name = "DateBegin")] string dBegin = null, [FromQuery(Name = "DateEnd")] string dEnd = null,
            [FromQuery(Name = "UserID")] int? userID = null)
        {
            List<TourBL> tours = _tourController.GetAllTours();
            List<UserTour> lTours = null;
            if (tours.Count != 0)
            {
                if (city != null)
                {
                    if (name != null)
                    {
                        List<TourBL> toursCN = _tourController.GetToursByCityName(city, name);
                        List<TourBL> res1 = tours.Intersect(toursCN).ToList();
                        tours = res1;
                    }
                    else
                    {
                        List<TourBL> toursC = _tourController.GetToursByCity(city);
                        List<TourBL> res2 = tours.Intersect(toursC).ToList();
                        tours = res2;
                    }
                }

                if (dBegin != null && dEnd != null)
                {
                    if (!isCorrectDate(dBegin) || !isCorrectDate(dEnd))
                    {
                        return BadRequest();
                    }
                    
                    DateTime beg = Convert.ToDateTime(dBegin);
                    DateTime end = Convert.ToDateTime(dEnd);

                    List<TourBL> toursDate = _tourController.GetToursByDate(beg, end);
                    List<TourBL> res1 = tours.Intersect(toursDate).ToList();
                    tours = res1;
                }

                if (userID != null)
                {
                    if (_userController.GetAllUserInfo((int) userID) == null)
                    {
                        return NotFound();
                    }
            
                    List<int> toursID = _userController.GetBookedTours((int) userID);
                    if (toursID.Count == 0)
                    {
                        return NoContent();
                    }
            
                    List<TourBL> toursUser = new List<TourBL>();
                    foreach (int tour in toursID)
                    {
                        TourBL curTour = _tourController.GetTourByID(tour);
                        toursUser.Add(curTour);
                    }

                    lTours = _tourController.ToUserTour(toursUser);
                    return Ok(lTours);
                }
            }
            
            if (tours.Count == 0)
            {
                return NoContent();
            }

            List<UserTour> lUserToursDTO = _tourController.ToUserTour(tours);
            return Ok(lUserToursDTO);
        }
        
        /// <summary>Tour by ID</summary>
        /// <returns>Tour information</returns>
        /// <response code="200">Tour found</response>
        /// <response code="404">No tour</response>
        [HttpGet]
        [Route("{TourID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TourDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTourByID([FromRoute(Name = "TourID")] int tourID)
        {
            var tour = _tourController.GetTourByID(tourID);
            if (tour == null)
            {
                return NotFound();
            }

            TourDTO tourDTO = new TourDTO(tour);
            return Ok(tourDTO);
        }

        /// <summary>Adding tour</summary>
        /// <param name="tourDTO">Tour to add</param>
        /// <returns>Added tour</returns>
        /// <response code="201">Tour added</response>
        /// <response code="409">Constraint error</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TourDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddTour([FromBody] TourUserDTO tourDTO)
        {
            TourBL aTour = tourDTO.GetTour();
            ExitCode result = _tourController.AddTour(aTour);
            
            if (result == ExitCode.Constraint) 
            {
                return Conflict();
            }

            if (result == ExitCode.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            TourDTO addedTour = new TourDTO(aTour);
            return StatusCode(StatusCodes.Status201Created, addedTour);
        }

        /// <summary>Updating tour</summary>
        /// <param name="tourDTO">Tour to update</param>
        /// <returns>Updated tour</returns>
        /// <response code="200">Tour updated</response>
        /// <response code="409">Constraint error</response>
        /// <response code="500">Internal server error</response>
        [HttpPut]
        [Route("{TourID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TourDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTour([FromRoute(Name = "TourID")] int tourID, [FromBody] TourUserDTO tourDTO)
        {
            TourBL uTour = tourDTO.GetTour(tourID);
            ExitCode result = _tourController.UpdateTour(uTour);
            
            if (result == ExitCode.Constraint) 
            {
                return Conflict();
            }

            if (result == ExitCode.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            TourDTO updatedTour = new TourDTO(uTour);
            return Ok(updatedTour);
        }

        /// <summary>Removing transfer by ID</summary>
        /// <returns>Removed transfer</returns>
        /// <response code="200">Transfer removed</response>
        /// <response code="404">No transfer</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete]
        [Route("{TourID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TourDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteTour([FromRoute(Name = "TourID")] int tourID)
        {
            TourBL delTour = _tourController.GetTourByID(tourID);
            if (delTour == null)
            {
                return NotFound();
            }
            
            ExitCode result = _tourController.DeleteTourByID(tourID);
            if (result == ExitCode.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
            TourDTO tour = new TourDTO(delTour);
            return Ok(tour);
        }
    }
}
