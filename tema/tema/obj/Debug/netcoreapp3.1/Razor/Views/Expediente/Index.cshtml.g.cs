#pragma checksum "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Expediente\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b959c518c06b2780e74ff58b56228b455fbd48b1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Expediente_Index), @"mvc.1.0.view", @"/Views/Expediente/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b959c518c06b2780e74ff58b56228b455fbd48b1", @"/Views/Expediente/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"949b9b49bb261766d1b9e03729e5bb6921513a6b", @"/Views/_ViewImports.cshtml")]
    public class Views_Expediente_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<tema.Models.Expediente>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/Plantilla/dist/css/Tablas.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-inline ml-3"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("Boton"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Expediente\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "b959c518c06b2780e74ff58b56228b455fbd48b15488", async() => {
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
            <li><a href=""../Expediente/Index"" style=""color:aliceblue"">Inicio</a></li>

        </ol>
        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b959c518c06b2780e74ff58b56228b455fbd48b16879", async() => {
                WriteLiteral("\r\n");
                WriteLiteral(@"            <div class=""input-group input-group-sm"">
                <input class=""form-control form-control-navbar"" type=""search"" placeholder=""Buscar Expediente "" aria-label=""Search"">
                <div class=""input-group-append"">
                    <button class=""btn btn-navbar"" type=""submit"">
                        <i class=""fas fa-search""></i>
                    </button>
                </div>
            </div>
        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n\r\n\r\n        <table class=\"Tablas\">\r\n            <thead>\r\n                <tr>\r\n\r\n                    <th>\r\n                        ");
#nullable restore
#line 34 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Expediente\Index.cshtml"
                   Write(Html.DisplayNameFor(model => model.nombreTecnico));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 37 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Expediente\Index.cshtml"
                   Write(Html.DisplayNameFor(model => model.asunto));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 40 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Expediente\Index.cshtml"
                   Write(Html.DisplayNameFor(model => model.descripcion));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 43 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Expediente\Index.cshtml"
                   Write(Html.DisplayNameFor(model => model.fechaCreacionExp));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 46 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Expediente\Index.cshtml"
                   Write(Html.DisplayNameFor(model => model.nombreCliente));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 49 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Expediente\Index.cshtml"
                   Write(Html.DisplayNameFor(model => model.marca));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 52 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Expediente\Index.cshtml"
                   Write(Html.DisplayNameFor(model => model.modelo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 55 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Expediente\Index.cshtml"
                   Write(Html.DisplayNameFor(model => model.placa));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th></th>\r\n                </tr>\r\n            </thead>\r\n            <tbody>\r\n");
#nullable restore
#line 61 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Expediente\Index.cshtml"
                 foreach (var item in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n\r\n                        <td>\r\n                            ");
#nullable restore
#line 66 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Expediente\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.nombreTecnico));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 69 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Expediente\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.asunto));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 72 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Expediente\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.descripcion));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 75 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Expediente\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.fechaCreacionExp));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 78 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Expediente\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.nombreCliente));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 81 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Expediente\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.marca));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 84 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Expediente\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.modelo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 87 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Expediente\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.placa));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 90 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Expediente\Index.cshtml"
                       Write(Html.ActionLink("Edit", "Edit", new { /* id=item.PrimaryKey */ }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\r\n                            ");
#nullable restore
#line 91 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Expediente\Index.cshtml"
                       Write(Html.ActionLink("Details", "Details", new { /* id=item.PrimaryKey */ }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\r\n                            ");
#nullable restore
#line 92 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Expediente\Index.cshtml"
                       Write(Html.ActionLink("Delete", "Delete", new { /* id=item.PrimaryKey */ }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 95 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Expediente\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\r\n        </table>\r\n\r\n        <p>\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b959c518c06b2780e74ff58b56228b455fbd48b116221", async() => {
                WriteLiteral("Registrar nuevo Expediente");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </p>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<tema.Models.Expediente>> Html { get; private set; }
    }
}
#pragma warning restore 1591