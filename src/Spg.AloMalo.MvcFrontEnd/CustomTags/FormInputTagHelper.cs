using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Spg.AloMalo.MvcFrontEnd.CustomTags
{
    [HtmlTargetElement("form-input", Attributes = "for, label-name, input-type, val-text-class")]
    public class FormInputTagHelper : TagHelper
    {
        [HtmlAttributeName("for")]
        public string For { get; set; } = string.Empty;
        [HtmlAttributeName("label-name")] 
        public string LabelName { get; set; } = string.Empty;
        [HtmlAttributeName("input-type")] 
        public string InputType { get; set; } = string.Empty;
        [HtmlAttributeName("val-text-class")]
        public string ValidationTextClass { get; set; } = string.Empty;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = @"<div class=""form-group""";
            
            output.PreContent.SetHtmlContent("");
            output.Content.AppendHtml("");
            output.PostContent.SetHtmlContent("");

            output.Attributes.Clear();
        }
    }
}
