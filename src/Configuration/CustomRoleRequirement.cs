using Microsoft.AspNetCore.Authorization;

namespace TrainingRestFullApi.src.Configuration
{
    public class CustomRoleRequirement(string role) : IAuthorizationRequirement
    {
        public string Role { get; } = role;
    }
}
