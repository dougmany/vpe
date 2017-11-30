using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Toastmasters.Web.Models
{
    public class Meeting
    {
        public Meeting()
        {
            Toastmaster = new Member();
            Inspirational = new Member();
            Joke = new Member();
            GeneralEvaluator = new Member();
            EvaluatorI = new Member();
            EvaluatorII = new Member();
            Timer = new Member();
            BallotCounter = new Member();
            Grammarian = new Member();
            TableTopics = new Member();
            SpeakerI = new Member();
            SpeakerII = new Member();
            President = new Member();
            Sargent = new Member();
        }
        [Display(Name = "Meeting")]
        public Int32 MeetingID { get; set; }
        [Display(Name ="Date")]
        public DateTime MeetingDate { get; set; }
        public String MeetingTheme { get; set; }

        public Int32? ToastmasterMemberID { get; set; }
        public Int32? InspirationalMemberID { get; set; }
        public Int32? JokeMemberID { get; set; }
        public Int32? GeneralEvaluatorMemberID { get; set; }
        public Int32? EvaluatorIMemberID { get; set; }
        public Int32? EvaluatorIIMemberID { get; set; }
        public Int32? TimerMemberID { get; set; }
        public Int32? BallotCounterMemberID { get; set; }
        public Int32? GrammarianMemberID { get; set; }
        public Int32? TableTopicsMemberID { get; set; }
        public Int32? SpeakerIMemberID { get; set; }
        public Int32? SpeakerIIMemberID { get; set; }
        public Int32? PresidentMemberID { get; set; }        
        public Int32? SargentMemberID { get; set; }

        public Member Toastmaster { get; set; }
        public Member Inspirational { get; set; }
        public Member Joke { get; set; }
        public Member GeneralEvaluator { get; set; }
        public Member EvaluatorI { get; set; }
        public Member EvaluatorII { get; set; }
        public Member Timer { get; set; }
        public Member BallotCounter { get; set; }
        public Member Grammarian { get; set; }
        public Member TableTopics { get; set; }
        public Member SpeakerI { get; set; }
        public Member SpeakerII { get; set; }
        public Member President { get; set; }
        public Member Sargent { get; set; }
    }
}
