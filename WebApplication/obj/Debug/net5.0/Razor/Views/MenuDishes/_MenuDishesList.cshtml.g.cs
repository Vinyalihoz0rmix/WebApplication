#pragma checksum "C:\Users\yuriy\OneDrive\Desktop\Учёба\Работа\WebApplication\WebApplication\Views\MenuDishes\_MenuDishesList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f1d19b08cedf11aac93df5cd5ebe6e9698df0679"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_MenuDishes__MenuDishesList), @"mvc.1.0.view", @"/Views/MenuDishes/_MenuDishesList.cshtml")]
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
#line 1 "C:\Users\yuriy\OneDrive\Desktop\Учёба\Работа\WebApplication\WebApplication\Views\_ViewImports.cshtml"
using WebApplication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\yuriy\OneDrive\Desktop\Учёба\Работа\WebApplication\WebApplication\Views\_ViewImports.cshtml"
using WebApplication.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f1d19b08cedf11aac93df5cd5ebe6e9698df0679", @"/Views/MenuDishes/_MenuDishesList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fa0ef8da47a84ffb33e8bc853509aa4fa5703a26", @"/Views/_ViewImports.cshtml")]
    public class Views_MenuDishes__MenuDishesList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<WebApplication.Models.MenuDishes.MenuDishesViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-sm btn-success"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Add", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Cart", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div class=\"col-4\">\r\n    <div class=\"shadow-lg p-3 bg-white mb-4\">\r\n        <div class=\"card\">\r\n");
#nullable restore
#line 6 "C:\Users\yuriy\OneDrive\Desktop\Учёба\Работа\WebApplication\WebApplication\Views\MenuDishes\_MenuDishesList.cshtml"
             if (Model.Path != null)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"card-img-wrapper\">\r\n                    <img");
            BeginWriteAttribute("title", " title=\"", 284, "\"", 303, 1);
#nullable restore
#line 9 "C:\Users\yuriy\OneDrive\Desktop\Учёба\Работа\WebApplication\WebApplication\Views\MenuDishes\_MenuDishesList.cshtml"
WriteAttributeValue("", 292, Model.Name, 292, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"card-img-top\"");
            BeginWriteAttribute("src", " src=\"", 325, "\"", 342, 1);
#nullable restore
#line 9 "C:\Users\yuriy\OneDrive\Desktop\Учёба\Работа\WebApplication\WebApplication\Views\MenuDishes\_MenuDishesList.cshtml"
WriteAttributeValue("", 331, Model.Path, 331, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 343, "\"", 360, 1);
#nullable restore
#line 9 "C:\Users\yuriy\OneDrive\Desktop\Учёба\Работа\WebApplication\WebApplication\Views\MenuDishes\_MenuDishesList.cshtml"
WriteAttributeValue("", 349, Model.Name, 349, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n                </div>");
#nullable restore
#line 10 "C:\Users\yuriy\OneDrive\Desktop\Учёба\Работа\WebApplication\WebApplication\Views\MenuDishes\_MenuDishesList.cshtml"
                      }

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"container\">\r\n");
#nullable restore
#line 12 "C:\Users\yuriy\OneDrive\Desktop\Учёба\Работа\WebApplication\WebApplication\Views\MenuDishes\_MenuDishesList.cshtml"
                 if (Model.Name.Length >= 16)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <h2>");
#nullable restore
#line 14 "C:\Users\yuriy\OneDrive\Desktop\Учёба\Работа\WebApplication\WebApplication\Views\MenuDishes\_MenuDishesList.cshtml"
                   Write(Model.Name.Remove(13));

#line default
#line hidden
#nullable disable
            WriteLiteral("...</h2> ");
#nullable restore
#line 14 "C:\Users\yuriy\OneDrive\Desktop\Учёба\Работа\WebApplication\WebApplication\Views\MenuDishes\_MenuDishesList.cshtml"
                                                       }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <h2>");
#nullable restore
#line 17 "C:\Users\yuriy\OneDrive\Desktop\Учёба\Работа\WebApplication\WebApplication\Views\MenuDishes\_MenuDishesList.cshtml"
                   Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>");
#nullable restore
#line 17 "C:\Users\yuriy\OneDrive\Desktop\Учёба\Работа\WebApplication\WebApplication\Views\MenuDishes\_MenuDishesList.cshtml"
                                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                <h6> Информация: ");
#nullable restore
#line 18 "C:\Users\yuriy\OneDrive\Desktop\Учёба\Работа\WebApplication\WebApplication\Views\MenuDishes\_MenuDishesList.cshtml"
                            Write(Model.Info);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </h6>\r\n                <h6>Вес: ");
#nullable restore
#line 19 "C:\Users\yuriy\OneDrive\Desktop\Учёба\Работа\WebApplication\WebApplication\Views\MenuDishes\_MenuDishesList.cshtml"
                    Write(Model.Weight);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h6>\r\n                <h6>Цена: <span class=\"text-danger\">");
#nullable restore
#line 20 "C:\Users\yuriy\OneDrive\Desktop\Учёба\Работа\WebApplication\WebApplication\Views\MenuDishes\_MenuDishesList.cshtml"
                                               Write(Model.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span> руб.</h6>\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f1d19b08cedf11aac93df5cd5ebe6e9698df06799550", async() => {
                WriteLiteral("\r\n                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f1d19b08cedf11aac93df5cd5ebe6e9698df06799828", async() => {
                    WriteLiteral("В корзину");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
                {
                    throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-dishId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
                }
                BeginWriteTagHelperAttribute();
#nullable restore
#line 27 "C:\Users\yuriy\OneDrive\Desktop\Учёба\Работа\WebApplication\WebApplication\Views\MenuDishes\_MenuDishesList.cshtml"
                                                                                                   WriteLiteral(Model.DishId);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["dishId"] = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-dishId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["dishId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                    <button onclick=\"if (!confirm(\'Вы уверены, что хотите удалить этот элемент?\')) { return false }\" type=\"submit\" class=\"btn btn-sm btn-danger\">\r\n                        Удалить\r\n                    </button>\r\n                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 21 "C:\Users\yuriy\OneDrive\Desktop\Учёба\Работа\WebApplication\WebApplication\Views\MenuDishes\_MenuDishesList.cshtml"
                                            WriteLiteral(Model.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 21 "C:\Users\yuriy\OneDrive\Desktop\Учёба\Работа\WebApplication\WebApplication\Views\MenuDishes\_MenuDishesList.cshtml"
                                                                         WriteLiteral(Model.MenuId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["menuId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-menuId", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["menuId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 22 "C:\Users\yuriy\OneDrive\Desktop\Учёба\Работа\WebApplication\WebApplication\Views\MenuDishes\_MenuDishesList.cshtml"
                                           WriteLiteral(ViewBag.SearchSelectionString);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["searchSelectionString"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-searchSelectionString", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["searchSelectionString"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 23 "C:\Users\yuriy\OneDrive\Desktop\Учёба\Работа\WebApplication\WebApplication\Views\MenuDishes\_MenuDishesList.cshtml"
                                  WriteLiteral(ViewBag.SearchString);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["searchString"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-searchString", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["searchString"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 24 "C:\Users\yuriy\OneDrive\Desktop\Учёба\Работа\WebApplication\WebApplication\Views\MenuDishes\_MenuDishesList.cshtml"
                                   WriteLiteral(ViewBag.FilterCatalog);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["filterCatalog"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-filterCatalog", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["filterCatalog"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 25 "C:\Users\yuriy\OneDrive\Desktop\Учёба\Работа\WebApplication\WebApplication\Views\MenuDishes\_MenuDishesList.cshtml"
                                  WriteLiteral(ViewBag.PriceSort);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["sortMenuDish"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-sortMenuDish", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["sortMenuDish"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                <br />\r\n                <br />\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WebApplication.Models.MenuDishes.MenuDishesViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
