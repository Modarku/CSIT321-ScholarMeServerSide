using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ScholarMeServer.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    ProfilePic = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "FlashcardSets",
                columns: table => new
                {
                    FlashcardSetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlashcardSets", x => x.FlashcardSetId);
                    table.ForeignKey(
                        name: "FK_FlashcardSets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Flashcards",
                columns: table => new
                {
                    FlashcardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FlashcardSetId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flashcards", x => x.FlashcardId);
                    table.ForeignKey(
                        name: "FK_Flashcards_FlashcardSets_FlashcardSetId",
                        column: x => x.FlashcardSetId,
                        principalTable: "FlashcardSets",
                        principalColumn: "FlashcardSetId");
                    table.ForeignKey(
                        name: "FK_Flashcards_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlashcardsChoice",
                columns: table => new
                {
                    FlashcardChoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlashcardId = table.Column<int>(type: "int", nullable: false),
                    Choice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAnswer = table.Column<bool>(type: "bit", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlashcardsChoice", x => x.FlashcardChoiceId);
                    table.ForeignKey(
                        name: "FK_FlashcardsChoice_Flashcards_FlashcardId",
                        column: x => x.FlashcardId,
                        principalTable: "Flashcards",
                        principalColumn: "FlashcardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlashcardSetFlashcards",
                columns: table => new
                {
                    FlashcardSetFlashcardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlashcardSetId = table.Column<int>(type: "int", nullable: false),
                    FlashcardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlashcardSetFlashcards", x => x.FlashcardSetFlashcardId);
                    table.ForeignKey(
                        name: "FK_FlashcardSetFlashcards_FlashcardSets_FlashcardSetId",
                        column: x => x.FlashcardSetId,
                        principalTable: "FlashcardSets",
                        principalColumn: "FlashcardSetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlashcardSetFlashcards_Flashcards_FlashcardId",
                        column: x => x.FlashcardId,
                        principalTable: "Flashcards",
                        principalColumn: "FlashcardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "DateAdded", "DateUpdated", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "ProfilePic", "Role", "Status", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 6, 20, 44, 43, 906, DateTimeKind.Local).AddTicks(380), new DateTime(2024, 12, 6, 20, 44, 43, 906, DateTimeKind.Local).AddTicks(7055), "cher@gmail.com", "Teach", "Cher", "$2a$16$mV3mrjCPDkm8JOODTVHyRukC32fkboTLvLtifz1Tyc1ypTCzLMBxu", "01234567890", null, 1, 0, "Cher" },
                    { 2, new DateTime(2024, 12, 6, 20, 44, 47, 882, DateTimeKind.Local).AddTicks(1981), new DateTime(2024, 12, 6, 20, 44, 47, 882, DateTimeKind.Local).AddTicks(1995), "perpy@gmail.com", "Van", "Perpetua", "$2a$16$.TjxBlMVRCbCTJmY3kFj1OPEXT4VE5WRghVANZ9rr3A4qNIcHk7sa", "01234567890", null, 0, 0, "Perpy" },
                    { 3, new DateTime(2024, 12, 6, 20, 44, 51, 676, DateTimeKind.Local).AddTicks(2609), new DateTime(2024, 12, 6, 20, 44, 51, 676, DateTimeKind.Local).AddTicks(2625), "junsayke@gmail.com", "Tonio", "Ubaldo", "$2a$16$.bbZicM7YpDYBEF5jlduo.pfIsedQ/6EbrwXwpg5ad3P0B/7DRvHS", "01234567890", null, 0, 0, "Junsayke" },
                    { 4, new DateTime(2024, 12, 6, 20, 44, 55, 549, DateTimeKind.Local).AddTicks(5251), new DateTime(2024, 12, 6, 20, 44, 55, 549, DateTimeKind.Local).AddTicks(5267), "modarku@gmail.com", "Jian", "Olamit", "$2a$16$wQjTY.ELQ8r7PpmL92/Jzu3PIycrTFDbmVvi8laJZCW9OZMiSDsoa", "01234567890", null, 0, 1, "Modarku" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flashcards_FlashcardSetId",
                table: "Flashcards",
                column: "FlashcardSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Flashcards_UserId",
                table: "Flashcards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FlashcardsChoice_FlashcardId",
                table: "FlashcardsChoice",
                column: "FlashcardId");

            migrationBuilder.CreateIndex(
                name: "IX_FlashcardSetFlashcards_FlashcardId",
                table: "FlashcardSetFlashcards",
                column: "FlashcardId");

            migrationBuilder.CreateIndex(
                name: "IX_FlashcardSetFlashcards_FlashcardSetId",
                table: "FlashcardSetFlashcards",
                column: "FlashcardSetId");

            migrationBuilder.CreateIndex(
                name: "IX_FlashcardSets_UserId",
                table: "FlashcardSets",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlashcardsChoice");

            migrationBuilder.DropTable(
                name: "FlashcardSetFlashcards");

            migrationBuilder.DropTable(
                name: "Flashcards");

            migrationBuilder.DropTable(
                name: "FlashcardSets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
