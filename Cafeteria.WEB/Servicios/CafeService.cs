using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Cafeteria.WEB.Modelos; // Asegúrate de tener las clases de modelo compartidas


namespace Cafeteria.WEB.Servicios
{
    public class CafeService
    {
        private readonly HttpClient httpClient;

        public CafeService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<Cafe>> GetCafesAsync()
        {
            try
            {
                return await httpClient.GetFromJsonAsync<List<Cafe>>("api/Cafe"); // Ajusta la ruta de la API según tu configuración
            }
            catch (Exception ex)
            {
                // Manejar errores aquí
                throw ex;
            }
        }
    }
}
