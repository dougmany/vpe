using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Toastmasters.Tex
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Converting File....");

            var inpath = "../../agenda/variableTest.tex"; //args[0];

            if (String.IsNullOrEmpty(inpath))
            {
                throw new Exception("No input file given!");
            }

            string outpath = "";
            if (String.IsNullOrEmpty(outpath))
            {
                outpath = inpath.Replace(".tex", "1.tex");
                if (String.IsNullOrEmpty(outpath))
                {
                    outpath = String.Concat(inpath, "1");
                }
            }

            List<String> file = new List<string>();

            var apiCall = new WebGet();
            var meetingTask = apiCall.GetMeetingAsync("Meetings/Next");
            meetingTask.Wait();

            var meeting = meetingTask.Result;

            using (var stream = new FileStream(inpath, FileMode.Open))
            {
                using (var reader = new StreamReader(stream))
                {
                    try
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            //var apiCall = new WebGet();
                            //var Meeting = apiCall.GetMeetingAsync("Meetings/Next");
                            if (line.Contains("##"))
                            {
                                var start = line.IndexOf("##");
                                var length = line.Substring(start).IndexOf("}");
                                var variableName = line.Substring(start + 2, length - 2);

                                var first = line.Substring(0, start);
                                var middle = meeting.GetType().GetProperty(variableName).GetValue(meeting);
                                var last = line.Substring(start + length);
                                line = first + middle + last;
                            }

                            file.Add(line);
                        }

                    }
                    finally
                    {

                    }
                }
            }

            using (FileStream outStream = new FileStream(outpath, FileMode.Create, FileAccess.Write))
            {
                var outWriter = new StreamWriter(outStream, Encoding.UTF8);
                for (var i = 0; i < file.Count; i++)
                {
                    outWriter.WriteLine(file[i]);

                    Console.WriteLine($"Line {i:d3}: {file[i]}");
                }
                outWriter.Flush();
            }

            Console.ReadKey();
        }
    }
}