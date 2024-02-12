using Microsoft.EntityFrameworkCore;

namespace TrainingRestFullApi.src.Configuration
{
    public class Configuration
    {
        private readonly IConfiguration _configuration;

        public Configuration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string PostGresConnection()
        {
            try
            {
                string? port = _configuration["PostgreSQL:PORT"];
                string? host = _configuration["PostgreSQL:HOST"];
                string? user = _configuration["PostgreSQL:USER"];
                string? password = _configuration["PostgreSQL:PASSWORD"];
                string? database = _configuration["PostgreSQL:DATABASE"];

                if (string.IsNullOrEmpty(port) || string.IsNullOrEmpty(host) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(database))
                {
                    throw new InvalidOperationException("Variáveis de ambiente necessárias não foram definidas corretamente.");
                }

                string connectionString = $"Host={host};Port={port};Database={database};Username={user};Password={password}";
                return connectionString;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao gerar configuração do PostgreSQL: {ex.Message}", ex);
                throw;
            }
        }
    }
}

