using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Toastmasters.Web.Migrations
{
    public partial class AddSpeeches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Speeches",
                columns: table => new
                {
                    SpeechID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    MemberID = table.Column<int>(nullable: false),
                    Project = table.Column<string>(nullable: true),
                    TimeConstraints = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speeches", x => x.SpeechID);
                });

            migrationBuilder.AddColumn<int>(
                name: "SpeechIISpeechID",
                table: "Meetings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpeechISpeechID",
                table: "Meetings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_SpeechIISpeechID",
                table: "Meetings",
                column: "SpeechIISpeechID");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_SpeechISpeechID",
                table: "Meetings",
                column: "SpeechISpeechID");

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_Speeches_SpeechIISpeechID",
                table: "Meetings",
                column: "SpeechIISpeechID",
                principalTable: "Speeches",
                principalColumn: "SpeechID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_Speeches_SpeechISpeechID",
                table: "Meetings",
                column: "SpeechISpeechID",
                principalTable: "Speeches",
                principalColumn: "SpeechID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_Speeches_SpeechIISpeechID",
                table: "Meetings");

            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_Speeches_SpeechISpeechID",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_SpeechIISpeechID",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_SpeechISpeechID",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "SpeechIISpeechID",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "SpeechISpeechID",
                table: "Meetings");

            migrationBuilder.DropTable(
                name: "Speeches");
        }
    }
}
