using System;

namespace Toastmasters.Web.Models
{
    public class MeetingViewModel
    {
        public MeetingViewModel(Meeting meeting, Meeting nextMeeting)
        {
            MeetingDate = meeting.MeetingDate;
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

        public DateTime MeetingDate { get; set; }
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
    }
}
