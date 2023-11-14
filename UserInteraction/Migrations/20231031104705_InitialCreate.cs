using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserInteraction.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    CartID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cart__51BCD797403469AD", x => x.CartID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orders__C3905BAFDC6FF50C", x => x.OrderID);
                });

            migrationBuilder.CreateTable(
                name: "WishList",
                columns: table => new
                {
                    WishListID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__WishList__E41F87A7D91FF381", x => x.WishListID);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    CartItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartID = table.Column<int>(type: "int", nullable: true),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CartItem__488B0B2AE8A9C086", x => x.CartItemID);
                    table.ForeignKey(
                        name: "FK__CartItems__CartI__4D94879B",
                        column: x => x.CartID,
                        principalTable: "Cart",
                        principalColumn: "CartID");
                });

            migrationBuilder.CreateTable(
                name: "WishListItems",
                columns: table => new
                {
                    WishListItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WishListID = table.Column<int>(type: "int", nullable: true),
                    ProductID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__WishList__DAC208299FAD83A6", x => x.WishListItemID);
                    table.ForeignKey(
                        name: "FK__WishListI__WishL__5441852A",
                        column: x => x.WishListID,
                        principalTable: "Cart",
                        principalColumn: "CartID");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: true),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderDet__D3B9D30CC55FD0F7", x => x.OrderDetailID);
                    table.ForeignKey(
                        name: "FK__OrderDeta__Order__46E78A0C",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartID",
                table: "CartItems",
                column: "CartID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderID",
                table: "OrderDetails",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_WishListItems_WishListID",
                table: "WishListItems",
                column: "WishListID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "WishList");

            migrationBuilder.DropTable(
                name: "WishListItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Cart");
        }
    }
}
