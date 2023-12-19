using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace cliqx.gds.repository.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

           

            migrationBuilder.CreateTable(
                name: "UserForgetCodes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ForgetCode = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExpirationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserForgetCodes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            

            migrationBuilder.CreateTable(
                name: "PaymentServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApiBaseUrl = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CredentialsJsonObject = table.Column<string>(type: "varchar(8192)", maxLength: 8192, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExternalId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    UserCreatedId = table.Column<int>(type: "int", nullable: true),
                    UserUpdatedId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    InternalProperty = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentServices_AspNetUsers_UserCreatedId",
                        column: x => x.UserCreatedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentServices_AspNetUsers_UserUpdatedId",
                        column: x => x.UserUpdatedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Plugin",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExternalId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    UserCreatedId = table.Column<int>(type: "int", nullable: true),
                    UserUpdatedId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    InternalProperty = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plugin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plugin_AspNetUsers_UserCreatedId",
                        column: x => x.UserCreatedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Plugin_AspNetUsers_UserUpdatedId",
                        column: x => x.UserUpdatedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ShoppingCartServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApiBaseUrl = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CredentialsJsonObject = table.Column<string>(type: "varchar(8192)", maxLength: 8192, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExternalId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    UserCreatedId = table.Column<int>(type: "int", nullable: true),
                    UserUpdatedId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    InternalProperty = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCartServices_AspNetUsers_UserCreatedId",
                        column: x => x.UserCreatedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShoppingCartServices_AspNetUsers_UserUpdatedId",
                        column: x => x.UserUpdatedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

           

            migrationBuilder.CreateTable(
                name: "PluginConfiguration",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PluginId = table.Column<string>(type: "varchar(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApiBaseUrl = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CredentialsJsonObject = table.Column<string>(type: "varchar(8192)", maxLength: 8192, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Options = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TransactionName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TransactionLocator = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StoreId = table.Column<long>(type: "bigint", nullable: false),
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false),
                    PaymentServiceId = table.Column<int>(type: "int", nullable: false),
                    ExtraData = table.Column<string>(type: "varchar(4000)", maxLength: 4000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExternalId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    UserCreatedId = table.Column<int>(type: "int", nullable: true),
                    UserUpdatedId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    InternalProperty = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PluginConfiguration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PluginConfiguration_AspNetUsers_UserCreatedId",
                        column: x => x.UserCreatedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PluginConfiguration_AspNetUsers_UserUpdatedId",
                        column: x => x.UserUpdatedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PluginConfiguration_PaymentServices_PaymentServiceId",
                        column: x => x.PaymentServiceId,
                        principalTable: "PaymentServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PluginConfiguration_Plugin_PluginId",
                        column: x => x.PluginId,
                        principalTable: "Plugin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PluginConfiguration_ShoppingCartServices_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCartServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UsersPlugins",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Index = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PluginConfigurationsId = table.Column<string>(type: "varchar(36)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersPlugins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersPlugins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersPlugins_PluginConfiguration_PluginConfigurationsId",
                        column: x => x.PluginConfigurationsId,
                        principalTable: "PluginConfiguration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

          

            migrationBuilder.InsertData(
                table: "PaymentServices",
                columns: new[] { "Id", "ApiBaseUrl", "CredentialsJsonObject", "Description", "ExternalId", "InternalProperty", "IsActive", "Name", "UserCreatedId", "UserUpdatedId" },
                values: new object[,]
                {
                    { 1, "{\"UrlBaseTreeal\":\"https://bank-api-production-dot-snog-382317.ue.r.appspot.com/api/v1/bank\"}", "{\"TokenTreeal\":\"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiU25vZyIsImVudiI6InByb2QiLCJpZCI6ImU1ZWRmNzM2LTY2NjQtNGRiYi1iYTA0LTI5ZDY4NDFmZDBmZCJ9.OscGPowi60QeGSjdPX-09mIhqQ4EPxdEE4GN0jjGrNU\",\"PixKeyTreeal\":\"ce276502-0206-4b9c-abfe-57e9db60f01e\",\"ExpirationSecondsTreeal\":600,\"UrlReturnTreeal\":\"https://np33nn2ki5.execute-api.us-east-2.amazonaws.com/v1/webhook/treeal\"}", null, new Guid("f2f6921f-470b-442e-957f-80d61a0b951f"), false, true, "DefaultPay", null, null },
                    { 2, "{\"UrlBase\":\"https://checkout.cliqx.com.br\"}", "{\"Token\":\"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiU25vZyIsImVudiI6InByb2QiLCJpZCI6ImU1ZWRmNzM2LTY2NjQtNGRiYi1iYTA0LTI5ZDY4NDFmZDBmZCJ9.OscGPowi60QeGSjdPX-09mIhqQ4EPxdEE4GN0jjGrNU\",\"PixKey\":\"ce276502-0206-4b9c-abfe-57e9db60f01e\",\"ExpirationSeconds\":600,\"UrlReturn\":\"https://np33nn2ki5.execute-api.us-east-2.amazonaws.com/v1/webhook/treeal\",\"CartaoJuros\":0.0299,\"MaxParcelas\":\"3\",\"CartaoExpiracaoLink\":\"\",\"PixExpiracaoLink\":\"\"}", null, new Guid("feed115c-0ca8-4607-bec1-a9b91a8f2f55"), false, true, "FacilitaPay", null, null }
                });

            migrationBuilder.InsertData(
                table: "Plugin",
                columns: new[] { "Id", "ExternalId", "InternalProperty", "IsActive", "Name", "UserCreatedId", "UserUpdatedId" },
                values: new object[,]
                {
                    { "09a5347d-2eeb-4a6a-a5b8-dea9e9525b16", new Guid("cc13905e-b063-4496-a968-bab49a310adb"), false, true, "Smartbus", null, null },
                    { "1c7c73b5-a694-4426-b751-baa165b79ed1", new Guid("798f43ed-098d-4bc8-aa34-fff94fdeae12"), false, true, "ClickBus", null, null },
                    { "4434b7e9-942d-4fb0-bd4b-09e3a4408a41", new Guid("24f6d213-f507-4b60-ac4b-ecb63fa0f670"), false, true, "Distribusion", null, null },
                    { "8ecb43ae-7e34-4789-a6a3-1f3ea3884be5", new Guid("d99224d9-6746-4351-baa2-914b44c5f245"), false, true, "Rodosoft", null, null },
                    { "bcdf6d22-2823-478c-b44f-5fecb733b022", new Guid("b8717116-882d-48b7-b5da-5dbfdf596dd7"), false, true, "RjConsultores", null, null }
                });

            migrationBuilder.InsertData(
                table: "ShoppingCartServices",
                columns: new[] { "Id", "ApiBaseUrl", "CredentialsJsonObject", "Description", "ExternalId", "InternalProperty", "IsActive", "Name", "UserCreatedId", "UserUpdatedId" },
                values: new object[] { 1, null, null, null, new Guid("fcae4400-4309-44fd-89c9-beaeaf100223"), false, true, "RecargaPlus", null, null });

            migrationBuilder.InsertData(
                table: "PluginConfiguration",
                columns: new[] { "Id", "ApiBaseUrl", "CredentialsJsonObject", "Description", "ExternalId", "ExtraData", "InternalProperty", "IsActive", "Options", "PaymentServiceId", "PluginId", "ShoppingCartId", "StoreId", "TransactionLocator", "TransactionName", "UserCreatedId", "UserUpdatedId" },
                values: new object[,]
                {
                    { "39bd4c85-68a7-4c74-a8d5-bd90c21837f6", "{\"BaseUrl\":\"http://rjconsultores:2023/api\",\"UrlRodosoft\":\"http://localhost:2023/api\",\"BaseUrlRecargaPlus\":\"http://recargaplus:2023/api\"}", "{\"apiKey\":\"eyAasdas123\"}", "", new Guid("49237d22-b05a-4d47-a596-598dfae13d56"), null, false, true, null, 2, "8ecb43ae-7e34-4789-a6a3-1f3ea3884be5", 1, 1L, "RODOSOFT_LOCALIZADOR", "RODOSOFT_TRANSACAO", null, null },
                    { "9e4d313b-7041-43d1-9f22-87bc72c188a5", "{\"BaseUrl\":\"https://api.distribusion.com\",\"RecargaPlusUrl\":\"https://app.snog.com.br/recarga-plus\",\"DistribusionEtlUrl\":\"http://localhost:20231\"}", "{\"ApiKey\":\"o6tLyEqo12zLetXXyZ0eM2kKr2C1abrfe3jhO74j\",\"RecargaPlusStore\":\"1\",\"RetailerPartnerNumber\":\"609180\",\"SmtpPassword\":\"eyAasdas123\"}", "Distribusion", new Guid("326daf04-db35-4485-8a18-6478e2be522e"), null, false, true, null, 2, "4434b7e9-942d-4fb0-bd4b-09e3a4408a41", 1, 1L, "DISTRIBUSION_LOCALIZADOR", "DISTRIBUSION_TRANSACAO", null, null },
                    { "a3c2b595-d552-4e83-b538-365c09f62063", "{\"BaseUrl\":\"http://rjconsultores:2023/api\",\"UrlClickBus\":\"http://localhost:2023/api\",\"BaseUrlRecargaPlus\":\"http://recargaplus:2023/api\"}", "{\"apiKey\":\"eyAasdas123\"}", "", new Guid("4ac98bb8-ffbf-446b-b285-fe3727ed1166"), null, false, true, null, 2, "1c7c73b5-a694-4426-b751-baa165b79ed1", 1, 1L, "CLICKBUS_LOCALIZADOR", "CLICKBUS_TRANSACAO", null, null },
                    { "b420bf9b-068a-41f7-b726-dcc32898f399", "{\"BaseUrl\":\"http://3.141.251.80\"}", "{\"Authorization\":\"Basic c25vZzpzbm9n\",\"TenantId\":\"e9ef81ac-1fd0-40a2-84c0-df077dc46e4b\",\"RecargaPlusUser\":\"omshub-api\",\"RecargaPlusPassword\":\"999999\",\"RecargaPlusStore\":\"23\"}", "", new Guid("428a921d-2c0b-4846-a246-11f9a413c6a0"), null, false, true, null, 2, "bcdf6d22-2823-478c-b44f-5fecb733b022", 1, 1L, "RJ_LOCALIZADOR", "RJ_TRANSACAO", null, null },
                    { "f74146f1-9c82-4581-b2e8-ef75de6b7e14", "{\"BaseUrl\":\"https://prod-andorinha-gateway-smartbus.oreons.com/J3/clickbus\",\"UrlAuth\":\"http://prod-andorinha-gateway-smartbus.oreons.com:58677/OAuth\",\"UrlEtl\":\"https://app.snog.com.br/smartbus\"}", "{\"userName\":\"SNOG\",\"password\":\"SN@cc90Pxd\"}", "Smartbus Plugin", new Guid("e3f92adb-83c9-4772-9f49-a75a7913ccc5"), "{\"contrato\":\"MMS2021\"}", false, true, null, 2, "09a5347d-2eeb-4a6a-a5b8-dea9e9525b16", 1, 1L, "SMARTBUS_LOCALIZADOR", "SMARTBUS_TRANSACAO", null, null }
                });

            

           
            migrationBuilder.CreateIndex(
                name: "IX_PaymentServices_UserCreatedId",
                table: "PaymentServices",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentServices_UserUpdatedId",
                table: "PaymentServices",
                column: "UserUpdatedId");

            migrationBuilder.CreateIndex(
                name: "IX_Plugin_UserCreatedId",
                table: "Plugin",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_Plugin_UserUpdatedId",
                table: "Plugin",
                column: "UserUpdatedId");

            migrationBuilder.CreateIndex(
                name: "IX_PluginConfiguration_PaymentServiceId",
                table: "PluginConfiguration",
                column: "PaymentServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_PluginConfiguration_PluginId",
                table: "PluginConfiguration",
                column: "PluginId");

            migrationBuilder.CreateIndex(
                name: "IX_PluginConfiguration_ShoppingCartId",
                table: "PluginConfiguration",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_PluginConfiguration_UserCreatedId",
                table: "PluginConfiguration",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_PluginConfiguration_UserUpdatedId",
                table: "PluginConfiguration",
                column: "UserUpdatedId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartServices_UserCreatedId",
                table: "ShoppingCartServices",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartServices_UserUpdatedId",
                table: "ShoppingCartServices",
                column: "UserUpdatedId");


            migrationBuilder.CreateIndex(
                name: "IX_UsersPlugins_PluginConfigurationsId",
                table: "UsersPlugins",
                column: "PluginConfigurationsId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersPlugins_UserId",
                table: "UsersPlugins",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoinType");

            migrationBuilder.DropTable(
                name: "UserForgetCodes");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UsersPlugins");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "PluginConfiguration");

            migrationBuilder.DropTable(
                name: "PaymentServices");

            migrationBuilder.DropTable(
                name: "Plugin");

            migrationBuilder.DropTable(
                name: "ShoppingCartServices");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
