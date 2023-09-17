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
    [Migration("20230916165404_Inicial")]
    partial class Inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.22");

            modelBuilder.Entity("Cafeteria.Domain.Entidades.Comanda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EmpleadoId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EmpleadoId");

                    b.ToTable("Comandas");
                });

            modelBuilder.Entity("Cafeteria.Domain.Entidades.ItemPedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PedidoId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Precio")
                        .HasColumnType("TEXT");

                    b.Property<int>("TiempoRealizacion")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.ToTable("ItemPedidos");
                });

            modelBuilder.Entity("Cafeteria.Domain.Entidades.MateriaPrima", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemPedidoId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ItemPedidoId");

                    b.ToTable("MateriasPrimas");
                });

            modelBuilder.Entity("Cafeteria.Domain.Entidades.Parametro", b =>
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

                    b.Property<int>("Tipo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Valor")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Parametros");
                });

            modelBuilder.Entity("Cafeteria.Domain.Entidades.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ComandaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaPedido")
                        .HasColumnType("TEXT");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ComandaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("Cafeteria.Domain.Entidades.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Cafeteria.Domain.Entidades.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RolId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RolId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Cafeteria.Domain.Entidades.Comanda", b =>
                {
                    b.HasOne("Cafeteria.Domain.Entidades.Usuario", "Empleado")
                        .WithMany()
                        .HasForeignKey("EmpleadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empleado");
                });

            modelBuilder.Entity("Cafeteria.Domain.Entidades.ItemPedido", b =>
                {
                    b.HasOne("Cafeteria.Domain.Entidades.Pedido", "Pedido")
                        .WithMany("Items")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pedido");
                });

            modelBuilder.Entity("Cafeteria.Domain.Entidades.MateriaPrima", b =>
                {
                    b.HasOne("Cafeteria.Domain.Entidades.ItemPedido", "ItemPedido")
                        .WithMany("MateriaPrima")
                        .HasForeignKey("ItemPedidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ItemPedido");
                });

            modelBuilder.Entity("Cafeteria.Domain.Entidades.Pedido", b =>
                {
                    b.HasOne("Cafeteria.Domain.Entidades.Comanda", "Comanda")
                        .WithMany("Pedidos")
                        .HasForeignKey("ComandaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cafeteria.Domain.Entidades.Usuario", "Usuario")
                        .WithMany("Pedidos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comanda");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Cafeteria.Domain.Entidades.Usuario", b =>
                {
                    b.HasOne("Cafeteria.Domain.Entidades.Rol", "Rol")
                        .WithMany("Usuarios")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("Cafeteria.Domain.Entidades.Comanda", b =>
                {
                    b.Navigation("Pedidos");
                });

            modelBuilder.Entity("Cafeteria.Domain.Entidades.ItemPedido", b =>
                {
                    b.Navigation("MateriaPrima");
                });

            modelBuilder.Entity("Cafeteria.Domain.Entidades.Pedido", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Cafeteria.Domain.Entidades.Rol", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("Cafeteria.Domain.Entidades.Usuario", b =>
                {
                    b.Navigation("Pedidos");
                });
#pragma warning restore 612, 618
        }
    }
}