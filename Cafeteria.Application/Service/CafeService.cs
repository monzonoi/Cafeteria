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
            _cafeRepository = cafeRepository ?? throw new ArgumentNullException(nameof(cafeRepository));
        }

        public Cafe GetCById(int id)
        {
            return _cafeRepository.GetById(id);
        }

        public IEnumerable<Cafe> GetAll()
        {
            return _cafeRepository.GetAll();
        }

        public void Create(Cafe cafe)
        {
            // Puedes realizar validaciones o lógica de negocio aquí antes de agregar el café
            _cafeRepository.Add(cafe);
        }

        public void Update(Cafe cafe)
        {
            // Puedes realizar validaciones o lógica de negocio aquí antes de actualizar el café
            _cafeRepository.Update(cafe);
        }

        public void Delete(int id)
        {
            // Puedes realizar validaciones o lógica de negocio aquí antes de eliminar el café
            _cafeRepository.Delete(id);
        }

        public Cafe GeteById(int id)
        {
            throw new NotImplementedException();
        }

        public Cafe GetById(int id)
        {
            throw new NotImplementedException();
        }
    }

}
