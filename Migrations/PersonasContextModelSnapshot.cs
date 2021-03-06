﻿// <auto-generated />
using System;
using EjercicioNET.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EjercicioNET.Migrations
{
    [DbContext(typeof(PersonasContext))]
    partial class PersonasContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EjercicioNET.Models.Persona", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("CreditoMaximo")
                        .HasColumnType("float");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Personas");
                });

            modelBuilder.Entity("EjercicioNET.Models.Telefono", b =>
                {
                    b.Property<int>("TelefonoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Numero_Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PersonaID")
                        .HasColumnType("int");

                    b.HasKey("TelefonoID");

                    b.HasIndex("PersonaID");

                    b.ToTable("Telefonos");
                });

            modelBuilder.Entity("EjercicioNET.Models.Telefono", b =>
                {
                    b.HasOne("EjercicioNET.Models.Persona", "Persona")
                        .WithMany()
                        .HasForeignKey("PersonaID");

                    b.Navigation("Persona");
                });
#pragma warning restore 612, 618
        }
    }
}
