﻿using System;

namespace B2B.DataAccess.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class TextAttribute : Attribute
    {
        public int MaxLength { get; set; }

        public TextAttribute()
        {
            MaxLength = int.MaxValue;
        }

        public TextAttribute(int maxLength)
        {
            MaxLength = maxLength;
        }
    }
}
