using System;

namespace B2B.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class StringLengthAttribute : AppValidationAttribute
    {
        public int MinimumLength { get; set; }
        public int MaximumLength { get; set; }

        public StringLengthAttribute(int maxLength)
        {
            MaximumLength = maxLength;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ExtraShortStringLengthAttribute : StringLengthAttribute
    {
        public ExtraShortStringLengthAttribute()
            : base(32)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ShortStringLengthAttribute : StringLengthAttribute
    {
        public ShortStringLengthAttribute() : base(64)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class CommonStringLengthAttribute : StringLengthAttribute
    {
        public CommonStringLengthAttribute() : base(100)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class MediumStringLengthAttribute : StringLengthAttribute
    {
        public MediumStringLengthAttribute()
            : base(256)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class LongStringLengthAttribute : StringLengthAttribute
    {
        public LongStringLengthAttribute()
            : base(512)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ExtraLongStringLengthAttribute : StringLengthAttribute
    {
        public ExtraLongStringLengthAttribute()
            : base(1024)
        {
        }
    }
}
