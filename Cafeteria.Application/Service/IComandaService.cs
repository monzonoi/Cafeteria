using Cafeteria.Domain.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Application.Service
{
    public interface IComandaService
    {
        Task<IEnumerable<Comanda>> ObtenerComandasAsync();
        Task<Comanda> ObtenerComandaPorIdAsync(int comandaId);
        Task<int> CrearComandaAsync(int pedidoId);
    }

}
