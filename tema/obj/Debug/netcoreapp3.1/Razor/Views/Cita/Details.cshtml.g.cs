#pragma checksum "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Cita\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "60f19d29064eafe63ca03473fa6f67a930bc3271"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Cita_Details), @"mvc.1.0.view", @"/Views/Cita/Details.cshtml")]
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
#line 1 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\_ViewImports.cshtml"
using tema;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\_ViewImports.cshtml"
using tema.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"60f19d29064eafe63ca03473fa6f67a930bc3271", @"/Views/Cita/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"949b9b49bb261766d1b9e03729e5bb6921513a6b", @"/Views/_ViewImports.cshtml")]
    public class Views_Cita_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<tema.Models.Cita>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/Plantilla/dist/css/Tablas.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("Boton"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Cita\Details.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "60f19d29064eafe63ca03473fa6f67a930bc32714761", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

<div class=""content-wrapper"">

    <div class=""container"">
        <ol class=""breadcrumb"" style=""border:solid;color:black;background-color: #01386E"">
            <li><a href=""../Cita/Index"" style=""color:aliceblue"">Inicio </a></li>
            <li><a class=""active"" style=""color:aliceblue"">/ Detalles Cita</a></li>
");
            WriteLiteral("        </ol>\r\n        <h1>Detalles Citas</h1>\r\n\r\n        <div>\r\n\r\n            <hr />\r\n            <dl class=\"row\">\r\n                <dt class=\"col-sm-2\">\r\n                    ");
#nullable restore
#line 26 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Cita\Details.cshtml"
               Write(Html.DisplayNameFor(model => model.IDCita));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 29 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Cita\Details.cshtml"
               Write(Html.DisplayFor(model => model.IDCita));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    ");
#nullable restore
#line 32 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Cita\Details.cshtml"
               Write(Html.DisplayNameFor(model => model.IDTecnico));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 35 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Cita\Details.cshtml"
               Write(Html.DisplayFor(model => model.IDTecnico));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    ");
#nullable restore
#line 38 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Cita\Details.cshtml"
               Write(Html.DisplayNameFor(model => model.cedulaCliente));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 41 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Cita\Details.cshtml"
               Write(Html.DisplayFor(model => model.cedulaCliente));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    ");
#nullable restore
#line 44 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Cita\Details.cshtml"
               Write(Html.DisplayNameFor(model => model.fecha));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 47 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Cita\Details.cshtml"
               Write(Html.DisplayFor(model => model.fecha));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    ");
#nullable restore
#line 50 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Cita\Details.cshtml"
               Write(Html.DisplayNameFor(model => model.hora));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 53 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Cita\Details.cshtml"
               Write(Html.DisplayFor(model => model.hora));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    ");
#nullable restore
#line 56 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Cita\Details.cshtml"
               Write(Html.DisplayNameFor(model => model.asunto));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 59 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Cita\Details.cshtml"
               Write(Html.DisplayFor(model => model.asunto));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    ");
#nullable restore
#line 62 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Cita\Details.cshtml"
               Write(Html.DisplayNameFor(model => model.descripcion));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 65 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Cita\Details.cshtml"
               Write(Html.DisplayFor(model => model.descripcion));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    ");
#nullable restore
#line 68 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Cita\Details.cshtml"
               Write(Html.DisplayNameFor(model => model.citaConfirmada));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 71 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Cita\Details.cshtml"
               Write(Html.DisplayFor(model => model.citaConfirmada));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    ");
#nullable restore
#line 74 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Cita\Details.cshtml"
               Write(Html.DisplayNameFor(model => model.IDCliente));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-10\">\r\n                    ");
#nullable restore
#line 77 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Cita\Details.cshtml"
               Write(Html.DisplayFor(model => model.IDCliente));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n            </dl>\r\n        </div>\r\n        <div>\r\n            ");
#nullable restore
#line 82 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Cita\Details.cshtml"
       Write(Html.ActionLink("Editar", "Edit", new { id = Model.IDCita }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "60f19d29064eafe63ca03473fa6f67a930bc327112935", async() => {
                WriteLiteral("Ver lista Citas");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<tema.Models.Cita> Html { get; private set; }
    }
}
#pragma warning restore 1591
