using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace FolhaPonto.Extensions
{
    public static class Extencoes
    {
        public static int NumPage
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["numPage"]);
            }
        }
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
        public static List<int> NaoContem(this List<int> lst, List<int> other)
        {
            return lst.Where(item => !other.Contains(item)).ToList();
        }
        public static IList<T> DatatableToClass<T>(this DataTable tabela) where T : class, new()
        {
            var tipoclass = typeof(T);
            IList<PropertyInfo> listaPropriedades = tipoclass.GetProperties();

            if (listaPropriedades.Count == 0)
                return new List<T>();

            var nomeColuna = tabela.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToList();

            var result = new List<T>();
            try
            {
                foreach (DataRow row in tabela.Rows)
                {
                    var objetoClass = new T();
                    foreach (var propriedade in listaPropriedades)
                    {
                        if (propriedade == null || !propriedade.CanWrite) continue;
                        if (!nomeColuna.Contains(propriedade.Name)) continue;
                        if (row[propriedade.Name] == DBNull.Value) continue;
                        var propriedadeValue = Convert.ChangeType(
                            row[propriedade.Name],
                            propriedade.PropertyType
                            );
                        propriedade.SetValue(objetoClass, propriedadeValue, null);
                    }
                    result.Add(objetoClass);
                }
                return result;
            }
            catch
            {
                return new List<T>();
            }
        }
        public static IHtmlString ActionIconLink(this HtmlHelper helper, string actionName, string controllerName, object routeValues, object iconHtmlAtribute, IDictionary<string, object> linkHtmlAtribute)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action(actionName, controllerName, routeValues);

            var linkTagBuilder = new TagBuilder("a");
            linkTagBuilder.MergeAttribute("href", url);
            linkTagBuilder.MergeAttributes(new RouteValueDictionary(linkHtmlAtribute));

            var iconTabBuilder = new TagBuilder("i");
            iconTabBuilder.MergeAttributes(new RouteValueDictionary(iconHtmlAtribute));

            linkTagBuilder.InnerHtml = iconTabBuilder.ToString(TagRenderMode.Normal);
            return new MvcHtmlString(linkTagBuilder.ToString(TagRenderMode.Normal));
        }
        public static IHtmlString ImageButton(this HtmlHelper helper, string actionName, string controllerName, object routeValues, string urlImagem, object linkHtmlAtribute, object imgHtmlAtribute)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action(actionName, controllerName, routeValues);

            var linkTagBuilder = new TagBuilder("a");
            linkTagBuilder.MergeAttribute("href", url);
            linkTagBuilder.MergeAttributes(new RouteValueDictionary(linkHtmlAtribute));

            var iconTabBuilder = new TagBuilder("img");
            iconTabBuilder.MergeAttribute("src", urlImagem);
            iconTabBuilder.MergeAttribute("border", "0");
            iconTabBuilder.MergeAttributes(new RouteValueDictionary(imgHtmlAtribute));

            linkTagBuilder.InnerHtml = iconTabBuilder.ToString(TagRenderMode.Normal);
            return new MvcHtmlString(linkTagBuilder.ToString(TagRenderMode.Normal));
        }
        public static IHtmlString ImageButton(this HtmlHelper helper, string actionName, string controllerName, object routeValues, string urlImagem, object linkHtmlAtribute)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action(actionName, controllerName, routeValues);

            var linkTagBuilder = new TagBuilder("a");
            linkTagBuilder.MergeAttribute("href", url);
            linkTagBuilder.MergeAttributes(new RouteValueDictionary(linkHtmlAtribute));

            var iconTabBuilder = new TagBuilder("img");
            iconTabBuilder.MergeAttribute("src", urlImagem);
            iconTabBuilder.MergeAttribute("border", "0");

            linkTagBuilder.InnerHtml = iconTabBuilder.ToString(TagRenderMode.Normal);
            return new MvcHtmlString(linkTagBuilder.ToString(TagRenderMode.Normal));
        }
        public static IHtmlString ButtonSubmit(this HtmlHelper helper, string name, string value, object inputAtributo)
        {
            var inputTag = new TagBuilder("input");
            inputTag.MergeAttribute("type", "submit");
            inputTag.MergeAttribute("name", name);
            inputTag.MergeAttribute("id", name);
            inputTag.MergeAttribute("value", value);
            inputTag.MergeAttributes(new RouteValueDictionary(inputAtributo));
            return new MvcHtmlString(inputTag.ToString(TagRenderMode.SelfClosing));
        }
        public static IHtmlString Button(this HtmlHelper helper, string name, string value, object inputAtributo)
        {
            var inputTag = new TagBuilder("input");
            inputTag.MergeAttribute("type", "button");
            inputTag.MergeAttribute("name", name);
            inputTag.MergeAttribute("id", name);
            inputTag.MergeAttribute("value", value);
            inputTag.MergeAttributes(new RouteValueDictionary(inputAtributo));
            return new MvcHtmlString(inputTag.ToString(TagRenderMode.SelfClosing));
        }
        public static MvcHtmlString CheckBoxSimple(this HtmlHelper htmlHelper, string name, object htmlAttributes)
        {
            var checkBoxWithHidden = htmlHelper.CheckBox(name, htmlAttributes).ToHtmlString().Trim();
            var pureCheckBox = checkBoxWithHidden.Substring(0, checkBoxWithHidden.IndexOf("<input", 1));
            return new MvcHtmlString(pureCheckBox);
        }
        public static void EnviarMensagem(this ControllerBase value, string msg, string op = "1")
        {
            value.TempData["msg"] = msg;
            value.TempData["op"] = op;
        }
        public static bool IsCpf(this string cpf)
        {
            var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            if (cpf == "00000000000" || cpf == "11111111111" || cpf == "22222222222" || cpf == "33333333333" || cpf == "44444444444" ||
                cpf == "55555555555" || cpf == "66666666666" || cpf == "77777777777" || cpf == "88888888888" || cpf == "99999999999")
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;
            for (var i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;
            for (var i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        #region Left Right Mid
        public static string Left(this string param, int length)
        {
            if (param.Length <= length) return param;
            var result = param.Substring(0, length) + "...";
            return result;
        }

        public static string Right(this string param, int length)
        {
            var result = param.Substring(param.Length - length, length);
            return result;
        }

        public static string Mid(this string param, int startIndex, int length)
        {
            var result = param.Substring(startIndex, length);
            return result;
        }

        public static string Mid(this string param, int startIndex)
        {
            var result = param.Substring(startIndex);
            return result;
        }
        #endregion
    }
}