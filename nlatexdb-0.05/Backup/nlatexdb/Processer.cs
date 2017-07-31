//    nlatexdb Version 0.04
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
using System.IO;



namespace nlatexdb
{
    class Processer
    {
        public Processer()
        {
            m_verbosity = 0;
            m_encoding = Encoding.GetEncoding(1252);
            m_commandlineargs = new List<string>();

            m_activecommand = null;
            m_cmds = new List<CommandParser>();
            m_cmds.Add(new CommandParser("texdbconnectionnet", 2, false, texdbconnection));
            m_cmds.Add(new CommandParser("texdbdef", 3, false, texdbdef));
            m_cmds.Add(new CommandParser("texdbfor", 2, true, texdbfor));
            m_cmds.Add(new CommandParser("texdbforfile", 3, true, texdbforfile));
            m_cmds.Add(new CommandParser("texdbif", 2, true, texdbif));
            m_cmds.Add(new CommandParser("texdbcommand", 1, false, texdbcommand));

            m_qrys = new Dictionary<string, SqlQuery>();
            m_varvalues = new Dictionary<string, SqlQueryVar>();
			m_latexpostprocess_wholefile = true;
			
			m_cmdlineargvarprefix = "##";
			
			SqlQueryVar.ClearLatexReplace();
			SqlQueryVar.AddLatexReplace('\\', @"\ensuremath{\backslash}");
            SqlQueryVar.AddLatexReplace('_', @"\_");
            SqlQueryVar.AddLatexReplace('$', @"\$");
            SqlQueryVar.AddLatexReplace('&', @"\&");
            SqlQueryVar.AddLatexReplace('#', @"\#");
            SqlQueryVar.AddLatexReplace('{', @"\{");
            SqlQueryVar.AddLatexReplace('}', @"\}");
            SqlQueryVar.AddLatexReplace('~', @"\~{}");
            SqlQueryVar.AddLatexReplace('%', @"\%");

			
			ReadAppConfig();
        }
		
		private void ReadAppConfig()
		{
			NlatexdbSettings settings = System.Configuration.ConfigurationManager.GetSection("NlatexdbSettings")
				as NlatexdbSettings;
			if (settings != null)
			{
				string cmdl2 = settings.CmdLineArgumentVarPrefix;
				if (!String.IsNullOrEmpty(cmdl2))
				{
					m_cmdlineargvarprefix = cmdl2;
					Debug("prefix: {0}", cmdl2);
				}
				
				string regexsplitter = settings.RegexSplitter;
				if (!String.IsNullOrEmpty(regexsplitter))
				{
					SqlQueryVar.SetRegexSplitter(regexsplitter);
				}
				
				string varsplitter = settings.VariableSplitter;
				if (!String.IsNullOrEmpty(varsplitter))
				{
					SqlQuery.SetVariableSplitter(varsplitter);
				}
				
				LatexCharReplaceCollection lrcoll = settings.LatexCharReplaceItems;
				foreach (LatexCharReplaceElement lrelem in lrcoll)
				{
					Debug("replace: {0} by: {1}", lrelem.Char[0], lrelem.Replace);
					SqlQueryVar.AddLatexReplace(lrelem.Char[0], lrelem.Replace);
				}
			}
		}

        public void setInpath(string path)
        {
            m_inpath = path;
        }
        public void setOutpath(string path)
        {
            m_outpath = path;
        }
        public void setLatexbefehl(string befehl)
        {
            m_latexbefehl = befehl;
        }
        public void setEncoding(string enc)
        {
            if (enc.Equals("utf-8", StringComparison.InvariantCultureIgnoreCase))
            {
                m_encoding = new UTF8EncodingWithoutPreamble();
            }
            else
            {
                try
                {
                    m_encoding = Encoding.GetEncoding(enc);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Debug(ex.ToString());
                }
            }
        }

        public void incVerbosity()
        {
            m_verbosity++;
        }

        public void addCmdlineArg(string arg)
        {
            m_commandlineargs.Add(arg);
            string key = m_cmdlineargvarprefix + m_commandlineargs.Count.ToString();
            m_varvalues[key] = new SqlQueryVar(key, arg);
;
        }

        public int Go()
        {
            int ergebnis = 0;
            try
            {
                if (String.IsNullOrEmpty(m_inpath))
                {
                    throw new Exception("No input file given!");
                }

				string outpath = m_outpath;
                if (String.IsNullOrEmpty(outpath))
                {
                    outpath = m_inpath.Replace(".tex", "1.tex");
                    if (String.IsNullOrEmpty(outpath))
                    {
                        outpath = String.Concat(m_inpath, "1");
                    }
                }

                using (StreamReader sr = new StreamReader(m_inpath, m_encoding))
                {
					try
					{
						OpenOutstream(outpath);

						string line;
						while ((line = sr.ReadLine()) != null)
						{
							HandleLine(line);
						}
					}
					finally
					{
						CloseOutstream();
					}
                }

				LatexPostprocess(outpath, true);
				

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Debug(ex.ToString());
                ergebnis = 1;
            }
            finally
            {
                if (m_conn != null)
                {
                    try
                    {
                        m_conn.Close();
                    }
                    catch
                    {
                    }
                }

                if (m_outstream != null)
                {
                    try
                    {
                        m_outstream.Close();
                    }
                    catch
                    {
                    }
                }
            }
            return ergebnis;
        }

		public void OpenOutstream(string outpath)
		{
			CloseOutstream(); // just in case
            System.IO.FileStream outstream =
                new System.IO.FileStream(outpath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
			m_outstream = new System.IO.StreamWriter(outstream, m_encoding);
		}
		
		public void CloseOutstream()
		{
			if (m_outstream != null)
			{
				m_outstream.Close();
				m_outstream=null;
			}
		}
		
		public void LatexPostprocess(string texinputpath, bool renametoinpath)
		{
			if (renametoinpath && !m_latexpostprocess_wholefile)
			{
				// If there was a \texdbforfile command, don't postprocess the
				// top file: It is expected to be empty.
				return;
			}
			
			if (!String.IsNullOrEmpty(m_latexbefehl))
			{
				System.Diagnostics.Process p = new System.Diagnostics.Process();
				p.StartInfo.FileName = m_latexbefehl;
				p.StartInfo.CreateNoWindow = true;
				p.StartInfo.Arguments = texinputpath;
				p.Start();
				p.WaitForExit();
				
				
				string extension = null;
				if (m_latexbefehl.Equals("latex"))
				{
					extension = "dvi";
				}
				else if (m_latexbefehl.Equals("pdflatex"))
				{
					extension = "pdf";
				}
				
				if (renametoinpath && !String.IsNullOrEmpty(extension))
				{
					string filefromlatex = System.IO.Path.ChangeExtension(texinputpath, extension);
					string filetorename = System.IO.Path.ChangeExtension(m_inpath, extension);
					System.IO.File.Delete(filetorename);
					Debug("Renaming {0} to {1}", filefromlatex, filetorename);
					System.IO.File.Move(filefromlatex, filetorename);
				}
				
				
			}
			
		}
		
		public void ListProviders()
		{
			System.Data.DataTable providers = System.Data.Common.DbProviderFactories.GetFactoryClasses();
			foreach (System.Data.DataRow provider in providers.Rows)
            {
                foreach (System.Data.DataColumn c in providers.Columns)
                    Console.WriteLine(c.ColumnName + ":" + provider[c]);
                Console.WriteLine("---");
            }
        }

        private void HandleRestOfLine(string line, int hintercmd)
        {
            if (hintercmd >= 0 && hintercmd < line.Length)
            {
                HandleLine(line.Substring(hintercmd));
            }
        }

        public void HandleLine(string line)
        {
            if (m_activecommand != null)
            {
                // Aha, ein Kommando ist aktiv. Sammele die Zeilen.
                int hintercmd = m_activecommand.ProcessLine(line);
                HandleRestOfLine(line, hintercmd);
                return;
            }

            // Erstmal schauen nach Kommentar
            int prozentsuche = 0;
            int prozentindex = -1;
            while (prozentsuche >= 0)
            {
                prozentindex = line.IndexOf('%', prozentsuche);
                if (prozentindex > 0 && line[prozentindex - 1] == '\\')
                {
                    // Dieses % ist auskommentiert. Dahinter weitersuchen.
                    prozentsuche = prozentindex + 1;
                    prozentindex = -1;
                }
                else
                {
                    break;
                }
            }

            int firstcommandindex = line.Length + 1;
			CommandParser foundCommand = null;
            foreach (CommandParser c in m_cmds)
            {
                int cmdindex = c.FirstIndexIn(line, prozentindex);
                if (cmdindex >= 0 && cmdindex < firstcommandindex)
                {
                    foundCommand = c;
                    firstcommandindex = cmdindex;
                }
            }

            if (foundCommand != null)
            {
                // Aha, Kommando gefunden. 
                if (firstcommandindex > 0)
                {
                    // Da ist noch Text davor.
                    HandleLine(line.Substring(0, firstcommandindex));
                }
				
				m_activecommand = foundCommand;
                
                // Rufe Kommando auf. Bzw. erstmal die Klammern suchen.
                int hintercmd = m_activecommand.StartProcess(line, firstcommandindex);
                HandleRestOfLine(line, hintercmd);
            }
            else
            {
                // Kein Kommando in dieser Zeile. Einfach ausgeben. Erst aber Variablen einfügen.
				line = VariablenEinfuegen(line);
                m_outstream.WriteLine(line);
            }
        }
		
		public string VariablenEinfuegen(string inputline)
		{
			string line = inputline;
            foreach (KeyValuePair<string, SqlQueryVar> var in m_varvalues)
            {
                line = line.Replace(var.Key, var.Value.getValueLatex());
            }
			return line;
		}

        private string listtostr(List<string> lines)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in lines)
            {
                if (sb.Length > 0)
                {
                    sb.Append(" ");
                }
                sb.Append(s);
            }
            return sb.ToString();
        }

        private void texdbconnection(List<List<string>> pars)
        {
            m_activecommand = null;
            string provider = listtostr(pars[0]);
            string connstring = listtostr(pars[1]);
			Debug("texdbconnection provider: {0} connstring: {1}", provider, connstring);
            System.Data.Common.DbProviderFactory dbfac = System.Data.Common.DbProviderFactories.GetFactory(provider);
            m_conn = dbfac.CreateConnection();
            m_conn.ConnectionString = connstring;
            m_conn.Open();
        }

        private void texdbdef(List<List<string>> pars)
        {
            m_activecommand = null;
            string queryname = listtostr(pars[0]);
            string querytext = listtostr(pars[1]);
            string queryvars = listtostr(pars[2]);
			Debug("texdbdef name: {0} sql: {1} vars: {2}", queryname, querytext, queryvars);
            m_qrys.Add(queryname, new SqlQuery(querytext, queryvars));
        }

        private void texdbfor(List<List<string>> pars)
        {
            m_activecommand = null;
            string queryname = listtostr(pars[0]);
            List<string> texstuff = pars[1];
			Debug("texdbfor query: {0} tex lines: {1}", queryname, texstuff.Count);
            SqlQuery qry = m_qrys[queryname];
            if (qry == null)
            {
                throw new Exception("Query " + queryname + " not found");
            }
            qry.Execute(texstuff, m_conn, m_varvalues, this, null);
        }

        private void texdbforfile(List<List<string>> pars)
        {
            m_activecommand = null;
			m_latexpostprocess_wholefile = false;
            string queryname = listtostr(pars[0]);
            string filepattern = listtostr(pars[1]);
            List<string> texstuff = pars[2];
			Debug("texdbforfile query: {0} filepattern: {1} tex lines: {2}", queryname, filepattern, texstuff.Count);
            SqlQuery qry = m_qrys[queryname];
            if (qry == null)
            {
                throw new Exception("Query " + queryname + " not found");
            }
            qry.Execute(texstuff, m_conn, m_varvalues, this, filepattern);
        }

        private void texdbif(List<List<string>> pars)
        {
            m_activecommand = null;
            string queryname = listtostr(pars[0]);
            List<string> texstuff = pars[1];
			Debug("texdbif query: {0} tex lines: {1}", queryname, texstuff.Count);
            SqlQuery qry = m_qrys[queryname];
            if (qry == null)
            {
                throw new Exception("Query " + queryname + " not found");
            }
            qry.ExecuteIf(texstuff, m_conn, m_varvalues, this);
        }

        private void texdbcommand(List<List<string>> pars)
        {
            m_activecommand = null;
            string cmd = listtostr(pars[0]);
			Debug("texdbcommand sql: {0}", cmd);
            using (System.Data.Common.DbCommand comm = m_conn.CreateCommand())
            {
                comm.CommandText = cmd;
                comm.ExecuteNonQuery();
            }
        }

        public static void Debug(string format, params object[] pars)
        {
            if (m_verbosity >= 1)
            {
                Console.WriteLine(format, pars);
            }
        }
		
		public static bool IsDebugEnabled
		{
		get {return (m_verbosity >= 1);}
		}

		private string m_cmdlineargvarprefix;
		
        private static int m_verbosity = 0;
        private string m_outpath;
        private string m_latexbefehl;
		private bool m_latexpostprocess_wholefile;
        private Encoding m_encoding;
        private string m_inpath;
        private List<string> m_commandlineargs;
        private System.Data.Common.DbConnection m_conn;

        private List<CommandParser> m_cmds;
        private CommandParser m_activecommand;
        private System.IO.StreamWriter m_outstream;

        private System.Collections.Generic.Dictionary<string, SqlQuery> m_qrys;
        private System.Collections.Generic.Dictionary<string, SqlQueryVar> m_varvalues;

    }
}


// Latex can't handle the BOM (EF BB BF) that is prepended to UTF-8 by default.
// So here's a UTF-8 encoding without BOM.

public class UTF8EncodingWithoutPreamble : System.Text.UTF8Encoding
{
    public static UTF8EncodingWithoutPreamble Instance = new
    UTF8EncodingWithoutPreamble();

    private static byte[] _preamble = new byte[0];

    public override byte[] GetPreamble()
    {
        return _preamble;
    }
}
