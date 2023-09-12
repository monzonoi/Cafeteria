using System.Collections.Generic;
using Cafeteria.Domain; // Asegúrate de importar el espacio de nombres del dominio


namespace Cafeteria.Application.Service
{
    public interface ICafeService
    {
        Cafe GetById(int id);
        void Create(Cafe cafe);
        void Update(int id, Cafe cafe);
        void Delete(int id);
        IEnumerable<Cafe> GetAll();
        // Puedes agregar otros métodos relacionados con la gestión de cafés
    }
}
