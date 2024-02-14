using Microsoft.EntityFrameworkCore;
using TrainingRestFullApi.src.DTOs;
using TrainingRestFullApi.src.Entities;
using TrainingRestFullApi.src.Interfaces;
using TrainingRestFullApi.src.Utils;
using TrainingRestFullApi.src.Records;
using TrainingRestFullApi.src.Middleware;
using static TrainingRestFullApi.src.Configuration.ServiceResponse;
using TrainingRestFullApi.src.DTOs.User;

namespace TrainingRestFullApi.src.Service
{
    public class UserService(ApplicationDbContext context, HashPassword password, JwtMiddleWare jwt, RolesService roleService) : IUserAccount
    {
        private readonly ApplicationDbContext _context = context;
        private readonly HashPassword _password = password;
        private readonly JwtMiddleWare _jwt = jwt;
        private readonly RolesService _rolesService = roleService;

        public async Task<GeneralResponse> CreateAccount(UserDTO userDTO)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try 
            {    
                if (userDTO == null) return new GeneralResponse(406, "Model is empty");
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == userDTO.Email);
                if (existingUser != null) return new GeneralResponse(400, "Email already exists");
                var salt = _password.GenerateSalt();
                string hashedPassword = _password.GeneratePassword(userDTO.Password!, salt);
                var userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "User");
                var newUser = new User()
                {
                    Name = userDTO.Name,
                    Email = userDTO.Email,
                    Password = hashedPassword,
                    NickName = userDTO.NickName,
                    Salt = Convert.ToBase64String(salt),
                    UserRoles = new ()
                        {
                            new (){ RoleId = userRole!.Id }
                        }
                    };
                await _context.Users.AddAsync(newUser);
                await _context.UserRoles.AddAsync(new UserRole { User = newUser, Role = userRole });
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new GeneralResponse(200, "Account created successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UserService.CreateAccount: {ex.Message}");
                await transaction.RollbackAsync();
                return new GeneralResponse(500, "An error occurred while creating the account");
            }
        }

        public async Task<LoginResponse> LoginAccount(LoginDTO loginDTO)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
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
                await transaction.CommitAsync();
                return new LoginResponse(200, token, "Login succes");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UserService.LoginAccount: {ex.Message}");
                await transaction.RollbackAsync();
                return new LoginResponse(500, null, $"Something happens. D: {ex.Message}" );
            }
        }   

        private async Task RemoveSessionUser(Guid userId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var tokenExist = await _context.Sessions.FirstOrDefaultAsync( s => s.UserId == userId);
                Console.WriteLine(tokenExist);
                if (tokenExist is null) return;
                _ = _context.Sessions.Remove(tokenExist);
                _ = await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Não foi possivel executar a função RemoveSessionUser: {ex}");
                throw;
            }
        }
    }
}
