using System;

namespace B2B.DataAccess.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TextAttribute : Attribute
    {
        public TextAttribute()
        {
            MaxLength = int.MaxValue;
        }

        public TextAttribute(int maxLength)
        {
            MaxLength = maxLength;
        }

        public int MaxLength { get; set; }
    }
}