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

        [TestMethod]
        public async Task Administrador_PuedeCrearUsuarioAsync()
        {
            // Arrange
            var administrador = new UsuarioDto { Rol = new RolDto { Nombre = "Administrador" } };
            var usuarioService = new UsuarioServiceMock(); // Usar el servicio simulado

            // Act
            var resultado = await usuarioService.CrearUsuarioAsync(administrador, new UsuarioDto());

            // Assert
            Assert.IsTrue(resultado); // El administrador debe poder crear un usuario
        }

        [TestMethod]
        public async Task Administrador_PuedeEditarUsuarioAsync()
        {
            // Arrange
            var administrador = new UsuarioDto {Id = 1,  Rol = new RolDto { Nombre = "Administrador" } };

            var usuariosExistentes = new UsuarioDto[] 
            {
                new UsuarioDto 
                {
                    Id = 2,
                    Nombre = "usuario prueba",
                    Rol = new RolDto { Id = 1, Nombre = "Empleado"}
                },
            };

            var usuarioService = new UsuarioServiceMock(usuariosExistentes.ToList());

            // Act
            var resultado = await usuarioService.EditarUsuarioAsync(administrador, new UsuarioDto() { Id = 2, Nombre = "nuevo nombre"});

            // Assert
            Assert.IsTrue(resultado); // El administrador debe poder editar un usuario
        }

        [TestMethod]
        public async Task Administrador_PuedeEliminarUsuarioAsync()
        {
            // Arrange
            var administrador = new UsuarioDto { Rol = new RolDto { Nombre = "Administrador" } };

            var usuariosExistentes = new UsuarioDto[]
           {
                new UsuarioDto
                {
                    Id = 2,
                    Nombre = "usuario prueba",
                    Rol = new RolDto { Id = 1, Nombre = "Empleado"}
                },
           };

            var usuarioService = new UsuarioServiceMock(usuariosExistentes.ToList()); // Usar el servicio simulado

            // Act
            var resultado = await usuarioService.EliminarUsuarioAsync(administrador, 2); // Supongamos que el usuario a eliminar tiene ID 1

            // Assert
            Assert.IsTrue(resultado); // El administrador debe poder eliminar un usuario
        }

        [TestMethod]
        public async Task Administrador_PuedeCrearRolAsync()
        {
            // Arrange
            var administrador = new UsuarioDto { Rol = new RolDto { Nombre = "Administrador" } };
            var usuarioService = new UsuarioServiceMock(); // Usar el servicio simulado

            // Act
            var resultado = await usuarioService.CrearRolAsync(administrador, new RolDto());

            // Assert
            Assert.IsTrue(resultado); // El administrador debe poder crear un rol
        }

        [TestMethod]
        public async Task Administrador_PuedeEditarRolAsync()
        {
            // Arrange
            var administrador = new UsuarioDto { Rol = new RolDto { Nombre = "Administrador" } };
            var roles = new RolDto[] { new RolDto { Id = 1, Nombre = "Empleado" } };
            var usuarioService = new UsuarioServiceMock(roles.ToList()); // Usar el servicio simulado

            // Act
            var resultado = await usuarioService.EditarRolAsync(administrador, new RolDto() { Id = 1});

            // Assert
            Assert.IsTrue(resultado); // El administrador debe poder editar un rol
        }

        [TestMethod]
        public async Task Administrador_PuedeEliminarRolAsync()
        {
            // Arrange
            var administrador = new UsuarioDto { Rol = new RolDto { Nombre = "Administrador" } };
        
            var roles = new RolDto[] { new RolDto { Id = 1, Nombre = "Empleado" } };

            var usuarioService = new UsuarioServiceMock(roles.ToList()); // Usar el servicio simulado

            // Act
            var resultado = await usuarioService.EliminarRolAsync(administrador, 1); // Supongamos que el rol a eliminar tiene ID 1

            // Assert
            Assert.IsTrue(resultado); // El administrador debe poder eliminar un rol
        }


        [TestMethod]
        public async Task SoloAdministradorPuedeCambiarParametros()
        {
            // Arrange
            var administrador = new UsuarioDto
            {
                Nombre = "Administrador",
                Rol = new RolDto { Nombre = "Administrador" }
            };

            var usuarioService = new UsuarioServiceMock();

            // Act
            var resultado = await usuarioService.CambiarParametroAsync(administrador, new ParametroDto() { Id = 1});

            // Assert
            Assert.IsTrue(resultado); // Debería ser verdadero, ya que el administrador puede cambiar parámetros
        }

    }
}
