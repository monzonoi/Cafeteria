using System.Collections.Generic;
using System.Linq;
using Cafeteria.Application.Dtos;
using Cafeteria.Application.Service;

namespace Cafeteria.Tests.Mock
{
    public class MateriaPrimaServiceMock : IMateriaPrimaService
    {
        private readonly List<MateriaPrimaDto> _materiasPrimas;
        private readonly IDictionary<string, int> _stockMateriaPrima = new Dictionary<string, int>();


        public MateriaPrimaServiceMock()
        {
            _materiasPrimas = new List<MateriaPrimaDto>
        {
            new MateriaPrimaDto { Id = 1, Nombre = "Materia Prima 1", CantidadDisponible = 100 },
            new MateriaPrimaDto { Id = 2, Nombre = "Materia Prima 2", CantidadDisponible = 50 },
            // Agrega más elementos según tus necesidades
        };

            _stockMateriaPrima.Add("Café", 100);
        }

    public Task ActualizarMateriaPrimaAsync(int id, MateriaPrimaDto materiaPrimaDto)
        {
            throw new NotImplementedException();
        }

        public void AgregarMateriaPrima(MateriaPrimaDto materiaPrima)
        {
            _materiasPrimas.Add(materiaPrima);
        }

        public Task<int> CrearMateriaPrimaAsync(MateriaPrimaDto materiaPrimaDto)
        {
            throw new NotImplementedException();
        }

        public Task EliminarMateriaPrimaAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<MateriaPrimaDto> ObtenerMateriaPrimaPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MateriaPrimaDto>> ObtenerTodasLasMateriasPrimasAsync()
        {
            throw new NotImplementedException();
        }

        public bool ValidarStockDisponible(ItemPedidoDto itemPedido)
        {
            // Verificar si hay suficiente materia prima disponible para el pedido
            var materiaPrima = _materiasPrimas.FirstOrDefault(mp => mp.Nombre == itemPedido.NombreMateriaPrima);
            if (materiaPrima == null || materiaPrima.CantidadDisponible < itemPedido.CantidadRequerida)
            {
                return false; // No hay suficiente materia prima disponible
            }

            return true;
        }

        public async Task<int> ObtenerStockAsync(string nombre)
        {
            if (_stockMateriaPrima.TryGetValue(nombre, out int stock))
            {
                return stock;
            }

            throw new KeyNotFoundException($"No se encontró la materia prima '{nombre}' en el stock.");
        }

        public async Task ReducirStockAsync(string nombre, int cantidad)
        {
            if (_stockMateriaPrima.TryGetValue(nombre, out int stock))
            {
                if (stock >= cantidad)
                {
                    _stockMateriaPrima[nombre] -= cantidad;
                }
                else
                {
                    throw new InvalidOperationException($"Stock insuficiente de '{nombre}' para reducir en {cantidad} unidades.");
                }
            }
            else
            {
                throw new KeyNotFoundException($"No se encontró la materia prima '{nombre}' en el stock.");
            }
        }



        public async Task AjustarMateriaPrimaAsync(UsuarioDto usuario, MateriaPrimaDto materiaPrima, int cantidad)
        {
            if (usuario == null || materiaPrima == null)
            {
                throw new ArgumentNullException("Usuario y materia prima no pueden ser nulos");
            }

            // Verifica si el usuario tiene permiso para ajustar la materia prima (rol Supervisor o Administrador)
            if (!EsSupervisorOAdministrador(usuario))
            {
                throw new UnauthorizedAccessException("El usuario no tiene permiso para ajustar la materia prima");
            }

            // Busca la materia prima en la lista ficticia
            var materiaPrimaExistente = _materiasPrimas.FirstOrDefault(mp => mp.Id == materiaPrima.Id);

            if (materiaPrimaExistente == null)
            {
                throw new InvalidOperationException("La materia prima no existe");
            }

            // Realiza el ajuste de stock según la cantidad especificada
            materiaPrimaExistente.Cantidad += cantidad;
        }

        private bool EsSupervisorOAdministrador(UsuarioDto usuario)
        {
            return usuario.Rol?.Nombre == "Supervisor" || usuario.Rol?.Nombre == "Administrador";
        }
    }
}
