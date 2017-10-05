using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

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

            var apiCall = new WebGet("http://localhost/");
            String error;
            var meeting = apiCall.GetMeeting<MeetingAgenda>("Meetings/Next", out error);

            if (!String.IsNullOrWhiteSpace(error) )
            {
                Console.WriteLine($"ERROR: {error}");
            }
        
            FileManager.WriteFile(outpath, FileManager.ReadAndReplace(inpath, meeting));

            Console.WriteLine("Converting NextFive....");

            inpath = "/vagrant/agenda/NextFiveTemplate.tex";
            //inpath = "/Users/Doug/Projects/vpe/agenda/NextFiveTemplate.tex";
            outpath = "1";

            outpath = inpath.Replace("Template.tex", ".tex");


            var meetingList = apiCall.GetMeetingList<Meeting>("Meetings/NextFive", out error);

            if (!String.IsNullOrWhiteSpace(error) )
            {
                Console.WriteLine($"ERROR: {error}");
            }
        
            FileManager.WriteFile(outpath, FileManager.ReadAndReplace(inpath, meetingList));

            Thread.Sleep(3000);
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "/vagrant/generate.sh";
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;

            Process proc = new Process
            {
                StartInfo = psi
            };

            proc.Start();

        }
    }
}