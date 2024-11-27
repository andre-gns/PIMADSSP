namespace PIMFazendaUrbanaRadzen.Services
{
    public class ClienteApiService<T>
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpointUrl;

        public ClienteApiService(HttpClient httpClient, string endpointUrl)
        {
            _httpClient = httpClient;
            _endpointUrl = endpointUrl;
        }

        public async Task<List<T>> GetClientesFiltradosAsync(string search)
        {
            try
            {
                Console.WriteLine($"Chamando API em: {_endpointUrl}/filtrados?search={Uri.EscapeDataString(search)}");
                return await _httpClient.GetFromJsonAsync<List<T>>($"{_endpointUrl}/filtrados?search={Uri.EscapeDataString(search)}");
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

        public async Task<List<T>> GetAllAsync()
        {
            try
            {
                Console.WriteLine($"Chamando API em: {_endpointUrl}/ativos");
                return await _httpClient.GetFromJsonAsync<List<T>>($"{_endpointUrl}/ativos");
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

        public async Task<T> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<T>($"{_endpointUrl}/{id}");
        }

        public async Task<HttpResponseMessage> CreateAsync(T entity)
        {
            return await _httpClient.PostAsJsonAsync($"{_endpointUrl}/cadastrar", entity);
        }

        public async Task<HttpResponseMessage> UpdateAsync(T entity)
        {
            return await _httpClient.PutAsJsonAsync($"{_endpointUrl}/alterar", entity);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"{_endpointUrl}/excluir/{id}");
        }


    }
}
