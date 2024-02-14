using Microsoft.EntityFrameworkCore;
using TrainingRestFullApi.src.Entities;

namespace TrainingRestFullApi.src.Service
{
    public class RolesService(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _context.Roles.AnyAsync(r => r.Name == roleName);
        }

        public async Task<bool> CreateRoleAsync(string roleName)
        {
            if (await RoleExistsAsync(roleName))
            {
                return false;
            }
            _context.Roles.Add(new Role { Name = roleName });
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
