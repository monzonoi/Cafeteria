using Cafeteria.Domain;
using Cafeteria.Domain.Entidad;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Intraestructura.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Usuario>> ObtenerTodosAsync()
        {
            return await _context.Set<Usuario>().ToListAsync();
        }

        public async Task<Usuario> ObtenerPorIdAsync(int id)
        {
            return await _context.Set<Usuario>().FindAsync(id);
        }

        public async Task AgregarAsync(Usuario usuario)
        {
            await _context.Set<Usuario>().AddAsync(usuario);
        }

        public void Actualizar(Usuario usuario)
        {
            _context.Set<Usuario>().Update(usuario);
        }

        public void Eliminar(Usuario usuario)
        {
            _context.Set<Usuario>().Remove(usuario);
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Set<Usuario>().AnyAsync(u => u.Id == id);
        }
    }
}
