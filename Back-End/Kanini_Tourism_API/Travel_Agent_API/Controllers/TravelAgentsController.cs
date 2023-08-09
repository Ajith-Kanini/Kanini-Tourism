using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelAgencyManagementAPI.DB;
using TravelAgencyManagementAPI.Models;
using TravelAgencyManagementAPI.Models.DTO;
using TravelAgencyManagementAPI.Repositories; 
namespace TravelAgencyManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelAgentsController : ControllerBase
    {
        private readonly ITravelAgentRepository _travelAgentRepository; 
        public TravelAgentsController(ITravelAgentRepository travelAgentRepository)
        {
            _travelAgentRepository = travelAgentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TravelAgent>>> GetTravelAgents()
        {
            try
            {
                var travelAgents = await _travelAgentRepository.GetTravelAgentsAsync();
                return Ok(travelAgents);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TravelAgent>> GetTravelAgent(int id)
        {
            try
            {
                var travelAgent = await _travelAgentRepository.GetTravelAgentByIdAsync(id);
                if (travelAgent == null)
                {
                    return NotFound();
                }
                return Ok(travelAgent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTravelAgent(int id, [FromForm] TravelAgent travelAgent, IFormFile imageFile)
        {
            try
            {
                if (id != travelAgent.AgentId)
                {
                    return BadRequest();
                }

                var updatedTravelAgent = await _travelAgentRepository.UpdateTravelAgentAsync(id, travelAgent,imageFile);
                if (updatedTravelAgent == null)
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
        public async Task<ActionResult<AgentDTO>> PostTravelAgent([FromForm] AgentDTO travelAgent, IFormFile imageFile)
        {
            try
            {
                if (_travelAgentRepository.GetTravelAgentsAsync() == null)
                {
                    return Problem("Entity set 'AgencyContext.TravelAgents' is null.");
                }

               

                var createdTravelAgent = await _travelAgentRepository.AddTravelAgentAsync(travelAgent,imageFile);
                return CreatedAtAction(nameof(GetTravelAgent), new { id = createdTravelAgent.AgentId }, createdTravelAgent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTravelAgent(int id)
        {
            try
            {
                var travelAgentToDelete = await _travelAgentRepository.GetTravelAgentByIdAsync(id);
                if (travelAgentToDelete == null)
                {
                    return NotFound();
                }

                await _travelAgentRepository.DeleteTravelAgentAsync(travelAgentToDelete);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("Register")]
        public async Task<AgentRegisterDTO> Register(AgentRegisterDTO user)
        {
            var addedUser = await _travelAgentRepository.Register(user);
            return addedUser;
        }

        [HttpPut("Updatestatus/{id}")]
        public async Task<IActionResult> UpdateAgentStatus(int id, StatusChangeDTO travelAgent)
        {
            try
            {
                if (id != travelAgent.AgentId)
                {
                    return BadRequest();
                }

                var updatedTravelAgent = await _travelAgentRepository.UpdateAgentStatus(id, travelAgent);
                if (updatedTravelAgent == null)
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
        
    }
}
