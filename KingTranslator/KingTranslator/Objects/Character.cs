using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace KingTranslator
{
    [XmlRoot("c"), XmlType("c")]
    public class Character
    {
        [XmlAttribute("s")]
        public string Simplified { get; set; }

        [XmlAttribute("t")]

        public string Traditional { get; set; }

        [XmlAttribute("p")]
        public string Pinyin { get; set; }

        [XmlAttribute("e")]
        public string English { get; set; }

        [XmlAttribute("u")]
        public string URL { get; set; }

        public string Header
        {
            get
            {
                return Simplified + " (" + Pinyin + ")";
            }
        }
    }
}
