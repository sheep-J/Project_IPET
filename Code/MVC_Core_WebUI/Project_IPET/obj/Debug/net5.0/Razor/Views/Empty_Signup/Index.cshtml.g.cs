#pragma checksum "C:\Users\sposnny\Source\Repos\Project_IPET\Code\MVC_Core_WebUI\Project_IPET\Views\Empty_Signup\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fc17096f856e8fca2c4c488ed3b5851758584157"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Empty_Signup_Index), @"mvc.1.0.view", @"/Views/Empty_Signup/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\sposnny\Source\Repos\Project_IPET\Code\MVC_Core_WebUI\Project_IPET\Views\_ViewImports.cshtml"
using Project_IPET;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\sposnny\Source\Repos\Project_IPET\Code\MVC_Core_WebUI\Project_IPET\Views\_ViewImports.cshtml"
using Project_IPET.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fc17096f856e8fca2c4c488ed3b5851758584157", @"/Views/Empty_Signup/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"285c1ed0c8c345cd35abeb36234d82082e6395b6", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Empty_Signup_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("#"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<!-- Breadcrumb Area Start -->
<div class=""section breadcrumb-area bg-name-bright"">
    <div class=""container"">
        <div class=""row"">
            <div class=""col-12 text-center"">
                <div class=""breadcrumb-wrapper"">
                    <h2 class=""breadcrumb-title"">建立帳號</h2>
");
            WriteLiteral(@"                </div>
            </div>
        </div>
    </div>
</div>
<!-- Breadcrumb Area End -->
<!-- Register Section Start -->
<div class=""section section-margin"">
    <div class=""container"">
        <div class=""row"">
            <div class=""col-lg-7 col-md-8 m-auto"">
                <div class=""login-wrapper"">

                    <!-- Register Title & Content Start -->
                    <div class=""section-content text-center m-b-30"">
                        <h2 class=""title m-b-10"">Create Account</h2>
                    </div>
                    <!-- Register Title & Content End -->
                    <!-- Form Action Start -->
                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fc17096f856e8fca2c4c488ed3b58517585841575049", async() => {
                WriteLiteral(@"

                        <!-- Input First Name Start -->
                        <div class=""single-input-item m-b-10"">
                            <input type=""text"" placeholder=""First Name"">
                        </div>
                        <!-- Input First Name End -->
                        <!-- Input Last Name Start -->
                        <div class=""single-input-item m-b-10"">
                            <input type=""text"" placeholder=""Last Name"">
                        </div>
                        <!-- Input Last Name End -->
                        <!-- Input Email Start -->
                        <div class=""single-input-item m-b-10"">
                            <input type=""email"" placeholder=""Email"">
                        </div>
                        <!-- Input Email End -->
                        <!-- Input Password Start -->
                        <div class=""single-input-item m-b-10"">
                            <input type=""password"" placeholder=""Password");
                WriteLiteral(@""">
                        </div>
                        <!-- Input Password End -->
                        <!-- Button/Forget Password Start -->
                        <div class=""single-input-item"">
                            <div class=""login-reg-form-meta m-b-n15"">
                                <button class=""btn btn btn-gray-deep btn-hover-primary m-b-15"">Create</button>
                            </div>
                        </div>
                        <!-- Button/Forget Password End -->

                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    <!-- Form Action End -->\r\n\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n<!-- Register Section End -->");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591