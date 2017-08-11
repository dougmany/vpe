using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Toastmasters.Tex
{
    class FileManager
    {
        public static void WriteFile(string outpath, List<string> file)
        {
            using (FileStream outStream = new FileStream(outpath, FileMode.Create, FileAccess.Write))
            {
                using (var outWriter = new StreamWriter(outStream, Encoding.UTF8))
                {
                    for (var i = 0; i < file.Count; i++)
                    {
                        outWriter.WriteLine(file[i]);

                        Console.WriteLine($"Line {i:d3}: {file[i]}");
                    }
                    outWriter.Flush();
                }
            }
        }

        public static List<string> ReadAndReplace(string inpath, Meeting meeting)
        {
            var file = new List<string>();
            using (var stream = new FileStream(inpath, FileMode.Open))
            {
                using (var reader = new StreamReader(stream))
                {
                    try
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
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

                    return file;
                }
            }
        }
    }
}
