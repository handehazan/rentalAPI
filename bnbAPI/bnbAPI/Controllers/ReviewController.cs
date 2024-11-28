using bnbAPI.model;
using bnbAPI.model.Dto;
using bnbAPI.Source.Svc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace bnbAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ReviewController : ControllerBase
    {


        private IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService; 
            
        }

        [HttpPost("ReviewStay")]
        [Authorize(Roles ="Guest")]
        public IActionResult ReviewStay([FromQuery] ReviewDto reviewDto)
        {
            try
            {
                if (reviewDto == null)
                {
                    return BadRequest(new { Status = "Error", Message = "Invalid review data." });
                }

                if (reviewDto.Rating < 0 || reviewDto.Rating > 5)
                {
                    return BadRequest(new { Status = "Error", Message = "Rating must be between 1 and 5." });
                }

                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                Review review = createReview(reviewDto);
                _reviewService.ReviewStay(review,userId);

                return Ok(new { Status = "Successful", Message = "Review added successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Status = "Error", Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Status = "Error", Message = "An internal server error occurred." });
            }
        }

        private Review createReview(ReviewDto reviewDto) 
        {
            Review ret = new Review()
            {
                StayId = reviewDto.StayId,
                Rating = reviewDto.Rating,
                Comment = reviewDto.Comment,
            };
            return ret;


        }
    }
}
