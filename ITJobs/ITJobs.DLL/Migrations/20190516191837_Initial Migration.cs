using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ITJobs.DLL.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    BussinesType = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    AboutCompany = table.Column<string>(nullable: true),
                    LogoLink = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    WebAddress = table.Column<string>(nullable: true),
                    NumberOfEmployees = table.Column<int>(nullable: false),
                    Established = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataBase",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataBase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Individual",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Biography = table.Column<string>(nullable: true),
                    Skills = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Individual", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProgrammingLanguage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammingLanguage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ad",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    AdCategoryId = table.Column<int>(nullable: true),
                    CompanyId = table.Column<int>(nullable: true),
                    JobSummary = table.Column<string>(nullable: true),
                    RequiredSkills = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    NumberOfWorkingHours = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ad_AdCategory_AdCategoryId",
                        column: x => x.AdCategoryId,
                        principalTable: "AdCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ad_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IndividualDataBase",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IndividualId = table.Column<int>(nullable: true),
                    DataBaseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualDataBase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndividualDataBase_DataBase_DataBaseId",
                        column: x => x.DataBaseId,
                        principalTable: "DataBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IndividualDataBase_Individual_IndividualId",
                        column: x => x.IndividualId,
                        principalTable: "Individual",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InduvidualProgrammingLanguage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IndividualId = table.Column<int>(nullable: true),
                    ProgrammingLanguageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InduvidualProgrammingLanguage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InduvidualProgrammingLanguage_Individual_IndividualId",
                        column: x => x.IndividualId,
                        principalTable: "Individual",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InduvidualProgrammingLanguage_ProgrammingLanguage_ProgrammingLanguageId",
                        column: x => x.ProgrammingLanguageId,
                        principalTable: "ProgrammingLanguage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ad_AdCategoryId",
                table: "Ad",
                column: "AdCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Ad_CompanyId",
                table: "Ad",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualDataBase_DataBaseId",
                table: "IndividualDataBase",
                column: "DataBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualDataBase_IndividualId",
                table: "IndividualDataBase",
                column: "IndividualId");

            migrationBuilder.CreateIndex(
                name: "IX_InduvidualProgrammingLanguage_IndividualId",
                table: "InduvidualProgrammingLanguage",
                column: "IndividualId");

            migrationBuilder.CreateIndex(
                name: "IX_InduvidualProgrammingLanguage_ProgrammingLanguageId",
                table: "InduvidualProgrammingLanguage",
                column: "ProgrammingLanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ad");

            migrationBuilder.DropTable(
                name: "IndividualDataBase");

            migrationBuilder.DropTable(
                name: "InduvidualProgrammingLanguage");

            migrationBuilder.DropTable(
                name: "AdCategory");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "DataBase");

            migrationBuilder.DropTable(
                name: "Individual");

            migrationBuilder.DropTable(
                name: "ProgrammingLanguage");
        }
    }
}
