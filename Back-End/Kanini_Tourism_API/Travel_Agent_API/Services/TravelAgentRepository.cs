using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelAgencyManagementAPI.Models;
using TravelAgencyManagementAPI.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Security.Cryptography.Xml;
using TravelAgencyManagementAPI.Models.DTO;
using System.Security.Cryptography;
using System.Text;
using System.Numerics;

namespace TravelAgencyManagementAPI.Repositories
{
    public class TravelAgentRepository : ITravelAgentRepository
    {
        private readonly AgencyContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TravelAgentRepository(AgencyContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IEnumerable<TravelAgent>> GetTravelAgentsAsync()
        {
            return await _context.TravelAgents.ToListAsync();
        }

        public async Task<TravelAgent> GetTravelAgentByIdAsync(int id)
        {
            return await _context.TravelAgents.FindAsync(id);
        }

        public async Task<AgentDTO> AddTravelAgentAsync([FromForm] AgentDTO travelAgent, IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/agent");
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            travelAgent.AgentImage = fileName;




            _context.TravelAgents.Add(new TravelAgent
            {
                AgentName = travelAgent.AgentName,
                AgentEmail = travelAgent.AgentEmail,
                AgentPhoneNumber = travelAgent.AgentPhoneNumber,
                AgentCity = travelAgent.AgentCity,
                AgentPassword = travelAgent.AgentPassword != null ? Encrypt(travelAgent.AgentPassword) : travelAgent.AgentPassword,
                AgentImage = fileName,
                AgentRegistrationDate = DateTime.Now.ToString(),

            }) ;

            await _context.SaveChangesAsync(); // Use asynchronous SaveChangesAsync()

            return travelAgent;
        }


        public async Task<TravelAgent> UpdateTravelAgentAsync(int id, [FromForm] TravelAgent travelAgent, IFormFile imageFile)
        {
            var existinguser = await _context.TravelAgents.FindAsync(id);

            if (existinguser == null)
            {
                throw new ArgumentException("travelAgent not found");
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/agent");

                if (!string.IsNullOrEmpty(existinguser.AgentImage))
                {
                    var existingFilePath = Path.Combine(uploadsFolder, existinguser.AgentImage);
                    if (File.Exists(existingFilePath))
                    {
                        File.Delete(existingFilePath);
                    }
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                travelAgent.AgentImage = fileName;
            }
            else
            {
                travelAgent.AgentImage = existinguser.AgentImage;
            }

            existinguser.AgentName = travelAgent.AgentName;
            existinguser.AgentPhoneNumber = travelAgent.AgentPhoneNumber;
            existinguser.AgentAddress = travelAgent.AgentAddress;
            existinguser.AgentCity = travelAgent.AgentCity;
            existinguser.AgentEmail = travelAgent.AgentEmail;
            existinguser.AgentPassword= travelAgent.AgentPassword;
            existinguser.AgentImage = travelAgent.AgentImage;
            existinguser.AgentStatus= travelAgent.AgentStatus;
            await _context.SaveChangesAsync();

            return existinguser;
        }

        public async Task DeleteTravelAgentAsync(TravelAgent travelAgent)
        {
            if (travelAgent == null)
            {
                throw new ArgumentNullException(nameof(travelAgent));
            }

            _context.TravelAgents.Remove(travelAgent);
            await _context.SaveChangesAsync();
        }
        public async Task<AgentRegisterDTO> Register(AgentRegisterDTO travelAgent)
        {
            if (travelAgent == null)
            {
                throw new ArgumentNullException(nameof(travelAgent));
            }

            _context.TravelAgents.Add(new TravelAgent
            {
                AgentName = travelAgent.AgentName,
                AgentEmail = travelAgent.AgentEmail,
                AgentPassword = travelAgent.AgentPassword != null ? Encrypt(travelAgent.AgentPassword) : travelAgent.AgentPassword,
                AgentRegistrationDate = DateTime.Now.ToString(),
                AgentStatus = false
            });
            await _context.SaveChangesAsync();
            return travelAgent;
        }
        public async Task<StatusChangeDTO> UpdateAgentStatus(int id,StatusChangeDTO travelAgent)
        {
            var agent = await _context.TravelAgents.FirstOrDefaultAsync(x => x.AgentId == id);
            agent.AgentStatus = travelAgent.AgentStatus;
            await _context.SaveChangesAsync();
            return  travelAgent;
        }

        private string Encrypt(string password)
        {
            // Example key and IV generation using hashing
            string passphrase = "your-passphrase";

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(passphrase));
                byte[] iv = sha256.ComputeHash(Encoding.UTF8.GetBytes(passphrase)).Take(16).ToArray();

                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;

                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter writer = new StreamWriter(cryptoStream))
                            {
                                writer.Write(password);
                            }
                        }

                        byte[] encryptedData = memoryStream.ToArray();
                        return Convert.ToBase64String(encryptedData);
                    }
                }
            }
        }
        }
}
