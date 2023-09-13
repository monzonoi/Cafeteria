using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cafeteria.Domain;
using Cafeteria.Domain.Entidad;
using Microsoft.EntityFrameworkCore;

namespace Cafeteria.Intraestructura
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        
        public DbSet<Cafe> Cafes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Comanda> Comandas { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItemsPedidos { get; set; }
        public DbSet<MateriaPrima> MateriasPrimas { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<DetalleFactura> DetallesFactura { get; set; }


    }
}
