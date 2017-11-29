using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Toastmasters.Web.Data;
using Toastmasters.Web.Models;

namespace Toastmasters.Web.Helpers
{
    public class MeetingHelpers
    {
        private readonly ApplicationDbContext _context;

        public MeetingHelpers(ApplicationDbContext context)
        {
            _context = context;
        }

        public void FillSomeMeetings(DateTime date, List<Meeting> list, Int32 number)
        {
            Meeting meeting = GetMeetingAfterDate(date);

            if (meeting != null)
            {
                list.Add(meeting);
                if (number > 0)
                {
                    FillSomeMeetings(meeting.MeetingDate, list, --number);
                }
                return;
            }
            return;
        }

        public Meeting GetMeetingAfterDate(DateTime beforeDate)
        {
            return _context.Meetings
                .Include(m => m.Toastmaster)
                .Include(m => m.TableTopics)
                .Include(m => m.SpeakerI)
                .Include(m => m.SpeakerII)
                .Include(m => m.GeneralEvaluator)
                .Include(m => m.EvaluatorI)
                .Include(m => m.EvaluatorII)
                .Include(m => m.Inspirational)
                .Include(m => m.Joke)
                .Include(m => m.Timer)
                .Include(m => m.Grammarian)
                .Include(m => m.BallotCounter)
                .Include(m => m.President)
                .Include(m => m.Sargent)
                .Where(m => m.MeetingDate > beforeDate)
                .OrderBy(m => m.MeetingDate)
                .FirstOrDefault();
        }

        public enum MeetingRole
        {
            Toastmaster,
            TableTopics,
            SpeakerI,
            SpeakerII,
            GeneralEvaluator,
            EvaluatorI,
            EvaluatorII,
            Inspirational,
            Joke,
            Timer,
            Grammarian,
            BallotCounter
        }
    }
}
