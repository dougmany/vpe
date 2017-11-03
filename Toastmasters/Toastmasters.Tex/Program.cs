using System;
using System.Collections.Generic;

namespace Toastmasters.Tex
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Converting Agenda....");

            var inpath = "/vagrant/agenda/AgendaTemplate.tex";
            //var inpath = "/Users/Doug/Projects/vpe/agenda/AgendaTemplate.tex";

            string outpath = "1";

            outpath = inpath.Replace("Template.tex", ".tex");

            var apiCall = new WebGet("https://agwoow.ga/");
            String error;
            var meeting = apiCall.GetMeeting<MeetingAgenda>("Meetings/Next", out error);

            if (!String.IsNullOrWhiteSpace(error))
            {
                Console.WriteLine($"ERROR: {error}");
            }

            FileManager.WriteFile(outpath, FileManager.ReadAndReplace(inpath, meeting));

            LocalFile.RunLocalFile("latex2rtf", "/vagrant/agenda/Agenda.tex");

            Console.WriteLine("Converting Email....");

            inpath = "/vagrant/agenda/EmailTemplate.tex";
            //inpath = "/Users/Doug/Projects/vpe/agenda/EmailTemplate.tex";
            outpath = "1";

            outpath = inpath.Replace("Template.tex", ".tex");


            var meetingList = apiCall.GetMeetingList<Meeting>("Meetings/NextFive", out error);

            if (!String.IsNullOrWhiteSpace(error))
            {
                Console.WriteLine($"ERROR: {error}");
            }

            FileManager.WriteFile(outpath, FileManager.ReadAndReplace(inpath, meetingList));

            LocalFile.RunLocalFile("latex2rtf", "/vagrant/agenda/Email.tex");

        }
    }
}