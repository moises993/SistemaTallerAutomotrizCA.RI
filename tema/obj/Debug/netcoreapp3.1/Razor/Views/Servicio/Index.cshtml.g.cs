#pragma checksum "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Servicio\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8f5b45551eb04c89137c34e27245568644f6c2b8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Servicio_Index), @"mvc.1.0.view", @"/Views/Servicio/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8f5b45551eb04c89137c34e27245568644f6c2b8", @"/Views/Servicio/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"949b9b49bb261766d1b9e03729e5bb6921513a6b", @"/Views/_ViewImports.cshtml")]
    public class Views_Servicio_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<tema.Models.Servicio>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/Plantilla/dist/css/Tablas.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("Boton"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("font-family: Arial;color: white"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 3 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Servicio\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "8f5b45551eb04c89137c34e27245568644f6c2b85207", async() => {
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
            WriteLiteral("\r\n\r\n<div class=\"content-wrapper\">\r\n\r\n    <div class=\"container\">\r\n        <h1>Servicio</h1>\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
            <script>
                $(document).ready(function () {
                    $('#tablaDatos').DataTable({
                        ""language"":
                        {
                            ""sProcessing"": ""Procesando..."",
                            ""sLengthMenu"": ""Mostrar _MENU_ registros"",
                            ""sZeroRecords"": ""No se encontraron resultados"",
                            ""sEmptyTable"": ""Ningún dato disponible en esta tabla"",
                            ""sInfo"": ""Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros"",
                            ""sInfoEmpty"": ""Mostrando registros del 0 al 0 de un total de 0 registros"",
                            ""sInfoFiltered"": ""(filtrado de un total de _MAX_ registros)"",
                            ""sInfoPostFix"": """",
                            ""sSearch"": ""Buscar:"",
                            ""sUrl"": """",
                            ""sInfoThousands"": "","",
                            ""sLoadingRec");
                WriteLiteral(@"ords"": ""Cargando..."",
                            ""oPaginate"": {
                                ""sFirst"": ""Primero"",
                                ""sLast"": ""Último"",
                                ""sNext"": ""Siguiente"",
                                ""sPrevious"": ""Anterior""
                            },
                            ""oAria"": {
                                ""sSortAscending"": "": Activar para ordenar la columna de manera ascendente"",
                                ""sSortDescending"": "": Activar para ordenar la columna de manera descendente""
                            },
                            ""buttons"": {
                                ""copy"": ""Copiar"",
                                ""colvis"": ""Visibilidad""
                            }
                        },
                        ""pageLength"": 5
                    });
                });
            </script>
        ");
            }
            );
            WriteLiteral("        <table id=\"tablaDatos\" class=\"Tablas\" style=\"width:100%\">\r\n            <thead>\r\n                <tr>\r\n                    <th style=\"color:white;font-family:Arial\">\r\n                        ");
#nullable restore
#line 56 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Servicio\Index.cshtml"
                   Write(Html.DisplayNameFor(model => model.IDServicio));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th style=\"color:white;font-family:Arial\">\r\n                        ");
#nullable restore
#line 59 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Servicio\Index.cshtml"
                   Write(Html.DisplayNameFor(model => model.IDVehiculo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th style=\"color:white;font-family:Arial\">\r\n                        ");
#nullable restore
#line 62 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Servicio\Index.cshtml"
                   Write(Html.DisplayNameFor(model => model.descripcion));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th></th>\r\n                </tr>\r\n            </thead>\r\n            <tbody>\r\n");
#nullable restore
#line 68 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Servicio\Index.cshtml"
                 foreach (var item in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td>\r\n                            ");
#nullable restore
#line 72 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Servicio\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.IDServicio));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 75 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Servicio\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.IDVehiculo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 78 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Servicio\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.descripcion));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td style=\"text-align:center;font-family:Arial\">\r\n                            ");
#nullable restore
#line 81 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Servicio\Index.cshtml"
                       Write(Html.ActionLink("Edit", "Edit", new { /* id=item.PrimaryKey */ }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\r\n                            ");
#nullable restore
#line 82 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Servicio\Index.cshtml"
                       Write(Html.ActionLink("Details", "Details", new { /* id=item.PrimaryKey */ }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\r\n                            ");
#nullable restore
#line 83 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Servicio\Index.cshtml"
                       Write(Html.ActionLink("Delete", "Delete", new { /* id=item.PrimaryKey */ }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 86 "C:\Users\Erick\Pictures\MVC ANÁLISIS\tema\tema\Views\Servicio\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\r\n        </table>\r\n        <p>\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8f5b45551eb04c89137c34e27245568644f6c2b812798", async() => {
                WriteLiteral("Registrar Servicio");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<tema.Models.Servicio>> Html { get; private set; }
    }
}
#pragma warning restore 1591
