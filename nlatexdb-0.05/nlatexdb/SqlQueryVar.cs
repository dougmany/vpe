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
using System.Text;

namespace nlatexdb
{
    class SqlQueryVar
    {
        public SqlQueryVar(string key, object value)
        {
            m_varname = key;
            m_regexreplace = new List<string>();
            setValue(value);
        }
            
        public SqlQueryVar(string vartext)
        {
            string[] varsplit = vartext.Split(m_regexsplitter);
            m_varname = varsplit[0];
            m_regexreplace = new List<string>(varsplit);
            m_regexreplace.RemoveAt(0);
        }

        public void setValue(object value)
        {
            m_value = value;
            string valuestring = objectToLatex(m_value);
            for (int i = 1; i < m_regexreplace.Count; i += 2)
            {
                string search = m_regexreplace[i - 1];
                string replace = m_regexreplace[i];
				Processer.Debug("Search: {0} Replace: {1}", search, replace);
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(search);
                valuestring = regex.Replace(valuestring, replace);
                // so im groben
            }
            m_valuelatex = valuestring;
			Processer.Debug("Set Variable {0} Latex Value: {1}", m_varname, valuestring);
        }

        private static string objectToLatex(object o)
        {
            string s = o.ToString();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
				if (m_latexreplace.ContainsKey(s[i]))
				{
					sb.Append(m_latexreplace[s[i]]);
				}
				else
				{
					sb.Append(s[i]);
				}
            }
            return sb.ToString();
        }

        public string Name
        {
            get
            {
                return m_varname;
            }
        }

        public string getValueLatex()
        {
            return m_valuelatex;
        }

        public object getValueSql()
        {
            return m_value;
        }
		
		public static void ClearLatexReplace()
		{
			if (m_latexreplace == null)
			{
				m_latexreplace = new Dictionary<char, string>();
			}
			m_latexreplace.Clear();
		}
		
		public static void AddLatexReplace(char latexchar, string replace)
		{
			m_latexreplace[latexchar] = replace;
		}
		
		public static void SetRegexSplitter(string regex_splitter)
		{
			m_regexsplitter = regex_splitter.ToCharArray();
		}
		

        private string m_varname;
        private object m_value;
        private string m_valuelatex;
        private List<string> m_regexreplace;
		private static char[] m_regexsplitter = { '/' };
		
		private static Dictionary<char, string> m_latexreplace;
    }
}
