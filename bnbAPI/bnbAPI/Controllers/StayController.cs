using bnbAPI.model;
using bnbAPI.model.Dto;
using bnbAPI.Source.Svc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace bnbAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class StayController : ControllerBase
    {
        private IStayService _stayService;

        public StayController(IStayService stayService)
        {
            _stayService = stayService;
            
        }
    
        [HttpPost("BookStay")]
        [Authorize(Roles ="Guest")]
        public IActionResult BookStay([FromQuery] StayDto stayDto)
        {
            try
            {
               
                if (stayDto == null || stayDto.GuestNames == null || !stayDto.GuestNames.Any())
                {
                    return BadRequest(new { Status = "Error", Message = "Invalid stay data." });
                }
               

                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                if (userId < 0)
                {
                    return Unauthorized(new { Status = "Error", Message = "User ID is required." });
                }

                Stay stay = createStay(stayDto,userId);

                _stayService.BookStay(stay);

                return Ok(new {Status ="Successful", Message="Stay booked successfully."});

            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { Status = "Error", Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Status = "Error", Message = "An internal server error occurred." });
            }

        }

        [HttpGet("ShowMyStays")]
        [Authorize(Roles = "Guest")]
        public IActionResult GetMyStays()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                List<Stay> userStays = _stayService.GetStaysByUserId(userId);

                if (userStays == null || !userStays.Any())
                {
                    return NotFound(new { Status = "Error", Message = "No stays found for the current user." });
                }

                var response = userStays.Select(stay => ShowStayDto(stay));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }

        private Stay createStay(StayDto stayDto,int userId)
        {
            if (stayDto.GuestNames == null || !stayDto.GuestNames.Any())
            {
                throw new ArgumentException("Guest names cannot be null or empty.");
            }

            Stay ret = new Stay()
            {
                ListingId = stayDto.ListingId,
                UserId = userId,
                GuestNames = string.Join(",", stayDto.GuestNames),
                StartDate = stayDto.StartDate,
                EndDate = stayDto.EndDate,

            };
            return ret;
        }

        private ShowStayDto ShowStayDto(Stay stay)
        {
            ShowStayDto stayDto = new ShowStayDto()
            {
                StayId = stay.Id,
                ListingId = stay.ListingId,
                UserId = stay.UserId,
                GuestNames = string.Join(",", stay.GuestNames),
                StartDate = stay.StartDate,
                EndDate = stay.EndDate
            };
            return stayDto;

        }
    }
}
