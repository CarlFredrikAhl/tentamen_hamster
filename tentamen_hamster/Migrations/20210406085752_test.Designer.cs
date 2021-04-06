﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using tentamen_hamster;

namespace tentamen_hamster.Migrations
{
    [DbContext(typeof(AppContext))]
    [Migration("20210406085752_test")]
    partial class test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("tentamen_hamster.ActivityLog", b =>
                {
                    b.Property<int>("ActivityLogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Activity")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("HamsterId")
                        .HasColumnType("int");

                    b.HasKey("ActivityLogId");

                    b.HasIndex("HamsterId");

                    b.ToTable("ActivityLog");
                });

            modelBuilder.Entity("tentamen_hamster.Cage", b =>
                {
                    b.Property<int>("CageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("CageId");

                    b.ToTable("Cages");
                });

            modelBuilder.Entity("tentamen_hamster.ExerciseSpace", b =>
                {
                    b.Property<int>("ExerciseSpaceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("ExerciseSpaceId");

                    b.ToTable("ExerciseSpace");
                });

            modelBuilder.Entity("tentamen_hamster.Hamster", b =>
                {
                    b.Property<int>("HamsterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Activity")
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int?>("CageId")
                        .HasColumnType("int");

                    b.Property<int?>("ExerciseSpaceId")
                        .HasColumnType("int");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("TimeCheckedIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("TimeLastExercise")
                        .HasColumnType("datetime2");

                    b.HasKey("HamsterId");

                    b.HasIndex("CageId");

                    b.HasIndex("ExerciseSpaceId");

                    b.ToTable("Hamsters");
                });

            modelBuilder.Entity("tentamen_hamster.ActivityLog", b =>
                {
                    b.HasOne("tentamen_hamster.Hamster", "Hamster")
                        .WithMany()
                        .HasForeignKey("HamsterId");

                    b.Navigation("Hamster");
                });

            modelBuilder.Entity("tentamen_hamster.Hamster", b =>
                {
                    b.HasOne("tentamen_hamster.Cage", "Cage")
                        .WithMany("Hamsters")
                        .HasForeignKey("CageId");

                    b.HasOne("tentamen_hamster.ExerciseSpace", null)
                        .WithMany("Hamsters")
                        .HasForeignKey("ExerciseSpaceId");

                    b.Navigation("Cage");
                });

            modelBuilder.Entity("tentamen_hamster.Cage", b =>
                {
                    b.Navigation("Hamsters");
                });

            modelBuilder.Entity("tentamen_hamster.ExerciseSpace", b =>
                {
                    b.Navigation("Hamsters");
                });
#pragma warning restore 612, 618
        }
    }
}
