using System;

namespace Toastmasters.Web.Models
{
    public class MeetingViewModel
    {
        public MeetingViewModel(Meeting meeting)
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
    }
}
