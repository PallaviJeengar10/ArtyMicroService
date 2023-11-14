using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateCatalog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__19093A2B04468055", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    SubCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubCategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SubCateg__26BE5BF9A9C4A67F", x => x.SubCategoryID);
                    table.ForeignKey(
                        name: "FK__SubCatego__Categ__3E52440B",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SubCategoryID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Products__B40CC6EDECD06F17", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK__Products__SubCat__412EB0B6",
                        column: x => x.SubCategoryID,
                        principalTable: "SubCategories",
                        principalColumn: "SubCategoryID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubCategoryID",
                table: "Products",
                column: "SubCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryID",
                table: "SubCategories",
                column: "CategoryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
