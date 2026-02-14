using System.Collections.Generic;
using System.Text;

public static class ISBNElementExtensions
{
    /// <summary>
    /// Legacy shared list kept for backward compatibility.
    /// </summary>
    public static List<ISBNElement> elements = new();

    public static void PrintElements(this IEnumerable<ISBNElement> items)
    {
        foreach (var element in items)
        {
            Console.WriteLine(element);
        }
    }

    public static string MakeHtmlTable(this IEnumerable<ISBNElement> items)
    {
        var sb = new StringBuilder();
        sb.Append("<!DOCTYPE html><html><head><meta charset=\"utf-8\"/>");
        sb.Append("<style>");
        sb.Append("body{font-family:Segoe UI,Tahoma,sans-serif;background:#f8fafc;color:#0f172a;padding:24px;} ");
        sb.Append("table{border-collapse:collapse;width:100%;background:#fff;box-shadow:0 8px 24px rgba(15,23,42,.08);} ");
        sb.Append("th,td{border:1px solid #e2e8f0;padding:10px;text-align:left;} th{background:#0f172a;color:#fff;}");
        sb.Append("</style></head><body>");
        sb.Append("<table><tr><th>ISBN-13</th><th>Book Title</th><th>Author</th></tr>");

        foreach (var element in items)
        {
            sb.Append(element.ToTableRow());
        }

        sb.Append("</table></body></html>");
        return sb.ToString();
    }

    public static string CompileArrayInString(this IEnumerable<string>? values)
    {
        return values is null ? string.Empty : string.Join(", ", values.Where(v => !string.IsNullOrWhiteSpace(v)));
    }

    // Legacy method names preserved to avoid breaking existing consumers.
    public static string makeHTMLTable(this List<ISBNElement> lst) => lst.MakeHtmlTable();
    public static string compileArrayInString(this string[] str) => str.CompileArrayInString();
}
