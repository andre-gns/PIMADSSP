using PIMFazendaUrbanaAPI.DTOs;

namespace PIMFazendaUrbanaRadzen.Services
{
    public class CepApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpointUrl;

        public CepApiService(HttpClient httpClient, string endpointUrl)
        {
            _httpClient = httpClient;
            _endpointUrl = endpointUrl;
        }

        public async Task<EnderecoDTO> GetEnderecoViaCep(string cep)
        {
            try
            {
                Console.WriteLine($"Chamando API em: {_endpointUrl}/get/{cep}");
                return await _httpClient.GetFromJsonAsync<EnderecoDTO>($"{_endpointUrl}/get?cep={Uri.EscapeDataString(cep)}");
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"Erro de requisição: {httpEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao chamar API: {ex.Message}");
                throw;
            }
        }
    }
}
