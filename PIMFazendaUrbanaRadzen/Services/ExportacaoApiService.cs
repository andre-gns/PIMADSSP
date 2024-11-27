using PIMFazendaUrbanaAPI.DTOs;

namespace PIMFazendaUrbanaRadzen.Services
{
    public class ExportacaoApiService<T>
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpointUrl;

        public ExportacaoApiService(HttpClient httpClient, string endpointUrl)
        {
            _httpClient = httpClient;
            _endpointUrl = endpointUrl;
        }

        public async Task<byte[]> ExportarAsync(IEnumerable<T> dados, string formato, string nomeArquivo)
        {
            try
            {
                var request = new ExportacaoRequestDTO
                {
                    Dados = dados.Cast<object>().ToList(),
                    Formato = formato,
                    NomeArquivo = nomeArquivo
                };

                var response = await _httpClient.PostAsJsonAsync($"{_endpointUrl}/exportar", request);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsByteArrayAsync();
                }

                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao exportar: {errorMessage}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha na comunicação com a API de exportação: {ex.Message}");
            }
        }
    }
}
