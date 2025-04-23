using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeService.Migrations.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceNote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceNote_Bikes_BikeId",
                table: "ServiceNote");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceNote_Parts_PartId",
                table: "ServiceNote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceNote",
                table: "ServiceNote");

            migrationBuilder.RenameTable(
                name: "ServiceNote",
                newName: "ServiceNotes");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceNote_PartId",
                table: "ServiceNotes",
                newName: "IX_ServiceNotes_PartId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceNote_BikeId",
                table: "ServiceNotes",
                newName: "IX_ServiceNotes_BikeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceNotes",
                table: "ServiceNotes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceNotes_Bikes_BikeId",
                table: "ServiceNotes",
                column: "BikeId",
                principalTable: "Bikes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceNotes_Parts_PartId",
                table: "ServiceNotes",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceNotes_Bikes_BikeId",
                table: "ServiceNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceNotes_Parts_PartId",
                table: "ServiceNotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceNotes",
                table: "ServiceNotes");

            migrationBuilder.RenameTable(
                name: "ServiceNotes",
                newName: "ServiceNote");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceNotes_PartId",
                table: "ServiceNote",
                newName: "IX_ServiceNote_PartId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceNotes_BikeId",
                table: "ServiceNote",
                newName: "IX_ServiceNote_BikeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceNote",
                table: "ServiceNote",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceNote_Bikes_BikeId",
                table: "ServiceNote",
                column: "BikeId",
                principalTable: "Bikes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceNote_Parts_PartId",
                table: "ServiceNote",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id");
        }
    }
}
