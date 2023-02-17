﻿// <auto-generated />
using System;
using DailyPlanner.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DailyPlanner.Context.MigrationsPostgreSQL.Migrations
{
    [DbContext(typeof(DailyPlannerContext))]
    [Migration("20230217103524_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DailyPlanner.Context.Entities.Notebook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Uid")
                        .IsUnique();

                    b.ToTable("authors", (string)null);
                });

            modelBuilder.Entity("DailyPlanner.Context.Entities.TodoTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ActualCompletionTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<DateTime>("EstimatedCompletionTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("NotebookId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("NotebookId");

                    b.HasIndex("Uid")
                        .IsUnique();

                    b.ToTable("tasks", (string)null);
                });

            modelBuilder.Entity("DailyPlanner.Context.Entities.TodoTask", b =>
                {
                    b.HasOne("DailyPlanner.Context.Entities.Notebook", "Notebook")
                        .WithMany("TodoTasks")
                        .HasForeignKey("NotebookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Notebook");
                });

            modelBuilder.Entity("DailyPlanner.Context.Entities.Notebook", b =>
                {
                    b.Navigation("TodoTasks");
                });
#pragma warning restore 612, 618
        }
    }
}