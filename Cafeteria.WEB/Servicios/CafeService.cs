using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Cafeteria.WEB.Models; // Asegúrate de tener las clases de modelo compartidas


namespace Cafeteria.WEB.Servicios
{
    //public class CafeService
    //{
    //    private readonly HttpClient httpClient;

    //    public CafeService(HttpClient httpClient)
    //    {
    //        this.httpClient = httpClient;
    //    }

    //    public async Task<List<Cafe>> GetCafesAsync()
    //    {
    //        try
    //        {
    //            return await httpClient.GetFromJsonAsync<List<Cafe>>("api/Cafe"); // Ajusta la ruta de la API según tu configuración
    //        }
    //        catch (Exception ex)
    //        {
    //            // Manejar errores aquí
    //            throw ex;
    //        }
    //    }
    //}

    public class CafeService
    {
        private readonly HttpClient _httpClient;

        public CafeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Cafe>> ObtenerCafesAsync()
        {
            var cafes = await _httpClient.GetFromJsonAsync<List<Cafe>>("api/cafe");
            return cafes;
        }

        public async Task<Cafe> ObtenerCafePorIdAsync(int id)
        {
            var cafe = await _httpClient.GetFromJsonAsync<Cafe>($"api/cafe/{id}");
            return cafe;
        }

        public async Task CrearCafeAsync(Cafe cafe)
        {
            await _httpClient.PostAsJsonAsync("api/cafe", cafe);
        }

        public async Task ActualizarCafeAsync(Cafe cafe)
        {
            await _httpClient.PutAsJsonAsync($"api/cafe/{cafe.Id}", cafe);
        }

        public async Task EliminarCafeAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/cafe/{id}");
        }
    }

}
