﻿using System;
using System.Linq;
using Toastmasters.Web.Models;

namespace Toastmasters.Web.Data
{
    public class Seed
    {
        public Seed(ApplicationDbContext context)
        {
            if (context.Members.Count() == 0)
            {
                context.Add(new Member { MemberID = 1, FirstName = "Dale", LastName = "Rinde", IsPresident = true, IsSargent = false });
                context.Add(new Member { MemberID = 2, FirstName = "Doug", LastName = "Meeker", IsPresident = true, IsSargent = false });
                context.Add(new Member { MemberID = 3, FirstName = "Gina", LastName = "Leal", IsPresident = true, IsSargent = false });
                context.Add(new Member { MemberID = 4, FirstName = "Gordon", LastName = "Owyang", IsPresident = true, IsSargent = false });
                context.Add(new Member { MemberID = 5, FirstName = "Hasheem", LastName = "Whitmore", IsPresident = true, IsSargent = false });
                context.Add(new Member { MemberID = 6, FirstName = "Janelle", LastName = "Graham", IsPresident = true, IsSargent = false });
                context.Add(new Member { MemberID = 7, FirstName = "Kim", LastName = "Glazzard", IsPresident = true, IsSargent = false });
                context.Add(new Member { MemberID = 8, FirstName = "Kim", LastName = "Nguyen", IsPresident = true, IsSargent = false });
                context.Add(new Member { MemberID = 9, FirstName = "Laleh", LastName = "Rastegarzadeh", IsPresident = true, IsSargent = false });
                context.Add(new Member { MemberID = 10, FirstName = "Leo", LastName = "Barsukov", IsPresident = true, IsSargent = false });
                context.Add(new Member { MemberID = 11, FirstName = "Marty", LastName = "Gunn", IsPresident = true, IsSargent = false });
                context.Add(new Member { MemberID = 12, FirstName = "Min", LastName = "Wu", IsPresident = true, IsSargent = false });
                context.Add(new Member { MemberID = 13, FirstName = "Tyler", LastName = "Jennings", IsPresident = true, IsSargent = false });
                context.Add(new Member { MemberID = 14, FirstName = "Bill", LastName = "Stuart", IsPresident = true, IsSargent = false });
                context.Add(new Member { MemberID = 15, FirstName = "Xiaoying", LastName = "Zhou", IsPresident = true, IsSargent = false });
                //context.Add(new Member { MemberID = 16, FirstName = "Not", LastName = "Set", IsPresident = true, IsSargent = false });
            }
            context.SaveChanges();

            if (context.Meetings.Count() == 0)
            {
                context.Add(new Meeting { MeetingDate = new DateTime(2017, 07, 10, 12, 05, 00), Toastmaster = new Member { MemberID = 9 }, Inspirational = new Member { MemberID = 7 }, Joke = new Member { MemberID = 4 }, GeneralEvaluator = new Member { MemberID = 12 }, EvaluatorI = new Member { MemberID = 11 }, EvaluatorII = new Member { MemberID = 14 }, Timer = new Member { MemberID = 4 }, BallotCounter = new Member { MemberID = 7 }, Grammarian = new Member { MemberID = 10 }, TableTopics = new Member { MemberID = 1 }, SpeakerI = new Member { MemberID = 3 }, SpeakerII = new Member { MemberID = 2 }, President = new Member { MemberID = 1 }, Sargent = new Member { MemberID = 10 } });
                context.Add(new Meeting { MeetingDate = new DateTime(2017, 07, 17, 12, 05, 00), Toastmaster = new Member { MemberID = 2 }, Inspirational = new Member { MemberID = 1 }, Joke = new Member { MemberID = 7, }, GeneralEvaluator = new Member { MemberID = 8 }, EvaluatorI = new Member { MemberID = 6 }, EvaluatorII = new Member { MemberID = 9 }, Timer = new Member { MemberID = 10 }, BallotCounter = new Member { MemberID = 13 }, Grammarian = new Member { MemberID = 14 }, TableTopics = new Member { MemberID = 3 }, SpeakerI = new Member { MemberID = 4 }, SpeakerII = new Member { MemberID = 11 }, President = new Member { MemberID = 1 }, Sargent = new Member { MemberID = 10 } });
                context.Add(new Meeting { MeetingDate = new DateTime(2017, 07, 24, 12, 05, 00), Toastmaster = new Member { MemberID = 15 }, Inspirational = new Member { MemberID = 5 }, Joke = new Member { MemberID = 11 }, GeneralEvaluator = new Member { MemberID = 6 }, EvaluatorI = new Member { MemberID = 7 }, EvaluatorII = new Member { MemberID = 12 }, Timer = new Member { MemberID = 11 }, BallotCounter = new Member { MemberID = 14 }, Grammarian = new Member { MemberID = 2 }, TableTopics = new Member { MemberID = 4 }, SpeakerI = new Member { MemberID = 13 }, SpeakerII = new Member { MemberID = 10 }, President = new Member { MemberID = 1 }, Sargent = new Member { MemberID = 10 } });
                context.Add(new Meeting { MeetingDate = new DateTime(2017, 07, 31, 12, 05, 00), Toastmaster = new Member { MemberID = 4 }, Inspirational = new Member { MemberID = 8 }, Joke = new Member { MemberID = 13 }, GeneralEvaluator = new Member { MemberID = 7 }, EvaluatorI = new Member { MemberID = 10 }, EvaluatorII = new Member { MemberID = 2 }, Timer = new Member { MemberID = 1 }, BallotCounter = new Member { MemberID = 9 }, Grammarian = new Member { MemberID = 12 }, TableTopics = new Member { MemberID = 6 }, SpeakerI = new Member { MemberID = 15 }, SpeakerII = new Member { MemberID = 14 }, President = new Member { MemberID = 1 }, Sargent = new Member { MemberID = 10 }, });
                context.Add(new Meeting { MeetingDate = new DateTime(2017, 08, 07, 12, 05, 00), Toastmaster = new Member { MemberID = 8 }, Inspirational = new Member { MemberID = 12 }, Joke = new Member { MemberID = 3 }, GeneralEvaluator = new Member { MemberID = 9 }, EvaluatorI = new Member { MemberID = 4 }, EvaluatorII = new Member { MemberID = 13 }, Timer = new Member { MemberID = 5 }, BallotCounter = new Member { MemberID = 12 }, Grammarian = new Member { MemberID = 2 }, TableTopics = new Member { MemberID = 11 }, SpeakerI = new Member { MemberID = 1 }, SpeakerII = new Member { MemberID = 6 }, President = new Member { MemberID = 1 }, Sargent = new Member { MemberID = 10 } });
                context.Add(new Meeting { MeetingDate = new DateTime(2017, 08, 28, 12, 05, 00), Toastmaster = new Member { MemberID = 6 }, Inspirational = new Member { MemberID = 3 }, Joke = new Member { MemberID = 2 }, GeneralEvaluator = new Member { MemberID = 15 }, EvaluatorI = new Member { MemberID = 1 }, EvaluatorII = new Member { MemberID = 5 }, Timer = new Member { MemberID = 12 }, BallotCounter = new Member { MemberID = 3 }, Grammarian = new Member { MemberID = 9 }, TableTopics = new Member { MemberID = 2 }, SpeakerI = new Member { MemberID = 7 }, SpeakerII = new Member { MemberID = 4 }, President = new Member { MemberID = 2 }, Sargent = new Member { MemberID = 10 } });
                context.Add(new Meeting { MeetingDate = new DateTime(2017, 09, 18, 12, 05, 00), Toastmaster = new Member { MemberID = 7 }, Inspirational = new Member { MemberID = 13 }, Joke = new Member { MemberID = 10 }, GeneralEvaluator = new Member { MemberID = 1 }, EvaluatorI = new Member { MemberID = 15 }, EvaluatorII = new Member { MemberID = 11 }, Timer = new Member { MemberID = 6 }, BallotCounter = new Member { MemberID = 2 }, Grammarian = new Member { MemberID = 5 }, TableTopics = new Member { MemberID = 14 }, SpeakerI = new Member { MemberID = 9 }, SpeakerII = new Member { MemberID = 8 }, President = new Member { MemberID = 1 }, Sargent = new Member { MemberID = 10 } });
                context.Add(new Meeting { MeetingDate = new DateTime(2017, 09, 25, 12, 05, 00), Toastmaster = new Member { MemberID = 11 }, Inspirational = new Member { MemberID = 7 }, Joke = new Member { MemberID = 4 }, GeneralEvaluator = new Member { MemberID = 5 }, EvaluatorI = new Member { MemberID = 10 }, EvaluatorII = new Member { MemberID = 3 }, Timer = new Member { MemberID = 7 }, BallotCounter = new Member { MemberID = 9 }, Grammarian = new Member { MemberID = 14 }, TableTopics = new Member { MemberID = 8 }, SpeakerI = new Member { MemberID = 14 }, SpeakerII = new Member { MemberID = 13 }, President = new Member { MemberID = 1 }, Sargent = new Member { MemberID = 10 } });
                context.Add(new Meeting { MeetingDate = new DateTime(2017, 10, 02, 12, 05, 00), Toastmaster = new Member { MemberID = 5 }, Inspirational = new Member { MemberID = 8 }, Joke = new Member { MemberID = 1 }, GeneralEvaluator = new Member { MemberID = 2 }, EvaluatorI = new Member { MemberID = 9 }, EvaluatorII = new Member { MemberID = 8 }, Timer = new Member { MemberID = 12 }, BallotCounter = new Member { MemberID = 4 }, Grammarian = new Member { MemberID = 10 }, TableTopics = new Member { MemberID = 13 }, SpeakerI = new Member { MemberID = 1 }, SpeakerII = new Member { MemberID = 12 }, President = new Member { MemberID = 1 }, Sargent = new Member { MemberID = 10 } });
                context.Add(new Meeting { MeetingDate = new DateTime(2017, 10, 09, 12, 05, 00), Toastmaster = new Member { MemberID = 1 }, Inspirational = new Member { MemberID = 12 }, Joke = new Member { MemberID = 6 }, GeneralEvaluator = new Member { MemberID = 3 }, EvaluatorI = new Member { MemberID = 14 }, EvaluatorII = new Member { MemberID = 11 }, Timer = new Member { MemberID = 2 }, BallotCounter = new Member { MemberID = 5 }, Grammarian = new Member { MemberID = 13 }, TableTopics = new Member { MemberID = 7 }, SpeakerI = new Member { MemberID = 4 }, SpeakerII = new Member { MemberID = 15 }, President = new Member { MemberID = 1 }, Sargent = new Member { MemberID = 10 } });
                context.Add(new Meeting { MeetingDate = new DateTime(2017, 10, 16, 12, 05, 00), Toastmaster = new Member { MemberID = 5 }, Inspirational = new Member { MemberID = 15 }, Joke = new Member { MemberID = 14 }, GeneralEvaluator = new Member { MemberID = 4 }, EvaluatorI = new Member { MemberID = 12 }, EvaluatorII = new Member { MemberID = 6 }, Timer = new Member { MemberID = 13 }, BallotCounter = new Member { MemberID = 7 }, Grammarian = new Member { MemberID = 1 }, TableTopics = new Member { MemberID = 10 }, SpeakerI = new Member { MemberID = 2 }, SpeakerII = new Member { MemberID = 9 }, President = new Member { MemberID = 1 }, Sargent = new Member { MemberID = 10 } });
                context.Add(new Meeting { MeetingDate = new DateTime(2017, 10, 23, 12, 05, 00), Toastmaster = new Member { MemberID = 13 }, Inspirational = new Member { MemberID = 14 }, Joke = new Member { MemberID = 8 }, GeneralEvaluator = new Member { MemberID = 11 }, EvaluatorI = new Member { MemberID = 2 }, EvaluatorII = new Member { MemberID = 4 }, Timer = new Member { MemberID = 9 }, BallotCounter = new Member { MemberID = 1 }, Grammarian = new Member { MemberID = 10 }, TableTopics = new Member { MemberID = 15 }, SpeakerI = new Member { MemberID = 6 }, SpeakerII = new Member { MemberID = 5 }, President = new Member { MemberID = 1 }, Sargent = new Member { MemberID = 10 } });
                context.Add(new Meeting { MeetingDate = new DateTime(2017, 10, 30, 12, 05, 00), Toastmaster = new Member { MemberID = 10 }, Inspirational = new Member { MemberID = 9 }, Joke = new Member { MemberID = 15 }, GeneralEvaluator = new Member { MemberID = 14 }, EvaluatorI = new Member { MemberID = 8 }, EvaluatorII = new Member { MemberID = 13 }, Timer = new Member { MemberID = 3 }, BallotCounter = new Member { MemberID = 6 }, Grammarian = new Member { MemberID = 7 }, TableTopics = new Member { MemberID = 12 }, SpeakerI = new Member { MemberID = 11 }, SpeakerII = new Member { MemberID = 1 }, President = new Member { MemberID = 1 }, Sargent = new Member { MemberID = 9 } });
                context.Add(new Meeting { MeetingDate = new DateTime(2017, 11, 13, 12, 05, 00), Toastmaster = new Member { MemberID = 12 }, Inspirational = new Member { MemberID = 2 }, Joke = new Member { MemberID = 7 }, GeneralEvaluator = new Member { MemberID = 13 }, EvaluatorI = new Member { MemberID = 7 }, EvaluatorII = new Member { MemberID = 1 }, Timer = new Member { MemberID = 14 }, BallotCounter = new Member { MemberID = 8 }, Grammarian = new Member { MemberID = 3 }, TableTopics = new Member { MemberID = 9 }, SpeakerI = new Member { MemberID = 10 }, SpeakerII = new Member { MemberID = 5 }, President = new Member { MemberID = 1 }, Sargent = new Member { MemberID = 10 } });
                context.Add(new Meeting { MeetingDate = new DateTime(2017, 11, 20, 12, 05, 00), Toastmaster = new Member { MemberID = 3 }, Inspirational = new Member { MemberID = 4 }, Joke = new Member { MemberID = 2 }, GeneralEvaluator = new Member { MemberID = 10 }, EvaluatorI = new Member { MemberID = 5 }, EvaluatorII = new Member { MemberID = 9 }, Timer = new Member { MemberID = 4 }, BallotCounter = new Member { MemberID = 13 }, Grammarian = new Member { MemberID = 11 }, TableTopics = new Member { MemberID = 5 }, SpeakerI = new Member { MemberID = 8 }, SpeakerII = new Member { MemberID = 14 }, President = new Member { MemberID = 1 }, Sargent = new Member { MemberID = 10 } });
                context.Add(new Meeting { MeetingDate = new DateTime(2017, 11, 27, 12, 05, 00), Toastmaster = new Member { MemberID = 14 }, Inspirational = new Member { MemberID = 10 }, Joke = new Member { MemberID = 9 }, GeneralEvaluator = new Member { MemberID = 12 }, EvaluatorI = new Member { MemberID = 6 }, EvaluatorII = new Member { MemberID = 3 }, Timer = new Member { MemberID = 11 }, BallotCounter = new Member { MemberID = 2 }, Grammarian = new Member { MemberID = 4 }, TableTopics = new Member { MemberID = 1 }, SpeakerI = new Member { MemberID = 3 }, SpeakerII = new Member { MemberID = 13 }, President = new Member { MemberID = 1 }, Sargent = new Member { MemberID = 10 } });
                context.Add(new Meeting { MeetingDate = new DateTime(2017, 11, 06, 12, 05, 00), Toastmaster = new Member { MemberID = 8 }, SpeakerI = new Member { MemberID = 7 }, President = new Member { MemberID = 1 }, Sargent = new Member { MemberID = 10 } });
            }

            context.SaveChanges();
        }
    }
}
