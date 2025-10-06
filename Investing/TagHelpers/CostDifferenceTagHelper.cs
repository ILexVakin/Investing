using Investing.Models;
using Investing.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Investing.TagHelpers
{
    [HtmlTargetElement("cost-difference")]
    public class CostDifferenceTagHelper : TagHelper
    {

        [HtmlAttributeName("item")]
        public Stocks? Stock { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext? ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "td"; // Делаем ячейку таблицы

            if (Stock?.Marketdata == null)
            {
                output.Content.SetContent("-");
                return;
            }

            var price = Stock.Marketdata.MARKETPRICE;
            var changePercent = Stock.Marketdata.LASTCHANGEPRCNT ?? 0;
            var changeValue = Stock.Marketdata.LASTTOPREVPRICE ?? 0;


            var container = new TagBuilder("div");
            container.AddCssClass("price-change-container");

            var priceSpan = new TagBuilder("span");
            priceSpan.AddCssClass("current-price"); 
            priceSpan.InnerHtml.Append(price?.ToString("0.00") ?? "0.00");
            container.InnerHtml.AppendHtml(priceSpan);

            if (changePercent != 0 || changeValue != 0)
            {
                var changeSpan = new TagBuilder("span");
                changeSpan.AddCssClass(changePercent > 0 ? "positive-change" : "negative-change");

                var arrow = changePercent > 0 ? "▲" : "▼";
                changeSpan.InnerHtml.AppendHtml($" {arrow} {Math.Abs((decimal)changePercent):N2}%");

                if (changeValue != 0)
                {
                    changeSpan.InnerHtml.AppendHtml($" ({arrow} {Math.Abs((decimal)changeValue):N2})");
                }

                container.InnerHtml.AppendHtml(changeSpan);
            }

            output.Content.AppendHtml(container);
        }    
    }
}
