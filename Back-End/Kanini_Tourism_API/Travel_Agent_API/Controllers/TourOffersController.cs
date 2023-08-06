using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelAgencyManagementAPI.DB;
using TravelAgencyManagementAPI.Models;
using TravelAgencyManagementAPI.Repositories; // Import your repository namespace

namespace TravelAgencyManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourOffersController : ControllerBase
    {
        private readonly ITourOfferRepository _tourOfferRepository; // Use your repository interface

        public TourOffersController(ITourOfferRepository tourOfferRepository)
        {
            _tourOfferRepository = tourOfferRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TourOffer>>> GetTourOffers()
        {
            try
            {
                var tourOffers = await _tourOfferRepository.GetTourOffersAsync();
                return Ok(tourOffers);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TourOffer>> GetTourOffer(int id)
        {
            try
            {
                var tourOffer = await _tourOfferRepository.GetTourOfferByIdAsync(id);
                if (tourOffer == null)
                {
                    return NotFound();
                }
                return Ok(tourOffer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTourOffer(int id, TourOffer tourOffer)
        {
            try
            {
                if (id != tourOffer.TourOfferId)
                {
                    return BadRequest();
                }

                var updatedTourOffer = await _tourOfferRepository.UpdateTourOfferAsync(id, tourOffer);
                if (updatedTourOffer == null)
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
        public async Task<ActionResult<TourOffer>> PostTourOffer(TourOffer tourOffer)
        {
            try
            {

                var createdTourOffer = await _tourOfferRepository.AddTourOfferAsync(tourOffer);
                return CreatedAtAction(nameof(GetTourOffer), new { id = createdTourOffer.TourOfferId }, createdTourOffer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTourOffer(int id)
        {
            try
            {
                var tourOfferToDelete = await _tourOfferRepository.GetTourOfferByIdAsync(id);
                if (tourOfferToDelete == null)
                {
                    return NotFound();
                }

                await _tourOfferRepository.DeleteTourOfferAsync(tourOfferToDelete);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
