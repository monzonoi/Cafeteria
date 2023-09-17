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
    public class MateriaPrimaServerTest
    {
        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public async Task SoloSupervisorYAdministradorPuedenEditarMateriaPrima()
        {
            // Arrange
            var supervisor = new UsuarioDto
            {
                Nombre = "Supervisor",
                Rol = new RolDto { Nombre = "Supervisor" }
            };

            var administrador = new UsuarioDto
            {
                Nombre = "Administrador",
                Rol = new RolDto { Nombre = "Administrador" }
            };

            var empleado = new UsuarioDto
            {
                Nombre = "Empleado",
                Rol = new RolDto { Nombre = "Empleado" }
            };

            var materiaPrima = new MateriaPrimaDto()
            {
                Id = 1,
                Cantidad = 1,
                CantidadDisponible = 1,
                Nombre = "materia prima prueba"
            };

            // Mock del servicio de Materia Prima
            var materiaPrimaServiceMock = new MateriaPrimaServiceMock();

            // Act y Assert            
            await materiaPrimaServiceMock.AjustarMateriaPrimaAsync(empleado, materiaPrima, 10); // Intento de empleado  
            await materiaPrimaServiceMock.AjustarMateriaPrimaAsync(supervisor, materiaPrima, 10); // Intento de supervisor           
            await materiaPrimaServiceMock.AjustarMateriaPrimaAsync(administrador, materiaPrima, 10); // Intento de administrador           
        }

    }


}
