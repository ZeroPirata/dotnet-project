    using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TrainingRestFullApi.src.Configuration;
using TrainingRestFullApi.src.Entities;
using TrainingRestFullApi.src.Records;

namespace TrainingRestFullApi.src.Middleware
{
    public class JwtMiddleWare(ApplicationDbContext context,IConfiguration configuration) : IMiddleware
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ApplicationDbContext _context = context;

        public string GenerateToken(UserSession user)
        {
            var tokenId = Guid.NewGuid();
            string? secretKey = _configuration["Jwt:SecretKey"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()!),
                new Claim(ClaimTypes.Name, user.Name!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.GivenName, user.NickName!),
                new Claim(JwtRegisteredClaimNames.Jti, tokenId.ToString()),
            };

            Session session = new()
            {
                TokenId = tokenId,
                UserId = user.Id,
            };
            var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: userClaims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: credentials
                );
            _context.Sessions.Add(session);
            _context.SaveChanges();
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var principal = ValidateTokenAndGetClaims(token);
                if (principal != null)
                {
                    var jti = principal.FindFirstValue(JwtRegisteredClaimNames.Jti);
                    var sub = principal.FindFirstValue(ClaimTypes.NameIdentifier);
                    if (!string.IsNullOrEmpty(jti) && !string.IsNullOrEmpty(sub))
                    {
                        var userRoles = await GetUserRolesAsync(Guid.Parse(sub));
                        context.Items["Jti"] = jti;
                        context.Items["Sub"] = sub;
                        context.Items["Roles"] = userRoles;
                    }
                }
            }
            await next(context);
        }

        private async Task<List<string>> GetUserRolesAsync(Guid userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {
                var userRoles = await _context.UserRoles
                    .Where(ur => ur.UserId == userId)
                    .Select(ur => ur.Role.Name)
                    .ToListAsync();

                return userRoles;
            }

            return new List<string>();
        }


        private ClaimsPrincipal ValidateTokenAndGetClaims(string jwtToken)
        {
            string? secretKey = _configuration["Jwt:SecretKey"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(jwtToken, tokenValidationParameters, out _);
                return principal;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
