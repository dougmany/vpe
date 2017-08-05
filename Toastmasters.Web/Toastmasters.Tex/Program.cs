using System;
using System.IO;
using System.Text;

namespace Toastmasters.Tex
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var inpath = args[0];

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

            using (var stream = new FileStream(inpath, FileMode.Open))
            using (var reader = new StreamReader(stream))
            {
                try
                {

                    FileStream outStream = new System.IO.FileStream(outpath, FileMode.Create, FileAccess.Write);
                    var outWriter = new StreamWriter(outStream, Encoding.UTF8);

                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains("##"))
                        {
                            var start = line.IndexOf("##");
                            var end = line.Substring(start).IndexOf("}");
                            var variableName = line.Substring(start, end);
                        }
                    }
                }
                finally
                {
                    //if (outstream != null)
                    //{
                    //    outstream.Close();
                    //    outstream = null;
                    //}
                }
            }
        }
    }
}