using System.Linq;
using System.Reflection;
using B2B.Shared.Attributes;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace B2B.Web.TagHelpers
{
    [HtmlTargetElement("input", Attributes = AttributeName, TagStructure = TagStructure.WithoutEndTag)]
    public class InputTagHelper : TagHelper
    {
        private const string AttributeName = "asp-placeholder";

        [HtmlAttributeName(AttributeName)]
        public ModelExpression For { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var placeholder = For.Name;
            var placeholderAttr = For.Metadata?.ContainerType?.GetProperty(For.Name)?
                .GetCustomAttribute<PlaceholderAttribute>();

            if (placeholderAttr != null && output.Attributes.All(x => x.Name != "placeholder"))
                placeholder = placeholderAttr.Placeholder ?? For.Name;

            output.Attributes.Add("placeholder", placeholder);
        }
    }
}
