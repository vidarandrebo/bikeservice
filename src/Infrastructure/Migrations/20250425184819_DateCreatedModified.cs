using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DateCreatedModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "ServiceNotes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "ServiceNotes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Parts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Parts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "EquipmentTypes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "EquipmentTypes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Bikes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Bikes",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "ServiceNotes");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "ServiceNotes");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "EquipmentTypes");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "EquipmentTypes");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Bikes");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Bikes");
        }
    }
}
