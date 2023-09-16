using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cafeteria.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Cafeteria.Intraestructura
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {
            // Constructor con parámetros
        }

        public ApplicationDbContext()
        {
            // Constructor sin parámetros
        }


        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Comanda> Comandas { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItemPedidos { get; set; }
        public DbSet<Parametro> Parametros { get; set; }
        public DbSet<MateriaPrima> MateriasPrimas { get; set; }
        public DbSet<Rol> Roles { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido>()
              .HasMany(p => p.Items)
              .WithOne(i => i.Pedido)
              .HasForeignKey(i => i.PedidoId)
              .OnDelete(DeleteBehavior.Cascade);

            //// Otras configuraciones y relaciones

            base.OnModelCreating(modelBuilder);
        }
    }

}
