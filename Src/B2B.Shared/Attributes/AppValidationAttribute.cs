using System;

namespace B2B.Shared.Attributes
{
    public abstract class AppValidationAttribute : Attribute
    {
        public string ErrorMessage { get; set; }
    }
}
