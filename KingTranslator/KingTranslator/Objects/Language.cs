using System;

namespace KingTranslator
{
    public class StringValueAttribute : Attribute
    {
        public string StringValue { get; protected set; }

        public StringValueAttribute(string value)
        {
            this.StringValue = value;
        }
    }

    public enum Language
    {
        [StringValue("zh-CHS")]
        Chinese,
        [StringValue("en")]
        English,
        [StringValue("zh-CHS-PI")]
        Pinyin
    }
}
