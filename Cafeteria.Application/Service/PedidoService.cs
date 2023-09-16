﻿using Cafeteria.Application.Dtos;
using Cafeteria.Domain;
using Cafeteria.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Cafeteria.Application.Service
{
    //public class PedidoService : IPedidoService
    //{
    //    private readonly IPedidoRepository _pedidoRepository;
    //    private readonly IMateriaPrimaRepository _materiaPrimaRepository;
    //    private readonly IComandaService _comandaService;

    //    public PedidoService(IPedidoRepository pedidoRepository, IMateriaPrimaRepository materiaPrimaRepository, IComandaService comandaService)
    //    {
    //        _pedidoRepository = pedidoRepository;
    //        _materiaPrimaRepository = materiaPrimaRepository;
    //        _comandaService = comandaService;
    //    }

    //    public void CrearPedido(Pedido pedido)
    //    {
    //        // Lógica para crear un pedido y verificar la disponibilidad de materias primas
    //    }

    //    public Pedido ObtenerPedido(int id)
    //    {
    //        // Lógica para obtener un pedido por ID
    //        return new Pedido();
    //    }

    //    // Implementa otros métodos relacionados con pedidos
    //}

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
}


}
