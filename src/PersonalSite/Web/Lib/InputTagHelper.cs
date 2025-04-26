using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Web
{
    /// <summary>
    /// This class makes up for the lack of HTML5 validation support
    /// (IN THE YEAR OF OUR LORD 2025). The [Required] decorator doesn't
    /// actually add the HTML5 "required" attribute, it adds some
    /// jQuery bullshit. This website doesn't use bloatQuery so I guess
    /// this is God (Billiam Gates) punishing me; but I am stronger.
    /// </summary>
    [HtmlTargetElement("input", Attributes = "asp-for")]
    [HtmlTargetElement("select", Attributes = "asp-for")]
    [HtmlTargetElement("textarea", Attributes = "asp-for")]
    public class RequiredInputTagHelper : InputTagHelper
    {
        public RequiredInputTagHelper(IHtmlGenerator generator) : base(generator) { }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            if (For.Metadata.ModelType != null)
            {
                var property = For.Metadata.ContainerType.GetProperty(For.Metadata.PropertyName);
                if (property != null && property.GetCustomAttribute<RequiredAttribute>() != null)
                {
                    output.Attributes.SetAttribute("required", "true");
                }
            }
        }
    }
}

