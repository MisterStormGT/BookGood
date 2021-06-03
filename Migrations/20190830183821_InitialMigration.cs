using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContosoUniversity.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
            name: "Sec",
            columns: table => new
            {
                SectionID = table.Column<int>(nullable: false)
             .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                SectionName = table.Column<string>(maxLength: 100, nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Sec", x => x.SectionID);
            });

            migrationBuilder.CreateTable(
                name: "Pub",
                columns: table => new
                {
                    PublisherID = table.Column<int>(nullable: false)
                 .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PublishingCity = table.Column<string>(maxLength: 100, nullable: true),
                    PublisherName = table.Column<string>(maxLength: 100, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pub", x => x.PublisherID);
                });

            migrationBuilder.CreateTable(
           name: "Authore",
           columns: table => new
           {
               AuthorID = table.Column<int>(nullable: false)
            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
               Surname = table.Column<string>(maxLength: 100, nullable: false),
               Name = table.Column<string>(maxLength: 100, nullable: false),
               MiddleName = table.Column<string>(maxLength: 100, nullable: true),
           },
           constraints: table =>
           {
               table.PrimaryKey("PK_Authore", x => x.AuthorID);
           });

            migrationBuilder.CreateTable(
                name: "Booked",
                columns: table => new
                {
                    BookID = table.Column<int>(nullable: false)
            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthorID = table.Column<int>(nullable: false),
                    SectionID = table.Column<int>(nullable: true),
                    PublisherID = table.Column<int>(nullable: true),
                    BookName = table.Column<string>(maxLength: 100, nullable: false),
                    YearOfPublishing = table.Column<DateTime>(maxLength: 100, nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booked", x => x.BookID);
                    table.ForeignKey(
                        name: "FK_Booked_Author_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "Authore",
                        principalColumn: "AuthorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Booked_Section_SectionID",
                        column: x => x.SectionID,
                        principalTable: "Sec",
                        principalColumn: "SectionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Booked_Publicher_PublisherID",
                        column: x => x.PublisherID,
                        principalTable: "Pub",
                        principalColumn: "PublisherID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
               name: "IX_Booked_AuthorID",
               table: "Booked",
               column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Booked_SectionID",
                table: "Booked",
               column: "SectionID");

            migrationBuilder.CreateIndex(
               name: "IX_Booked_PublisherID",
               table: "Booked",
               column: "PublisherID");

        }


protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sec");

            migrationBuilder.DropTable(
                name: "Pub");

            migrationBuilder.DropTable(
               name: "Authore");

            migrationBuilder.DropTable(
               name: "Booked");
        }
    }
}
