using System;

namespace Toastmasters.Tex
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Converting File....");

            var inpath = "/vagrant/agenda/AgendaTemplate.tex";

            string outpath = "1";

            outpath = inpath.Replace("Template.tex", ".tex");

            var apiCall = new WebGet("http://localhost/");
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