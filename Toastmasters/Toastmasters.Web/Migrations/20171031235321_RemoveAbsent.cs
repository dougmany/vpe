using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Toastmasters.Web.Migrations
{
    public partial class RemoveAbsent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_Members_AbsentIIMemberID",
                table: "Meetings");

            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_Members_AbsentIMemberID",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_AbsentIIMemberID",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_AbsentIMemberID",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "AbsentIIMemberID",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "AbsentIMemberID",
                table: "Meetings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AbsentIIMemberID",
                table: "Meetings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AbsentIMemberID",
                table: "Meetings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_AbsentIIMemberID",
                table: "Meetings",
                column: "AbsentIIMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_AbsentIMemberID",
                table: "Meetings",
                column: "AbsentIMemberID");

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_Members_AbsentIIMemberID",
                table: "Meetings",
                column: "AbsentIIMemberID",
                principalTable: "Members",
                principalColumn: "MemberID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_Members_AbsentIMemberID",
                table: "Meetings",
                column: "AbsentIMemberID",
                principalTable: "Members",
                principalColumn: "MemberID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
