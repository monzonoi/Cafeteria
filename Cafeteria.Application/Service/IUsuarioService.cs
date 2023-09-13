using Cafeteria.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Application.Service
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDto>> ObtenerTodosLosUsuariosAsync();
        Task<UsuarioDto> ObtenerUsuarioPorIdAsync(int id);
        Task<int> CrearUsuarioAsync(UsuarioDto usuarioDto);
        Task ActualizarUsuarioAsync(int id, UsuarioDto usuarioDto);
        Task EliminarUsuarioAsync(int id);
    }
}
