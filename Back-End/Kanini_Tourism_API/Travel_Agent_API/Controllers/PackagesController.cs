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
    public class PackagesController : ControllerBase
    {
        private readonly IPackageRepository _packageRepository; // Use your repository interface

        public PackagesController(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Packages>>> GetPackages()
        {
            try
            {
                var packages = await _packageRepository.GetPackagesAsync();
                return Ok(packages);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Packages>> GetPackage(int id)
        {
            try
            {
                var package = await _packageRepository.GetPackageByIdAsync(id);
                if (package == null)
                {
                    return NotFound();
                }
                return Ok(package);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPackage(int id, Packages packages)
        {
            try
            {
                if (id != packages.PackageId)
                {
                    return BadRequest();
                }

                var updatedPackage = await _packageRepository.UpdatePackageAsync(id, packages);
                if (updatedPackage == null)
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
        public async Task<ActionResult<Packages>> PostPackage([FromForm] Packages packages, IFormFile imageFile)
        {
            try
            {
              
                var createdPackage = await _packageRepository.AddPackageAsync(packages,imageFile);
                return CreatedAtAction(nameof(GetPackage), new { id = createdPackage.PackageId }, createdPackage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackage(int id)
        {
            try
            {
                var packageToDelete = await _packageRepository.GetPackageByIdAsync(id);
                if (packageToDelete == null)
                {
                    return NotFound();
                }

                await _packageRepository.DeletePackageAsync(packageToDelete);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
