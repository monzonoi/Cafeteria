//using Cafeteria.Domain.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Domain
{
    public interface ICafeRepository
    {
        Task<IEnumerable<Cafe>> ObtenerTodosAsync();
        Task<Cafe> ObtenerPorIdAsync(int id);
        Task AgregarAsync(Cafe cafe);
        Task ActualizarAsync(Cafe cafe);
        void Eliminar(Cafe cafe);
    }
}
