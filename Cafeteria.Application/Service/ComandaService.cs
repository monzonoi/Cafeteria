using Cafeteria.Domain.Entidad;
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
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IComandaRepository _comandaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ComandaService(IPedidoRepository pedidoRepository, IComandaRepository comandaRepository, IUnitOfWork unitOfWork)
        {
            _pedidoRepository = pedidoRepository;
            _comandaRepository = comandaRepository;
            _unitOfWork = unitOfWork;
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
            var pedido = await _pedidoRepository.ObtenerPorIdAsync(comandaDto.PedidoId);
            if (pedido == null || pedido.Estado != "Pendiente")
            {
                throw new ApplicationException("El pedido no es válido para crear una comanda.");
            }

            // Crea la comanda
            var comanda = new Comanda
            {
                PedidoId = pedido.Id,
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
                PedidoId = comanda.PedidoId,
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
