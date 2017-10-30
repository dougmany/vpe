using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Toastmasters.Web.Data;

namespace Toastmasters.Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20171030172848_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.4");

            modelBuilder.Entity("Toastmasters.Web.Models.Meeting", b =>
                {
                    b.Property<int>("MeetingID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AbsentIIMemberID");

                    b.Property<int?>("AbsentIMemberID");

                    b.Property<int?>("BallotCounterMemberID");

                    b.Property<int?>("EvaluatorIIMemberID");

                    b.Property<int?>("EvaluatorIMemberID");

                    b.Property<int?>("GeneralEvaluatorMemberID");

                    b.Property<int?>("GrammarianMemberID");

                    b.Property<int?>("InspirationalMemberID");

                    b.Property<int?>("JokeMemberID");

                    b.Property<DateTime>("MeetingDate");

                    b.Property<int?>("PresidentMemberID");

                    b.Property<int?>("SargentMemberID");

                    b.Property<int?>("SpeakerIIMemberID");

                    b.Property<int?>("SpeakerIMemberID");

                    b.Property<int?>("TableTopicsMemberID");

                    b.Property<int?>("TimerMemberID");

                    b.Property<int?>("ToastmasterMemberID");

                    b.HasKey("MeetingID");

                    b.HasIndex("AbsentIIMemberID");

                    b.HasIndex("AbsentIMemberID");

                    b.HasIndex("BallotCounterMemberID");

                    b.HasIndex("EvaluatorIIMemberID");

                    b.HasIndex("EvaluatorIMemberID");

                    b.HasIndex("GeneralEvaluatorMemberID");

                    b.HasIndex("GrammarianMemberID");

                    b.HasIndex("InspirationalMemberID");

                    b.HasIndex("JokeMemberID");

                    b.HasIndex("PresidentMemberID");

                    b.HasIndex("SargentMemberID");

                    b.HasIndex("SpeakerIIMemberID");

                    b.HasIndex("SpeakerIMemberID");

                    b.HasIndex("TableTopicsMemberID");

                    b.HasIndex("TimerMemberID");

                    b.HasIndex("ToastmasterMemberID");

                    b.ToTable("Meetings");
                });

            modelBuilder.Entity("Toastmasters.Web.Models.Member", b =>
                {
                    b.Property<int>("MemberID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsPresident");

                    b.Property<bool>("IsSargent");

                    b.Property<string>("LastName");

                    b.HasKey("MemberID");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("Toastmasters.Web.Models.Meeting", b =>
                {
                    b.HasOne("Toastmasters.Web.Models.Member", "AbsentII")
                        .WithMany()
                        .HasForeignKey("AbsentIIMemberID");

                    b.HasOne("Toastmasters.Web.Models.Member", "AbsentI")
                        .WithMany()
                        .HasForeignKey("AbsentIMemberID");

                    b.HasOne("Toastmasters.Web.Models.Member", "BallotCounter")
                        .WithMany()
                        .HasForeignKey("BallotCounterMemberID");

                    b.HasOne("Toastmasters.Web.Models.Member", "EvaluatorII")
                        .WithMany()
                        .HasForeignKey("EvaluatorIIMemberID");

                    b.HasOne("Toastmasters.Web.Models.Member", "EvaluatorI")
                        .WithMany()
                        .HasForeignKey("EvaluatorIMemberID");

                    b.HasOne("Toastmasters.Web.Models.Member", "GeneralEvaluator")
                        .WithMany()
                        .HasForeignKey("GeneralEvaluatorMemberID");

                    b.HasOne("Toastmasters.Web.Models.Member", "Grammarian")
                        .WithMany()
                        .HasForeignKey("GrammarianMemberID");

                    b.HasOne("Toastmasters.Web.Models.Member", "Inspirational")
                        .WithMany()
                        .HasForeignKey("InspirationalMemberID");

                    b.HasOne("Toastmasters.Web.Models.Member", "Joke")
                        .WithMany()
                        .HasForeignKey("JokeMemberID");

                    b.HasOne("Toastmasters.Web.Models.Member", "President")
                        .WithMany()
                        .HasForeignKey("PresidentMemberID");

                    b.HasOne("Toastmasters.Web.Models.Member", "Sargent")
                        .WithMany()
                        .HasForeignKey("SargentMemberID");

                    b.HasOne("Toastmasters.Web.Models.Member", "SpeakerII")
                        .WithMany()
                        .HasForeignKey("SpeakerIIMemberID");

                    b.HasOne("Toastmasters.Web.Models.Member", "SpeakerI")
                        .WithMany()
                        .HasForeignKey("SpeakerIMemberID");

                    b.HasOne("Toastmasters.Web.Models.Member", "TableTopics")
                        .WithMany()
                        .HasForeignKey("TableTopicsMemberID");

                    b.HasOne("Toastmasters.Web.Models.Member", "Timer")
                        .WithMany()
                        .HasForeignKey("TimerMemberID");

                    b.HasOne("Toastmasters.Web.Models.Member", "Toastmaster")
                        .WithMany()
                        .HasForeignKey("ToastmasterMemberID");
                });
        }
    }
}
