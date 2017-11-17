﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Toastmasters.Web.Models;

namespace Toastmasters.Web.Helpers
{
    public static class Commands
    {
        const String VAGRANTPATH = "vagrant"; 
        //const String VAGRANTPATH = "Users/Doug/Projects/vpe";

        public static void LoadAgenda(AgendaViewModel model)
        {
            var inpath = $"/{VAGRANTPATH}/agenda/AgendaTemplate.tex";
            string outpath = inpath.Replace("Template.tex", ".tex");

            WriteFile(outpath, ReadAndReplace(inpath, model));

            RunLocalFile("latex2rtf", $"/{VAGRANTPATH}/agenda/Agenda.tex");
        }

        public static void LoadEmail(IEnumerable<MeetingViewModel> model)
        {
            var inpath = $"/{VAGRANTPATH}/agenda/EmailTemplate.tex";
            var outpath = inpath.Replace("Template.tex", ".tex");

            WriteFile(outpath, ReadAndReplace(inpath, model));

            RunLocalFile("latex2rtf", $"/{VAGRANTPATH}/agenda/Email.tex");
        }

        public static FileStream GetFile(FilesToGet file)
        {
            return new FileStream($"/{VAGRANTPATH}/agenda/{file}.rtf", FileMode.Open);
        }

        public enum FilesToGet
        {
            Agenda,
            Email
        }

        private static void WriteFile(string outpath, List<string> file)
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

        public static List<string> ReadAndReplace<T>(string inpath, T data)
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
                                var middle = data.GetType().GetProperty(variableName).GetValue(data);
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

        public static List<string> ReadAndReplace<T>(string inpath, IEnumerable<T> dataList)
        {
            var IteratorChars = new String[] { "A", "B", "C", "D", "E" };

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

                                Console.WriteLine($"Line: {line}");
                                var dataIndex = Array.IndexOf(IteratorChars, variableName.Substring(variableName.Length - 1));

                                Console.WriteLine($"Line: {line}");
                                var data = (T)Activator.CreateInstance(typeof(T));

                                if (dataIndex == -1)
                                {

                                    data = dataList.ElementAt(0);
                                }
                                else
                                {

                                    data = dataList.ElementAt(dataIndex + 1);
                                    variableName = variableName.Substring(0, variableName.Length - 1);
                                }

                                var first = line.Substring(0, start);
                                var middle = data.GetType().GetProperty(variableName).GetValue(data);
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

        public static void RunLocalFile(string file, string arguments)
        {
            Thread.Sleep(3000);
            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = file,
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                }
            };

            proc.Start();
            proc.WaitForExit();
        }
    }
}
