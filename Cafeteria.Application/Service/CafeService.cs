using Cafeteria.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Application.Service
{
    public class CafeService : ICafeService
    {
        private readonly ICafeRepository _cafeRepository;

        public CafeService(ICafeRepository cafeRepository)
        {
            _cafeRepository = cafeRepository;
        }

        public IEnumerable<Cafe> GetAll()
        {
            return _cafeRepository.GetAll();
        }

        public Cafe GetById(int id)
        {
            return _cafeRepository.GetById(id);
        }

        public void Create(Cafe cafe)
        {
            _cafeRepository.Add(cafe);
            _cafeRepository.SaveChanges();
        }

        public void Update(int id, Cafe cafe)
        {
            var existingCafe = _cafeRepository.GetById(id);
            if (existingCafe != null)
            {
                existingCafe.Nombre = cafe.Nombre;
                existingCafe.Precio = cafe.Precio;
                _cafeRepository.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var cafeToDelete = _cafeRepository.GetById(id);
            if (cafeToDelete != null)
            {
                _cafeRepository.Remove(cafeToDelete);
                _cafeRepository.SaveChanges();
            }
        }

       
    }

}
