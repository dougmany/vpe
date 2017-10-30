using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Toastmasters.Web.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    MemberID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    IsPresident = table.Column<bool>(nullable: false),
                    IsSargent = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.MemberID);
                });

            migrationBuilder.CreateTable(
                name: "Meetings",
                columns: table => new
                {
                    MeetingID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    AbsentIIMemberID = table.Column<int>(nullable: true),
                    AbsentIMemberID = table.Column<int>(nullable: true),
                    BallotCounterMemberID = table.Column<int>(nullable: true),
                    EvaluatorIIMemberID = table.Column<int>(nullable: true),
                    EvaluatorIMemberID = table.Column<int>(nullable: true),
                    GeneralEvaluatorMemberID = table.Column<int>(nullable: true),
                    GrammarianMemberID = table.Column<int>(nullable: true),
                    InspirationalMemberID = table.Column<int>(nullable: true),
                    JokeMemberID = table.Column<int>(nullable: true),
                    MeetingDate = table.Column<DateTime>(nullable: false),
                    PresidentMemberID = table.Column<int>(nullable: true),
                    SargentMemberID = table.Column<int>(nullable: true),
                    SpeakerIIMemberID = table.Column<int>(nullable: true),
                    SpeakerIMemberID = table.Column<int>(nullable: true),
                    TableTopicsMemberID = table.Column<int>(nullable: true),
                    TimerMemberID = table.Column<int>(nullable: true),
                    ToastmasterMemberID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.MeetingID);
                    table.ForeignKey(
                        name: "FK_Meetings_Members_AbsentIIMemberID",
                        column: x => x.AbsentIIMemberID,
                        principalTable: "Members",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Meetings_Members_AbsentIMemberID",
                        column: x => x.AbsentIMemberID,
                        principalTable: "Members",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Meetings_Members_BallotCounterMemberID",
                        column: x => x.BallotCounterMemberID,
                        principalTable: "Members",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Meetings_Members_EvaluatorIIMemberID",
                        column: x => x.EvaluatorIIMemberID,
                        principalTable: "Members",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Meetings_Members_EvaluatorIMemberID",
                        column: x => x.EvaluatorIMemberID,
                        principalTable: "Members",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Meetings_Members_GeneralEvaluatorMemberID",
                        column: x => x.GeneralEvaluatorMemberID,
                        principalTable: "Members",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Meetings_Members_GrammarianMemberID",
                        column: x => x.GrammarianMemberID,
                        principalTable: "Members",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Meetings_Members_InspirationalMemberID",
                        column: x => x.InspirationalMemberID,
                        principalTable: "Members",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Meetings_Members_JokeMemberID",
                        column: x => x.JokeMemberID,
                        principalTable: "Members",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Meetings_Members_PresidentMemberID",
                        column: x => x.PresidentMemberID,
                        principalTable: "Members",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Meetings_Members_SargentMemberID",
                        column: x => x.SargentMemberID,
                        principalTable: "Members",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Meetings_Members_SpeakerIIMemberID",
                        column: x => x.SpeakerIIMemberID,
                        principalTable: "Members",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Meetings_Members_SpeakerIMemberID",
                        column: x => x.SpeakerIMemberID,
                        principalTable: "Members",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Meetings_Members_TableTopicsMemberID",
                        column: x => x.TableTopicsMemberID,
                        principalTable: "Members",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Meetings_Members_TimerMemberID",
                        column: x => x.TimerMemberID,
                        principalTable: "Members",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Meetings_Members_ToastmasterMemberID",
                        column: x => x.ToastmasterMemberID,
                        principalTable: "Members",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_AbsentIIMemberID",
                table: "Meetings",
                column: "AbsentIIMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_AbsentIMemberID",
                table: "Meetings",
                column: "AbsentIMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_BallotCounterMemberID",
                table: "Meetings",
                column: "BallotCounterMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_EvaluatorIIMemberID",
                table: "Meetings",
                column: "EvaluatorIIMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_EvaluatorIMemberID",
                table: "Meetings",
                column: "EvaluatorIMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_GeneralEvaluatorMemberID",
                table: "Meetings",
                column: "GeneralEvaluatorMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_GrammarianMemberID",
                table: "Meetings",
                column: "GrammarianMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_InspirationalMemberID",
                table: "Meetings",
                column: "InspirationalMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_JokeMemberID",
                table: "Meetings",
                column: "JokeMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_PresidentMemberID",
                table: "Meetings",
                column: "PresidentMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_SargentMemberID",
                table: "Meetings",
                column: "SargentMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_SpeakerIIMemberID",
                table: "Meetings",
                column: "SpeakerIIMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_SpeakerIMemberID",
                table: "Meetings",
                column: "SpeakerIMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_TableTopicsMemberID",
                table: "Meetings",
                column: "TableTopicsMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_TimerMemberID",
                table: "Meetings",
                column: "TimerMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_ToastmasterMemberID",
                table: "Meetings",
                column: "ToastmasterMemberID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meetings");

            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
