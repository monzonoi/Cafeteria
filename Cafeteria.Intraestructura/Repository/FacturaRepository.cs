using Cafeteria.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Intraestructura.Repository
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public FacturaRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<Factura>> ObtenerTodasAsync()
        {
            return await _dbContext.Set<Factura>().ToListAsync();
        }

        public async Task<Factura> ObtenerPorIdAsync(int id)
        {
            return await _dbContext.Set<Factura>().FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task AgregarAsync(Factura factura)
        {
            await _dbContext.Set<Factura>().AddAsync(factura);
        }

        public void Actualizar(Factura factura)
        {
            _dbContext.Entry(factura).State = EntityState.Modified;
        }

        public void Eliminar(Factura factura)
        {
            _dbContext.Set<Factura>().Remove(factura);
        }
    }
}
