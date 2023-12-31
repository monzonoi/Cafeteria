﻿using Cafeteria.Application.Dtos;
using Cafeteria.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Application.Service
{
    public interface IPedidoService
    {
        Task<IEnumerable<PedidoDto>> ObtenerTodosLosPedidosAsync();
        Task<PedidoDto> ObtenerPedidoPorIdAsync(int pedidoId);
        Task<int> CrearPedidoAsync(PedidoDto pedidoDto);
        Task ActualizarPedidoAsync(int pedidoId, PedidoDto pedidoDto);
        Task EliminarPedidoAsync(int pedidoId);
        Task<PedidoDto> CambiarEstadoPedidoAsync(int pedidoId, string nuevoEstado);
        Task<bool> EditarPedidoAsync(UsuarioDto usuario, PedidoDto pedido);
    }

}
