using System;

namespace B2B.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PlaceholderAttribute : Attribute
    {
        public string Placeholder { get; set; }

        public PlaceholderAttribute(string placeholder = null)
        {
            Placeholder = placeholder;
        }
    }
}
