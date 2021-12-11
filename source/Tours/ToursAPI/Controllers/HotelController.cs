using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToursWeb.ModelsBL;
using ToursWeb.ModelsDTO;
using ToursWeb.Controllers;

namespace ToursAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/Hotels")]

    public class ApiHotelController : ControllerBase
    {
        private readonly HotelController _hotelController;

        public ApiHotelController(HotelController hotelController)
        {
            _hotelController = hotelController;
        }

        bool isCorrectClass(int cls)
        {
            return (cls is >= 0 and <= 5);
        }
        
        bool isCorrectType(string type)
        {
            return (type.Equals("Hotel") || type.Equals("Apartment") ||
                    type.Equals("Hostel") || type.Equals("Guest house") ||
                    type.Equals("Motel") || type.Equals("Vila") ||
                    type.Equals("Camping") || type.Equals("BnB"));
        }
        
        private List<HotelDTO> ListHotelDTO(List<HotelBL> lHotels)
        {
            List<HotelDTO> lHotelDTO = new List<HotelDTO>();
            foreach (var hotel in lHotels)
            {
                HotelDTO hotelDTO = new HotelDTO(hotel);
                lHotelDTO.Add(hotelDTO);
            }
            return lHotelDTO;
        }
        
        /// <summary>Hotels by parameters</summary>
        /// <param name="city"></param>
        /// <param name="cls">Class (0 - 5)</param>
        /// <param name="type">Hotel, Apartment, Hostel, Guest house, Motel, Vila, Camping, BnB</param>
        /// <param name="sp"></param>
        /// <returns>Hotels information</returns>
        /// <response code="200">Hotels found</response>
        /// <response code="204">No hotels</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<HotelDTO>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllHotels([FromQuery(Name = "City")] string city = null,
            [FromQuery(Name = "Class")] int? cls = null, [FromQuery(Name = "Type")] string type = null,
            [FromQuery(Name = "Swimming pool")] bool? sp = null)
        {
            List<HotelBL> hotels = _hotelController.GetAllHotels();
            if (hotels.Count != 0)
            {
                if (city != null)
                {
                    hotels = _hotelController.GetHotelsByCity(city);
                }

                if (cls != null)
                {
                    if (!isCorrectClass((int) cls))
                    {
                        return BadRequest();
                    }
                    List<HotelBL> hotelsCls = _hotelController.GetHotelsByClass((int)cls);
                    List<HotelBL> res1 = hotels.Intersect(hotelsCls).ToList();
                    hotels = res1;
                }

                if (type != null)
                {
                    if (!isCorrectType(type))
                    {
                        return BadRequest();
                    }
                    List<HotelBL> hotelsType = _hotelController.GetHotelsByType(type);
                    List<HotelBL> res2 = hotels.Intersect(hotelsType).ToList();
                    hotels = res2;
                }
                
                if (sp != null)
                {
                    List<HotelBL> hotelsSP = _hotelController.GetHotelsBySwimPool((bool)sp);
                    List<HotelBL> res3 = hotels.Intersect(hotelsSP).ToList();
                    hotels = res3;
                }
            }
            
            if (hotels.Count == 0)
            {
                return NoContent();
            }

            List<HotelDTO> lHotelsDTO = ListHotelDTO(hotels);
            return Ok(lHotelsDTO);
        }
        
        /// <summary>Hotel by ID</summary>
        /// <returns>Hotel information</returns>
        /// <response code="200">Hotel found</response>
        /// <response code="404">No hotel</response>
        [HttpGet]
        [Route("{HotelID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HotelDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetHotelByID([FromRoute(Name = "HotelID")] int hotelID)
        {
            var hotel = _hotelController.GetHotelByID(hotelID);
            if (hotel is null)
            {
                return NotFound();
            }

            HotelDTO hotelDTO = new HotelDTO(hotel);
            return Ok(hotelDTO);
        }

        /// <summary>Adding hotel</summary>
        /// <param name="hotelDTO">Hotel to add</param>
        /// <returns>Added hotel</returns>
        /// <response code="201">Hotel added</response>
        /// <response code="409">Constraint error</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(HotelDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddHotel([FromBody] HotelUserDTO hotelDTO)
        {
            HotelBL aHotel = hotelDTO.GetHotel();
            ExitCode result = _hotelController.AddHotel(aHotel);
            
            if (result == ExitCode.Constraint) 
            {
                return Conflict();
            }

            if (result == ExitCode.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            HotelDTO addedHotel = new HotelDTO(aHotel);
            return StatusCode(StatusCodes.Status201Created, addedHotel);
        }

        /// <summary>Updating hotel</summary>
        /// <param name="hotelDTO">Hotel to update</param>
        /// <returns>Updated hotel</returns>
        /// <response code="200">Hotel updated</response>
        /// <response code="409">Constraint error</response>
        /// <response code="500">Internal server error</response>
        [HttpPut]
        [Route("{HotelID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HotelDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateHotel([FromRoute(Name = "HotelID")] int hotelID, [FromBody] HotelUserDTO hotelDTO)
        {
            HotelBL uHotel = hotelDTO.GetHotel(hotelID);
            ExitCode result = _hotelController.UpdateHotel(uHotel);

            if (result == ExitCode.Constraint) 
            {
                return Conflict();
            }

            if (result == ExitCode.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            HotelDTO updatedHotel = new HotelDTO(uHotel);
            return Ok(updatedHotel);
        }

        /// <summary>Removing hotel by ID</summary>
        /// <returns>Removed hotel</returns>
        /// <response code="200">Hotel removed</response>
        /// <response code="404">No hotel</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete]
        [Route("{HotelID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HotelDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteHotel([FromRoute(Name = "HotelID")] int hotelID)
        {
            HotelBL delHotel = _hotelController.GetHotelByID(hotelID);
            if (delHotel == null)
            {
                return NotFound();
            }
            
            ExitCode result = _hotelController.DeleteHotelByID(hotelID);
            if (result == ExitCode.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
            HotelDTO hotel = new HotelDTO(delHotel);
            return Ok(hotel);
        }
    }
}
