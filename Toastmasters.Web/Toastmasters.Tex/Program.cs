using System;

namespace Toastmasters.Tex
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Converting File....");

            var inpath = "../../agenda/variableTest.tex";

            string outpath = "1";

            outpath = inpath.Replace(".tex", "1.tex");

            var apiCall = new WebGet("http://localhost:8656/");
            var meetingTask = apiCall.GetMeetingAsync("Meetings/Next");
            meetingTask.Wait();

            var meeting = meetingTask.Result;
            var file = FileManager.ReadAndReplace(inpath, meeting);

            FileManager.WriteFile(outpath, file);

            Console.ReadKey();
        }
    }
}