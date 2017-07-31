
using System;
using System.Configuration;

namespace nlatexdb
{


	public class NlatexdbSettings: ConfigurationSection
	{
       [ConfigurationProperty( "LatexCharReplace" )]
        public LatexCharReplaceCollection LatexCharReplaceItems
        {
            get { return ( (LatexCharReplaceCollection)( base[ "LatexCharReplace" ] ) ); }
        }

		   
[ConfigurationProperty("CmdLineArgumentVarPrefix", IsRequired=false)]
   public string CmdLineArgumentVarPrefix
   {
      get
      {
         return (string) this["CmdLineArgumentVarPrefix"];
      }
   }

[ConfigurationProperty("RegexSplitter", IsRequired=false)]
   public string RegexSplitter
   {
      get
      {
         return (string) this["RegexSplitter"];
      }
   }
	
[ConfigurationProperty("VariableSplitter", IsRequired=false)]
   public string VariableSplitter
   {
      get
      {
         return (string) this["VariableSplitter"];
      }
   }
	
	
	}
}
