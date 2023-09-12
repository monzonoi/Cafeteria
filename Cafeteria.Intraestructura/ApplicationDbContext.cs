using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cafeteria.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cafeteria.Intraestructura
{
  

    public class ApplicationDbContext : DbContext
    {
        public DbSet<Cafe> Cafes { get; set; }
        // Define otras propiedades DbSet para tus otras entidades

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Agrega configuraciones de modelo aquí si es necesario
    }
}
