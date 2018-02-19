using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Toastmasters.Web.Models
{
    public class MeetingViewModel
    {
        public MeetingViewModel() { }

        public MeetingViewModel(Meeting meeting)
        {
            MeetingID = meeting.MeetingID;
            MeetingDate = meeting.MeetingDate;
            MeetingTheme = meeting.MeetingTheme;
            ToastmasterID = meeting.ToastmasterMemberID ?? 0;
            Toastmaster = meeting.Toastmaster == null ? "" : meeting.Toastmaster.FirstInitial;
            Inspirational = meeting.Inspirational == null ? "" : meeting.Inspirational.FirstInitial;
            Joke = meeting.Joke == null ? "" : meeting.Joke.FirstInitial;
            GeneralEvaluator = meeting.GeneralEvaluator == null ? "" : meeting.GeneralEvaluator.FirstInitial;
            EvaluatorI = meeting.EvaluatorI == null ? "" : meeting.EvaluatorI.FirstInitial;
            EvaluatorII = meeting.EvaluatorII == null ? "" : meeting.EvaluatorII.FirstInitial;
            Timer = meeting.Timer == null ? "" : meeting.Timer.FirstInitial;
            BallotCounter = meeting.BallotCounter == null ? "" : meeting.BallotCounter.FirstInitial;
            Grammarian = meeting.Grammarian == null ? "" : meeting.Grammarian.FirstInitial;
            TableTopics = meeting.TableTopics == null ? "" : meeting.TableTopics.FirstInitial;
            SpeakerI = meeting.SpeakerI == null ? "" : meeting.SpeakerI.FirstInitial;
            SpeakerII = meeting.SpeakerII == null ? "" : meeting.SpeakerII.FirstInitial;
            President = meeting.President == null ? "" : meeting.President.FirstInitial;
            Sargent = meeting.Sargent == null ? "" : meeting.Sargent.FirstInitial;

            SpeakerIMemberID = meeting.SpeakerIMemberID ?? 0;
            SpeakerIIMemberID = meeting.SpeakerIIMemberID ?? 0;

            SpeechISpeechID = meeting.SpeechISpeechID ?? 0;
            SpeechITitle = meeting.SpeechI == null ? "" : meeting.SpeechI.Title;
            SpeechIISpeechID = meeting.SpeechIISpeechID ?? 0;
            SpeechIITitle = meeting.SpeechII == null ? "" : meeting.SpeechII.Title; ;
        }

        public Int32 MeetingID { get; set; }
        [Display(Name = "Meeting Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime MeetingDate { get; set; }
        [Display(Name ="Theme")]
        public String MeetingTheme { get; set; }
        public Int32 ToastmasterID { get; set; }
        public String Toastmaster { get; set; }
        public String Inspirational { get; set; }
        public String Joke { get; set; }
        [Display(Name = "General Evaluator")]
        public String GeneralEvaluator { get; set; }
        [Display(Name = "Evaluator I")]
        public String EvaluatorI { get; set; }
        [Display(Name = "Evaluator II")]
        public String EvaluatorII { get; set; }
        public String Timer { get; set; }
        [Display(Name = "Ballot Counter")]
        public String BallotCounter { get; set; }
        public String Grammarian { get; set; }
        [Display(Name = "Table Topics")]
        public String TableTopics { get; set; }
        public Int32 SpeakerIMemberID { get; set; }
        [Display(Name = "Speaker I")]
        public String SpeakerI { get; set; }
        public Int32 SpeakerIIMemberID { get; set; }
        [Display(Name = "Speaker II")]
        public String SpeakerII { get; set; }
        public String President { get; set; }
        public String Sargent { get; set; }
        public String AbsentI { get; set; }
        public String AbsentII { get; set; }
        public String MeetingDateString { get { return MeetingDate.ToString("M"); } }

        public Int32 SpeechISpeechID { get; set; }
        public String SpeechITitle { get; set; }
        public Int32 SpeechIISpeechID { get; set; }
        public String SpeechIITitle { get; set; }

        public String ClubName { get; set; }
        public String ClubNumber { get; set; }
        public String District { get; set; }
        public String MeetingRoom { get; set; }

        public String ToastmasterClass { get; set; }
        public String InspirationalClass { get; set; }
        public String JokeClass { get; set; }
        public String GeneralEvaluatorClass { get; set; }
        public String EvaluatorIClass { get; set; }
        public String EvaluatorIIClass { get; set; }
        public String TimerClass { get; set; }
        public String BallotCounterClass { get; set; }
        public String GrammarianClass { get; set; }
        public String TableTopicsClass { get; set; }
        public String SpeakerIClass { get; set; }
        public String SpeakerIIClass { get; set; }
        public String PresidentClass { get; set; }
        public String SargentClass { get; set; }

    }

    public class AgendaViewModel
    {
        public AgendaViewModel() { }

        public AgendaViewModel(Meeting meeting, Meeting nextMeeting, Club club)
        {
            ClubName = club.ClubName;
            ClubNumber = club.ClubNumber;
            District = club.District;
            MeetingRoom = club.MeetingRoom;

            MeetingDate = meeting.MeetingDate;
            MeetingTheme = meeting.MeetingTheme;
            Toastmaster = meeting.Toastmaster.FullName;
            Inspirational = meeting.Inspirational.FullName;
            Joke = meeting.Joke.FullName;
            GeneralEvaluator = meeting.GeneralEvaluator.FullName;
            EvaluatorI = meeting.EvaluatorI.FullName;
            EvaluatorII = meeting.EvaluatorII.FullName;
            Timer = meeting.Timer.FullName;
            BallotCounter = meeting.BallotCounter.FullName;
            Grammarian = meeting.Grammarian.FullName;
            TableTopics = meeting.TableTopics.FullName;
            SpeakerI = meeting.SpeakerI.FullName;
            SpeakerII = meeting.SpeakerII.FullName;
            President = meeting.President.FullName;
            Sargent = meeting.Sargent.FullName;

            if (meeting.SpeechI != null)
            {
                SpeechITitle =  meeting.SpeechI.Title;
                SpeechIProject =  meeting.SpeechI.Project;
                SpeechITimeConstraints = meeting.SpeechI.TimeConstraints;
            }

            if (meeting.SpeechII != null)
            {
                SpeechIITitle = meeting.SpeechII.Title;
                SpeechIIProject = meeting.SpeechII.Project;
                SpeechIITimeConstraints = meeting.SpeechII.TimeConstraints;
            }

            NextMeetingDate = nextMeeting.MeetingDate;
            NextToastmaster = nextMeeting.Toastmaster.FullName;
            NextInspirational = nextMeeting.Inspirational.FullName;
            NextJoke = nextMeeting.Joke.FullName;
            NextGeneralEvaluator = nextMeeting.GeneralEvaluator.FullName;
            NextEvaluatorI = nextMeeting.EvaluatorI.FullName;
            NextEvaluatorII = nextMeeting.EvaluatorII.FullName;
            NextTimer = nextMeeting.Timer.FullName;
            NextBallotCounter = nextMeeting.BallotCounter.FullName;
            NextGrammarian = nextMeeting.Grammarian.FullName;
            NextTableTopics = nextMeeting.TableTopics.FullName;
            NextSpeakerI = nextMeeting.SpeakerI.FullName;
            NextSpeakerII = nextMeeting.SpeakerII.FullName;
            NextPresident = nextMeeting.President.FullName;
            NextSargent = nextMeeting.Sargent.FullName;
        }

        public String ClubName { get; set; }
        public String ClubNumber { get; set; }
        public String District { get; set; }
        public String MeetingRoom { get; set; }

        public DateTime MeetingDate { get; set; }
        public String MeetingTheme { get; set; }
        public String Toastmaster { get; set; }
        public String Inspirational { get; set; }
        public String Joke { get; set; }
        public String GeneralEvaluator { get; set; }
        public String EvaluatorI { get; set; }
        public String EvaluatorII { get; set; }
        public String Timer { get; set; }
        public String BallotCounter { get; set; }
        public String Grammarian { get; set; }
        public String TableTopics { get; set; }
        public String SpeakerI { get; set; }
        public String SpeakerII { get; set; }
        public String President { get; set; }
        public String Sargent { get; set; }
        public String AbsentI { get; set; }
        public String AbsentII { get; set; }
        public String MeetingDateString { get { return MeetingDate.ToString("f"); } }

        public String SpeechITitle { get; set; }
        public String SpeechIProject { get; set; }
        public String SpeechITimeConstraints { get; set; }
        public String SpeechIITitle { get; set; }
        public String SpeechIIProject { get; set; }
        public String SpeechIITimeConstraints { get; set; }


        public DateTime NextMeetingDate { get; set; }
        public String NextToastmaster { get; set; }
        public String NextInspirational { get; set; }
        public String NextJoke { get; set; }
        public String NextGeneralEvaluator { get; set; }
        public String NextEvaluatorI { get; set; }
        public String NextEvaluatorII { get; set; }
        public String NextTimer { get; set; }
        public String NextBallotCounter { get; set; }
        public String NextGrammarian { get; set; }
        public String NextTableTopics { get; set; }
        public String NextSpeakerI { get; set; }
        public String NextSpeakerII { get; set; }
        public String NextPresident { get; set; }
        public String NextSargent { get; set; }
        public String NextMeetingDateString { get { return NextMeetingDate.ToString("d"); } }
    }

    public class MemberHistory
    {
        public String MemberName { get; set; }
        public DateTime MeetingDate { get; set; }
    }

    public class MemberHistories
    {
        public MemberHistories()
        {
            Histories = new List<MemberHistory>();
        }

        public List<MemberHistory> Histories { get; set; }

        public String HtmlList
        {
            get
            {
                String memberHistory = "<ul>";
                foreach (var item in Histories.OrderBy(mh => mh.MeetingDate))
                {
                    memberHistory += $"<li>{item.MemberName} | {item.MeetingDate.ToString("d")}</li>";
                }
                memberHistory += "</ul>";

                return memberHistory;

            }
        }
        public void Add(MemberHistory history)
        {
            Histories.Add(history);
        }

    }
}
