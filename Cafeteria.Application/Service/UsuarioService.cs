using Cafeteria.Application.Dtos;
using Cafeteria.Domain;
using Cafeteria.Domain.Entidades;
using SendGrid.Helpers.Errors.Model;

namespace Cafeteria.Application.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioService(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UsuarioDto>> ObtenerTodosLosUsuariosAsync()
        {
            var usuarios = await _usuarioRepository.ObtenerTodosAsync();
            return usuarios.Select(u => new UsuarioDto
            {
                Id = u.Id,
                Nombre = u.Nombre,
                // Mapear otras propiedades según sea necesario
            });
        }

        public async Task<UsuarioDto> ObtenerUsuarioPorIdAsync(int id)
        {
            var usuario = await _usuarioRepository.ObtenerPorIdAsync(id);
            if (usuario == null)
            {
                throw new NotFoundException($"Usuario con ID {id} no encontrado.");
            }

            return new UsuarioDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                // Mapear otras propiedades según sea necesario
            };
        }   

        public async Task<bool> EliminarUsuarioAsync(UsuarioDto administrador, int usuarioIdAEliminar)
        {
            //falta traer logica lograda con los test

            var usuarioExistente = await _usuarioRepository.ObtenerPorIdAsync(usuarioIdAEliminar);

            if (usuarioExistente == null)
            {
                throw new NotFoundException($"Usuario con ID {usuarioIdAEliminar} no encontrado.");
            }

            _usuarioRepository.Eliminar(usuarioExistente);
            await _unitOfWork.CommitAsync();

            return true;
        }

        public async Task<UsuarioDto> RegistrarUsuarioAsync(UsuarioDto usuarioDto)
        {
            var nuevoUsuario = new Usuario
            {
                Nombre = usuarioDto.Nombre,
                Rol = new Rol { Id = 1, Nombre = "usuario" }                                                                 
            };

            await _usuarioRepository.AgregarAsync(nuevoUsuario);
            await _unitOfWork.CommitAsync();

            usuarioDto.Id = nuevoUsuario.Id;
            return usuarioDto;
        }

        public Task RealizarOrdenAsync(UsuarioDto usuario, ComandaDto comanda)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CrearUsuarioAsync(UsuarioDto administrador, UsuarioDto nuevoUsuario)
        {
            throw new NotImplementedException();
        }

        Task<bool> IUsuarioService.ActualizarUsuarioAsync(int id, UsuarioDto usuarioDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditarUsuarioAsync(UsuarioDto administrador, UsuarioDto usuarioEditado)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CrearRolAsync(UsuarioDto administrador, RolDto nuevoRol)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditarRolAsync(UsuarioDto administrador, RolDto rolEditado)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EliminarRolAsync(UsuarioDto administrador, int rolIdAEliminar)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CambiarParametroAsync(UsuarioDto usuario, ParametroDto parametro)
        {
            throw new NotImplementedException();
        }
    }
}
