using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyManagementAPI.Models;
using TravelAgencyManagementAPI.Repositories;

namespace TravelAgencyManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageGalleriesController : ControllerBase
    {
        private readonly IImageGalleryRepository _repository;

        public ImageGalleriesController(IImageGalleryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageGallery>>> GetImageGalleries()
        {
            try
            {
                var imageGalleries = await _repository.GetAllImageGalleries();
                return Ok(imageGalleries);
            }
            catch (Exception ex)
            {
                // Log exception and return error response
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ImageGallery>> GetImageGallery(int id)
        {
            try
            {
                var imageGallery = await _repository.GetImageGalleryById(id);
                if (imageGallery == null)
                {
                    return NotFound();
                }
                return Ok(imageGallery);
            }
            catch (Exception ex)
            {
                // Log exception and return error response
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutImageGallery(int id, ImageGallery imageGallery)
        {
            try
            {
                var updated = await _repository.UpdateImageGallery(id, imageGallery);
                if (!updated)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log exception and return error response
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ImageGallery>> PostImageGallery([FromForm] ImageGallery imageGallery, IFormFile imageFile)
        {
            try
            {
                var createdImageGallery = await _repository.CreateImageGallery(imageGallery,imageFile);
                return CreatedAtAction("GetImageGallery", new { id = createdImageGallery.ImageId }, createdImageGallery);
            }
            catch (Exception ex)
            {
                // Log exception and return error response
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImageGallery(int id)
        {
            try
            {
                var deleted = await _repository.DeleteImageGallery(id);
                if (!deleted)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log exception and return error response
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
    }
}
