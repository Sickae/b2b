using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace B2B.Web.TagHelpers
{
    [HtmlTargetElement("site-config", Attributes = "key,value", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class SiteConfigTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            context.AllAttributes.TryGetAttribute("key", out var key);
            context.AllAttributes.TryGetAttribute("value", out var value);

            output.Attributes.Clear();

            output.TagName = "input";
            output.TagMode = TagMode.SelfClosing;
            output.Attributes.Add("value", $"cfg-{key.Value}_{value.Value}");
            output.Attributes.Add("hidden", "hidden");
        }
    }
}
