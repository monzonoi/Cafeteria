using Cafeteria.Application.Dtos;
using Cafeteria.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Application.Service
{
    public interface IComandaService
    {
        Task CambiarEstadoComandaAsync(int comandaId, string nuevoEstado);
        Task<IEnumerable<ComandaDto>> ObtenerTodasLasComandasAsync();
        Task<ComandaDto> ObtenerComandaPorIdAsync(int id);
        Task<int> CrearComandaAsync(ComandaDto comandaDto);
        Task ActualizarComandaAsync(int id, ComandaDto comandaDto);
        Task EliminarComandaAsync(int id);

        
        Task<ComandaDto> ObtenerComandaAsync(int comandaId);
        Task<IEnumerable<ComandaDto>> ObtenerComandasPorUsuarioAsync(int usuarioId);
        Task<IEnumerable<PedidoDto>> ObtenerPedidosPorComandaAsync(int comandaId);
        Task<ComandaDto> AgregarPedidoAsync(int comandaId, PedidoDto pedido, UsuarioDto usuario);
        Task<ComandaDto> CambiarEstadoComandaAsync(int comandaId, string nuevoEstado, UsuarioDto usuario);
        Task<ComandaDto> FacturarComandaAsync(int comandaId, UsuarioDto supervisor);

        Task<List<PedidoDto>> ObtenerTodosAsync(UsuarioDto usuario);
        Task<List<PedidoDto>> ObtenerTrabajosRealizadosAsync(UsuarioDto empleado);
        Task<bool> EditarComandaAsync(UsuarioDto usuario, ComandaDto comanda);
        Task CambiarEstadoPedidoAsync(PedidoDto pedido, string nuevoEstado);
    }

}
