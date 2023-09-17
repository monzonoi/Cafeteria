using Cafeteria.Application.Dtos; // Asegúrate de importar tus clases de DTO y servicios
using Cafeteria.Application.Service;
using Cafeteria.Domain.Entidades;
using Cafeteria.Tests.Mock;
using Moq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;

namespace Cafeteria.Tests
{
    [TestClass]
    public class ComandaServerTest
    {

        [TestMethod]
        public async Task CambiarEstadoPedido_EjecutarPedidoCambiaEstadoDeComanda()
        {
            // Arrange
            var usuarioServiceMock = new Mock<IUsuarioService>();
            var materiaPrimaServiceMock = new Mock<IMateriaPrimaService>();
         

            // Configura un pedido existente y una comanda
            var pedido = new PedidoDto
            {
                Id = 1,
                Estado = "Pendiente" // Supongamos que el pedido inicialmente está pendiente
            };

            var comanda = new ComandaDto
            {
                Id = 1,
                Estado = "EnProceso" // Supongamos que la comanda está en proceso
                , Pedidos = (new PedidoDto[] { pedido }).ToList()
            };

            pedido.Comanda = comanda;
            pedido.ComandaId = comanda.Id;

            var comandaServiceMock = new ComandaServiceMock(new ComandaDto[] { comanda }.ToList(), new PedidoDto[] { pedido }.ToList());
                        

            // Act
            await comandaServiceMock.CambiarEstadoPedidoAsync(pedido, "Terminado");


            // Assert
            Assert.AreEqual("Terminado", pedido.Estado); // Verifica que el estado del pedido cambió a "Ejecutado"
            Assert.AreEqual("PendienteFacturacion", comanda.Estado);
            //Assert.AreEqual("PendienteFacturacion", comandaServiceMock._comandasEnMemoria.FirstOrDefault(s => s.Id == comanda.Id).Estado); // Verifica que el estado de la comanda cambió a "ListoParaFacturar"
        }

               

        [TestMethod]
        public async Task SupervisorPuedeVerTrabajosDeEmpleados()
        {
            // Arrange
            //var comandaService = new ComandaServiceMock(); // Utilizamos ComandaServiceMock

            // Creamos un usuario con rol "Supervisor"
            var supervisor = new UsuarioDto
            {
                Nombre = "Supervisor",
                Rol = new RolDto { Nombre = "Supervisor" } // Supongamos que "Supervisor" es un rol válido
            };

            // Creamos un usuario con rol "Empleado"
            var empleado = new UsuarioDto
            {
                Nombre = "Empleado",
                Rol = new RolDto { Nombre = "Empleado" } // Supongamos que "Empleado" es un rol válido
            };

            // Configura un pedido existente y una comanda
            var pedido = new PedidoDto
            {
                Id = 1,
                Estado = "Pendiente" // Supongamos que el pedido inicialmente está pendiente
                ,Usuario = empleado
            };

            // Creamos una comanda con un trabajo realizado por el empleado
            var comanda = new ComandaDto
            {
                Id = 1, // ID de la comanda
                Pedidos = (new PedidoDto[] { pedido }).ToList()
            };


            var comandaService = new ComandaServiceMock(new ComandaDto[] { comanda }.ToList(), new PedidoDto[] { pedido }.ToList());


            // Act
            var lstPedidos = await comandaService.ObtenerTodosAsync(supervisor);

            // Assert
            Assert.IsNotNull(lstPedidos, "La lista de pedidos no debe ser nula.");
            Assert.IsTrue(lstPedidos.Any(), "La lista de pedidos debe contener al menos un pedido.");
        }

      
        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public async Task EmpleadoNoPuedeVerTrabajosDeOtrosEmpleados()
        {
            // Arrange
            // Crear un usuario con el rol "Empleado"
            var empleado = new UsuarioDto
            {
                Nombre = "Empleado",
                Rol = new RolDto { Nombre = "Empleado" }
            };

            // Crear una lista de comandas simuladas con trabajos de varios empleados
            var comandas = new List<ComandaDto>
            {
                // Comanda con trabajos del "Empleado"
                new ComandaDto
                {
                    Id = 1,
                    Estado = "Completada",
                    Usuario = empleado,
                    Pedidos = new List<PedidoDto>
                    {
                        new PedidoDto
                        {
                            Id = 1,
                            Estado = "Completado",
                            Usuario = empleado
                        }
                    }
                },
                // Comanda con trabajos de otro "Empleado"
                new ComandaDto
                {
                    Id = 2,
                    Estado = "Completada",
                    Usuario = new UsuarioDto { Nombre = "OtroEmpleado", Rol = new RolDto { Nombre = "Empleado" } },
                    Pedidos = new List<PedidoDto>
                    {
                        new PedidoDto
                        {
                            Id = 2,
                            Estado = "Completado",
                            Usuario = new UsuarioDto { Nombre = "OtroEmpleado", Rol = new RolDto { Nombre = "Empleado" } }
                        }
                    }
                }
            };

            var todosLosPedidos = comandas.SelectMany(c => c.Pedidos).ToList();


            // Mock del servicio de Comanda
            var comandaService = new ComandaServiceMock(comandas, todosLosPedidos);


            // Act
            //var trabajosRealizados = await comandaService.ObtenerTrabajosRealizadosAsync(empleado);

            //await Assert.ThrowsExceptionAsync<UnauthorizedAccessException>(async () =>
            //{
            //    var trabajosRealizados = await comandaService.ObtenerTrabajosRealizadosAsync(empleado);
            //});

            var trabajosRealizados = await comandaService.ObtenerTrabajosRealizadosAsync(empleado);

            //// Assert
            //// Verificar que el empleado solo pueda ver sus propios trabajos
            //Assert.AreEqual(1, trabajosRealizados.Count); // Debería haber solo 1 trabajo en la lista
            //Assert.AreEqual(1, trabajosRealizados[0].Id); // El trabajo debe pertenecer al empleado
        }
        
    }
}
