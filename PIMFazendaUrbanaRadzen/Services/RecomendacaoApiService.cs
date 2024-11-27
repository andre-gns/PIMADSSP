using PIMFazendaUrbanaAPI.DTOs;
using System.Text.Json;

namespace PIMFazendaUrbanaRadzen.Services
{
    public class RecomendacaoApiService<T>
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpointUrl;

        public RecomendacaoApiService(HttpClient httpClient, string endpointUrl)
        {
            _httpClient = httpClient;
            _endpointUrl = endpointUrl;
        }

        public async Task<List<CultivoDTO>> GetRecomendacoesAsync(string regiao, string estacao, bool ambienteControlado)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_endpointUrl}/gerar?regiao={Uri.EscapeDataString(regiao)}&estacao={Uri.EscapeDataString(estacao)}&ambienteControlado={ambienteControlado}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<CultivoDTO>>();
                }
                else
                {
                    var errorResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                    // Verifica se há a propriedade "errors" na resposta
                    if (errorResponse.TryGetProperty("errors", out var errors))
                    {
                        throw new Exception(string.Join(", ", errors.EnumerateArray().Select(e => e.GetString())));
                    }

                    throw new Exception("Erro desconhecido ao chamar a API");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao chamar API: {ex.Message}");
                throw; // Re-levanta a exceção para o chamador tratar
            }
        }

    }
}
