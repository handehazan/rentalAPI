using bnbAPI.model;
using bnbAPI.model.Dto;
using bnbAPI.Source.Svc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace bnbAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ListingController : ControllerBase
    {
        private IListingService _listingService;

        public ListingController(IListingService listingService)
        {
            _listingService = listingService;
        }

       
        [HttpPost("InsertListing")]
        [Authorize(Roles ="Host")]
        public IActionResult InsertListing([FromQuery] InsertListingDto listingdto)
        {
            try
            {
                if (listingdto == null)
                {
                    return BadRequest("Invalid listing data.");
                }

                Listing newListing = createListing(listingdto);

                _listingService.AddListing(newListing);
                return Ok(new { Message = "Listing inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Error = ex.Message });

            }
        }



        [HttpGet("QueryListingWithPaging")]
        [AllowAnonymous]
        public List<ShowListingDto> QueryListing(
            [FromQuery] string country,
            [FromQuery] string city,
            [FromQuery] int? maxPeople,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            int PageNumber=1,int PageSize=2)
        {
            var listings = _listingService.QueryListings(country, city, maxPeople, startDate, endDate);

            var paginatedListings = listings.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();

            List<ShowListingDto> response = new List<ShowListingDto>();
            paginatedListings.ForEach(listing => response.Add(showListing(listing)));

            return response;

        }


        [HttpGet("ReportListingsWithPaging")]
        [Authorize(Roles = "Admin")]
        public IActionResult ReportListings([FromQuery] string country, [FromQuery] string? city, [FromQuery] decimal? minRating, int PageNumber=1, int PageSize=2)
        {
            try
            {
                
                var listings = _listingService.ReportListings(country, city, minRating);

                if (listings == null || !listings.Any())
                {
                    return NotFound(new { Status = "Error", Message = "No listings found matching the criteria." });
                }


                var paginatedListings = listings.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();

                List<ShowListingDto> response = new List<ShowListingDto>();
                paginatedListings.ForEach(listing => response.Add(showListing(listing)));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }



        private Listing createListing(InsertListingDto insertListingDto)
        {
            Listing ret = new Listing()
            {
                Title = insertListingDto.Title,
                Country = insertListingDto.Country,
                City = insertListingDto.City,
                MaxPeople = insertListingDto.MaxPeople,
                Price = insertListingDto.Price,
                Rating = 0,
                TotalNumberOfRatings = 0,
                TotalRatingSum = 0
            };

            return ret;
        }

        private ShowListingDto showListing(Listing listing)
        {
            ShowListingDto ret = new ShowListingDto()
            {
                Id = listing.Id,
                Title = listing.Title,
                Country = listing.Country,
                City = listing.City,
                MaxPeople = listing.MaxPeople,
                Price = listing.Price,
                Rating = listing.Rating
            };

            return ret;
        }




    }
}
