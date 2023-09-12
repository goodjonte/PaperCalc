using Microsoft.AspNetCore.Mvc.Rendering;

namespace PaperCalc.Pages.Shared
{
    public static class HtmlHelperExtensions
    {
        public static string ActiveClass(this IHtmlHelper htmlHelper, string route)
        {
            var routeData = htmlHelper.ViewContext.RouteData;

            var pageRoute = routeData.Values["page"].ToString();

            return route == pageRoute ? "active" : "";
        }

        public static string NoScroll(this IHtmlHelper htmlHelper)
        {
            var routeData = htmlHelper.ViewContext.RouteData;

            var pageRoute = routeData.Values["page"].ToString();

            switch (pageRoute)
            {
                case "/Stock/AspeosStock/Index":
                    return "style=overflow-y:hidden;";
                case "/Stock/EpsonStock/Index":
                    return "style=overflow-y:hidden;";
                case "/Stock/FlatStock/Index":
                    return "style=overflow-y:hidden;";
                case "/Stock/LaminationStock/Index":
                    return "style=overflow-y:hidden;";
                default:
                    return "";
            }
        }
    }
}
