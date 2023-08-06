using Microsoft.AspNetCore.Mvc;
using TourManagementAPI.Models;
using TourManagementAPI.Repositories; // Import your repository namespace

namespace TourManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TouristAttractionsController : ControllerBase
    {
        private readonly ITouristAttractionRepository _touristAttractionRepository; // Use your repository interface

        public TouristAttractionsController(ITouristAttractionRepository touristAttractionRepository)
        {
            _touristAttractionRepository = touristAttractionRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TouristAttraction>>> GetTouristAttractions()
        {
            try
            {
                var touristAttractions = await _touristAttractionRepository.GetTouristAttractionsAsync();
                return Ok(touristAttractions);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TouristAttraction>> GetTouristAttraction(int id)
        {
            try
            {
                var touristAttraction = await _touristAttractionRepository.GetTouristAttractionByIdAsync(id);
                if (touristAttraction == null)
                {
                    return NotFound();
                }
                return Ok(touristAttraction);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTouristAttraction(int id, [FromForm] TouristAttraction touristAttraction, IFormFile imageFile)
        {
            try
            {
                if (id != touristAttraction.TouristAttractionId)
                {
                    return BadRequest();
                }

                var updatedTouristAttraction = await _touristAttractionRepository.UpdateTouristAttractionAsync(id, touristAttraction,imageFile);
                if (updatedTouristAttraction == null)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<TouristAttraction>> PostTouristAttraction([FromForm] TouristAttraction touristAttraction, IFormFile imageFile)
        {
            try
            {
                var createdTouristAttraction = await _touristAttractionRepository.AddTouristAttractionAsync(touristAttraction,imageFile);
                return CreatedAtAction(nameof(GetTouristAttraction), new { id = createdTouristAttraction.TouristAttractionId }, createdTouristAttraction);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTouristAttraction(int id)
        {
            try
            {
                var touristAttractionToDelete = await _touristAttractionRepository.GetTouristAttractionByIdAsync(id);
                if (touristAttractionToDelete == null)
                {
                    return NotFound();
                }

                await _touristAttractionRepository.DeleteTouristAttractionAsync(touristAttractionToDelete);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
