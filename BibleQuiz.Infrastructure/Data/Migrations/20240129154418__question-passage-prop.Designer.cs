﻿// <auto-generated />
using BibleQuiz.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BibleQuiz.Infrastructure.Data.Migrations
{
    [DbContext(typeof(QuizDbContext))]
    [Migration("20240129154418__question-passage-prop")]
    partial class _questionpassageprop
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BibleQuiz.Domain.Entities.BibleBook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("BibleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LongName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NormalizedShortName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("BibleBook", "com");
                });

            modelBuilder.Entity("BibleQuiz.Domain.Entities.TestQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Passage")
                        .HasColumnType("text");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Verse")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TestQuestion", "com");
                });

            modelBuilder.Entity("BibleQuiz.Domain.Entities.Verse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Passage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Verse", "com");
                });
#pragma warning restore 612, 618
        }
    }
}
