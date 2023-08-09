// UserRepository.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Models;
using UserManagementAPI.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Security.Cryptography;
using System.Text;
using System.Security.Cryptography;
using System.Text;
using UserManagementAPI.Models.DTO;

namespace UserManagementAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserRepository(UserContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> AddUserAsync([FromForm] User user, IFormFile imageFile)
        {
                if (imageFile == null || imageFile.Length == 0)
                {
                    throw new ArgumentException("Invalid file");
                }

                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/user");
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                user.UserImage = fileName;
                user.Password= user.Password != null ? Encrypt(user.Password) : user.Password;
          
                _context.Users.Add(user);

                await _context.SaveChangesAsync(); // Use asynchronous SaveChangesAsync()

                return user;
        }

        public async Task<User> UpdateUserAsync(int id, [FromForm] User user, IFormFile imageFile)
        {
            var existinguser = await _context.Users.FindAsync(id);

            if (existinguser == null)
            {
                throw new ArgumentException("User not found");
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/user");

                if (!string.IsNullOrEmpty(existinguser.UserImage))
                {
                    var existingFilePath = Path.Combine(uploadsFolder, existinguser.UserImage);
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

                user.UserImage = fileName;
            }
            else
            {
                user.UserImage = existinguser.UserImage;
            }

            existinguser.FullName = user.FullName;
            existinguser.PhoneNumber = user.PhoneNumber;
            existinguser.Address = user.Address;
            existinguser.City = user.City;

            existinguser.UserImage = user.UserImage;
            await _context.SaveChangesAsync();

            return existinguser;
        }

        public async Task DeleteUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<UserDTO> Register(UserDTO user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Add(new User
            {
                FullName=user.FullName,
                Email=user.Email,
                Password= user.Password != null ? Encrypt(user.Password) : user.Password,
                RegistrationDate=DateTime.Now,
                UserStatus=true
            });
            await _context.SaveChangesAsync();
            return user;
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
