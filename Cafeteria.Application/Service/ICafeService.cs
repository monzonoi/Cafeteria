using System.Collections.Generic;
using Cafeteria.Domain; // Asegúrate de importar el espacio de nombres del dominio


namespace Cafeteria.Application.Service
{
    public interface ICafeService
    {
        Cafe GetById(int id);
        IEnumerable<Cafe> GetAll();
        void Create(Cafe cafe);
        void Update(Cafe cafe);
        void Delete(int id);
        // Puedes agregar otros métodos relacionados con la gestión de cafés
    }
}
