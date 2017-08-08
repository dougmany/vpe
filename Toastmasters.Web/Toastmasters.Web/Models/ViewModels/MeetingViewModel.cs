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
            Evaluator1 = meeting.Evaluator1.FullName;
            Evaluator2 = meeting.Evaluator2.FullName;
            Timer = meeting.Timer.FullName;
            BallotCounter = meeting.BallotCounter.FullName;
            Grammarian = meeting.Grammarian.FullName;
            TableTopics = meeting.TableTopics.FullName;
            Speaker1 = meeting.Speaker1.FullName;
            Speaker2 = meeting.Speaker2.FullName;
            President = meeting.President.FullName;
            Sargent = meeting.Sargent.FullName;
        }

        public DateTime MeetingDate { get; set; }
        public String Toastmaster { get; set; }
        public String Inspirational { get; set; }
        public String Joke { get; set; }
        public String GeneralEvaluator { get; set; }
        public String Evaluator1 { get; set; }
        public String Evaluator2 { get; set; }
        public String Timer { get; set; }
        public String BallotCounter { get; set; }
        public String Grammarian { get; set; }
        public String TableTopics { get; set; }
        public String Speaker1 { get; set; }
        public String Speaker2 { get; set; }
        public String President { get; set; }
        public String Sargent { get; set; }
        public String Absent1 { get; set; }
        public String Absent2 { get; set; }
    }
}
