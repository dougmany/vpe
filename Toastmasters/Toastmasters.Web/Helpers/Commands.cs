using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Toastmasters.Web.Helpers
{
    public static class Commands
    {
        public static void LoadFile()
        {
            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = @"/vagrant/generate.sh",
                    Arguments = "",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };


            proc.Start();
            //while (!proc.StandardOutput.EndOfStream)
            //{

            //}

        }

        public static FileStream GetFile(FilesToGet file)
        {
            return new FileStream($"/vagrant/agenda/{file}.rtf", FileMode.Open);
            //return new FileStream($"/Users/doug/Projects/vpe/agenda/{file}.rtf", FileMode.Open);
        }

        public enum FilesToGet
        {
            Agenda,
            Email
        }
    }
}
