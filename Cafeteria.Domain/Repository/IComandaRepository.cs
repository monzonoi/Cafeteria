﻿using Cafeteria.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Domain
{
    public interface IComandaRepository
    {
        Task<IEnumerable<Comanda>> ObtenerTodasAsync();
        Task<Comanda> ObtenerPorIdAsync(int comandaId);
        Task AgregarAsync(Comanda comanda);
        Task ActualizarAsync(Comanda comanda);
        Task EliminarAsync(int comandaId);
        Task<List<Comanda>> ObtenerComandasPorUsuarioAsync(int id);    
        Task<IEnumerable<Pedido>> ObtenerComandasNoCompletadasAsync();
    }

}
