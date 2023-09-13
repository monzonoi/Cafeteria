using Cafeteria.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Intraestructura.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        private bool _disposed;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CommitAsync()
        {
            try
            {
                return await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Maneja excepciones de concurrencia si es necesario
                throw;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
