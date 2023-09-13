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
        Task<IEnumerable<MateriaPrima>> ObtenerTodasLasMateriasPrimasAsync();
        Task<MateriaPrima> ObtenerMateriaPrimaPorIdAsync(int materiaPrimaId);
        Task<int> CrearMateriaPrimaAsync(MateriaPrima materiaPrima);
        Task ActualizarMateriaPrimaAsync(int materiaPrimaId, MateriaPrima materiaPrima);
        Task EliminarMateriaPrimaAsync(int materiaPrimaId);
    }

}
