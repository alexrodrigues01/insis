﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Votes.Helpers;

#nullable disable

namespace Votes.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230402154443_CreateVotesTable")]
    partial class CreateVotesTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("VotesApp.Model.Vote", b =>
                {
                    b.Property<long>("reviewId")
                        .HasPrecision(10)
                        .HasColumnType("bigint")
                        .HasColumnName("ReviewID");

                    b.Property<long>("userId")
                        .HasPrecision(10)
                        .HasColumnType("bigint")
                        .HasColumnName("UserID");

                    b.Property<string>("vote")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("text")
                        .HasColumnName("Vote");

                    b.HasKey("reviewId", "userId")
                        .HasName("PK_VOTES");

                    b.HasIndex(new[] { "reviewId" }, "IDX_REVIEWID");

                    b.HasIndex(new[] { "userId" }, "IDX_USERID");

                    b.ToTable("Votes");
                });
#pragma warning restore 612, 618
        }
    }
}
