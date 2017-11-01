using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Toastmasters.Web.Models.ViewModels;

namespace Toastmasters.Web.Models
{
    public class MeetingActionModel
    {
        public Int32 MeetingID { get; set; }
        [Display(Name = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime MeetingDate { get; set; }
        [Display (Name="Toastmaster")]
        public Int32 ToastmasterMemberID { get; set; }
        [Display(Name = "Inspirational")]
        public Int32 InspirationalMemberID { get; set; }
        [Display(Name = "Joke")]
        public Int32 JokeMemberID { get; set; }
        [Display(Name = "General Evaluator")]
        public Int32 GeneralEvaluatorMemberID { get; set; }
        [Display(Name = "Evaluator 1")]
        public Int32 EvaluatorIMemberID { get; set; }
        [Display(Name = "Evaluator 2")]
        public Int32 EvaluatorIIMemberID { get; set; }
        [Display(Name = "Timer")]
        public Int32 TimerMemberID { get; set; }
        [Display(Name = "Ballot Counter")]
        public Int32 BallotCounterMemberID { get; set; }
        [Display(Name = "Grammarian")]
        public Int32 GrammarianMemberID { get; set; }
        [Display(Name = "Table Topics")]
        public Int32 TableTopicsMemberID { get; set; }
        [Display(Name = "Speaker 1")]
        public Int32 SpeakerIMemberID { get; set; }
        [Display(Name = "Speaker 2")]
        public Int32 SpeakerIIMemberID { get; set; }
        [Display(Name = "President")]
        public Int32 PresidentMemberID { get; set; }
        [Display(Name = "Sargent")]
        public Int32 SargentMemberID { get; set; }
        [Display(Name = "Absent")]
        public Int32 AbsentIMemberID { get; set; }
        [Display(Name = "Absent")]
        public Int32 AbsentIIMemberID { get; set; }

        public SelectList Members { get; set; }
        public Dictionary<MeetingRoleNames, MemberHistories> Histories { get; set; }
    }

    public class MeetingActionBuilder : IModelBuilder<MeetingActionModel, Meeting>
    {
        private Member[] _members { get; set; }
        public Meeting[] _meetings { get; set; }

        public MeetingActionBuilder(Member[] members, Meeting[] meetings)
        {
            _members = members;
            _meetings = meetings;
        }

        public MeetingActionBuilder(Member[] members)
        {
            _members = members;
            _meetings = new Meeting[0];
        }

        public Meeting Create(MeetingActionModel actionModel, out string ChangeLog)
        {
            ChangeLog = "";

            var meeting =  new Meeting
            {
                MeetingID = actionModel.MeetingID,
                MeetingDate = actionModel.MeetingDate,
                Toastmaster = actionModel.ToastmasterMemberID == 0 ? null : _members.Single(m => m.MemberID == actionModel.ToastmasterMemberID),
                TableTopics = actionModel.TableTopicsMemberID == 0 ? null : _members.Single(m => m.MemberID == actionModel.TableTopicsMemberID),
                SpeakerI = actionModel.SpeakerIMemberID == 0 ? null : _members.Single(m => m.MemberID == actionModel.SpeakerIMemberID),
                SpeakerII = actionModel.SpeakerIIMemberID == 0 ? null : _members.Single(m => m.MemberID == actionModel.SpeakerIIMemberID),
                GeneralEvaluator = actionModel.GeneralEvaluatorMemberID == 0 ? null : _members.Single(m => m.MemberID == actionModel.GeneralEvaluatorMemberID),
                EvaluatorI = actionModel.EvaluatorIMemberID == 0 ? null : _members.Single(m => m.MemberID == actionModel.EvaluatorIMemberID),
                EvaluatorII = actionModel.EvaluatorIIMemberID == 0 ? null : _members.Single(m => m.MemberID == actionModel.EvaluatorIIMemberID),
                Inspirational = actionModel.InspirationalMemberID == 0 ? null : _members.Single(m => m.MemberID == actionModel.InspirationalMemberID),
                Joke = actionModel.JokeMemberID == 0 ? null : _members.Single(m => m.MemberID == actionModel.JokeMemberID),
                Grammarian = actionModel.GrammarianMemberID == 0 ? null : _members.Single(m => m.MemberID == actionModel.GrammarianMemberID),
                Timer = actionModel.TimerMemberID == 0 ? null : _members.Single(m => m.MemberID == actionModel.TimerMemberID),
                BallotCounter = actionModel.BallotCounterMemberID == 0 ? null : _members.Single(m => m.MemberID == actionModel.BallotCounterMemberID),
                President = actionModel.PresidentMemberID == 0 ? null : _members.Single(m => m.MemberID == actionModel.PresidentMemberID),
                Sargent = actionModel.SargentMemberID == 0 ? null : _members.Single(m => m.MemberID == actionModel.SargentMemberID),
            };
            return meeting;
        }

        public bool Update(MeetingActionModel actionModel, Meeting entity, out List<string> ChangeLogs)
        {
            ChangeLogs = new List<String>();
            if (entity.MeetingID != actionModel.MeetingID)
            {
                return false;
            }
            if (entity.MeetingDate != actionModel.MeetingDate)
            {
                entity.MeetingDate = actionModel.MeetingDate;
            }
            if (actionModel.ToastmasterMemberID != 0)
            {
                entity.Toastmaster = _members.Single(m => m.MemberID == actionModel.ToastmasterMemberID);
            }
            if (actionModel.TableTopicsMemberID != 0)
            {
                entity.TableTopics = _members.Single(m => m.MemberID == actionModel.TableTopicsMemberID);
            }
            if (actionModel.SpeakerIMemberID != 0)
            {
                entity.SpeakerI = _members.Single(m => m.MemberID == actionModel.SpeakerIMemberID);
            }
            if (actionModel.SpeakerIIMemberID != 0)
            {
                entity.SpeakerII = _members.Single(m => m.MemberID == actionModel.SpeakerIIMemberID);
            }
            if (actionModel.GeneralEvaluatorMemberID != 0)
            {
                entity.GeneralEvaluator = _members.Single(m => m.MemberID == actionModel.GeneralEvaluatorMemberID);
            }
            if (actionModel.EvaluatorIMemberID != 0)
            {
                entity.EvaluatorI = _members.Single(m => m.MemberID == actionModel.EvaluatorIMemberID);
            }
            if (actionModel.EvaluatorIIMemberID != 0)
            {
                entity.EvaluatorII = _members.Single(m => m.MemberID == actionModel.EvaluatorIIMemberID);
            }
            if (actionModel.InspirationalMemberID != 0)
            {
                entity.Inspirational = _members.Single(m => m.MemberID == actionModel.InspirationalMemberID);
            }
            if (actionModel.JokeMemberID != 0)
            {
                entity.Joke = _members.Single(m => m.MemberID == actionModel.JokeMemberID);
            }
            if (actionModel.GrammarianMemberID != 0)
            {
                entity.Grammarian = _members.Single(m => m.MemberID == actionModel.GrammarianMemberID);
            }
            if (actionModel.TimerMemberID != 0)
            {
                entity.Timer = _members.Single(m => m.MemberID == actionModel.TimerMemberID);
            }
            if (actionModel.BallotCounterMemberID != 0)
            {
                entity.BallotCounter = _members.Single(m => m.MemberID == actionModel.BallotCounterMemberID);
            }
            if (actionModel.PresidentMemberID != 0)
            {
                entity.President = _members.Single(m => m.MemberID == actionModel.PresidentMemberID);
            }
            if (actionModel.SargentMemberID != 0)
            {
                entity.Sargent = _members.Single(m => m.MemberID == actionModel.SargentMemberID);
            }

            return ChangeLogs.Count == 0 ? false : true;
        }

        public MeetingActionModel View(Meeting entity)
        {
            var blankMember = new Member { MemberID = 0, FirstName = "-Select", LastName = "Member -" };
            var memberSelect = _members.Prepend(blankMember).ToArray();
            var members = new SelectList(memberSelect, "MemberID", "FullName");

            return new MeetingActionModel
            {
                MeetingID = entity.MeetingID,
                MeetingDate = entity.MeetingDate,
                ToastmasterMemberID = entity.Toastmaster == null ? 0 : entity.Toastmaster.MemberID,
                TableTopicsMemberID = entity.TableTopics == null ? 0 : entity.TableTopics.MemberID,
                SpeakerIMemberID = entity.SpeakerI == null ? 0 : entity.SpeakerI.MemberID,
                SpeakerIIMemberID = entity.SpeakerII == null ? 0 : entity.SpeakerII.MemberID,
                GeneralEvaluatorMemberID = entity.GeneralEvaluator == null ? 0 : entity.GeneralEvaluator.MemberID,
                EvaluatorIMemberID = entity.EvaluatorI == null ? 0 : entity.EvaluatorI.MemberID,
                EvaluatorIIMemberID = entity.EvaluatorII == null ? 0 : entity.EvaluatorII.MemberID,
                InspirationalMemberID = entity.Inspirational == null ? 0 : entity.Inspirational.MemberID,
                JokeMemberID = entity.Joke == null ? 0 : entity.Joke.MemberID,
                GrammarianMemberID = entity.Grammarian == null ? 0 : entity.Grammarian.MemberID,
                TimerMemberID = entity.Timer == null ? 0 : entity.Timer.MemberID,
                BallotCounterMemberID = entity.BallotCounter == null ? 0 : entity.BallotCounter.MemberID,
                PresidentMemberID = entity.President == null ? 0 : entity.President.MemberID,
                SargentMemberID = entity.Sargent == null ? 0 : entity.Sargent.MemberID,
                
                Members = members,
                Histories = GetHistories()
            };
        }

        public MeetingActionModel View()
        {
            var model = new MeetingActionModel();
            var blankMember = new Member { MemberID = 0, FirstName = "-Select", LastName = "Member -" };
            var memberSelect = _members.Prepend(blankMember).ToArray();
            model.Members = new SelectList(memberSelect, "MemberID", "FullName");

            model.Histories = GetHistories();

            return model;
        }

        private Dictionary<MeetingRoleNames, MemberHistories> GetHistories()
        {
            var histories = new Dictionary<MeetingRoleNames, MemberHistories>
                {
                    {MeetingRoleNames.Toastmaster, new MemberHistories() },
                    {MeetingRoleNames.Tabletopics,  new MemberHistories() },
                    {MeetingRoleNames.GeneralEvaluator, new MemberHistories() },
                    {MeetingRoleNames.Evaluator,  new MemberHistories() },
                    {MeetingRoleNames.Speaker,  new MemberHistories() },
                    {MeetingRoleNames.Timer,  new MemberHistories() },
                    {MeetingRoleNames.Grammarian,  new MemberHistories() },
                    {MeetingRoleNames.Inspirational,  new MemberHistories() },
                    {MeetingRoleNames.Joke,  new MemberHistories()},
                    {MeetingRoleNames.BallotCounter,  new MemberHistories() }
                };

            foreach (var item in _members.ToArray())
            {
                var toastmasterMeeting = _meetings
                    .Where(m => m.Toastmaster.MemberID == item.MemberID)
                    .OrderByDescending(m => m.MeetingDate)
                    .FirstOrDefault();
                histories[MeetingRoleNames.Toastmaster].Add(new MemberHistory
                {
                    MemberName = item.FullName,
                    MeetingDate = toastmasterMeeting == null ? new DateTime() : toastmasterMeeting.MeetingDate
                });
                var tabletopicsMeeting = _meetings
                    .Where(m => m.TableTopics.MemberID == item.MemberID)
                    .OrderByDescending(m => m.MeetingDate)
                    .FirstOrDefault();
                histories[MeetingRoleNames.Tabletopics].Add(new MemberHistory
                {
                    MemberName = item.FullName,
                    MeetingDate = tabletopicsMeeting == null ? new DateTime() : tabletopicsMeeting.MeetingDate
                });
                var generalEvaluatorMeeting = _meetings
                    .Where(m => m.GeneralEvaluator.MemberID == item.MemberID)
                    .OrderByDescending(m => m.MeetingDate)
                    .FirstOrDefault();
                histories[MeetingRoleNames.GeneralEvaluator].Add(new MemberHistory
                {
                    MemberName = item.FullName,
                    MeetingDate = generalEvaluatorMeeting == null ? new DateTime() : generalEvaluatorMeeting.MeetingDate
                });
                var evaluatorMeeting = _meetings
                    .Where(m => m.EvaluatorI.MemberID == item.MemberID || m.EvaluatorII.MemberID == item.MemberID)
                    .OrderByDescending(m => m.MeetingDate)
                    .FirstOrDefault();
                histories[MeetingRoleNames.Evaluator].Add(new MemberHistory
                {
                    MemberName = item.FullName,
                    MeetingDate = evaluatorMeeting == null ? new DateTime() : evaluatorMeeting.MeetingDate
                });

                var speakerMeeting = _meetings
                    .Where(m => m.SpeakerI.MemberID == item.MemberID || m.SpeakerII.MemberID == item.MemberID)
                    .OrderByDescending(m => m.MeetingDate)
                    .FirstOrDefault();
                histories[MeetingRoleNames.Speaker].Add(new MemberHistory
                {
                    MemberName = item.FullName,
                    MeetingDate = speakerMeeting == null ? new DateTime() : speakerMeeting.MeetingDate
                });
                var timerMeeting = _meetings
                    .Where(m => m.Timer.MemberID == item.MemberID)
                    .OrderByDescending(m => m.MeetingDate)
                    .FirstOrDefault();
                histories[MeetingRoleNames.Timer].Add(new MemberHistory
                {
                    MemberName = item.FullName,
                    MeetingDate = timerMeeting == null ? new DateTime() : timerMeeting.MeetingDate
                });
                var grammarianMeeting = _meetings
                    .Where(m => m.Grammarian.MemberID == item.MemberID)
                    .OrderByDescending(m => m.MeetingDate)
                    .FirstOrDefault();
                histories[MeetingRoleNames.Grammarian].Add(new MemberHistory
                {
                    MemberName = item.FullName,
                    MeetingDate = grammarianMeeting == null ? new DateTime() : grammarianMeeting.MeetingDate
                });
                var inspirationalMeeting = _meetings
                    .Where(m => m.Inspirational.MemberID == item.MemberID)
                    .OrderByDescending(m => m.MeetingDate)
                    .FirstOrDefault();
                histories[MeetingRoleNames.Inspirational].Add(new MemberHistory
                {
                    MemberName = item.FullName,
                    MeetingDate = inspirationalMeeting == null ? new DateTime() : inspirationalMeeting.MeetingDate
                });
                var jokeMeeting = _meetings
                    .Where(m => m.Joke.MemberID == item.MemberID)
                    .OrderByDescending(m => m.MeetingDate)
                    .FirstOrDefault();
                histories[MeetingRoleNames.Joke].Add(new MemberHistory
                {
                    MemberName = item.FullName,
                    MeetingDate = jokeMeeting == null ? new DateTime() : jokeMeeting.MeetingDate
                });
                var ballotCounterMeeting = _meetings
                    .Where(m => m.BallotCounter.MemberID == item.MemberID)
                    .OrderByDescending(m => m.MeetingDate)
                    .FirstOrDefault();
                histories[MeetingRoleNames.BallotCounter].Add(new MemberHistory
                {
                    MemberName = item.FullName,
                    MeetingDate = ballotCounterMeeting == null ? new DateTime() : ballotCounterMeeting.MeetingDate
                });
            }

            return histories;
        }

        public MeetingActionModel Rebuild(MeetingActionModel model)
        {
            throw new NotImplementedException();
        }
    }

    public enum MeetingRoleNames
    {
        Toastmaster,
        Tabletopics,
        GeneralEvaluator,
        Evaluator,
        Speaker,
        Timer,
        Grammarian,
        Inspirational,
        Joke,
        BallotCounter,
    }
}
