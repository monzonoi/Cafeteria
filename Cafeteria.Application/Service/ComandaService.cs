using Cafeteria.Domain.Entidades;
using Cafeteria.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cafeteria.Application.Dtos;
using SendGrid.Helpers.Errors.Model;


namespace Cafeteria.Application.Service
{
    public class ComandaService : IComandaService
    {
        private readonly IComandaRepository _comandaRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ComandaService(IComandaRepository comandaRepository, IPedidoRepository pedidoRepository,
                           IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
        {
            _comandaRepository = comandaRepository;
            _pedidoRepository = pedidoRepository;
            _usuarioRepository = usuarioRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task CambiarEstadoComandaAsync(int comandaId, string nuevoEstado)
        {
            var comanda = await _comandaRepository.ObtenerPorIdAsync(comandaId);

            if (comanda == null)
            {
                throw new NotFoundException("La comanda no fue encontrada.");
            }

            // Realiza validaciones adicionales según tus reglas de negocio, por ejemplo, si un usuario tiene permiso para cambiar el estado.

            // Aplica el cambio de estado.
            comanda.Estado = nuevoEstado;

            await _comandaRepository.ActualizarAsync(comanda);
            // Guarda los cambios en el repositorio.
            await _comandaRepository.ActualizarAsync(comanda);
        }

        public async Task<IEnumerable<ComandaDto>> ObtenerTodasLasComandasAsync()
        {
            var comandas = await _comandaRepository.ObtenerTodasAsync();
            // Mapear las comandas a ComandaDto según sea necesario
            var comandaDtos = comandas.Select(MapToComandaDto);
            return comandaDtos;
        }

        public async Task<ComandaDto> ObtenerComandaPorIdAsync(int comandaId)
        {
            var comanda = await _comandaRepository.ObtenerPorIdAsync(comandaId);
            // Mapear la comanda a ComandaDto según sea necesario
            var comandaDto = MapToComandaDto(comanda);
            return comandaDto;
        }

        public async Task<int> CrearComandaAsync(ComandaDto comandaDto)
        {
            // Validar que el pedido existe y está en el estado adecuado para crear una comanda
            var pedido = await _pedidoRepository.ObtenerPorIdAsync(comandaDto.Pedidos.Select(s => s.Id).FirstOrDefault());
            if (pedido == null || pedido.Estado != "pendiente")
            {
                throw new ApplicationException("El pedido no es válido para crear una comanda.");
            }

            // Crea la comanda
            var comanda = new Comanda
            {
                Pedidos = (new Pedido[] { new Pedido { Id = pedido.Id} }).ToList(),
                FechaCreacion = DateTime.Now
                // Otras propiedades de la comanda si es necesario
            };

            await _comandaRepository.AgregarAsync(comanda);
            await _unitOfWork.CommitAsync(); // Guardar cambios en la base de datos

            return comanda.Id; // Devuelve el ID de la
        }

        // Métodos de mapeo entre entidades y DTOs si es necesario
        private ComandaDto MapToComandaDto(Comanda comanda)
        {
            return new ComandaDto
            {
                Id = comanda.Id,
                //Pedidos = comanda.Pedidos.Select(s => s.Id).ToList(),
                FechaCreacion = comanda.FechaCreacion
                // Mapear otras propiedades si es necesario
            };
        }

        public async Task ActualizarComandaAsync(int id, ComandaDto comandaDto)
        {
            var comandaExistente = await _comandaRepository.ObtenerPorIdAsync(id);

            if (comandaExistente == null)
            {
                throw new NotFoundException($"Comanda con ID {id} no encontrada.");
            }

            // comandaDto.ActualizarComanda(comandaExistente); // Método para actualizar desde DTO
            await _comandaRepository.ActualizarAsync(comandaExistente);
            await _unitOfWork.CommitAsync();
        }

        public async Task EliminarComandaAsync(int id)
        {
            var comandaExistente = await _comandaRepository.ObtenerPorIdAsync(id);
            if (comandaExistente == null)
            {
                throw new NotFoundException("El café no existe.");
            }

            _comandaRepository.EliminarAsync(comandaExistente.Id);
            await _unitOfWork.CommitAsync(); // Guardar cambios en la base de datos
        }


        public async Task<Comanda> CrearComandaPorUsuarioAsync(int usuarioId)
        {
            var usuario = await _usuarioRepository.ObtenerPorIdAsync(usuarioId);

            if (usuario == null)
            {
                throw new Exception("El usuario no existe o no tiene permiso para crear una comanda.");
            }

            var comanda = new Comanda
            {
                Id = usuario.Id,
                Estado = "borrador", // La comanda se crea en estado "Borrador" inicialmente
                FechaCreacion = DateTime.Now
            };

            await _comandaRepository.AgregarAsync(comanda);
            await _unitOfWork.CommitAsync();

            return comanda;
        }


        public async Task<Pedido> AgregarPedidoAComandaPorUsuarioAsync(int comandaId, PedidoDto pedidoDto, int usuarioId)
        {
            var usuario = await _usuarioRepository.ObtenerPorIdAsync(usuarioId);
            var comanda = await _comandaRepository.ObtenerPorIdAsync(comandaId);

            //if (usuario == null || comanda == null || usuario.Rol != Rol.Usuario || comanda.Estado != EstadoComanda.Borrador)
            //{
            //    throw new Exception("No se puede agregar un pedido a la comanda.");
            //}

            // Validar que el usuario solo pueda agregar pedidos de ciertos ítems disponibles
            // Implementar lógica aquí para verificar la disponibilidad de materias primas y precios

            var pedido = new Pedido
            {
                ComandaId = comanda.Id,
                //Nombre = pedidoDto.Nombre,
                //Precio = pedidoDto.Precio,
                // Agregar más propiedades según corresponda
            };

            await _pedidoRepository.AgregarAsync(pedido);
            await _unitOfWork.CommitAsync();

            return pedido;
        }

        public Task<ComandaDto> ObtenerComandaAsync(int comandaId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ComandaDto>> ObtenerComandasPorUsuarioAsync(int usuarioId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PedidoDto>> ObtenerPedidosPorComandaAsync(int comandaId)
        {
            throw new NotImplementedException();
        }

        public Task<ComandaDto> AgregarPedidoAsync(int comandaId, PedidoDto pedido, UsuarioDto usuario)
        {
            throw new NotImplementedException();
        }

        public Task<ComandaDto> CambiarEstadoComandaAsync(int comandaId, string nuevoEstado, UsuarioDto usuario)
        {
            throw new NotImplementedException();
        }

        public Task<ComandaDto> FacturarComandaAsync(int comandaId, UsuarioDto supervisor)
        {
            throw new NotImplementedException();
        }

        public Task<List<PedidoDto>> ObtenerTodosAsync(UsuarioDto usuario)
        {
            throw new NotImplementedException();
        }

        public Task<List<PedidoDto>> ObtenerTrabajosRealizadosAsync(UsuarioDto empleado)
        {
            throw new NotImplementedException();
        }


        /*
         
         public async Task ActualizarCafeAsync(int cafeId, CafeDto cafeDto)
        {
            var cafeExistente = await _cafeRepository.ObtenerPorIdAsync(cafeId);
            if (cafeExistente == null)
            {
                throw new NotFoundException("El café no existe.");
            }

            // Actualiza las propiedades del café existente con los valores de CafeDto
            // Mapea las propiedades si es necesario

            await _cafeRepository.ActualizarAsync(cafeExistente);
            await _unitOfWork.CommitAsync(); // Guardar cambios en la base de datos
        }

        public async Task EliminarCafeAsync(int cafeId)
        {
            var cafeExistente = await _cafeRepository.ObtenerPorIdAsync(cafeId);
            if (cafeExistente == null)
            {
                throw new NotFoundException("El café no existe.");
            }

            _cafeRepository.Eliminar(cafeExistente);
            await _unitOfWork.CommitAsync(); // Guardar cambios en la base de datos
        }
         */

    }
}
