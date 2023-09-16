using Cafeteria.Domain.Entidades;
using Cafeteria.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Intraestructura.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PedidoRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Pedido> ObtenerPorIdAsync(int pedidoId)
        {
            return await _dbContext.Pedidos
                //.Include(p => p.Items) // Incluir ítems de pedido
                .SingleOrDefaultAsync(p => p.Id == pedidoId);
        }

        public async Task<IEnumerable<Pedido>> ObtenerTodosAsync()
        {
            return await _dbContext.Pedidos
                //.Include(p => p.Items) // Incluir ítems de pedido
                .ToListAsync();
        }

        public async Task AgregarAsync(Pedido pedido)
        {
            _dbContext.Pedidos.Add(pedido);
            await _dbContext.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Pedido pedido)
        {
            _dbContext.Entry(pedido).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task EliminarAsync(int pedidoId)
        {
            var pedido = await _dbContext.Pedidos.FindAsync(pedidoId);
            if (pedido != null)
            {
                _dbContext.Pedidos.Remove(pedido);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
