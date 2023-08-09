using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelAgencyManagementAPI.Models;
using TravelAgencyManagementAPI.DB;
using Microsoft.AspNetCore.Mvc;

namespace TravelAgencyManagementAPI.Repositories
{
    public class PackageRepository : IPackageRepository
    {
        private readonly AgencyContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PackageRepository(AgencyContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IEnumerable<Packages>> GetPackagesAsync()
        {
            return await _context.Packages.Include(x => x.HotelIds).Include(x => x.TouristAttractionIds).ToListAsync();
        }

        public async Task<Packages> GetPackageByIdAsync(int id)
        {
            return await _context.Packages.FindAsync(id);
        }

        public async Task<Packages> AddPackageAsync([FromForm] Packages packages, IFormFile imageFile) 
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/packages");
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }
            packages.PackageImage = fileName;
            await _context.AddAsync(packages);

            await _context.SaveChangesAsync(); 

            return packages;
        }

        public async Task<Packages> UpdatePackageAsync(int id, Packages packages)
        {
            if (packages == null)
            {
                throw new ArgumentNullException(nameof(packages));
            }

            var existingPackage = await _context.Packages.FindAsync(id);
            if (existingPackage == null)
            {
                return null;
            }

           

            _context.Entry(existingPackage).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return existingPackage;
        }

        public async Task DeletePackageAsync(Packages packages)
        {
            if (packages == null)
            {
                throw new ArgumentNullException(nameof(packages));
            }

            _context.Packages.Remove(packages);
            await _context.SaveChangesAsync();
        }
    }
}
