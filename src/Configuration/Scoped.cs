using TrainingRestFullApi.src.Interfaces;
using TrainingRestFullApi.src.Middleware;
using TrainingRestFullApi.src.Service;
using TrainingRestFullApi.src.Utils;

namespace TrainingRestFullApi.src.Configuration
{
    public class Scoped
    {
        public void AddScopedService(IServiceCollection service) {
            service.AddScoped<JwtMiddleWare>();
            service.AddScoped<HashPassword>();
            service.AddScoped<IUserAccount, UserService>();
        }
    }
}
