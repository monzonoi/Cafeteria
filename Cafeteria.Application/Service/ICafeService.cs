using System.Collections.Generic;
using Cafeteria.Application.Dtos;
using Cafeteria.Domain; // Asegúrate de importar el espacio de nombres del dominio


namespace Cafeteria.Application.Service
{
    public interface ICafeService
    {
        Task<IEnumerable<CafeDto>> ObtenerTodosLosCafesAsync();
        Task<CafeDto> ObtenerCafePorIdAsync(int cafeId);
        Task<int> CrearCafeAsync(CafeDto cafeDto);
        Task ActualizarCafeAsync(int cafeId, CafeDto cafeDto);
        Task EliminarCafeAsync(int cafeId);
    }
}
