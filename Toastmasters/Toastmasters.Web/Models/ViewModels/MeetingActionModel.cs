using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
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
    }
    public class MeetingActionBuilder : IModelBuilder<MeetingActionModel, Meeting>
    {
        private Member[] _members { get; set; }

        public MeetingActionBuilder(Member[] members)
        {
            _members = members;
        }

        public Meeting Create(MeetingActionModel actionModel, out string ChangeLog)
        {
            //DateTime meetingDate;
            //if (!DateTime.TryParse(actionModel.MeetingDate, out meetingDate))
            //{
            //    meetingDate = DateTime.Now;
            //}
            ChangeLog = "";

            return new Meeting
            {
                MeetingID = actionModel.MeetingID,
                MeetingDate = actionModel.MeetingDate,
                Toastmaster = _members.Single(m => m.MemberID == actionModel.ToastmasterMemberID),
                TableTopics = _members.Single(m => m.MemberID == actionModel.TableTopicsMemberID),
                SpeakerI = _members.Single(m => m.MemberID == actionModel.SpeakerIMemberID),
                SpeakerII = _members.Single(m => m.MemberID == actionModel.SpeakerIIMemberID),
                GeneralEvaluator = _members.Single(m => m.MemberID == actionModel.GeneralEvaluatorMemberID),
                EvaluatorI = _members.Single(m => m.MemberID == actionModel.EvaluatorIMemberID),
                EvaluatorII = _members.Single(m => m.MemberID == actionModel.EvaluatorIIMemberID),
                Inspirational = _members.Single(m => m.MemberID == actionModel.InspirationalMemberID),
                Joke = _members.Single(m => m.MemberID == actionModel.JokeMemberID),
                Grammarian = _members.Single(m => m.MemberID == actionModel.GrammarianMemberID),
                Timer = _members.Single(m => m.MemberID == actionModel.TimerMemberID),
                BallotCounter = _members.Single(m => m.MemberID == actionModel.BallotCounterMemberID),
                President = _members.Single(m => m.MemberID == actionModel.PresidentMemberID),
                Sargent = _members.Single(m => m.MemberID == actionModel.SargentMemberID),
                AbsentI = _members.Single(m => m.MemberID == actionModel.AbsentIMemberID),
                AbsentII = _members.Single(m => m.MemberID == actionModel.AbsentIIMemberID),
            };
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
            if (actionModel.AbsentIMemberID != 0)
            {
                entity.AbsentI = _members.Single(m => m.MemberID == actionModel.AbsentIMemberID);
            }
            if (actionModel.AbsentIIMemberID != 0)
            {
                entity.AbsentII = _members.Single(m => m.MemberID == actionModel.AbsentIIMemberID);
            }

            return ChangeLogs.Count == 0 ? false : true;
        }

        public MeetingActionModel View(Meeting entity)
        {
            var blankMember = new Member { MemberID = 0, FirstName = "-Select", LastName = "Member -" };
            _members = _members.Prepend(blankMember).ToArray();
            var members = new SelectList(_members, "MemberID", "FullName");

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
                AbsentIMemberID = entity.AbsentI == null ? 0 : entity.AbsentI.MemberID,
                AbsentIIMemberID = entity.AbsentII == null ? 0 : entity.AbsentII.MemberID,
                
                Members = members
            };
        }

        public MeetingActionModel Rebuild(MeetingActionModel model)
        {
            throw new NotImplementedException();
        }
    }
}
