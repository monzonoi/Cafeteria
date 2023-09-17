using Cafeteria.Application.Dtos;
using Cafeteria.Domain.Entidades;
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
        Task<bool> CrearUsuarioAsync(UsuarioDto administrador, UsuarioDto nuevoUsuario);
        Task<bool> ActualizarUsuarioAsync(int id, UsuarioDto usuarioDto);
        Task<bool> EliminarUsuarioAsync(UsuarioDto administrador, int usuarioIdAEliminar);
        Task<UsuarioDto> RegistrarUsuarioAsync(UsuarioDto usuarioDto);
        Task RealizarOrdenAsync(UsuarioDto usuario, ComandaDto comanda);//Quitar este metodo de aca y llevarlo a comanda
        Task<bool> EditarUsuarioAsync(UsuarioDto administrador, UsuarioDto usuarioEditado);
        Task<bool> CrearRolAsync(UsuarioDto administrador, RolDto nuevoRol);
        Task<bool> EditarRolAsync(UsuarioDto administrador, RolDto rolEditado);
        Task<bool> EliminarRolAsync(UsuarioDto administrador, int rolIdAEliminar);
        Task<bool> CambiarParametroAsync(UsuarioDto usuario, ParametroDto parametro);
    }
}
