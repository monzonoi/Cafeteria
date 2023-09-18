using Cafeteria.Application.Dtos;
using Cafeteria.Domain;
using Cafeteria.Domain.Entidades;

namespace Cafeteria.Application.Service
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PedidoService(IPedidoRepository pedidoRepository, IUnitOfWork unitOfWork)
        {
            _pedidoRepository = pedidoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PedidoDto>> ObtenerTodosLosPedidosAsync()
        {
            var pedidos = await _pedidoRepository.ObtenerTodosAsync();
            // Mapear los pedidos a PedidoDto según sea necesario
            var pedidoDtos = pedidos.Select(MapToPedidoDto);
            return pedidoDtos;
        }

        public async Task<PedidoDto> ObtenerPedidoPorIdAsync(int pedidoId)
        {
            var pedido = await _pedidoRepository.ObtenerPorIdAsync(pedidoId);
            // Mapear el pedido a PedidoDto según sea necesario
            var pedidoDto = MapToPedidoDto(pedido);
            return pedidoDto;
        }

        public async Task<int> CrearPedidoAsync(PedidoDto pedidoDto)
        {
            // Mapear PedidoDto a una entidad Pedido si es necesario
            var pedido = MapToPedido(pedidoDto);

            await _pedidoRepository.AgregarAsync(pedido);
            await _unitOfWork.CommitAsync(); // Guardar cambios en la base de datos

            return pedido.Id; // Devuelve el ID del pedido recién creado
        }

        public async Task ActualizarPedidoAsync(int pedidoId, PedidoDto pedidoDto)
        {
            var pedidoExistente = await _pedidoRepository.ObtenerPorIdAsync(pedidoId);
            if (pedidoExistente == null)
            {
                throw new ApplicationException("El pedido no existe.");
            }

            // Actualiza las propiedades del pedido existente con los valores de PedidoDto
            // Mapea las propiedades si es necesario

            await _pedidoRepository.ActualizarAsync(pedidoExistente);
            await _unitOfWork.CommitAsync(); // Guardar cambios en la base de datos
        }

        public async Task EliminarPedidoAsync(int pedidoId)
        {
            await _pedidoRepository.EliminarAsync(pedidoId);
            await _unitOfWork.CommitAsync(); // Guardar cambios en la base de datos
        }

        // Métodos de mapeo entre entidades y DTOs si es necesario
        private PedidoDto MapToPedidoDto(Pedido pedido)
        {
            return new PedidoDto
            {
                Id = pedido.Id,
                // Mapear otras propiedades si es necesario
            };
        }

        private Pedido MapToPedido(PedidoDto pedidoDto)
        {
            return new Pedido
            {
                Id = pedidoDto.Id,
                // Mapear otras propiedades si es necesario
            };
        }

        public Task<PedidoDto> CambiarEstadoPedidoAsync(int pedidoId, string nuevoEstado)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EditarPedidoAsync(UsuarioDto usuario, PedidoDto pedido)
        {
            // Verificar si el usuario tiene permiso para editar pedidos
            if (!EsAdministrador(usuario))
            {
                throw new UnauthorizedAccessException("El usuario no tiene permiso para editar pedidos.");
            }

            // Buscar el pedido en el repositorio por su ID
            var pedidoExistente = await _pedidoRepository.ObtenerPorIdAsync(pedido.Id);

            if (pedidoExistente != null)
            {
                // Actualizar los campos del pedido existente con los valores del pedido recibido
                pedidoExistente.Estado = pedido.Estado;

                // Aquí podrías actualizar otros campos del pedido si es necesario

                // Guardar los cambios en el repositorio
                await _pedidoRepository.ActualizarAsync(pedidoExistente);

                return true; // Edición exitosa
            }

            return false; // Pedido no encontrado
        }


        #region Metodos Privados
        private bool EsAdministrador(UsuarioDto usuario)
        {
            // Verificar si el usuario tiene el rol de Administrador
            return usuario.Rol.Nombre == "Administrador";
        }
        #endregion
    }
}
