﻿// <auto-generated />
using System;
using Cafeteria.Intraestructura;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cafeteria.Intraestructura.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230913200533_cambiosentidadcomanda")]
    partial class cambiosentidadcomanda
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.22");

            modelBuilder.Entity("CafeMateriaPrima", b =>
                {
                    b.Property<int>("CafesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MateriasPrimasId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CafesId", "MateriasPrimasId");

                    b.HasIndex("MateriasPrimasId");

                    b.ToTable("CafeMateriaPrima");
                });

            modelBuilder.Entity("Cafeteria.Domain.Cafe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Precio")
                        .HasColumnType("TEXT");

                    b.Property<int>("TiempoPreparacionMinutos")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Cafes");
                });

            modelBuilder.Entity("Cafeteria.Domain.DetalleFactura", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CafeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Cantidad")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FacturaId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("PrecioUnitario")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CafeId");

                    b.HasIndex("FacturaId");

                    b.ToTable("DetallesFactura");
                });

            modelBuilder.Entity("Cafeteria.Domain.Entidad.Comanda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Estado")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("TEXT");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Comandas");
                });

            modelBuilder.Entity("Cafeteria.Domain.Entidad.ItemPedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CafeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Cantidad")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PedidoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CafeId");

                    b.HasIndex("PedidoId");

                    b.ToTable("ItemsPedidos");
                });

            modelBuilder.Entity("Cafeteria.Domain.Entidad.MateriaPrima", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Cantidad")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MateriasPrimas");
                });

            modelBuilder.Entity("Cafeteria.Domain.Entidad.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ComandaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("TEXT");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ComandaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("Cafeteria.Domain.Entidad.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Rol")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Cafeteria.Domain.Factura", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaFacturacion")
                        .HasColumnType("TEXT");

                    b.Property<int>("PedidoId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Total")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.ToTable("Facturas");
                });

            modelBuilder.Entity("CafeMateriaPrima", b =>
                {
                    b.HasOne("Cafeteria.Domain.Cafe", null)
                        .WithMany()
                        .HasForeignKey("CafesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cafeteria.Domain.Entidad.MateriaPrima", null)
                        .WithMany()
                        .HasForeignKey("MateriasPrimasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Cafeteria.Domain.DetalleFactura", b =>
                {
                    b.HasOne("Cafeteria.Domain.Cafe", "Cafe")
                        .WithMany()
                        .HasForeignKey("CafeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cafeteria.Domain.Factura", "Factura")
                        .WithMany("Detalles")
                        .HasForeignKey("FacturaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cafe");

                    b.Navigation("Factura");
                });

            modelBuilder.Entity("Cafeteria.Domain.Entidad.Comanda", b =>
                {
                    b.HasOne("Cafeteria.Domain.Entidad.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Cafeteria.Domain.Entidad.ItemPedido", b =>
                {
                    b.HasOne("Cafeteria.Domain.Cafe", "Cafe")
                        .WithMany()
                        .HasForeignKey("CafeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cafeteria.Domain.Entidad.Pedido", "Pedido")
                        .WithMany("Items")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cafe");

                    b.Navigation("Pedido");
                });

            modelBuilder.Entity("Cafeteria.Domain.Entidad.Pedido", b =>
                {
                    b.HasOne("Cafeteria.Domain.Entidad.Comanda", "Comanda")
                        .WithMany("Pedidos")
                        .HasForeignKey("ComandaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cafeteria.Domain.Entidad.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comanda");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Cafeteria.Domain.Factura", b =>
                {
                    b.HasOne("Cafeteria.Domain.Entidad.Pedido", "Pedido")
                        .WithMany()
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pedido");
                });

            modelBuilder.Entity("Cafeteria.Domain.Entidad.Comanda", b =>
                {
                    b.Navigation("Pedidos");
                });

            modelBuilder.Entity("Cafeteria.Domain.Entidad.Pedido", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Cafeteria.Domain.Factura", b =>
                {
                    b.Navigation("Detalles");
                });
#pragma warning restore 612, 618
        }
    }
}
