//using Cafeteria.Domain.Entidad;
using Cafeteria.Infrastructure.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Infrastructure
{
    public class ApplicationDbContext2 : DbContext
    {
        public ApplicationDbContext2(DbContextOptions<ApplicationDbContext2> options) : base(options)
        {
        }

        public DbSet<Entidades.Cafe> Cafes { get; set; }
        // Define otros DbSet para tus entidades aquí

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuraciones adicionales del modelo si es necesario
        }

        //public DbSet<Usuario> Usuarios { get; set; }
        //public DbSet<Rol> Roles { get; set; }
        //public DbSet<Cafe> Cafes { get; set; }
        //public DbSet<Pedido> Pedidos { get; set; }
        //public DbSet<ItemPedido> ItemsPedido { get; set; }
        //public DbSet<MateriaPrima> MateriasPrimas { get; set; }
        //public DbSet<DetalleFactura> DetallesFactura { get; set; }
        //public DbSet<Factura> Facturas { get; set; }
        //public DbSet<StockMateriaPrima> StockMateriasPrimas { get; set; }
        //public DbSet<Trabajo> Trabajos { get; set; }

        //public TuProyectoDbContext(DbContextOptions<TuProyectoDbContext> options)
        //    : base(options) { }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Configura tus relaciones aquí
        //    // Por ejemplo, configurar las relaciones entre las tablas utilizando modelBuilder.Entity<T>().HasMany() o HasOne()

        //    modelBuilder.Entity<Usuario>()
        //        .HasOne(u => u.Rol)
        //        .WithMany()
        //        .HasForeignKey(u => u.RolID);

        //    modelBuilder.Entity<Pedido>()
        //        .HasOne(p => p.Usuario)
        //        .WithMany(u => u.Pedidos)
        //        .HasForeignKey(p => p.UsuarioID);

        //    modelBuilder.Entity<ItemPedido>()
        //        .HasOne(ip => ip.Pedido)
        //        .WithMany(p => p.ItemsPedido)
        //        .HasForeignKey(ip => ip.PedidoID);

        //    modelBuilder.Entity<ItemPedido>()
        //        .HasOne(ip => ip.Cafe)
        //        .WithMany()
        //        .HasForeignKey(ip => ip.CafeID);

        //    modelBuilder.Entity<DetalleFactura>()
        //        .HasOne(df => df.Factura)
        //        .WithMany(f => f.DetallesFactura)
        //        .HasForeignKey(df => df.FacturaID);

        //    modelBuilder.Entity<DetalleFactura>()
        //        .HasOne(df => df.ItemPedido)
        //        .WithMany()
        //        .HasForeignKey(df => df.ItemPedidoID);

        //    modelBuilder.Entity<Factura>()
        //        .HasOne(f => f.Pedido)
        //        .WithOne()
        //        .HasForeignKey<Factura>(f => f.PedidoID);

        //    modelBuilder.Entity<StockMateriaPrima>()
        //        .HasOne(smp => smp.MateriaPrima)
        //        .WithMany()
        //        .HasForeignKey(smp => smp.MateriaPrimaID);

        //    modelBuilder.Entity<Trabajo>()
        //        .HasOne(t => t.Empleado)
        //        .WithMany()
        //        .HasForeignKey(t => t.EmpleadoID);

        //    // Otros mapeos y configuraciones de entidades aquí

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
