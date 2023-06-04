﻿// <auto-generated />
using System;
using Data.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230604204410_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.Evento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DataEvento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagemURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Local")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QtdPessoas")
                        .HasColumnType("int");

                    b.Property<string>("Telefone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tema")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Eventos");
                });

            modelBuilder.Entity("Domain.Models.Lote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DataFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataInicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("EventoId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventoId");

                    b.ToTable("Lotes");
                });

            modelBuilder.Entity("Domain.Models.Palestrante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagemURl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiniCurriculo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Palestrantes");
                });

            modelBuilder.Entity("Domain.Models.PalestrantesEvento", b =>
                {
                    b.Property<int>("EventoId")
                        .HasColumnType("int");

                    b.Property<int>("PalestranteId")
                        .HasColumnType("int");

                    b.HasKey("EventoId", "PalestranteId");

                    b.ToTable("PalestrantesEventos");
                });

            modelBuilder.Entity("Domain.Models.RedeSocial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("EventoId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PalestranteId")
                        .HasColumnType("int");

                    b.Property<string>("URL")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EventoId");

                    b.HasIndex("PalestranteId");

                    b.ToTable("RedesSociais");
                });

            modelBuilder.Entity("EventoPalestrantesEvento", b =>
                {
                    b.Property<int>("EventosId")
                        .HasColumnType("int");

                    b.Property<int>("PalestrantesEventoEventoId")
                        .HasColumnType("int");

                    b.Property<int>("PalestrantesEventoPalestranteId")
                        .HasColumnType("int");

                    b.HasKey("EventosId", "PalestrantesEventoEventoId", "PalestrantesEventoPalestranteId");

                    b.HasIndex("PalestrantesEventoEventoId", "PalestrantesEventoPalestranteId");

                    b.ToTable("EventoPalestrantesEvento");
                });

            modelBuilder.Entity("PalestrantePalestrantesEvento", b =>
                {
                    b.Property<int>("PalestrantesId")
                        .HasColumnType("int");

                    b.Property<int>("PalestrantesEventoEventoId")
                        .HasColumnType("int");

                    b.Property<int>("PalestrantesEventoPalestranteId")
                        .HasColumnType("int");

                    b.HasKey("PalestrantesId", "PalestrantesEventoEventoId", "PalestrantesEventoPalestranteId");

                    b.HasIndex("PalestrantesEventoEventoId", "PalestrantesEventoPalestranteId");

                    b.ToTable("PalestrantePalestrantesEvento");
                });

            modelBuilder.Entity("Domain.Models.Lote", b =>
                {
                    b.HasOne("Domain.Models.Evento", "Evento")
                        .WithMany("Lote")
                        .HasForeignKey("EventoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Evento");
                });

            modelBuilder.Entity("Domain.Models.RedeSocial", b =>
                {
                    b.HasOne("Domain.Models.Evento", "Evento")
                        .WithMany("RedesSociais")
                        .HasForeignKey("EventoId");

                    b.HasOne("Domain.Models.Palestrante", "Palestrante")
                        .WithMany("RedesSociais")
                        .HasForeignKey("PalestranteId");

                    b.Navigation("Evento");

                    b.Navigation("Palestrante");
                });

            modelBuilder.Entity("EventoPalestrantesEvento", b =>
                {
                    b.HasOne("Domain.Models.Evento", null)
                        .WithMany()
                        .HasForeignKey("EventosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.PalestrantesEvento", null)
                        .WithMany()
                        .HasForeignKey("PalestrantesEventoEventoId", "PalestrantesEventoPalestranteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PalestrantePalestrantesEvento", b =>
                {
                    b.HasOne("Domain.Models.Palestrante", null)
                        .WithMany()
                        .HasForeignKey("PalestrantesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.PalestrantesEvento", null)
                        .WithMany()
                        .HasForeignKey("PalestrantesEventoEventoId", "PalestrantesEventoPalestranteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.Evento", b =>
                {
                    b.Navigation("Lote");

                    b.Navigation("RedesSociais");
                });

            modelBuilder.Entity("Domain.Models.Palestrante", b =>
                {
                    b.Navigation("RedesSociais");
                });
#pragma warning restore 612, 618
        }
    }
}