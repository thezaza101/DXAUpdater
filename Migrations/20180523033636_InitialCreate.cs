using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DXAUpdater.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UpdatedData",
                columns: table => new
                {
                    DataID = table.Column<string>(nullable: false),
                    Active = table.Column<string>(nullable: true),
                    NextDataID = table.Column<string>(nullable: true),
                    Payload = table.Column<string>(nullable: true),
                    PayloadType = table.Column<string>(nullable: true),
                    UpdateDateTimeTicks = table.Column<string>(nullable: true),
                    UpdateDescription = table.Column<string>(nullable: true),
                    UpdatedDomain = table.Column<string>(nullable: true),
                    iUpdatedIdentifiers = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpdatedData", x => x.DataID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UpdatedData");
        }
    }
}
