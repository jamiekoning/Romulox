﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Romulox.DataAccess;

namespace Romulox.DataAccess.Migrations
{
    [DbContext(typeof(RomuloxContext))]
    [Migration("20190708054508_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Romulox.Core.Entities.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Developers");

                    b.Property<string>("IconImage");

                    b.Property<string>("Image");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Path")
                        .IsRequired();

                    b.Property<Guid>("PlatformId");

                    b.Property<string>("Publishers");

                    b.Property<DateTime?>("ReleaseDate");

                    b.HasKey("Id");

                    b.HasIndex("PlatformId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Romulox.Core.Entities.Platform", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Company");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("NoIntroDatFilePath");

                    b.Property<string>("Path")
                        .IsRequired();

                    b.Property<int>("PlatformType");

                    b.HasKey("Id");

                    b.ToTable("Platforms");
                });

            modelBuilder.Entity("Romulox.Core.Entities.Game", b =>
                {
                    b.HasOne("Romulox.Core.Entities.Platform", "Platform")
                        .WithMany("Games")
                        .HasForeignKey("PlatformId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
