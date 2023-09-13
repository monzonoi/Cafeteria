using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Domain
{
    public interface IFacturaRepository
    {
        Task<IEnumerable<Factura>> ObtenerTodasAsync();
        Task<Factura> ObtenerPorIdAsync(int id);
        Task AgregarAsync(Factura factura);
        void Actualizar(Factura factura);
        void Eliminar(Factura factura);
    }
}
