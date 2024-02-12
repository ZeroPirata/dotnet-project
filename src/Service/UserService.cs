using Microsoft.EntityFrameworkCore;
using TrainingRestFullApi.src.DTOs;
using TrainingRestFullApi.src.Entities;
using TrainingRestFullApi.src.Interfaces;
using TrainingRestFullApi.src.Utils;
using TrainingRestFullApi.src.Records;
using TrainingRestFullApi.src.Middleware;
using static TrainingRestFullApi.src.Configuration.ServiceResponse;

namespace TrainingRestFullApi.src.Service
{
    public class UserService(ApplicationDbContext context, HashPassword password, JwtMiddleWare jwt) : IUserAccount
    {
        private readonly ApplicationDbContext _context = context;
        private readonly HashPassword _password = password;
        private readonly JwtMiddleWare _jwt = jwt;

        public async Task<GeneralResponse> CreateAccount(UserDTO userDTO)
        {
            try
            {
                    
                if (userDTO == null) return new GeneralResponse(406, "Model is empty");

                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == userDTO.Email);
                if (existingUser != null) return new GeneralResponse(400, "Email already exists");

                var salt = _password.GenerateSalt();
                string hashedPassword = _password.GeneratePassword(userDTO.Password!, salt);

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
                bool verifyPassword = _password.VerifyPassword(loginDTO.Password!, existingUser.Password!, salt);
                if (!verifyPassword) return new LoginResponse(406, null, "Invalid Email/Password");


                var userSession = new UserSession(existingUser.Id, existingUser.Name!, existingUser.NickName!, existingUser.Email!);
                await RemoveSessionUser(existingUser.Id);
                string token = _jwt.GenerateToken(userSession);
                return new LoginResponse(200, token, "Login succes");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UserService.LoginAccount: {ex.Message}");
                Console.WriteLine(ex);
                return new LoginResponse(500, null, $"Something happens. D: {ex.Message}" );
            }
        }   

        private async Task RemoveSessionUser(Guid userId)
        {
            try
            {
                var tokenExist = await _context.Sessions.FirstOrDefaultAsync( s => s.UserId == userId);
                Console.WriteLine(tokenExist);
                if (tokenExist is null) return;
                _ = _context.Sessions.Remove(tokenExist);
                _ = await _context.SaveChangesAsync();
            }catch (Exception ex)
            {
                Console.WriteLine($"Não foi possivel executar a função RemoveSessionUser: {ex}");
            }
        }
    }
}
