﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebSocket.Infrastructure.Context;

#nullable disable

namespace WebSocket.Infra.Migrations
{
    [DbContext(typeof(DbContextPg))]
    [Migration("20230417010544_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WebSocket.Domain.Entitys.Credentials", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("password_hash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("password_salt")
                        .HasColumnType("text");

                    b.Property<Guid>("person_id")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("person_id")
                        .IsUnique();

                    b.ToTable("Credential");
                });

            modelBuilder.Entity("WebSocket.Domain.Entitys.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("email")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("WebSocket.Domain.Entitys.Credentials", b =>
                {
                    b.HasOne("WebSocket.Domain.Entitys.Person", "person")
                        .WithOne("Credentials")
                        .HasForeignKey("WebSocket.Domain.Entitys.Credentials", "person_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("person");
                });

            modelBuilder.Entity("WebSocket.Domain.Entitys.Person", b =>
                {
                    b.Navigation("Credentials")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
