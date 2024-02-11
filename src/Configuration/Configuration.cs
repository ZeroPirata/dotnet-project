using Microsoft.EntityFrameworkCore;

namespace TrainingRestFullApi.src.Configuration
{
    public class Configuration
    {
        private readonly ILogger? _logger;

        public string PostGresConnection()
        {
            try
            {
                string? port = Environment.GetEnvironmentVariable("POSTGRES_PORT");
                string? host = Environment.GetEnvironmentVariable("POSTGRES_HOST");
                string? user = Environment.GetEnvironmentVariable("POSTGRES_USER");
                string? password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
                string? database = Environment.GetEnvironmentVariable("POSTGRES_DATABASE");

                if (string.IsNullOrEmpty(port) || string.IsNullOrEmpty(host) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(database))
                {
                    throw new InvalidOperationException("Variáveis de ambiente necessárias não foram definidas corretamente.");
                }

                string connectionString = $"Host={host};Port={port};Database={database};Username={user};Password={password}";
                return connectionString;
            }
            catch (Exception ex)
            {
                _logger?.LogError($"Erro ao gerar configuração do PostgreSQL: {ex.Message}", ex);
                throw;
            }
        }
    }
}

