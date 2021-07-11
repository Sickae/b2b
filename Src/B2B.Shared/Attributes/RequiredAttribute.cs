using System;

namespace B2B.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredAttribute : AppValidationAttribute
    {
    }
}
