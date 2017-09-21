﻿// <auto-generated />
using EF1DemoSchemaUpdater;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace EF1DemoSchemaUpdater.Migrations
{
    [DbContext(typeof(InheritedContext))]
    [Migration("20170920135555_AddedDescription")]
    partial class AddedDescription
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DbLayer.ActionTarget", b =>
                {
                    b.Property<Guid>("IdActionTarget")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("DateCreated");

                    b.Property<DateTimeOffset>("DateLastEdited");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("IdActionTarget");

                    b.ToTable("ActionTargets");
                });

            modelBuilder.Entity("DbLayer.Event", b =>
                {
                    b.Property<Guid>("IdEvent")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ActionTargetIdActionTarget");

                    b.Property<DateTimeOffset>("DateCreated");

                    b.Property<DateTimeOffset>("DateLastEdited");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(200)");

                    b.Property<long>("Duration");

                    b.Property<DateTimeOffset>("EndTime");

                    b.Property<Guid?>("EventSourceIdEventSource");

                    b.Property<DateTimeOffset>("StartTime");

                    b.HasKey("IdEvent");

                    b.HasIndex("ActionTargetIdActionTarget");

                    b.HasIndex("EventSourceIdEventSource");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("DbLayer.EventSource", b =>
                {
                    b.Property<Guid>("IdEventSource")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("DateCreated");

                    b.Property<DateTimeOffset>("DateLastEdited");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("IdEventSource");

                    b.ToTable("EventSources");
                });

            modelBuilder.Entity("DbLayer.Event", b =>
                {
                    b.HasOne("DbLayer.ActionTarget", "ActionTarget")
                        .WithMany()
                        .HasForeignKey("ActionTargetIdActionTarget");

                    b.HasOne("DbLayer.EventSource", "EventSource")
                        .WithMany()
                        .HasForeignKey("EventSourceIdEventSource");
                });
#pragma warning restore 612, 618
        }
    }
}
