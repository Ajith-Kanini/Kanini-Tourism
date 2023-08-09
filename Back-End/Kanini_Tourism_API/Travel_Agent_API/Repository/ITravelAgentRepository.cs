using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelAgencyManagementAPI.Models;
using TravelAgencyManagementAPI.Models.DTO;

namespace TravelAgencyManagementAPI.Repositories
{
    public interface ITravelAgentRepository
    {
        Task<IEnumerable<TravelAgent>> GetTravelAgentsAsync();
        Task<TravelAgent> GetTravelAgentByIdAsync(int id);
        Task<AgentDTO> AddTravelAgentAsync([FromForm] AgentDTO travelAgent, IFormFile imageFile);
        Task<TravelAgent> UpdateTravelAgentAsync(int id, [FromForm] TravelAgent travelAgent, IFormFile imageFile);
        Task DeleteTravelAgentAsync(TravelAgent travelAgent);
        Task<StatusChangeDTO> UpdateAgentStatus(int id,StatusChangeDTO travelAgent);
        Task<AgentRegisterDTO> Register(AgentRegisterDTO user);
    }
}
