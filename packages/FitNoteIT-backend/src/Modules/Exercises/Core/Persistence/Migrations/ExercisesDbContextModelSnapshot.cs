﻿// <auto-generated />
using System;
using FitNoteIT.Modules.Exercises.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FitNoteIT.Modules.Exercises.Core.Persistence.Migrations
{
    [DbContext(typeof(ExercisesDbContext))]
    partial class ExercisesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("exercises")
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FitNoteIT.Modules.Exercises.Core.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Category", "exercises");
                });

            modelBuilder.Entity("FitNoteIT.Modules.Exercises.Core.Entities.Exercise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Exercise", "exercises");
                });

            modelBuilder.Entity("FitNoteIT.Modules.Exercises.Core.Entities.Record", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ExerciseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RecordDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Result")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.ToTable("Record", "exercises");
                });

            modelBuilder.Entity("FitNoteIT.Modules.Exercises.Core.Entities.Exercise", b =>
                {
                    b.HasOne("FitNoteIT.Modules.Exercises.Core.Entities.Category", "Category")
                        .WithMany("Exercises")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Category");
                });

            modelBuilder.Entity("FitNoteIT.Modules.Exercises.Core.Entities.Record", b =>
                {
                    b.HasOne("FitNoteIT.Modules.Exercises.Core.Entities.Exercise", "Exercise")
                        .WithMany("Records")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");
                });

            modelBuilder.Entity("FitNoteIT.Modules.Exercises.Core.Entities.Category", b =>
                {
                    b.Navigation("Exercises");
                });

            modelBuilder.Entity("FitNoteIT.Modules.Exercises.Core.Entities.Exercise", b =>
                {
                    b.Navigation("Records");
                });
#pragma warning restore 612, 618
        }
    }
}
