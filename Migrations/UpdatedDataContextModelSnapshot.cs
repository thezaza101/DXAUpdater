﻿// <auto-generated />
using DXAUpdater.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace DXAUpdater.Migrations
{
    [DbContext(typeof(UpdatedDataContext))]
    partial class UpdatedDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026");

            modelBuilder.Entity("DXANET.DataElement", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("UpdatedDataDataID");

                    b.Property<string>("datatypeID");

                    b.Property<string>("definition");

                    b.Property<string>("domain");

                    b.Property<string>("guidance");

                    b.Property<string>("identifier");

                    b.Property<string>("lastUpdateDate");

                    b.Property<string>("name");

                    b.Property<string>("sourceURL");

                    b.Property<string>("status");

                    b.Property<string>("version");

                    b.HasKey("ID");

                    b.HasIndex("UpdatedDataDataID");

                    b.HasIndex("datatypeID");

                    b.ToTable("DataElement");
                });

            modelBuilder.Entity("DXANET.Datatype", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("facetsID");

                    b.Property<string>("type");

                    b.HasKey("ID");

                    b.HasIndex("facetsID");

                    b.ToTable("Datatype");
                });

            modelBuilder.Entity("DXANET.Facets", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("maxLength");

                    b.Property<string>("minInclusive");

                    b.Property<string>("minLength");

                    b.Property<string>("pattern");

                    b.HasKey("ID");

                    b.ToTable("Facets");
                });

            modelBuilder.Entity("DXAUpdater.Models.UpdatedData", b =>
                {
                    b.Property<string>("DataID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("NextDataID");

                    b.HasKey("DataID");

                    b.ToTable("UpdatedData");
                });

            modelBuilder.Entity("DXANET.DataElement", b =>
                {
                    b.HasOne("DXAUpdater.Models.UpdatedData")
                        .WithMany("Payload")
                        .HasForeignKey("UpdatedDataDataID");

                    b.HasOne("DXANET.Datatype", "datatype")
                        .WithMany()
                        .HasForeignKey("datatypeID");
                });

            modelBuilder.Entity("DXANET.Datatype", b =>
                {
                    b.HasOne("DXANET.Facets", "facets")
                        .WithMany()
                        .HasForeignKey("facetsID");
                });
#pragma warning restore 612, 618
        }
    }
}
