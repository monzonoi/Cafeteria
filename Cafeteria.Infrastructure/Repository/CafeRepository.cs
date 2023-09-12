//using Cafeteria.Domain.Entidad;
//using Cafeteria.Domain.Repository;
using Cafeteria.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;



namespace Cafeteria.Infrastructure.Repository
{
   
    public class CafeRepository : ICafeRepository
    {
        private readonly DbContext _dbContext;

        public CafeRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Cafe GetById(int id)
        {
            return _dbContext.Set<Cafe>().Find(id);
        }

        public void Add(Cafe cafe)
        {
            _dbContext.Set<Cafe>().Add(cafe);
        }

        public void Update(Cafe cafe)
        {
            _dbContext.Set<Cafe>().Update(cafe);
        }

        public void Remove(Cafe cafe)
        {
            _dbContext.Set<Cafe>().Remove(cafe);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cafe> GetAll()
        {
            throw new NotImplementedException();
        }

        // Implementa otros métodos de repositorio
    }
}
