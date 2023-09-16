using Cafeteria.Application.Dtos; // Asegúrate de importar tus clases de DTO y servicios
using Cafeteria.Application.Service;
using Cafeteria.Tests.Mock;

namespace Cafeteria.Tests
{
    [TestClass]
    public class UsuarioServiceTests
    {
        [TestMethod]
        public void RegistrarUsuario_DeberiaAsignarRolUsuario()
        {
            // Arrange
            var usuarioService = new UsuarioServiceMock(); // Usar el servicio simulado

            var nuevoUsuarioDto = new UsuarioDto
            {
                Nombre = "Usuario de Prueba",
                // Otras propiedades del usuario
            };

            // Act
            var usuarioRegistrado = usuarioService.RegistrarUsuario(nuevoUsuarioDto);

            // Assert
            Assert.IsNotNull(usuarioRegistrado);
            Assert.AreEqual("Usuario", usuarioRegistrado.Rol);
        }

        [TestMethod]
        public void RegistrarUsuario_NoDeberiaAsignarRolEmpleado()
        {
            // Arrange
            var usuarioService = new UsuarioServiceMock(); // Usar el servicio simulado

            var nuevoUsuarioDto = new UsuarioDto
            {
                Nombre = "Usuario de Prueba",
                // Otras propiedades del usuario
            };

            // Act
            var usuarioRegistrado = usuarioService.RegistrarUsuario(nuevoUsuarioDto);

            // Assert
            Assert.IsNotNull(usuarioRegistrado);
            Assert.AreNotEqual("Empleado", usuarioRegistrado.Rol);
            Assert.AreNotEqual("Supervisor", usuarioRegistrado.Rol);
            Assert.AreNotEqual("Administrador", usuarioRegistrado.Rol);
        }

    }
}
