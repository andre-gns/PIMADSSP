using Microsoft.Extensions.Configuration;

namespace PIMFazendaUrbanaLib
{
    internal class ConnectionString
    {
        private static IConfiguration _configuration;

        public static void SetConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string GetConnectionString()
        {
            // Verifica se a configuração foi inicializada
            if (_configuration == null)
            {
                // Carregar a configuração do ConnectionString.json
                var builder = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory) // Define o caminho base para o arquivo JSON
                    .AddJsonFile("ConnectionString.json", optional: false, reloadOnChange: true);

                _configuration = builder.Build();
            }

            return _configuration.GetConnectionString("DefaultConnection");
        }
    }
}
