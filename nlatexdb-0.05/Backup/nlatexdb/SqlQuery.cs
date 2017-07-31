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
    class SqlQuery
    {
        public SqlQuery(string querytext, string queryvars)
        {
            m_querytext = querytext;
            string[] qvars = queryvars.Split(m_variablesplitter);
            m_queryvars = new List<SqlQueryVar>();
            foreach (string s in qvars)
            {
				Processer.Debug("Var: {0}", s);
                m_queryvars.Add(new SqlQueryVar(s.Trim()));
            }
        }
				
		public static void SetVariableSplitter(string var_splitter)
		{
			m_variablesplitter = var_splitter.ToCharArray();
		}


        private void ReplaceVariables(System.Data.Common.DbCommand comm,
            System.Collections.Generic.Dictionary<string, SqlQueryVar> varvalues)
        {
            string querytext = m_querytext;
            System.Collections.Generic.SortedDictionary<int, KeyValuePair<string, object>> repls =
               new SortedDictionary<int, KeyValuePair<string, object>>();
            foreach (KeyValuePair<string, SqlQueryVar> var in varvalues)
            {
                int startindex = 0;
                while (startindex >= 0 && startindex < querytext.Length)
                {
                    int varindex = querytext.IndexOf(var.Key, startindex);
                    if (varindex >= 0)
                    {
                        repls[varindex] = new KeyValuePair<string, object>(var.Key, var.Value.getValueSql());
                        startindex = varindex + 1;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            int j = 1;
            // Alle Variablenvorkommen sind nun sortiert im repl-Verzeichnis.
            foreach (KeyValuePair<string, object> replacement in repls.Values)
            {
                string parname = String.Format("@{0}", j);
				Processer.Debug("SQL Parameter {0} Value {1}", parname, replacement.Value);
                querytext = querytext.Replace(replacement.Key, parname);
                System.Data.Common.DbParameter par = comm.CreateParameter();
                par.Value = replacement.Value;
                par.ParameterName = parname;
                comm.Parameters.Add(par);
                j++;
            }
			Processer.Debug("SQL Query: {0}", querytext);
            comm.CommandText = querytext;
        }
		
		private List<object[]> ExecuteSql(System.Data.Common.DbConnection connection,
		                                  System.Collections.Generic.Dictionary<string, SqlQueryVar> varvalues)
		{
            List<object[]> alldata = new List<object[]>();
            using (System.Data.Common.DbCommand comm = connection.CreateCommand())
            {
                ReplaceVariables(comm, varvalues);

                using (System.Data.Common.DbDataReader rdr = comm.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        object[] myline = new object[rdr.FieldCount];
                        rdr.GetValues(myline);
                        alldata.Add(myline);
						if (Processer.IsDebugEnabled)
						{
							StringBuilder sb = new StringBuilder();
							foreach (object o in myline)
							{
								sb.Append("\t");
								sb.Append(o.ToString());
							}
							Processer.Debug("Got record:{0}", sb.ToString());
						}
                    }
                    rdr.Close();
                }
            }
			return alldata;
		}

        public void Execute(List<string> texstuff, System.Data.Common.DbConnection connection,
		                    System.Collections.Generic.Dictionary<string, SqlQueryVar> varvalues, 
		                    Processer proc,
		                    string filenamepattern)
        {
			List<object[]> alldata = ExecuteSql(connection, varvalues);
            foreach (object[] objline in alldata)
            {
				if (objline.Length < m_queryvars.Count)
				{
					throw new Exception(String.Format("There are {0} variables defined, but the query has only {0} rows!",
					                                  m_queryvars.Count, objline.Length));
				}

                for (int i = 0; i < m_queryvars.Count; i++)
                {
                    m_queryvars[i].setValue(objline[i]);
                    if (!varvalues.ContainsKey(m_queryvars[i].Name))
                    {
                        varvalues.Add(m_queryvars[i].Name, m_queryvars[i]);
                    }
                }
				
				string outpath = null;
				if (!String.IsNullOrEmpty(filenamepattern))
				{
					outpath = proc.VariablenEinfuegen(filenamepattern);
					proc.OpenOutstream(outpath);
				}

                foreach (string line in texstuff)
                {
                    proc.HandleLine(line);
                }

				if (!String.IsNullOrEmpty(outpath))
				{
					proc.CloseOutstream();
					proc.LatexPostprocess(outpath, false);
				}
            }
        }
        

        public void ExecuteIf(List<string> texstuff, System.Data.Common.DbConnection connection,
            System.Collections.Generic.Dictionary<string, SqlQueryVar> varvalues, Processer proc)
        {
            using (System.Data.Common.DbCommand comm = connection.CreateCommand())
            {
                ReplaceVariables(comm, varvalues);

                using (System.Data.Common.DbDataReader rdr = comm.ExecuteReader())
                {
                    if (!rdr.HasRows)
                    {
						Processer.Debug("Yes, query has rows. Process texdbif.");
                        rdr.Close();
                        return;
                    }
                    rdr.Close();
                }
            } 
            foreach (string line in texstuff)
            {
                proc.HandleLine(line);
            }
        }

        private string m_querytext;
        private List<SqlQueryVar> m_queryvars;
		private static char[] m_variablesplitter = { ',' };
    }
}
