using Cafeteria.Application.Dtos; // Asegúrate de importar tus clases de DTO y servicios
using Cafeteria.Application.Service;
using Cafeteria.Domain.Entidades;
using Cafeteria.Tests.Mock;

namespace Cafeteria.Tests
{
    [TestClass]
    public class UsuarioServiceTests
    {
       
        [TestMethod]
        public async Task RegistrarUsuario_RolUsuario_RegistraUsuarioConRolUsuario()
        {
            // Arrange
            var usuarioService = new UsuarioServiceMock(); // Usar el servicio simulado
            
            var nuevoUsuario = new UsuarioDto
            {             
                Nombre = "Usuario de Prueba",                
            };

            // Act
            var usuarioRegistrado = await usuarioService.RegistrarUsuarioAsync(nuevoUsuario);

            // Assert
            Assert.IsNotNull(usuarioRegistrado);
            Assert.AreEqual("usuario", usuarioRegistrado.Rol.Nombre);
        }

        [TestMethod]
        public async Task RegistrarUsuario_NoDeberiaAsignarOtroRolQueNoSeaEmpleado()
        {
            // Arrange
            var usuarioService = new UsuarioServiceMock(); // Usar el servicio simulado

            var rolUsuario = new RolDto
            {
                Id = 2,
                Nombre = "Supervisor"
            };

            var nuevoUsuario = new UsuarioDto
            {
                Nombre = "Usuario de Prueba",
                RolId = rolUsuario.Id,
                Rol = rolUsuario
            };

            // Act
            var usuarioRegistrado = await usuarioService.RegistrarUsuarioAsync(nuevoUsuario);

            // Assert
            Assert.AreEqual("usuario", usuarioRegistrado.Rol.Nombre);            
        }


        [TestMethod]
        public async Task UsuarioRolUsuario_PuedeRealizarPedidosSegunStockMateriaPrima()
        {
            // Arrange
            var usuarioService = new UsuarioServiceMock(); // Usar el servicio simulado
            var materiaPrimaService = new MateriaPrimaServiceMock(); // Servicio de materia prima simulado

            var usuario = new UsuarioDto
            {
                Id = 1,
                Nombre = "Usuario de Prueba",
                Rol = new RolDto { Nombre = "Usuario" }
            };

            // Crear materia prima disponible suficiente
            var materiaPrimaDisponible = new MateriaPrimaDto
            {
                Id = 1,
                Nombre = "Materia Prima Disponible",
                CantidadDisponible = 10 // Ajusta la cantidad según tus necesidades
            };

            // Crear materia prima insuficiente
            var materiaPrimaInsuficiente = new MateriaPrimaDto
            {
                Id = 2,
                Nombre = "Materia Prima Insuficiente",
                Cantidad = 5,
            };

            // Act
            var pedidoConMateriaPrimaDisponible = new PedidoDto
            {
                // Configurar otros atributos del pedido según sea necesario
                FechaPedido = DateTime.Now,
                UsuarioId = usuario.Id, // Supongamos que tienes el ID del usuario que realiza el pedido

                // Configurar los ítems del pedido
                Items = new List<ItemPedidoDto>
                    {
                        new ItemPedidoDto
                        {
                            Nombre = "Café Espresso",
                            Cantidad = 2, // Cantidad de ítems que se desean pedir
                            Precio = 2.5m, // Precio por ítem
                            // Otros atributos del ítem según tus necesidades
                            MateriaPrimaId = 1 // Supongamos que esta es la ID de la materia prima necesaria
                            ,MateriaPrima = materiaPrimaDisponible
                        }
                        // Puedes agregar más ítems al pedido si es necesario
                    }
            };

            var pedidoConMateriaPrimaInsuficiente = new PedidoDto
            {
                // Configurar otros atributos del pedido según sea necesario
                FechaPedido = DateTime.Now,
                UsuarioId = 1, // Supongamos que tienes el ID del usuario que realiza el pedido

                // Configurar los ítems del pedido
                Items = new List<ItemPedidoDto>
                {
                    new ItemPedidoDto
                    {
                        Nombre = "Café Irlandes",
                        Cantidad = 5, // Cantidad de ítems que se desean pedir (mayor que la cantidad disponible de materia prima)
                        Precio = 2.5m, // Precio por ítem
                        // Otros atributos del ítem según tus necesidades
                        MateriaPrimaId = 2 // Supongamos que esta es la ID de la materia prima necesaria
                        , MateriaPrima = materiaPrimaInsuficiente
                    },
                    // Puedes agregar más ítems al pedido si es necesario
                }
            };

            // Intentar realizar los pedidos
            var resultadoPedidoConMateriaPrimaDisponible = await usuarioService.RealizarPedidoAsync(usuario, pedidoConMateriaPrimaDisponible);
            var resultadoPedidoConMateriaPrimaInsuficiente = await  usuarioService.RealizarPedidoAsync(usuario, pedidoConMateriaPrimaInsuficiente);

           
            // Assert
            Assert.AreEqual("Pendiente", resultadoPedidoConMateriaPrimaDisponible.Estado);
            Assert.AreEqual("SinMateriaPrima", resultadoPedidoConMateriaPrimaInsuficiente.Estado);
        }


    }
}
