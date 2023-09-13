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




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comanda>()
            .HasMany(c => c.Pedidos)
            .WithOne(p => p.Comanda)
            .HasForeignKey(p => p.ComandaId);

            modelBuilder.Entity<Comanda>()
                .HasOne(c => c.Factura)
                .WithOne(f => f.Comanda)
                .HasForeignKey<Factura>(f => f.ComandaId);

            modelBuilder.Entity<Factura>()
                .HasMany(f => f.Detalles)
                .WithOne(d => d.Factura)
                .HasForeignKey(d => d.FacturaId);

            // Otras configuraciones y relaciones

            base.OnModelCreating(modelBuilder);
        }
    }

}
