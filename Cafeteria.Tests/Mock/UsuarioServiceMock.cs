
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

        public UsuarioServiceMock()
        {
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
    }
}
