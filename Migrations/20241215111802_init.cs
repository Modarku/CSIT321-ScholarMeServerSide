using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ScholarMeServer.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AvatarPath = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlashcardDecks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserAccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlashcardDecks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlashcardDecks_UserAccounts_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserAccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    ExpiresOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_UserAccounts_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Flashcards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FlashcardDeckId = table.Column<Guid>(type: "uuid", nullable: false),
                    Question = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flashcards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flashcards_FlashcardDecks_FlashcardDeckId",
                        column: x => x.FlashcardDeckId,
                        principalTable: "FlashcardDecks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlashcardChoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FlashcardId = table.Column<Guid>(type: "uuid", nullable: false),
                    Choice = table.Column<string>(type: "text", nullable: false),
                    IsAnswer = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlashcardChoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlashcardChoices_Flashcards_FlashcardId",
                        column: x => x.FlashcardId,
                        principalTable: "Flashcards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "UserAccounts",
                columns: new[] { "Id", "AvatarPath", "CreatedAt", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "UpdatedAt", "Username" },
                values: new object[] { new Guid("535f49f4-bd0f-4bf4-aa48-7fca28f329e9"), null, new DateTime(2024, 12, 15, 11, 18, 1, 772, DateTimeKind.Utc).AddTicks(1033), "cher@gmail.com", "Teach", "Cher", "$2a$11$4ea5m6VbQnQtlEP36QTh9etC2gcbBg.v9V4gPXOs/1H1j.rstd5Zu", null, new DateTime(2024, 12, 15, 11, 18, 1, 772, DateTimeKind.Utc).AddTicks(1177), "cher" });

            migrationBuilder.InsertData(
                table: "FlashcardDecks",
                columns: new[] { "Id", "CreatedAt", "Description", "Title", "UpdatedAt", "UserAccountId" },
                values: new object[] { new Guid("aab90a73-75c1-4e61-b948-02d3397e21ee"), new DateTime(2024, 12, 15, 11, 18, 1, 772, DateTimeKind.Utc).AddTicks(1907), "This is the first flashcard set", "Flashcard Set 1", new DateTime(2024, 12, 15, 11, 18, 1, 772, DateTimeKind.Utc).AddTicks(1975), new Guid("535f49f4-bd0f-4bf4-aa48-7fca28f329e9") });

            migrationBuilder.InsertData(
                table: "Flashcards",
                columns: new[] { "Id", "CreatedAt", "FlashcardDeckId", "Question", "UpdatedAt" },
                values: new object[] { new Guid("41e2e4af-abd9-4832-ae73-86798d0e534b"), new DateTime(2024, 12, 15, 11, 18, 1, 772, DateTimeKind.Utc).AddTicks(2410), new Guid("aab90a73-75c1-4e61-b948-02d3397e21ee"), "What is the capital of France?", new DateTime(2024, 12, 15, 11, 18, 1, 772, DateTimeKind.Utc).AddTicks(2473) });

            migrationBuilder.InsertData(
                table: "FlashcardChoices",
                columns: new[] { "Id", "Choice", "CreatedAt", "FlashcardId", "IsAnswer", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("9649769f-9c9b-4175-b668-b2bc104a5b58"), "Berlin", new DateTime(2024, 12, 15, 11, 18, 1, 772, DateTimeKind.Utc).AddTicks(3062), new Guid("41e2e4af-abd9-4832-ae73-86798d0e534b"), false, new DateTime(2024, 12, 15, 11, 18, 1, 772, DateTimeKind.Utc).AddTicks(3062) },
                    { new Guid("9b8839c3-58f1-4518-be94-4f923dc2f52c"), "London", new DateTime(2024, 12, 15, 11, 18, 1, 772, DateTimeKind.Utc).AddTicks(3060), new Guid("41e2e4af-abd9-4832-ae73-86798d0e534b"), false, new DateTime(2024, 12, 15, 11, 18, 1, 772, DateTimeKind.Utc).AddTicks(3060) },
                    { new Guid("c2eee04b-f123-4809-915e-1db2032c20d9"), "Paris", new DateTime(2024, 12, 15, 11, 18, 1, 772, DateTimeKind.Utc).AddTicks(2934), new Guid("41e2e4af-abd9-4832-ae73-86798d0e534b"), true, new DateTime(2024, 12, 15, 11, 18, 1, 772, DateTimeKind.Utc).AddTicks(2997) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlashcardChoices_FlashcardId",
                table: "FlashcardChoices",
                column: "FlashcardId");

            migrationBuilder.CreateIndex(
                name: "IX_FlashcardDecks_UserAccountId",
                table: "FlashcardDecks",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Flashcards_FlashcardDeckId",
                table: "Flashcards",
                column: "FlashcardDeckId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserAccountId",
                table: "RefreshTokens",
                column: "UserAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlashcardChoices");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Flashcards");

            migrationBuilder.DropTable(
                name: "FlashcardDecks");

            migrationBuilder.DropTable(
                name: "UserAccounts");
        }
    }
}
