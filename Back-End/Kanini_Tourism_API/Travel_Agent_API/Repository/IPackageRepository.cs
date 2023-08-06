using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelAgencyManagementAPI.Models;

namespace TravelAgencyManagementAPI.Repositories
{
    public interface IPackageRepository
    {
        Task<IEnumerable<Packages>> GetPackagesAsync();
        Task<Packages> GetPackageByIdAsync(int id);
        Task<Packages> AddPackageAsync( [FromForm] Packages packages, IFormFile imageFile);
        Task<Packages> UpdatePackageAsync(int id, Packages packages);
        Task DeletePackageAsync(Packages packages);
    }
}
