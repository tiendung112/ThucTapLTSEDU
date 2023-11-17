using System;
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThucTapLTSEDU.Migrations
{
    /// <inheritdoc />
    public partial class TT_V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "decentralization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Authority_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_decentralization", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "order_status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status_name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    payment_method = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "product_type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name_product_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    image_type_product = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    decedecentralizationID = table.Column<int>(type: "int", nullable: true),
                    ResetPasswordToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetPasswordTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_account_decentralization_decedecentralizationID",
                        column: x => x.decedecentralizationID,
                        principalTable: "decentralization",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_typeID = table.Column<int>(type: "int", nullable: false),
                    name_product = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    price = table.Column<double>(type: "float", nullable: true),
                    avartar_image_product = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    discount = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    number_of_views = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_product_product_type_product_typeID",
                        column: x => x.product_typeID,
                        principalTable: "product_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Refresh_token",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiredTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    accountID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Refresh_token", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Refresh_token_account_accountID",
                        column: x => x.accountID,
                        principalTable: "account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    accountID = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_account_accountID",
                        column: x => x.accountID,
                        principalTable: "account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "XacNhanEmail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountID = table.Column<int>(type: "int", nullable: true),
                    ThoiGianHetHan = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaXacNhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DaXacNhan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XacNhanEmail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_XacNhanEmail_account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_user_userID",
                        column: x => x.userID,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    paymentID = table.Column<int>(type: "int", nullable: false),
                    userID = table.Column<int>(type: "int", nullable: false),
                    original_price = table.Column<double>(type: "float", nullable: true),
                    actual_price = table.Column<double>(type: "float", nullable: true),
                    full_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    order_statusID = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_order_status_order_statusID",
                        column: x => x.order_statusID,
                        principalTable: "order_status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_payment_paymentID",
                        column: x => x.paymentID,
                        principalTable: "payment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_user_userID",
                        column: x => x.userID,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_review",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productID = table.Column<int>(type: "int", nullable: false),
                    userID = table.Column<int>(type: "int", nullable: false),
                    content_rated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    point_evaluation = table.Column<int>(type: "int", nullable: false),
                    content_seen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_product_review_product_productID",
                        column: x => x.productID,
                        principalTable: "product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_review_user_userID",
                        column: x => x.userID,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cart_item",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: true),
                    cartID = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart_item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cart_item_Carts_cartID",
                        column: x => x.cartID,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cart_item_product_productId",
                        column: x => x.productId,
                        principalTable: "product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "order_detail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    orderID = table.Column<int>(type: "int", nullable: false),
                    productID = table.Column<int>(type: "int", nullable: true),
                    price_total = table.Column<double>(type: "float", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_detail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_detail_order_orderID",
                        column: x => x.orderID,
                        principalTable: "order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_detail_product_productID",
                        column: x => x.productID,
                        principalTable: "product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_account_decedecentralizationID",
                table: "account",
                column: "decedecentralizationID");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_item_cartID",
                table: "Cart_item",
                column: "cartID");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_item_productId",
                table: "Cart_item",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_userID",
                table: "Carts",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_order_order_statusID",
                table: "order",
                column: "order_statusID");

            migrationBuilder.CreateIndex(
                name: "IX_order_paymentID",
                table: "order",
                column: "paymentID");

            migrationBuilder.CreateIndex(
                name: "IX_order_userID",
                table: "order",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_order_detail_orderID",
                table: "order_detail",
                column: "orderID");

            migrationBuilder.CreateIndex(
                name: "IX_order_detail_productID",
                table: "order_detail",
                column: "productID");

            migrationBuilder.CreateIndex(
                name: "IX_product_product_typeID",
                table: "product",
                column: "product_typeID");

            migrationBuilder.CreateIndex(
                name: "IX_product_review_productID",
                table: "product_review",
                column: "productID");

            migrationBuilder.CreateIndex(
                name: "IX_product_review_userID",
                table: "product_review",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_Refresh_token_accountID",
                table: "Refresh_token",
                column: "accountID");

            migrationBuilder.CreateIndex(
                name: "IX_user_accountID",
                table: "user",
                column: "accountID");

            migrationBuilder.CreateIndex(
                name: "IX_XacNhanEmail_AccountID",
                table: "XacNhanEmail",
                column: "AccountID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cart_item");

            migrationBuilder.DropTable(
                name: "order_detail");

            migrationBuilder.DropTable(
                name: "product_review");

            migrationBuilder.DropTable(
                name: "Refresh_token");

            migrationBuilder.DropTable(
                name: "XacNhanEmail");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "order_status");

            migrationBuilder.DropTable(
                name: "payment");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "product_type");

            migrationBuilder.DropTable(
                name: "account");

            migrationBuilder.DropTable(
                name: "decentralization");
        }
    }
}
