﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Toastmasters.Web.Data;
using Toastmasters.Web.Models;

namespace Toastmasters.Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20171212055952_AddSpeeches")]
    partial class AddSpeeches
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.4");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Toastmasters.Web.Models.Absence", b =>
                {
                    b.Property<int>("AbsenceID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MeetingID");

                    b.Property<int>("MemberID");

                    b.Property<int>("Role");

                    b.HasKey("AbsenceID");

                    b.ToTable("Absences");
                });

            modelBuilder.Entity("Toastmasters.Web.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<int?>("MemberID");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("MemberID");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Toastmasters.Web.Models.Meeting", b =>
                {
                    b.Property<int>("MeetingID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BallotCounterMemberID");

                    b.Property<int?>("EvaluatorIIMemberID");

                    b.Property<int?>("EvaluatorIMemberID");

                    b.Property<int?>("GeneralEvaluatorMemberID");

                    b.Property<int?>("GrammarianMemberID");

                    b.Property<int?>("InspirationalMemberID");

                    b.Property<int?>("JokeMemberID");

                    b.Property<DateTime>("MeetingDate");

                    b.Property<string>("MeetingTheme");

                    b.Property<int?>("PresidentMemberID");

                    b.Property<int?>("SargentMemberID");

                    b.Property<int?>("SpeakerIIMemberID");

                    b.Property<int?>("SpeakerIMemberID");

                    b.Property<int?>("SpeechIISpeechID");

                    b.Property<int?>("SpeechISpeechID");

                    b.Property<int?>("TableTopicsMemberID");

                    b.Property<int?>("TimerMemberID");

                    b.Property<int?>("ToastmasterMemberID");

                    b.HasKey("MeetingID");

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

                    b.HasIndex("SpeechIISpeechID");

                    b.HasIndex("SpeechISpeechID");

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

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsPresident");

                    b.Property<bool>("IsSargent");

                    b.Property<string>("LastName");

                    b.HasKey("MemberID");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("Toastmasters.Web.Models.Speech", b =>
                {
                    b.Property<int>("SpeechID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MemberID");

                    b.Property<string>("Project");

                    b.Property<string>("TimeConstraints");

                    b.Property<string>("Title");

                    b.HasKey("SpeechID");

                    b.ToTable("Speeches");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Toastmasters.Web.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Toastmasters.Web.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Toastmasters.Web.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Toastmasters.Web.Models.ApplicationUser", b =>
                {
                    b.HasOne("Toastmasters.Web.Models.Member", "Member")
                        .WithMany()
                        .HasForeignKey("MemberID");
                });

            modelBuilder.Entity("Toastmasters.Web.Models.Meeting", b =>
                {
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

                    b.HasOne("Toastmasters.Web.Models.Speech", "SpeechII")
                        .WithMany()
                        .HasForeignKey("SpeechIISpeechID");

                    b.HasOne("Toastmasters.Web.Models.Speech", "SpeechI")
                        .WithMany()
                        .HasForeignKey("SpeechISpeechID");

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
