﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using socialbrothers_quotes_api;
using socialbrothers_quotes_api.Models;

namespace socialbrothersquotesapi.Migrations
{
    [DbContext(typeof(QuoteContext))]
    [Migration("20190306100851_GenericContext")]
    partial class GenericContext
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("socialbrothers_quotes_api.Quote", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("Date");

                    b.Property<int>("Rating");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Quotes");
                });
#pragma warning restore 612, 618
        }
    }
}
