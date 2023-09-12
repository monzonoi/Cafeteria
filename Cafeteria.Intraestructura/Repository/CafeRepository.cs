//using Cafeteria.Domain.Entidad;
//using Cafeteria.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Cafeteria.Intraestructura.Entidades;
using Cafeteria.Domain;
using Cafe = Cafeteria.Domain.Cafe;

namespace Cafeteria.Intraestructura.Repository
{
   
    public class CafeRepository : ICafeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CafeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Cafe> GetAll()
        {
            return _dbContext.Cafes.ToList();
        }

        public Cafe GetById(int id)
        {
            return _dbContext.Cafes.Find(id);
        }

        public void Add(Cafe cafe)
        {
            _dbContext.Cafes.Add(cafe);
        }

        public void Update(Cafe cafe)
        {
            _dbContext.Entry(cafe).State = EntityState.Modified;
        }

        public void Remove(Cafe cafe)
        {
            _dbContext.Cafes.Remove(cafe);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
