
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using TrainingRestFullApi.src.Middleware;

Scoped service = new();
var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();
// Service Eject
service.AddScopedService(builder.Services);
// JWT
service.AddAuthenticationJwt(builder.Services, builder.Configuration);

// DataBase Connection
Configuration config = new(builder.Configuration);
string databaseSettings = config.PostGresConnection();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(databaseSettings));

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}
app.UseMiddleware<JwtMiddleWare>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
