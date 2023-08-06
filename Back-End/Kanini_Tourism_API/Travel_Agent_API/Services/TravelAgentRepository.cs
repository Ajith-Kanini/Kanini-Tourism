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
                AgentName=travelAgent.AgentName,
                AgentEmail=travelAgent.AgentEmail,
                AgentPhoneNumber=travelAgent.AgentPhoneNumber,
                AgentCity=travelAgent.AgentCity,
                AgentPassword= travelAgent.AgentPassword !=null? Encrypt(travelAgent.AgentPassword) : travelAgent.AgentPassword,
                AgentImage=fileName,
            
            });

            await _context.SaveChangesAsync(); // Use asynchronous SaveChangesAsync()

            return travelAgent;
        }


        public async Task<TravelAgent> UpdateTravelAgentAsync(int id, [FromForm] TravelAgent travelAgent, IFormFile imageFile)
        {
            if (travelAgent == null)
            {
                throw new ArgumentNullException(nameof(travelAgent));
            }

            var existingTravelAgent = await _context.TravelAgents.FindAsync(id);
            if (existingTravelAgent == null)
            {
                return null;
            }

            
            _context.Entry(existingTravelAgent).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return existingTravelAgent;
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
