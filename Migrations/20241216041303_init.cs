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
                values: new object[] { new Guid("e9832949-5c3c-4678-ad08-b8abbf32e17c"), null, new DateTime(2024, 12, 16, 4, 13, 1, 847, DateTimeKind.Utc).AddTicks(513), "cher@gmail.com", "Teach", "Cher", "$2a$11$VoDF6zKA859j7uNqe02JV.2LrFAqbw3AHfWLyaQstjNhHa.SsfJDi", null, new DateTime(2024, 12, 16, 4, 13, 1, 847, DateTimeKind.Utc).AddTicks(734), "cher" });

            migrationBuilder.InsertData(
                table: "FlashcardDecks",
                columns: new[] { "Id", "CreatedAt", "Description", "Title", "UpdatedAt", "UserAccountId" },
                values: new object[] { new Guid("ffdb410f-5f2c-4c15-91b0-08d779689fc7"), new DateTime(2024, 12, 16, 4, 13, 1, 847, DateTimeKind.Utc).AddTicks(2121), "This is the first flashcard set", "Flashcard Set 1", new DateTime(2024, 12, 16, 4, 13, 1, 847, DateTimeKind.Utc).AddTicks(2256), new Guid("e9832949-5c3c-4678-ad08-b8abbf32e17c") });

            migrationBuilder.InsertData(
                table: "Flashcards",
                columns: new[] { "Id", "CreatedAt", "FlashcardDeckId", "Question", "UpdatedAt" },
                values: new object[] { new Guid("94e872b1-f671-402d-8db9-75f9d325c2e8"), new DateTime(2024, 12, 16, 4, 13, 1, 847, DateTimeKind.Utc).AddTicks(3104), new Guid("ffdb410f-5f2c-4c15-91b0-08d779689fc7"), "What is the capital of France?", new DateTime(2024, 12, 16, 4, 13, 1, 847, DateTimeKind.Utc).AddTicks(3283) });

            migrationBuilder.InsertData(
                table: "FlashcardChoices",
                columns: new[] { "Id", "Choice", "CreatedAt", "FlashcardId", "IsAnswer", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("35e72846-6482-4155-abf8-1863105bbffd"), "Berlin", new DateTime(2024, 12, 16, 4, 13, 1, 847, DateTimeKind.Utc).AddTicks(4591), new Guid("94e872b1-f671-402d-8db9-75f9d325c2e8"), false, new DateTime(2024, 12, 16, 4, 13, 1, 847, DateTimeKind.Utc).AddTicks(4591) },
                    { new Guid("9bb18ba2-3670-486e-8f55-0607fad14a14"), "London", new DateTime(2024, 12, 16, 4, 13, 1, 847, DateTimeKind.Utc).AddTicks(4588), new Guid("94e872b1-f671-402d-8db9-75f9d325c2e8"), false, new DateTime(2024, 12, 16, 4, 13, 1, 847, DateTimeKind.Utc).AddTicks(4588) },
                    { new Guid("f1dc90e8-b429-4f69-b5e6-b74b05e032e0"), "Paris", new DateTime(2024, 12, 16, 4, 13, 1, 847, DateTimeKind.Utc).AddTicks(4250), new Guid("94e872b1-f671-402d-8db9-75f9d325c2e8"), true, new DateTime(2024, 12, 16, 4, 13, 1, 847, DateTimeKind.Utc).AddTicks(4377) }
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
