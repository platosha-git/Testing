using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToursWeb.Controllers;
using ToursWeb.ModelsDTO;
using ToursWeb.ModelsBL;

namespace ToursAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/Users")]
    
    public class ApiUserController : ControllerBase
    {
        private readonly UserController _userController;
        private readonly TourController _tourController;

        public ApiUserController(UserController userController, TourController tourController)
        {
            _userController = userController;
            _tourController = tourController;
        }

        private List<UserDTO> ListUsersDTO(List<UserBL> lUsers)
        {
            List<UserDTO> lUsersDTO = new List<UserDTO>();
            foreach (var user in lUsers)
            {
                UserDTO userDTO = new UserDTO(user);
                lUsersDTO.Add(userDTO);
            }
            return lUsersDTO;
        }
        
        private List<UserTour> GetAllUserBookings(int userID)
        {
            if (_userController.GetAllUserInfo(userID) == null)
            {
                return null;
            }
            
            List<int> toursID = _userController.GetBookedTours(userID);
            if (toursID.Count == 0)
            {
                return null;
            }
            
            List<TourBL> tours = new List<TourBL>();
            foreach (int tour in toursID)
            {
                TourBL curTour = _tourController.GetTourByID(tour);
                tours.Add(curTour);
            }

            List<UserTour> lTours = _tourController.ToUserTour(tours);
            return lTours;
        }
        
        /// <summary>Users by params</summary>
        /// <returns>Users information</returns>
        /// <response code="200">Users found</response>
        /// <response code="401">Error authorization</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDTO>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetAllUsers([FromQuery(Name = "Login")] string login = null)
        {
            List<UserBL> users = _userController.GetAllUsers();
            if (login != null)
            {
                List<UserBL> usersL = _userController.GetUserByLogin(login);
                users = usersL;
            }
            
            if (users.Count == 0)
            {
                return Unauthorized();
            }

            List<UserDTO> lUsersDTO = ListUsersDTO(users);
            return Ok(lUsersDTO);
        }
        
        /// <summary>User by ID</summary>
        /// <returns>User information</returns>
        /// <response code="200">User found</response>
        /// <response code="404">No user</response>
        [HttpGet]
        [Route("{UserID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllUserInfo([FromRoute(Name = "UserID")] int userID)
        {
            UserBL user = _userController.GetAllUserInfo(userID);
            if (user == null)
            {
                return NotFound();
            }

            UserDTO userDTO = new UserDTO(user);
            return Ok(userDTO);
        }

        public enum Action
        {
            Book,
            Cancel
        }

        /// <summary>Manage tours</summary>
        /// <returns>Tours information</returns>
        /// <response code="200">Tours updated</response>
        /// <response code="204">No booked tours</response>
        /// <response code="409">Constraint error</response>
        /// <response code="500">Internal server error</response>
        [HttpPatch]
        [Route("{UserID:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserTour>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ManageTours([FromQuery(Name = "Action"), Required] Action action, 
            [FromRoute(Name = "UserID")] int userID, [FromQuery(Name = "TourID"), Required] int tourID)
        {
            if (_userController.GetAllUserInfo(userID) == null)
            {
                return BadRequest();
            }
            
            List<int> toursID = _userController.GetBookedTours(userID);
            int size = toursID.Count;

            bool isExists = false;
            for (int i = 0; i < size && !isExists; i++)
            {
                if (toursID[i] == tourID)
                {
                    isExists = true;
                }
            }

            ExitCode result = ExitCode.Success;
            if (action == Action.Book)
            {
                if (isExists)
                {
                    return BadRequest();
                }
                result = _userController.BookTour(userID, tourID);
            }
            else
            {
                if (!isExists)
                {
                    return BadRequest();
                }
                result = _userController.CancelTour(userID, tourID);
            }
            
            if (result == ExitCode.Constraint) 
            {
                return Conflict();
            }

            if (result == ExitCode.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            List<UserTour> lTours = GetAllUserBookings(userID);
            if (lTours == null)
            {
                return NoContent();
            }
            return Ok(lTours);
        }
    }
}
