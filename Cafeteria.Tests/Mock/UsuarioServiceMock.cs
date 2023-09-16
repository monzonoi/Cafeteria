
using Cafeteria.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using Cafeteria.Application.Service;
using Cafeteria.Domain.Entidad;

namespace Cafeteria.Tests.Mock
{
    public class UsuarioServiceMock : IUsuarioService
    {
        private readonly List<UsuarioDto> _usuarios;

        public UsuarioServiceMock()
        {
            _usuarios = new List<UsuarioDto>();
        }

        public UsuarioDto RegistrarUsuario(UsuarioDto usuarioDto)
        {
            // Simula el registro de un usuario
            usuarioDto.Id = 1;
            usuarioDto.Rol = "Usuario"; // Establece el rol simulado
            _usuarios.Add(usuarioDto); // Agrega el usuario simulado a la lista
            return usuarioDto;
        }

        public UsuarioDto ObtenerUsuarioPorId(int id)
        {
            return _usuarios.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<UsuarioDto> ObtenerTodosLosUsuarios()
        {
            return _usuarios.AsEnumerable();
        }

        public Task<IEnumerable<UsuarioDto>> ObtenerTodosLosUsuariosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioDto> ObtenerUsuarioPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> CrearUsuarioAsync(UsuarioDto usuarioDto)
        {
            throw new NotImplementedException();
        }

        public Task ActualizarUsuarioAsync(int id, UsuarioDto usuarioDto)
        {
            throw new NotImplementedException();
        }

        public Task EliminarUsuarioAsync(int id)
        {
            throw new NotImplementedException();
        }

        // Otros métodos de la interfaz IUsuarioService
    }
}
