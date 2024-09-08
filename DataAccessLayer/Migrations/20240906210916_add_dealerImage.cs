using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class add_dealerImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Dealers",
                newName: "Phone2");

            migrationBuilder.RenameColumn(
                name: "ImageUrls",
                table: "Dealers",
                newName: "Phone1");

            migrationBuilder.CreateTable(
                name: "DealerImages",
                columns: table => new
                {
                    DealerImageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Index = table.Column<int>(type: "int", nullable: false),
                    DealerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DealerImages", x => x.DealerImageID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DealerImages");

            migrationBuilder.RenameColumn(
                name: "Phone2",
                table: "Dealers",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "Phone1",
                table: "Dealers",
                newName: "ImageUrls");
        }
    }
}
