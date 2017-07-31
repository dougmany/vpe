
using System;
using System.Configuration;

namespace nlatexdb
{
	    
	[ConfigurationCollection( typeof( LatexCharReplaceElement ) )]
    public class LatexCharReplaceCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new LatexCharReplaceElement();
        }
 
        protected override object GetElementKey( ConfigurationElement element )
        {
            return ( (LatexCharReplaceElement)( element ) ).Char;
        }
 
        public LatexCharReplaceElement this[int idx ]
        {
            get
            {
                return (LatexCharReplaceElement) BaseGet(idx);
            }
        }
    }
	
    public class LatexCharReplaceElement : ConfigurationElement
    {
        [ConfigurationProperty("char", DefaultValue="", IsKey=true, IsRequired=true)]
        public string Char
        {
            get
            {
                return ((string) (base["char"]));
            }
            set
            {
                base["char"] = value;
            }
        }
 
        [ConfigurationProperty( "replace", DefaultValue = "", IsKey = false, IsRequired = true )]
        public string Replace
        {
            get
            {
                return ( (string)( base[ "replace" ] ) );
            }
            set
            {
                base[ "replace" ] = value;
            }
        }
 
    }


}
