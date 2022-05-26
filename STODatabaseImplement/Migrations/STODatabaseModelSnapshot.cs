﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using STODatabaseImplement;

#nullable disable

namespace STODatabaseImplement.Migrations
{
    [DbContext(typeof(STODatabase))]
    partial class STODatabaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-preview.4.22229.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("STODatabaseImplement.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CarBrand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CarModel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VIN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FIO")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.ServiceRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateBegin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateEnd")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.ToTable("ServiceRecords");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.SparePart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FactoryNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SparePartName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("UMeasurement")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SpareParts");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.StoreKeeper", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FIO")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StoreKeepers");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.TimeOfWork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Hours")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TimeOfWorks");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.TO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateImplement")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOver")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("Sum")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("TOs");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.TOWork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("TOId")
                        .HasColumnType("int");

                    b.Property<int>("WorkId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TOId");

                    b.HasIndex("WorkId");

                    b.ToTable("TOWorks");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.Work", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("NetPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("StoreKeeperId")
                        .HasColumnType("int");

                    b.Property<string>("WorkName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WorkStatus")
                        .HasColumnType("int");

                    b.Property<int>("WorkTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StoreKeeperId");

                    b.HasIndex("WorkTypeId");

                    b.ToTable("Works");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.WorkSparePart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Count")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("SparePartId")
                        .HasColumnType("int");

                    b.Property<int>("WorkTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SparePartId");

                    b.HasIndex("WorkTypeId");

                    b.ToTable("WorkSpareParts");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.WorkType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("TimeOfWorkId")
                        .HasColumnType("int");

                    b.Property<string>("WorkName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TimeOfWorkId");

                    b.ToTable("WorkTypes");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.ServiceRecord", b =>
                {
                    b.HasOne("STODatabaseImplement.Models.Car", "Car")
                        .WithMany("CarRecords")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.TO", b =>
                {
                    b.HasOne("STODatabaseImplement.Models.Car", "Car")
                        .WithMany("CarsTO")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("STODatabaseImplement.Models.Employee", "Employee")
                        .WithMany("TOs")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.TOWork", b =>
                {
                    b.HasOne("STODatabaseImplement.Models.TO", "TO")
                        .WithMany("TOWorks")
                        .HasForeignKey("TOId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("STODatabaseImplement.Models.Work", "Work")
                        .WithMany("TOWorks")
                        .HasForeignKey("WorkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TO");

                    b.Navigation("Work");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.Work", b =>
                {
                    b.HasOne("STODatabaseImplement.Models.StoreKeeper", "StoreKeeper")
                        .WithMany("Works")
                        .HasForeignKey("StoreKeeperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("STODatabaseImplement.Models.WorkType", "WorkType")
                        .WithMany()
                        .HasForeignKey("WorkTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StoreKeeper");

                    b.Navigation("WorkType");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.WorkSparePart", b =>
                {
                    b.HasOne("STODatabaseImplement.Models.SparePart", "SparePart")
                        .WithMany("WorkSpareParts")
                        .HasForeignKey("SparePartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("STODatabaseImplement.Models.WorkType", "WorkType")
                        .WithMany("WorkSpareParts")
                        .HasForeignKey("WorkTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SparePart");

                    b.Navigation("WorkType");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.WorkType", b =>
                {
                    b.HasOne("STODatabaseImplement.Models.TimeOfWork", "TimeOfWork")
                        .WithMany("WorkTypes")
                        .HasForeignKey("TimeOfWorkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TimeOfWork");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.Car", b =>
                {
                    b.Navigation("CarRecords");

                    b.Navigation("CarsTO");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.Employee", b =>
                {
                    b.Navigation("TOs");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.SparePart", b =>
                {
                    b.Navigation("WorkSpareParts");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.StoreKeeper", b =>
                {
                    b.Navigation("Works");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.TimeOfWork", b =>
                {
                    b.Navigation("WorkTypes");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.TO", b =>
                {
                    b.Navigation("TOWorks");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.Work", b =>
                {
                    b.Navigation("TOWorks");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.WorkType", b =>
                {
                    b.Navigation("WorkSpareParts");
                });
#pragma warning restore 612, 618
        }
    }
}
