//using Cafeteria.Domain.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Domain
{
    public interface ICafeRepository
    {
        Cafe GetById(int id);
        void Add(Cafe cafe);
        void Update(Cafe cafe);
        void Remove(Cafe cafe);    
        IEnumerable<Cafe> GetAll();
        // Otros métodos de repositorio
        void SaveChanges();
    }
}
