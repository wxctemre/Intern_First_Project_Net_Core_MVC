﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrsaAkademi.demo.WebApi.model;

namespace OrsaAkademi.demo.WebApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240524113759_personelaktifmiSilindimi")]
    partial class personelaktifmiSilindimi
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OrsaAkademi.demo.models.Entity.MedyaKutuphanesi", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MedyaAdi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MedyaUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MedyaKutuphanesi");
                });

            modelBuilder.Entity("OrsaAkademi.demo.models.Entity.Okullar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short>("Mezunolduguyil")
                        .HasColumnType("smallint");

                    b.Property<int>("Okulid")
                        .HasColumnType("int");

                    b.Property<int>("aktifMi")
                        .HasColumnType("int");

                    b.Property<short>("personelid")
                        .HasColumnType("smallint");

                    b.Property<int>("silindiMi")
                        .HasColumnType("int");

                    b.Property<int>("sirano")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("okullar");
                });

            modelBuilder.Entity("OrsaAkademi.demo.models.Entity.ParamOkullar", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Okuladi")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("paramokullar");
                });

            modelBuilder.Entity("OrsaAkademi.demo.models.Entity.PersonelMedyalar", b =>
                {
                    b.Property<int>("TabloId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AktifMi")
                        .HasColumnType("int");

                    b.Property<int>("MedyaId")
                        .HasColumnType("int");

                    b.Property<int>("PersonelId")
                        .HasColumnType("int");

                    b.Property<int>("SilindiMi")
                        .HasColumnType("int");

                    b.HasKey("TabloId");

                    b.ToTable("PersonelMedyalar");
                });

            modelBuilder.Entity("OrsaAkademi.demo.models.Entity.PersonelOkulMedyalariiliski", b =>
                {
                    b.Property<int>("TabloId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MedyaID")
                        .HasColumnType("int");

                    b.Property<int>("PersonelTabloId")
                        .HasColumnType("int");

                    b.Property<int>("aktifMi")
                        .HasColumnType("int");

                    b.Property<int>("silindiMi")
                        .HasColumnType("int");

                    b.HasKey("TabloId");

                    b.ToTable("PersonelEgitimId");
                });

            modelBuilder.Entity("OrsaAkademi.demo.models.Entity.Personeller", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DogumTarihi")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sifre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Soyad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("aktifMi")
                        .HasColumnType("int");

                    b.Property<int>("silindiMi")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("personellers");
                });
#pragma warning restore 612, 618
        }
    }
}
