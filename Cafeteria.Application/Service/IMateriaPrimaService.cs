using Cafeteria.Application.Dtos;
using Cafeteria.Domain.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Application.Service
{
    public interface IMateriaPrimaService
    {
        Task<IEnumerable<MateriaPrimaDto>> ObtenerTodasLasMateriasPrimasAsync();
        Task<MateriaPrimaDto> ObtenerMateriaPrimaPorIdAsync(int id);
        Task<int> CrearMateriaPrimaAsync(MateriaPrimaDto materiaPrimaDto);
        Task ActualizarMateriaPrimaAsync(int id, MateriaPrimaDto materiaPrimaDto);
        Task EliminarMateriaPrimaAsync(int id);
    }
}
