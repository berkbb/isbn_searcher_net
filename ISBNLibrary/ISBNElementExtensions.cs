using System;
using System.Collections.Generic;
using System.Text;

public static class ISBNElementExtensions
{
    /// <summary>
    /// Elements list.
    /// </summary>
    public static List<ISBNElement> elements = new();


    /// <summary>
    /// Print raw elements.
    /// </summary>
    /// <param name="lst">ISBNElement list.</param>
    public static void PrintElements(this List<ISBNElement> lst)
    {
        foreach (var element in lst)
        {
            Console.WriteLine(element);
        }
    }


    /// <summary>
    /// Make table HTML from list.
    /// </summary>
    /// <param name="lst">ISBNElement list.</param>
    /// <returns>string (HTML page with table)</returns>
    public static string makeHTMLTable(this List<ISBNElement> lst)
    {
        var sb = new StringBuilder();
        sb.Append("<!DOCTYPE html>");
        sb.Append("<html>");
        sb.Append("<head>");
        sb.Append("<style>");
        sb.Append("body {background-color: powderblue;}");
        sb.Append("table { background-color: white; width: 100%; table-layout: fixed; overflow-wrap: break-word;}");
        sb.Append("table, th, td {border: 1px solid;}");
        sb.Append("</style>");
        sb.Append("</head>");
        sb.Append("<body>");
        sb.Append("<table>");
        sb.Append(@$"<tr style=""background-color:grey;""><th>ISBN</th><th>Kitap Adı</th><th>Yazar</th></tr>");
        foreach (var element in lst)
        {
            sb.Append(element.ToTableRow());
        }
        sb.Append("</table>");
        sb.Append("</body>");
        sb.Append("</html>");
        return sb.ToString();
    }
    /// <summary>
    /// Combines elements in array witth comma .
    /// </summary>
    /// <param name="str">string array</param>
    /// <returns></returns>
    public static string compileArrayInString(this string[] str)
    {
        StringBuilder sb = new();
        for (int i = 0; i < str.Length; i++)
        {
            sb.Append(str[i]);
            if (i != str.Length - 1)
            {
                sb.Append(", ");
            }
        }
        return sb.ToString();
    }
}