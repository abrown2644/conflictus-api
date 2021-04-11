﻿// <auto-generated />
using System;
using Conflictus.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Conflictus.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210214060452_DBInit")]
    partial class DBInit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Conflictus.Model.Battle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("Outcome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SideAId")
                        .HasColumnType("int");

                    b.Property<int?>("SideBId")
                        .HasColumnType("int");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WarId")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("SideAId");

                    b.HasIndex("SideBId");

                    b.HasIndex("WarId");

                    b.ToTable("Battle");
                });

            modelBuilder.Entity("Conflictus.Model.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasCoordinates")
                        .HasColumnType("bit");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("Conflictus.Model.Participant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("FlagUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Participant");
                });

            modelBuilder.Entity("Conflictus.Model.SideA", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Commanders")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Losses")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Strength")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Victory")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("SideA");
                });

            modelBuilder.Entity("Conflictus.Model.SideB", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Commanders")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Losses")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Strength")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Victory")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("SideB");
                });

            modelBuilder.Entity("Conflictus.Model.War", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("War");
                });

            modelBuilder.Entity("ParticipantSideA", b =>
                {
                    b.Property<int>("ParticipantsId")
                        .HasColumnType("int");

                    b.Property<int>("SideAsId")
                        .HasColumnType("int");

                    b.HasKey("ParticipantsId", "SideAsId");

                    b.HasIndex("SideAsId");

                    b.ToTable("ParticipantSideA");
                });

            modelBuilder.Entity("ParticipantSideB", b =>
                {
                    b.Property<int>("ParticipantsId")
                        .HasColumnType("int");

                    b.Property<int>("SideBsId")
                        .HasColumnType("int");

                    b.HasKey("ParticipantsId", "SideBsId");

                    b.HasIndex("SideBsId");

                    b.ToTable("ParticipantSideB");
                });

            modelBuilder.Entity("ParticipantWar", b =>
                {
                    b.Property<int>("ParticipantsId")
                        .HasColumnType("int");

                    b.Property<int>("WarsId")
                        .HasColumnType("int");

                    b.HasKey("ParticipantsId", "WarsId");

                    b.HasIndex("WarsId");

                    b.ToTable("ParticipantWar");
                });

            modelBuilder.Entity("Conflictus.Model.Battle", b =>
                {
                    b.HasOne("Conflictus.Model.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.HasOne("Conflictus.Model.SideA", "SideA")
                        .WithMany()
                        .HasForeignKey("SideAId");

                    b.HasOne("Conflictus.Model.SideB", "SideB")
                        .WithMany()
                        .HasForeignKey("SideBId");

                    b.HasOne("Conflictus.Model.War", "War")
                        .WithMany("Battles")
                        .HasForeignKey("WarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("SideA");

                    b.Navigation("SideB");

                    b.Navigation("War");
                });

            modelBuilder.Entity("ParticipantSideA", b =>
                {
                    b.HasOne("Conflictus.Model.Participant", null)
                        .WithMany()
                        .HasForeignKey("ParticipantsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Conflictus.Model.SideA", null)
                        .WithMany()
                        .HasForeignKey("SideAsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ParticipantSideB", b =>
                {
                    b.HasOne("Conflictus.Model.Participant", null)
                        .WithMany()
                        .HasForeignKey("ParticipantsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Conflictus.Model.SideB", null)
                        .WithMany()
                        .HasForeignKey("SideBsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ParticipantWar", b =>
                {
                    b.HasOne("Conflictus.Model.Participant", null)
                        .WithMany()
                        .HasForeignKey("ParticipantsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Conflictus.Model.War", null)
                        .WithMany()
                        .HasForeignKey("WarsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Conflictus.Model.War", b =>
                {
                    b.Navigation("Battles");
                });
#pragma warning restore 612, 618
        }
    }
}
