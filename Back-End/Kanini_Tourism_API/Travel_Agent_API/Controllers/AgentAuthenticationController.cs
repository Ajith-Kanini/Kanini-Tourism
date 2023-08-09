using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using TravelAgencyManagementAPI.DB;
using TravelAgencyManagementAPI.Models.DTO;
using TravelAgencyManagementAPI.Models;

namespace TravelAgencyManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentAuthenticationController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly AgencyContext _context;
        private const string UserRole = "Agent";
        private const string AdminRole = "Admin";


        public AgentAuthenticationController(IConfiguration config, AgencyContext context)
        {
            _configuration = config;
            _context = context;
        }

        [HttpPost("Agent")]
        public async Task<IActionResult> Post(AgentLoginDTO _userData)
        {
            if (_userData != null && _userData.AgentEmail != null && _userData.AgentPassword != null)
            {
                var user = await GetAdmin(_userData.AgentEmail, Encrypt(_userData.AgentPassword));

                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),

                         new Claim("AgentEmail", user.AgentEmail),
                        new Claim("Password",user.AgentPassword),
                        new Claim(ClaimTypes.Role,UserRole),

                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                       signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }

        }
        private async Task<TravelAgent> GetAdmin(string email, string password)
        {
            return await _context.TravelAgents.FirstOrDefaultAsync(u => u.AgentEmail == email && u.AgentPassword == password);
        }

        [HttpPost("Admin")]
        public async Task<IActionResult> Post(AdminDTO _userData)
        {
            try
            {
                if (_userData != null && !string.IsNullOrEmpty(_userData.AdminEmailId) && !string.IsNullOrEmpty(_userData.AdminPassword))
                {
                    if (_userData.AdminEmailId == "admin@gmail.com" && _userData.AdminPassword == "Admin@123")
                    {
                        // Create claims details based on the user information
                        var claims = new[] {
                            new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                            new Claim("AdminEmailId", _userData.AdminEmailId),
                            new Claim("AdminPassword", _userData.AdminPassword),
                            new Claim(ClaimTypes.Role, AdminRole)
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            _configuration["Jwt:Issuer"],
                            _configuration["Jwt:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddMinutes(10),
                            signingCredentials: signIn);

                        return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                    }
                    else
                    {
                        return BadRequest("Invalid credentials");
                    }
                }
                else
                {
                    return BadRequest("Invalid data");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                Console.WriteLine($"Exception: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
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
