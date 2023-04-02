﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Xpand.CrewsAPI.Data;

#nullable disable

namespace Xpand.CrewsAPI.Migrations
{
    [DbContext(typeof(CrewContext))]
    [Migration("20230402123143_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Xpand.CrewsAPI.Models.Crew", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CaptainId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CaptainId")
                        .IsUnique();

                    b.ToTable("Crews");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CaptainId = 1,
                            Name = "Crew1"
                        },
                        new
                        {
                            Id = 2,
                            CaptainId = 2,
                            Name = "Crew1"
                        },
                        new
                        {
                            Id = 3,
                            CaptainId = 3,
                            Name = "Crew1"
                        },
                        new
                        {
                            Id = 4,
                            CaptainId = 4,
                            Name = "Crew1"
                        });
                });

            modelBuilder.Entity("Xpand.CrewsAPI.Models.Human", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Humans");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Jonathan Smith"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Hannah Intellis"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Eva Brains"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Rick Anderson"
                        });
                });

            modelBuilder.Entity("Xpand.CrewsAPI.Models.Robot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CrewId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CrewId");

                    b.ToTable("Robots");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CrewId = 1,
                            Name = "T2011"
                        },
                        new
                        {
                            Id = 2,
                            CrewId = 1,
                            Name = "T2020"
                        },
                        new
                        {
                            Id = 3,
                            CrewId = 1,
                            Name = "T2031"
                        },
                        new
                        {
                            Id = 4,
                            CrewId = 2,
                            Name = "T21"
                        },
                        new
                        {
                            Id = 5,
                            CrewId = 2,
                            Name = "T28"
                        },
                        new
                        {
                            Id = 6,
                            CrewId = 2,
                            Name = "T29"
                        },
                        new
                        {
                            Id = 7,
                            CrewId = 3,
                            Name = "T201"
                        },
                        new
                        {
                            Id = 8,
                            CrewId = 4,
                            Name = "T18"
                        },
                        new
                        {
                            Id = 9,
                            CrewId = 4,
                            Name = "T19"
                        },
                        new
                        {
                            Id = 10,
                            CrewId = 4,
                            Name = "T31"
                        });
                });

            modelBuilder.Entity("Xpand.CrewsAPI.Models.Crew", b =>
                {
                    b.HasOne("Xpand.CrewsAPI.Models.Human", "Captain")
                        .WithOne("Crew")
                        .HasForeignKey("Xpand.CrewsAPI.Models.Crew", "CaptainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Captain");
                });

            modelBuilder.Entity("Xpand.CrewsAPI.Models.Robot", b =>
                {
                    b.HasOne("Xpand.CrewsAPI.Models.Crew", "Crew")
                        .WithMany("Robots")
                        .HasForeignKey("CrewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Crew");
                });

            modelBuilder.Entity("Xpand.CrewsAPI.Models.Crew", b =>
                {
                    b.Navigation("Robots");
                });

            modelBuilder.Entity("Xpand.CrewsAPI.Models.Human", b =>
                {
                    b.Navigation("Crew");
                });
#pragma warning restore 612, 618
        }
    }
}