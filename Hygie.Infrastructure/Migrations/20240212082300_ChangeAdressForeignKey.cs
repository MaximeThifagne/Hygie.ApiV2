using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hygie.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAdressForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Adresses_AdrressId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "AdrressId",
                table: "AspNetUsers",
                newName: "AdressId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_AdrressId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_AdressId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Adresses_AdressId",
                table: "AspNetUsers",
                column: "AdressId",
                principalTable: "Adresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Adresses_AdressId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "AdressId",
                table: "AspNetUsers",
                newName: "AdrressId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_AdressId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_AdrressId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Adresses_AdrressId",
                table: "AspNetUsers",
                column: "AdrressId",
                principalTable: "Adresses",
                principalColumn: "Id");
        }
    }
}
