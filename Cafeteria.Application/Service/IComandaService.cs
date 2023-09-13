using Cafeteria.Application.Dtos;
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
        Task CambiarEstadoComandaAsync(int comandaId, EstadoComanda nuevoEstado);
        Task<IEnumerable<ComandaDto>> ObtenerTodasLasComandasAsync();
        Task<ComandaDto> ObtenerComandaPorIdAsync(int id);
        Task<int> CrearComandaAsync(ComandaDto comandaDto);
        Task ActualizarComandaAsync(int id, ComandaDto comandaDto);
        Task EliminarComandaAsync(int id);
    }

}
