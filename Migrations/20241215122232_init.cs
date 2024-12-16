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
                values: new object[] { new Guid("225b644a-1128-45b2-9007-34755c658051"), null, new DateTime(2024, 12, 15, 12, 22, 31, 634, DateTimeKind.Utc).AddTicks(5532), "cher@gmail.com", "Teach", "Cher", "$2a$11$agbHFLr6LSnpbutcyP.wau6M4G.WuRS0PbeK1Yg9IXAm/7obvj65C", null, new DateTime(2024, 12, 15, 12, 22, 31, 634, DateTimeKind.Utc).AddTicks(5785), "cher" });

            migrationBuilder.InsertData(
                table: "FlashcardDecks",
                columns: new[] { "Id", "CreatedAt", "Description", "Title", "UpdatedAt", "UserAccountId" },
                values: new object[] { new Guid("d591de67-3864-44cf-bfc0-442512793281"), new DateTime(2024, 12, 15, 12, 22, 31, 634, DateTimeKind.Utc).AddTicks(7324), "This is the first flashcard set", "Flashcard Set 1", new DateTime(2024, 12, 15, 12, 22, 31, 634, DateTimeKind.Utc).AddTicks(7476), new Guid("225b644a-1128-45b2-9007-34755c658051") });

            migrationBuilder.InsertData(
                table: "Flashcards",
                columns: new[] { "Id", "CreatedAt", "FlashcardDeckId", "Question", "UpdatedAt" },
                values: new object[] { new Guid("ce4195e3-96bc-44fe-8ae5-89219eaac44f"), new DateTime(2024, 12, 15, 12, 22, 31, 634, DateTimeKind.Utc).AddTicks(8437), new Guid("d591de67-3864-44cf-bfc0-442512793281"), "What is the capital of France?", new DateTime(2024, 12, 15, 12, 22, 31, 634, DateTimeKind.Utc).AddTicks(8575) });

            migrationBuilder.InsertData(
                table: "FlashcardChoices",
                columns: new[] { "Id", "Choice", "CreatedAt", "FlashcardId", "IsAnswer", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("13d67301-4769-46ba-80fe-e161aff6c3e3"), "Berlin", new DateTime(2024, 12, 15, 12, 22, 31, 634, DateTimeKind.Utc).AddTicks(9875), new Guid("ce4195e3-96bc-44fe-8ae5-89219eaac44f"), false, new DateTime(2024, 12, 15, 12, 22, 31, 634, DateTimeKind.Utc).AddTicks(9875) },
                    { new Guid("41a7f78f-db33-4ebb-86be-0a70e1a93942"), "London", new DateTime(2024, 12, 15, 12, 22, 31, 634, DateTimeKind.Utc).AddTicks(9872), new Guid("ce4195e3-96bc-44fe-8ae5-89219eaac44f"), false, new DateTime(2024, 12, 15, 12, 22, 31, 634, DateTimeKind.Utc).AddTicks(9873) },
                    { new Guid("e9a57ff8-66c3-4754-9595-3e5a04a99ac2"), "Paris", new DateTime(2024, 12, 15, 12, 22, 31, 634, DateTimeKind.Utc).AddTicks(9600), new Guid("ce4195e3-96bc-44fe-8ae5-89219eaac44f"), true, new DateTime(2024, 12, 15, 12, 22, 31, 634, DateTimeKind.Utc).AddTicks(9739) }
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
