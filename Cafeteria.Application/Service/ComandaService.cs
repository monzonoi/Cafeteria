using Cafeteria.Domain.Entidad;
using Cafeteria.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Comanda>> ObtenerComandasAsync()
        {
            var comandas = await _comandaRepository.ObtenerTodasAsync();
            // Mapear las comandas a ComandaDto según sea necesario
           // var comandaDtos = comandas.Select(MapToComandaDto);
            return comandas;
        }

        public async Task<Comanda> ObtenerComandaPorIdAsync(int comandaId)
        {
            var comanda = await _comandaRepository.ObtenerPorIdAsync(comandaId);
            // Mapear la comanda a ComandaDto según sea necesario
            //var comandaDto = MapToComandaDto(comanda);
            return comanda;
        }

        public async Task<int> CrearComandaAsync(int pedidoId)
        {
            // Validar que el pedido existe y está en el estado adecuado para crear una comanda
            var pedido = await _pedidoRepository.ObtenerPorIdAsync(pedidoId);
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

            return comanda.Id; // Devuelve el ID de la comanda recién creada
        }

        //// Métodos de mapeo entre entidades y DTOs si es necesario
        //private Comanda MapToComandaDto(Comanda comanda)
        //{
        //    return new ComandaDto
        //    {
        //        Id = comanda.Id,
        //        PedidoId = comanda.PedidoId,
        //        FechaCreacion = comanda.FechaCreacion
        //        // Mapear otras propiedades si es necesario
        //    };
        //}
    }
}
