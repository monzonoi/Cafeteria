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
    public class ComandaRepository : IComandaRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ComandaRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Comanda>> ObtenerTodasAsync()
        {
            return await _dbContext.Comandas.ToListAsync();
        }

        public async Task<Comanda> ObtenerPorIdAsync(int comandaId)
        {
            return await _dbContext.Comandas.FirstOrDefaultAsync(c => c.Id == comandaId);
        }

        public async Task AgregarAsync(Comanda comanda)
        {
            _dbContext.Comandas.Add(comanda);
            await _dbContext.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Comanda comanda)
        {
            _dbContext.Entry(comanda).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task EliminarAsync(int comandaId)
        {
            var comanda = await _dbContext.Comandas.FindAsync(comandaId);
            if (comanda != null)
            {
                _dbContext.Comandas.Remove(comanda);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
