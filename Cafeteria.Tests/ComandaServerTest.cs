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


    }
}
