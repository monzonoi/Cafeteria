using Cafeteria.Application.Dtos;
using Cafeteria.Domain;
using Cafeteria.Domain.Entidades;
using SendGrid.Helpers.Errors.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<Usuario> CrearUsuarioAsync(UsuarioDto usuarioDto)
        {
            var nuevoUsuario = new Usuario
            {
                Nombre = usuarioDto.Nombre,
                Rol = new Rol { Nombre = usuarioDto.Rol.Nombre, Id = usuarioDto.RolId },
                // Mapear otras propiedades desde el DTO
            };

            await _usuarioRepository.AgregarAsync(nuevoUsuario);
            await _unitOfWork.CommitAsync();
            return nuevoUsuario;
        }

        public async Task ActualizarUsuarioAsync(int id, UsuarioDto usuarioDto)
        {
            var usuarioExistente = await _usuarioRepository.ObtenerPorIdAsync(id);

            if (usuarioExistente == null)
            {
                throw new NotFoundException($"Usuario con ID {id} no encontrado.");
            }

            usuarioExistente.Nombre = usuarioDto.Nombre;
            // Actualizar otras propiedades desde el DTO según sea necesario

            _usuarioRepository.Actualizar(usuarioExistente);
            await _unitOfWork.CommitAsync();
        }

        public async Task EliminarUsuarioAsync(int id)
        {
            var usuarioExistente = await _usuarioRepository.ObtenerPorIdAsync(id);

            if (usuarioExistente == null)
            {
                throw new NotFoundException($"Usuario con ID {id} no encontrado.");
            }

            _usuarioRepository.Eliminar(usuarioExistente);
            await _unitOfWork.CommitAsync();
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
    }
}
