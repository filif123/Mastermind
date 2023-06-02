﻿// <auto-generated />
using System;
using MastermindCore.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MastermindCore.Migrations
{
    [DbContext(typeof(MastermindDbContext))]
    [Migration("20220502114834_Mig1")]
    partial class Mig1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MastermindCore.Entity.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CommentedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("PlayerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PlayerName");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("MastermindCore.Entity.Player", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegisteredAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Name");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("MastermindCore.Entity.Rating", b =>
                {
                    b.Property<string>("PlayerName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("RatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Stars")
                        .HasColumnType("int");

                    b.HasKey("PlayerName");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("MastermindCore.Entity.Score", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("PlayedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("PlayerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerName");

                    b.ToTable("Scores");
                });

            modelBuilder.Entity("MastermindCore.Entity.Comment", b =>
                {
                    b.HasOne("MastermindCore.Entity.Player", "Player")
                        .WithMany("Comments")
                        .HasForeignKey("PlayerName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("MastermindCore.Entity.Rating", b =>
                {
                    b.HasOne("MastermindCore.Entity.Player", "Player")
                        .WithOne("Rating")
                        .HasForeignKey("MastermindCore.Entity.Rating", "PlayerName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("MastermindCore.Entity.Score", b =>
                {
                    b.HasOne("MastermindCore.Entity.Player", "Player")
                        .WithMany("Scores")
                        .HasForeignKey("PlayerName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("MastermindCore.Entity.Player", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Rating")
                        .IsRequired();

                    b.Navigation("Scores");
                });
#pragma warning restore 612, 618
        }
    }
}