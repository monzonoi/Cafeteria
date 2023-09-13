namespace Cafeteria.WEB.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            return await _httpClient.GetFromJsonAsync<T>(endpoint);
        }

        // Agrega métodos para otras operaciones CRUD según tus necesidades
    }
}
