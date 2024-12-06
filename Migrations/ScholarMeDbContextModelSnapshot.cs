﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RestTest;

#nullable disable

namespace ScholarMeServer.Migrations
{
    [DbContext(typeof(ScholarMeDbContext))]
    partial class ScholarMeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RestTest.Models.Flashcard", b =>
                {
                    b.Property<int>("FlashcardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FlashcardId"));

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int?>("FlashcardSetId")
                        .HasColumnType("int");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("FlashcardId");

                    b.HasIndex("FlashcardSetId");

                    b.HasIndex("UserId");

                    b.ToTable("Flashcards");
                });

            modelBuilder.Entity("RestTest.Models.FlashcardChoice", b =>
                {
                    b.Property<int>("FlashcardChoiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FlashcardChoiceId"));

                    b.Property<string>("Choice")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int>("FlashcardId")
                        .HasColumnType("int");

                    b.Property<bool>("IsAnswer")
                        .HasColumnType("bit");

                    b.HasKey("FlashcardChoiceId");

                    b.HasIndex("FlashcardId");

                    b.ToTable("FlashcardsChoice");
                });

            modelBuilder.Entity("RestTest.Models.FlashcardSet", b =>
                {
                    b.Property<int>("FlashcardSetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FlashcardSetId"));

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("FlashcardSetId");

                    b.HasIndex("UserId");

                    b.ToTable("FlashcardSets");
                });

            modelBuilder.Entity("RestTest.Models.FlashcardSetFlashcard", b =>
                {
                    b.Property<int>("FlashcardSetFlashcardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FlashcardSetFlashcardId"));

                    b.Property<int>("FlashcardId")
                        .HasColumnType("int");

                    b.Property<int>("FlashcardSetId")
                        .HasColumnType("int");

                    b.HasKey("FlashcardSetFlashcardId");

                    b.HasIndex("FlashcardId");

                    b.HasIndex("FlashcardSetId");

                    b.ToTable("FlashcardSetFlashcards");
                });

            modelBuilder.Entity("RestTest.Models.UserAccount", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("ProfilePic")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            DateAdded = new DateTime(2024, 12, 6, 20, 44, 43, 906, DateTimeKind.Local).AddTicks(380),
                            DateUpdated = new DateTime(2024, 12, 6, 20, 44, 43, 906, DateTimeKind.Local).AddTicks(7055),
                            Email = "cher@gmail.com",
                            FirstName = "Teach",
                            LastName = "Cher",
                            Password = "$2a$16$mV3mrjCPDkm8JOODTVHyRukC32fkboTLvLtifz1Tyc1ypTCzLMBxu",
                            PhoneNumber = "01234567890",
                            Role = 1,
                            Status = 0,
                            Username = "Cher"
                        },
                        new
                        {
                            UserId = 2,
                            DateAdded = new DateTime(2024, 12, 6, 20, 44, 47, 882, DateTimeKind.Local).AddTicks(1981),
                            DateUpdated = new DateTime(2024, 12, 6, 20, 44, 47, 882, DateTimeKind.Local).AddTicks(1995),
                            Email = "perpy@gmail.com",
                            FirstName = "Van",
                            LastName = "Perpetua",
                            Password = "$2a$16$.TjxBlMVRCbCTJmY3kFj1OPEXT4VE5WRghVANZ9rr3A4qNIcHk7sa",
                            PhoneNumber = "01234567890",
                            Role = 0,
                            Status = 0,
                            Username = "Perpy"
                        },
                        new
                        {
                            UserId = 3,
                            DateAdded = new DateTime(2024, 12, 6, 20, 44, 51, 676, DateTimeKind.Local).AddTicks(2609),
                            DateUpdated = new DateTime(2024, 12, 6, 20, 44, 51, 676, DateTimeKind.Local).AddTicks(2625),
                            Email = "junsayke@gmail.com",
                            FirstName = "Tonio",
                            LastName = "Ubaldo",
                            Password = "$2a$16$.bbZicM7YpDYBEF5jlduo.pfIsedQ/6EbrwXwpg5ad3P0B/7DRvHS",
                            PhoneNumber = "01234567890",
                            Role = 0,
                            Status = 0,
                            Username = "Junsayke"
                        },
                        new
                        {
                            UserId = 4,
                            DateAdded = new DateTime(2024, 12, 6, 20, 44, 55, 549, DateTimeKind.Local).AddTicks(5251),
                            DateUpdated = new DateTime(2024, 12, 6, 20, 44, 55, 549, DateTimeKind.Local).AddTicks(5267),
                            Email = "modarku@gmail.com",
                            FirstName = "Jian",
                            LastName = "Olamit",
                            Password = "$2a$16$wQjTY.ELQ8r7PpmL92/Jzu3PIycrTFDbmVvi8laJZCW9OZMiSDsoa",
                            PhoneNumber = "01234567890",
                            Role = 0,
                            Status = 1,
                            Username = "Modarku"
                        });
                });

            modelBuilder.Entity("RestTest.Models.Flashcard", b =>
                {
                    b.HasOne("RestTest.Models.FlashcardSet", null)
                        .WithMany("Flashcards")
                        .HasForeignKey("FlashcardSetId");

                    b.HasOne("RestTest.Models.UserAccount", "UserAccount")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("RestTest.Models.FlashcardChoice", b =>
                {
                    b.HasOne("RestTest.Models.Flashcard", "Flashcard")
                        .WithMany()
                        .HasForeignKey("FlashcardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flashcard");
                });

            modelBuilder.Entity("RestTest.Models.FlashcardSet", b =>
                {
                    b.HasOne("RestTest.Models.UserAccount", "UserAccount")
                        .WithMany("FlashcardSets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("RestTest.Models.FlashcardSetFlashcard", b =>
                {
                    b.HasOne("RestTest.Models.Flashcard", "Flashcard")
                        .WithMany()
                        .HasForeignKey("FlashcardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RestTest.Models.FlashcardSet", "FlashcardSet")
                        .WithMany()
                        .HasForeignKey("FlashcardSetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flashcard");

                    b.Navigation("FlashcardSet");
                });

            modelBuilder.Entity("RestTest.Models.FlashcardSet", b =>
                {
                    b.Navigation("Flashcards");
                });

            modelBuilder.Entity("RestTest.Models.UserAccount", b =>
                {
                    b.Navigation("FlashcardSets");
                });
#pragma warning restore 612, 618
        }
    }
}
