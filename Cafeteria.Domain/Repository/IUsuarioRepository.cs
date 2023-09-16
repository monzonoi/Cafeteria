using Cafeteria.Domain.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Domain
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> ObtenerTodosAsync();
        Task<Usuario> ObtenerPorIdAsync(int id);
        Task AgregarAsync(Usuario usuario);
        void Actualizar(Usuario usuario);
        void Eliminar(Usuario usuario);
        Task<bool> ExisteAsync(int id);
        
    }
}
