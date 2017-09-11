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

            var apiCall = new WebGet("http://localhost:8000/");
            String error;
            var meeting = apiCall.GetMeeting("Meetings/Next", out error);

            if (!String.IsNullOrWhiteSpace(error) )
            {
                Console.WriteLine($"ERROR: {error}");
            }
        
            FileManager.WriteFile(outpath, FileManager.ReadAndReplace(inpath, meeting));

            Console.ReadKey();
        }
    }
}