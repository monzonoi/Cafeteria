using Cafeteria.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Application.Service
{
    public interface IFacturacionService
    {
        Task<IEnumerable<FacturaDto>> ObtenerTodasLasFacturasAsync();
        Task<FacturaDto> ObtenerFacturaPorIdAsync(int id);
        Task<int> CrearFacturaAsync(FacturaDto facturaDto);
        Task ActualizarFacturaAsync(int id, FacturaDto facturaDto);
        Task EliminarFacturaAsync(int id);
    }
}
