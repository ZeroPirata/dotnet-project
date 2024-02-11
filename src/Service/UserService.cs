using Konscious.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System.Text;
using TrainingRestFullApi.src.DTOs;
using TrainingRestFullApi.src.Entities;
using TrainingRestFullApi.src.Interfaces;
using System.Security.Cryptography;
using static TrainingRestFullApi.src.DTOs.ServiceResponse;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace TrainingRestFullApi.src.Service
{
    public class UserService : IUserAccount
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse> CreateAccount(UserDTO userDTO)
        {
            try
            {
                    
                if (userDTO == null) return new GeneralResponse(406, "Model is empty");

                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == userDTO.Email);
                if (existingUser != null) return new GeneralResponse(400, "Email already exists");

                var salt = GenerateSalt();
                string hashedPassword = HashPassword(userDTO.Password, salt);

                var newUser = new User()
                {
                    Name = userDTO.Name,
                    Email = userDTO.Email,
                    Password = hashedPassword,
                    NickName = userDTO.NickName,
                    Salt = Convert.ToBase64String(salt)
                };

                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();

                return new GeneralResponse(200, "Account created successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UserService.CreateAccount: {ex.Message}");
                return new GeneralResponse(500, "An error occurred while creating the account");
            }
        }

        public async Task<LoginResponse> LoginAccount(LoginDTO loginDTO)
        {
            try
            {
                if (loginDTO == null)
                    return new LoginResponse(406, null, "Login container is empty");

                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDTO.Email);
                if (existingUser is null)
                    return new LoginResponse(404, null, "User not found");

                byte[] salt = Convert.FromBase64String(existingUser.Salt!);
                bool verifyPassword = VerifyPassword(loginDTO.Password!, existingUser.Password!, salt);
                if (!verifyPassword) return new LoginResponse(406, null, "Invalid Email/Password");

                var userSession = new UserSession(existingUser.Id, existingUser.Name, existingUser.NickName, existingUser.Email);
                string token = GenerateToken(userSession);

                return new LoginResponse(200, token, "Login succes");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UserService.LoginAccount: {ex.Message}");
                Console.WriteLine(ex);
                return new LoginResponse(500, null, $"Something happens. D: {ex.Message}" );
            }
        }

        private string HashPassword(string? password, byte[] salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = 4,
                MemorySize = 1024 * 1024,
                Iterations = 10
            };

            byte[] hashBytes = argon2.GetBytes(64);

            string hashString = BitConverter.ToString(hashBytes).Replace("-", "");

            return hashString;
        }
        private bool VerifyPassword(string password, string storedHash, byte[] salt)
        {
            byte[] storedHashBytes = Enumerable.Range(0, storedHash.Length)
                                             .Where(x => x % 2 == 0)
                                             .Select(x => Convert.ToByte(storedHash.Substring(x, 2), 16))
                                             .ToArray();

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = 4,
                MemorySize = 1024 * 1024,
                Iterations = 10
            };
            byte[] hashBytes = argon2.GetBytes(64);
            return storedHashBytes.SequenceEqual(hashBytes);
        }

        private string GenerateToken(UserSession user)
        {
            string? secretKey = Environment.GetEnvironmentVariable("JWTScreteKey");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()!),
                new Claim(ClaimTypes.Name, user.Name!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.GivenName, user.NickName!)
            };
            var token = new JwtSecurityToken(
                issuer: Environment.GetEnvironmentVariable("Issuer"),
                audience: Environment.GetEnvironmentVariable("Audience"),
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private byte[] GenerateSalt()
        {
            // Gere um salt aleatório
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
    }
}
