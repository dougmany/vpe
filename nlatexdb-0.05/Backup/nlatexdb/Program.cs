//    nlatexdb Version 0.05
//    Database Access in LaTeX
//    Copyright (C) 2011 Robin Höns, Integranova GmbH
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
//    For more information see the web page http://hoens.net/robin


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XGetoptCS;

namespace nlatexdb
{
    class Program
    {
        static void Help()
        {
            Console.WriteLine("NlatexDB 0.05 Copyright (C) 2011-2012 Robin Hoens");
            Console.WriteLine("This program comes with ABSOLUTELY NO WARRANTY.");
            Console.WriteLine("This is free software, and you are welcome to redistribute it");
            Console.WriteLine("under certain conditions.");
            Console.WriteLine("See file COPYING for details.");
            Console.WriteLine();
            Console.WriteLine("Usage:");
            Console.WriteLine("nlatexdb [Arguments] <texfile.tex> [Parameters...]");
            Console.WriteLine("Arguments can be:");
            Console.WriteLine("-p = call pdflatex on result");
            Console.WriteLine("-l = call latex on result");
            Console.WriteLine("-c <latexcommand> = call <latexcommand> on result");
            Console.WriteLine("-e <encoding> = use string <encoding> on input and output file");
            Console.WriteLine("-o <outpath> = write result to <outpath>");
            Console.WriteLine("-n = do not replace LaTeX special characters from database");
            Console.WriteLine("-P = list known database providers");
            Console.WriteLine("-v = increase verbosity");
            Console.WriteLine("-h = output this help text");
        }


        static int Main(string[] args)
        {
            Processer proc = new Processer();
            int argc = args.Length;
            bool help = false;
            char c;
            XGetopt go = new XGetopt();
            while ((c = go.Getopt(argc, args, "plvho:c:Pe:n")) != '\0')
            {
                switch (c)
                {
                    case 'P':
                        proc.ListProviders();
                        break;

                    case 'p':
                        proc.setLatexbefehl("pdflatex");
                        break;

                    case 'l':
                        proc.setLatexbefehl("latex");
                        break;

                    case 'c':
                        proc.setLatexbefehl(go.Optarg);
                        break;

                    case 'e':
                        proc.setEncoding(go.Optarg);
                        break;

                    case 'o':
                        proc.setOutpath(go.Optarg);
                        break;

                    case 'v':
                        proc.incVerbosity();
                        break;
					
					case 'n':
						SqlQueryVar.ClearLatexReplace();
						break;
	
                    case 'h':
                        help = true;
                        break;

                    case '?':
                        Console.WriteLine("illegal option or missing arg");
                        help = true;
                        break;
                }
            }

            if (go.Optarg != string.Empty)
            {
                proc.setInpath(go.Optarg);
                int i = go.Optind + 1;
                while (i < args.Length)
                {
                    proc.addCmdlineArg(args[i]);
                    i++;
                }
            }
            else 
            {
                help = true;
            }

            if (help)
            {
                Help();
                return 1;
            }

            return proc.Go();
        }
    }
}
