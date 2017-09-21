using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EF1DemoSchemaUpdater.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActionTargets",
                columns: table => new
                {
                    IdActionTarget = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateLastEdited = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionTargets", x => x.IdActionTarget);
                });

            migrationBuilder.CreateTable(
                name: "EventSources",
                columns: table => new
                {
                    IdEventSource = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateLastEdited = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSources", x => x.IdEventSource);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    IdEvent = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActionTargetIdActionTarget = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateLastEdited = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Duration = table.Column<long>(type: "bigint", nullable: false),
                    EndTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EventSourceIdEventSource = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StartTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.IdEvent);
                    table.ForeignKey(
                        name: "FK_Events_ActionTargets_ActionTargetIdActionTarget",
                        column: x => x.ActionTargetIdActionTarget,
                        principalTable: "ActionTargets",
                        principalColumn: "IdActionTarget",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Events_EventSources_EventSourceIdEventSource",
                        column: x => x.EventSourceIdEventSource,
                        principalTable: "EventSources",
                        principalColumn: "IdEventSource",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_ActionTargetIdActionTarget",
                table: "Events",
                column: "ActionTargetIdActionTarget");

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventSourceIdEventSource",
                table: "Events",
                column: "EventSourceIdEventSource");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "ActionTargets");

            migrationBuilder.DropTable(
                name: "EventSources");
        }
    }
}
