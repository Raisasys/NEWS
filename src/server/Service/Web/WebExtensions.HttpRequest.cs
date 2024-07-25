using Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Service.Web;

partial class WebExtensions
{
    public static bool Has(this HttpRequest @this, string key)
    {
        return
            (@this.HasFormContentType && @this.Form.ContainsKey(key)) ||
            @this.Query.ContainsKey(key) ||
            @this.GetRouteValues()?[key] != null;
    }

    public static RouteValueDictionary GetRouteValues(this HttpRequest @this)
    {
        return Context.Current.ActionContext()?.RouteData?.Values;
    }

    public static string Get(this HttpRequest @this, string key) => Param(@this, key);

    public static T Get<T>(this HttpRequest @this, string key) => (T)Param(@this, key).To(typeof(T));

    public static string Param(this HttpRequest @this, string key)
    {
        if (@this.HasFormContentType && @this.Form.ContainsKey(key))
            return @this.Form[key].ToStringOrEmpty();

        if (@this.Query.ContainsKey(key))
            return @this.Query[key].ToStringOrEmpty();

        return (@this.GetRouteValues()?[key]).ToStringOrEmpty();
    }

}