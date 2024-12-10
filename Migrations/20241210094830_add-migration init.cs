using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ScholarMeServer.Migrations
{
    /// <inheritdoc />
    public partial class addmigrationinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlashcardDecks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserAccountId = table.Column<int>(type: "integer", nullable: false),
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
                name: "Flashcards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FlashcardDeckId = table.Column<int>(type: "integer", nullable: false),
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FlashcardId = table.Column<int>(type: "integer", nullable: false),
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
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "UpdatedAt", "Username" },
                values: new object[] { 1, new DateTime(2024, 12, 10, 9, 48, 28, 984, DateTimeKind.Utc).AddTicks(2635), "cher@gmail.com", "Teach", "Cher", "$2a$11$gmNDL/jS7gca2lzEiL25j.HBKUjS.YT2XBRvhURmGxm8TDZyY.dCK", null, new DateTime(2024, 12, 10, 9, 48, 28, 984, DateTimeKind.Utc).AddTicks(2894), "cher" });

            migrationBuilder.InsertData(
                table: "FlashcardDecks",
                columns: new[] { "Id", "CreatedAt", "Description", "Title", "UpdatedAt", "UserAccountId" },
                values: new object[] { 1, new DateTime(2024, 12, 10, 9, 48, 28, 985, DateTimeKind.Utc).AddTicks(636), "This is the first flashcard set", "Flashcard Set 1", new DateTime(2024, 12, 10, 9, 48, 28, 985, DateTimeKind.Utc).AddTicks(809), 1 });

            migrationBuilder.InsertData(
                table: "Flashcards",
                columns: new[] { "Id", "CreatedAt", "FlashcardDeckId", "Question", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2024, 12, 10, 9, 48, 28, 985, DateTimeKind.Utc).AddTicks(1925), 1, "What is the capital of France?", new DateTime(2024, 12, 10, 9, 48, 28, 985, DateTimeKind.Utc).AddTicks(2100) });

            migrationBuilder.InsertData(
                table: "FlashcardChoices",
                columns: new[] { "Id", "Choice", "CreatedAt", "FlashcardId", "IsAnswer", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Paris", new DateTime(2024, 12, 10, 9, 48, 28, 985, DateTimeKind.Utc).AddTicks(3110), 1, true, new DateTime(2024, 12, 10, 9, 48, 28, 985, DateTimeKind.Utc).AddTicks(3244) },
                    { 2, "London", new DateTime(2024, 12, 10, 9, 48, 28, 985, DateTimeKind.Utc).AddTicks(3402), 1, false, new DateTime(2024, 12, 10, 9, 48, 28, 985, DateTimeKind.Utc).AddTicks(3402) },
                    { 3, "Berlin", new DateTime(2024, 12, 10, 9, 48, 28, 985, DateTimeKind.Utc).AddTicks(3404), 1, false, new DateTime(2024, 12, 10, 9, 48, 28, 985, DateTimeKind.Utc).AddTicks(3404) }
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlashcardChoices");

            migrationBuilder.DropTable(
                name: "Flashcards");

            migrationBuilder.DropTable(
                name: "FlashcardDecks");

            migrationBuilder.DropTable(
                name: "UserAccounts");
        }
    }
}
