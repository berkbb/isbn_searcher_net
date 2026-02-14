using HtmlAgilityPack;

public static class ISBNSearchResponse
{
    private static readonly HttpClient HttpClient = new();

    public static async Task<string> GetHtmlFromIsbnSearch(string isbn)
    {
        var url = $"https://isbnsearch.org/isbn/{isbn}";
        using var response = await HttpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public static Task<ISBNElement> ParseBookInfo(string html, string fallbackIsbn = "*")
    {
        if (string.IsNullOrWhiteSpace(html))
        {
            return Task.FromResult(ISBNElement.NotFound(fallbackIsbn));
        }

        try
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var title = doc.DocumentNode.SelectSingleNode("//div[@class='bookinfo']/h1")?.InnerText?.Trim();
            var authorRaw = doc.DocumentNode.SelectSingleNode("//div[@class='bookinfo']//p[strong[text()='Author:']]")?.InnerText;
            var isbn13 = doc.DocumentNode.SelectSingleNode("//div[@class='bookinfo']//p[strong[text()='ISBN-13:']]/a")?.InnerText?.Trim();
            var isbn10 = doc.DocumentNode.SelectSingleNode("//div[@class='bookinfo']//p[strong[text()='ISBN-10:']]/a")?.InnerText?.Trim();
            var publishedRaw = doc.DocumentNode.SelectSingleNode("//div[@class='bookinfo']//p[strong[text()='Published:']]")?.InnerText;

            var author = StripLabel(authorRaw, "Author:");
            var published = StripLabel(publishedRaw, "Published:");

            if (string.IsNullOrWhiteSpace(title))
            {
                return Task.FromResult(ISBNElement.NotFound(fallbackIsbn));
            }

            return Task.FromResult(new ISBNElement(
                title,
                string.IsNullOrWhiteSpace(author) ? "Unknown" : author,
                string.IsNullOrWhiteSpace(published) ? "Unknown" : published,
                string.IsNullOrWhiteSpace(isbn10) ? "*" : isbn10,
                string.IsNullOrWhiteSpace(isbn13) ? fallbackIsbn : isbn13
            ));
        }
        catch
        {
            return Task.FromResult(ISBNElement.NotFound(fallbackIsbn));
        }
    }

    private static string StripLabel(string? value, string label)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        return value.Replace(label, string.Empty, StringComparison.OrdinalIgnoreCase).Trim();
    }
}
