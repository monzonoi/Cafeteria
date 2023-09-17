
using Cafeteria.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using Cafeteria.Application.Service;
using Cafeteria.Domain.Entidades;

namespace Cafeteria.Tests.Mock
{
    public class UsuarioServiceMock : IUsuarioService
    {
        private readonly List<UsuarioDto> _usuarios;
        private readonly IMateriaPrimaService _materiaPrimaService;
        private List<RolDto> _roles;


        public UsuarioServiceMock()
        {
            _usuarios = new List<UsuarioDto>();
            _roles = new List<RolDto>();
        }

        public UsuarioServiceMock(List<RolDto> roles)
        {
            _roles = roles;
        }

        public UsuarioServiceMock(List<UsuarioDto> usuarios)
        {
            _usuarios = usuarios;
        }

        public UsuarioServiceMock(IMateriaPrimaService materiaPrimaService)
        {
            _materiaPrimaService = materiaPrimaService;
            _usuarios = new List<UsuarioDto>();
        }

      

        public async Task<UsuarioDto> RegistrarUsuarioAsync(UsuarioDto usuarioDto)
        {
            // Simula el registro de un usuario de forma asíncrona (esto es simulado)
            await Task.Delay(100); // Simula una operación asincrónica

            // Simula el registro de un usuario
            usuarioDto.Id = 1;
            usuarioDto.Rol = new RolDto { Id = 1, Nombre = "usuario" }; // Establece el rol simulado
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

        public async Task<bool> CrearUsuarioAsync(UsuarioDto administrador, UsuarioDto nuevoUsuario)
        {
            if (administrador.Rol.Nombre == "Administrador")
            {
                // Verificar si el usuario ya existe (por ejemplo, por nombre)
                if (_usuarios.Any(u => u.Nombre == nuevoUsuario.Nombre))
                {
                    return false; // El usuario ya existe, no se puede crear
                }

                // Asignar el nuevo usuario a la lista de usuarios
                _usuarios.Add(nuevoUsuario);

                return true; // El usuario se ha creado exitosamente
            }

            return false; // El administrador no tiene permiso para crear usuarios
        }



        //public Task RealizarPedidoAsync(UsuarioDto usuario, PedidoDto pedidoConMateriaPrimaDisponible, MateriaPrimaServiceMock materiaPrimaService)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<PedidoDto> RealizarPedidoAsync(UsuarioDto usuario, PedidoDto pedido)
        {
            if (usuario == null || pedido == null)
            {
                throw new ArgumentNullException("El usuario y el pedido no pueden ser nulos");
            }

            // Verificar si el usuario tiene el rol "Usuario"
            if (!usuario.Rol.Nombre.Equals("Usuario", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("Solo los usuarios con rol 'Usuario' pueden realizar pedidos");
            }

            // Realizar validaciones adicionales, como verificar el stock de materia prima disponible
            if (!ValidarStockMateriaPrima(pedido?.Items.Select(s => s.MateriaPrima).ToList()))
            {
                //throw new InvalidOperationException("No hay suficiente materia prima disponible para este pedido");
                pedido.Estado = "SinMateriaPrima"; // Puedes establecer el estado según tu lógica de negocio

                return pedido;
            }

            // Simular la asignación de un ID al pedido y establecer el estado
            pedido.Id = GenerarIdPedido();
            pedido.Estado = "Pendiente"; // Puedes establecer el estado según tu lógica de negocio

            // Agregar el pedido a la lista de pedidos del usuario (esto es solo una simulación)
            usuario.Pedidos.Add(pedido);

            return pedido;
        }


        private bool ValidarStockMateriaPrima(List<MateriaPrimaDto> materiaPrimaNecesaria)
        {
            // Supongamos que tienes una lista de materia prima disponible en la aplicación.
            // Esta lista podría obtenerse de una base de datos u otra fuente de datos.

            List<MateriaPrimaDto> materiaPrimaDisponible = ObtenerMateriaPrimaDisponible();

            // Para cada materia prima necesaria en la lista, verifica si hay suficiente cantidad disponible.
            foreach (var materiaPrima in materiaPrimaNecesaria)
            {
                var item = materiaPrima;
                var materiaPrimaDisponibleActual = materiaPrimaDisponible.FirstOrDefault(mp => mp.Id == item.Id);

                if (materiaPrimaDisponibleActual == null || materiaPrimaDisponibleActual.CantidadDisponible < item.Cantidad)
                {
                    return false; // No hay suficiente materia prima disponible.
                }
            }

            return true; // Se dispone de suficiente materia prima para todos los ingredientes.
        }


        private List<MateriaPrimaDto> ObtenerMateriaPrimaDisponible()
        {
            // Aquí implementa la lógica para obtener la lista de materia prima disponible.
            // Esto podría involucrar consultas a una base de datos u otra fuente de datos.
            // Por simplicidad, supongamos que tienes una lista predefinida.

            var materiaPrimaDisponible = new List<MateriaPrimaDto>
            {
                new MateriaPrimaDto { Id = 1, Nombre = "Café", CantidadDisponible = 100 },
                new MateriaPrimaDto { Id = 2, Nombre = "Leche", CantidadDisponible = 1 },
                // Agrega más materia prima según tus necesidades.
            };

            return materiaPrimaDisponible;
        }


        private int GenerarIdPedido()
        {
            // Simulación: Generar un ID único para el pedido
            // En una aplicación real, esto podría obtenerse de una base de datos o generarse de otra manera.
            return new Random().Next(1, 1000);
        }

        public async Task RealizarOrdenAsync(UsuarioDto usuario, ComandaDto comanda)
        {
            // Validar que el usuario tenga el rol correcto
            if (usuario.Rol.Nombre == "Usuario")
            {
                throw new InvalidOperationException("Los usuarios no pueden cambia el estado de una orden.");
            }

            // Validar el estado de la comanda
            if (comanda.Estado != "EnProceso")
            {
                throw new InvalidOperationException("La comanda debe estar en estado 'EnProceso' para realizar una orden.");
            }

            // Realizar validaciones adicionales según tus reglas de negocio
            // ...

            // Actualizar el estado de la comanda
            comanda.Estado = "Completada";

            // Aquí puedes implementar la lógica para reducir el stock de materia prima según los ítems del pedido
            foreach (var pedido in comanda.Pedidos)
            {
                foreach (var item in pedido.Items)
                {
                    // Actualizar el stock de materia prima aquí
                    await _materiaPrimaService.ReducirStockAsync(item.Nombre, item.Cantidad);
                }
            }

            // Realizar otras acciones necesarias, como notificar al supervisor o hacer facturación
        }

        public Task<bool> ActualizarUsuarioAsync(int id, UsuarioDto usuarioDto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EditarUsuarioAsync(UsuarioDto administrador, UsuarioDto usuarioEditado)
        {
            if (administrador.Rol.Nombre == "Administrador")
            {
                // Buscar el usuario a editar en la lista por ID u otro identificador
                var usuarioExistente = _usuarios.FirstOrDefault(u => u.Id == usuarioEditado.Id);

                if (usuarioExistente != null)
                {
                    // Actualizar los datos del usuario existente con los datos del usuario editado
                    usuarioExistente.Nombre = usuarioEditado.Nombre;
                    // Aquí actualiza otras propiedades según sea necesario

                    return true; // Usuario editado exitosamente
                }

                return false; // No se encontró el usuario a editar
            }

            return false; // El administrador no tiene permiso para editar usuarios
        }

        public async Task<bool> EliminarUsuarioAsync(UsuarioDto administrador, int usuarioIdAEliminar)
        {
            if (administrador.Rol.Nombre == "Administrador")
            {
                // Buscar el usuario a eliminar en la lista por ID u otro identificador
                var usuarioAEliminar = _usuarios.FirstOrDefault(u => u.Id == usuarioIdAEliminar);

                if (usuarioAEliminar != null)
                {
                    // Eliminar el usuario de la lista
                    _usuarios.Remove(usuarioAEliminar);
                    return true; // Usuario eliminado exitosamente
                }

                return false; // No se encontró el usuario a eliminar
            }

            return false; // El administrador no tiene permiso para eliminar usuarios
        }

        public async Task<bool> CrearRolAsync(UsuarioDto administrador, RolDto nuevoRol)
        {
            if (administrador.Rol.Nombre == "Administrador")
            {
                // Verificar si ya existe un rol con el mismo nombre
                if (_roles.Any(r => r.Nombre == nuevoRol.Nombre))
                {
                    return false; // El rol ya existe
                }

                // Asignar un nuevo ID al rol (puedes hacerlo según tu lógica)
                nuevoRol.Id = _roles.Count + 1;

                // Agregar el nuevo rol a la lista
                _roles.Add(nuevoRol);
                return true; // Rol creado exitosamente
            }

            return false; // El administrador no tiene permiso para crear roles
        }

        public async Task<bool> EditarRolAsync(UsuarioDto administrador, RolDto rolEditado)
        {
            if (administrador.Rol.Nombre == "Administrador")
            {
                // Verificar si existe un rol con el mismo ID
                var rolExistente = _roles.FirstOrDefault(r => r.Id == rolEditado.Id);

                if (rolExistente == null)
                {
                    return false; // El rol no existe
                }

                // Verificar si el administrador tiene permiso para editar el rol (puedes definir tus propias reglas)
                if (!TienePermisoParaEditarRol(administrador, rolExistente))
                {
                    return false; // El administrador no tiene permiso para editar el rol
                }

                // Actualizar los datos del rol existente con los datos del rol editado
                rolExistente.Nombre = rolEditado.Nombre;
                return true; // Rol editado exitosamente
            }

            return false; // El administrador no tiene permiso para editar roles
        }

        // Esta función verifica si el administrador tiene permiso para editar el rol
        private bool TienePermisoParaEditarRol(UsuarioDto administrador, RolDto rolExistente)
        {
            // Aquí puedes definir tus propias reglas de permisos, por ejemplo,
            // permitir editar solo roles que no sean "Administrador"
            return rolExistente.Nombre != "Administrador";
        }


        public async Task<bool> EliminarRolAsync(UsuarioDto administrador, int rolIdAEliminar)
        {
            if (administrador.Rol.Nombre == "Administrador")
            {
                // Verificar si existe un rol con el ID especificado
                var rolExistente = _roles.FirstOrDefault(r => r.Id == rolIdAEliminar);

                if (rolExistente == null)
                {
                    return false; // El rol no existe
                }

                // Verificar si el administrador tiene permiso para eliminar el rol (puedes definir tus propias reglas)
                if (!TienePermisoParaEliminarRol(administrador, rolExistente))
                {
                    return false; // El administrador no tiene permiso para eliminar el rol
                }

                // Eliminar el rol existente
                _roles.Remove(rolExistente);
                return true; // Rol eliminado exitosamente
            }

            return false; // El administrador no tiene permiso para eliminar roles
        }

        // Esta función verifica si el administrador tiene permiso para eliminar el rol
        private bool TienePermisoParaEliminarRol(UsuarioDto administrador, RolDto rolExistente)
        {
            // Aquí puedes definir tus propias reglas de permisos, por ejemplo,
            // permitir eliminar solo roles que no sean "Administrador"
            return rolExistente.Nombre != "Administrador";
        }


    }
}
