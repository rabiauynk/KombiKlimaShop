using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_add_relation_dealer_category : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DealerId",
                table: "Dealers",
                newName: "DealerID");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Dealers",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "Authorized",
                table: "Dealers",
                newName: "DealerOwner");

            migrationBuilder.RenameColumn(
                name: "DealerCategoryId",
                table: "DealerCategories",
                newName: "DealerCategoryID");

            migrationBuilder.RenameColumn(
                name: "ContactId",
                table: "Contacts",
                newName: "ContactID");

            migrationBuilder.AddColumn<string>(
                name: "DealerAddress",
                table: "Dealers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DealerCategoryID",
                table: "Dealers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DealerCity",
                table: "Dealers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DealerDistrict",
                table: "Dealers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Dealers_DealerCategoryID",
                table: "Dealers",
                column: "DealerCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Dealers_DealerCategories_DealerCategoryID",
                table: "Dealers",
                column: "DealerCategoryID",
                principalTable: "DealerCategories",
                principalColumn: "DealerCategoryID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dealers_DealerCategories_DealerCategoryID",
                table: "Dealers");

            migrationBuilder.DropIndex(
                name: "IX_Dealers_DealerCategoryID",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "DealerAddress",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "DealerCategoryID",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "DealerCity",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "DealerDistrict",
                table: "Dealers");

            migrationBuilder.RenameColumn(
                name: "DealerID",
                table: "Dealers",
                newName: "DealerId");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Dealers",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "DealerOwner",
                table: "Dealers",
                newName: "Authorized");

            migrationBuilder.RenameColumn(
                name: "DealerCategoryID",
                table: "DealerCategories",
                newName: "DealerCategoryId");

            migrationBuilder.RenameColumn(
                name: "ContactID",
                table: "Contacts",
                newName: "ContactId");
        }
    }
}
