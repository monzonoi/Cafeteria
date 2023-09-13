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
        private readonly ApplicationDbContext _context;

        public CafeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cafe>> ObtenerTodosAsync()
        {
            return await _context.Cafes.ToListAsync();
        }

        public async Task<Cafe> ObtenerPorIdAsync(int id)
        {
            return await _context.Cafes.FindAsync(id);
        }

        public async Task AgregarAsync(Cafe cafe)
        {
            if (cafe == null)
            {
                throw new ArgumentNullException(nameof(cafe));
            }

            await _context.Cafes.AddAsync(cafe);
        }

        public async Task ActualizarAsync(Cafe cafe)
        {
            if (cafe == null)
            {
                throw new ArgumentNullException(nameof(cafe));
            }

            _context.Entry(cafe).State = EntityState.Modified;
        }

        public void Eliminar(Cafe cafe)
        {
            if (cafe == null)
            {
                throw new ArgumentNullException(nameof(cafe));
            }

            _context.Cafes.Remove(cafe);
        }
    }
}
