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

namespace nlatexdb
{
    class CommandParser
    {
        public delegate void ExecCommand(List<List<string>> pars);


        public CommandParser(string commandname, int nbbraces, bool lastbraceislatex, ExecCommand cmd)
        {
            m_commandsearch = String.Concat("\\", commandname, "{");
            m_nbbraces = nbbraces;
            m_lastbraceislatex = lastbraceislatex;
            m_cmd = cmd;
        }

        public int FirstIndexIn(string line, int commentbegin)
        {
            if (commentbegin >= 0)
            {
                return line.IndexOf(m_commandsearch, 0, commentbegin);
            }
            return line.IndexOf(m_commandsearch);
        }

        public int StartProcess(string line, int foundindex)
        {
            int myindex = foundindex + m_commandsearch.Length;
            m_bracedepth = 1;
            m_bracescompleted = 0;
            m_pars = new List<List<string>>();
            m_pars.Add(new List<string>());
            return FindBraces(line, myindex);
        }

        public int ProcessLine(string line)
        {
            return FindBraces(line, 0);
        }

        private int FindBraces(string line, int startindex)
        {
            int i = startindex;
            StringBuilder sb = new StringBuilder();
            bool backslash = false;
            while (i < line.Length)
            {
                if (backslash)
                {
                    backslash = false;
                }
                else if (line[i] == '\\')
                {
                    backslash = true;
                }
                else if (line[i] == '%')
                {
                    // Ein Kommentarzeichen. Der Rest der Zeile wird ignoriert, wenn wir in Latex sind
                    if (m_lastbraceislatex && (m_bracescompleted == m_nbbraces - 1))
                    {
                        sb.Append(line.Substring(i));
                        i = line.Length;
                        break;
                    }
                }
                else if (line[i] == '{')
                {
                    m_bracedepth++;
                }
                else if (line[i] == '}')
                {
                    m_bracedepth--;
                    if (m_bracedepth == 0)
                    {
                        m_pars[m_bracescompleted].Add(sb.ToString());
                        m_bracescompleted++;
                        if (m_bracescompleted == m_nbbraces)
                        {
                            // Alles fertig geparst. Rufe Kommando auf

                            m_cmd(m_pars);

                            // return Index nach Befehl
                            i++;
                            return i;
                        }
                        else
                        {
                            // nächstes Klammerpaar. Hier MUSS jetzt wieder eine Klammer aufgehen!
                            i++;
                            if (line[i] != '{')
                            {
                                throw new ParseErrorException("Expecting '{' directly after '}'", line, i);
                            }
                            m_pars.Add(new List<string>());
                            m_bracedepth = 1;
                            sb = new StringBuilder();
                            // Die { nicht zu sb hinzufügen, deshalb continue
                            i++;
                            continue;
                        }
                    }

                }
                sb.Append(line[i]);
                i++;
            }
            m_pars[m_bracescompleted].Add(sb.ToString());
            return -1; // Command still active
        }

        private string m_commandsearch;
        private int m_nbbraces;
        private int m_bracedepth;
        private int m_bracescompleted;
        private bool m_lastbraceislatex;

        private List<List<string>> m_pars;
        private ExecCommand m_cmd;
    }
}
