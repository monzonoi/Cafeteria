using System.Collections.Generic;
using System.Linq;
using Cafeteria.Application.Dtos;
using Cafeteria.Application.Service;

namespace Cafeteria.Tests.Mock
{
    public class MateriaPrimaServiceMock : IMateriaPrimaService
    {
        private readonly List<MateriaPrimaDto> _materiasPrimas;

        public MateriaPrimaServiceMock()
        {
            _materiasPrimas = new List<MateriaPrimaDto>();
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

        // Otros métodos y lógica relacionada con la materia prima según tus necesidades
    }
}
