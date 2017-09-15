using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Toastmasters.Web.Helpers
{
    public class Commands
    {
        public String GetAgenda(Int32 meetingID)
        {

            var line = "";
            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "program.exe",
                    Arguments = "command line arguments to your executable",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };


            proc.Start();
            while (!proc.StandardOutput.EndOfStream)
            {
                line = proc.StandardOutput.ReadLine();
                // do something with line
            }

            return line;
        }
    }
}
