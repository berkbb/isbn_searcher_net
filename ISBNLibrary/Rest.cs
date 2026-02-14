using System.Net;

public static class Rest
{
    private static readonly HttpClient HttpClient = new();

    public static async Task<ISBNElement> GetGoogleBookInfoAsync(string isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn))
        {
            return ISBNElement.NotFound("*");
        }

        var normalizedIsbn = isbn.Trim();

        try
        {
            using var response = await HttpClient.GetAsync($"https://www.googleapis.com/books/v1/volumes?q=isbn:{normalizedIsbn}");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return ISBNElement.NotFound(normalizedIsbn);
            }

            var result = await response.Content.ReadAsStringAsync();
            var payload = Welcome.FromJson(result);
            var volumeInfo = payload?.Items?.FirstOrDefault()?.VolumeInfo;
            if (volumeInfo is null)
            {
                return ISBNElement.NotFound(normalizedIsbn);
            }

            var isbn10 = volumeInfo.IndustryIdentifiers?
                .FirstOrDefault(x => string.Equals(x.Type, "ISBN_10", StringComparison.OrdinalIgnoreCase))?
                .Identifier ?? "*";

            var isbn13 = volumeInfo.IndustryIdentifiers?
                .FirstOrDefault(x => string.Equals(x.Type, "ISBN_13", StringComparison.OrdinalIgnoreCase))?
                .Identifier ?? normalizedIsbn;

            return new ISBNElement(
                volumeInfo.Title ?? "Unknown",
                volumeInfo.Authors.CompileArrayInString(),
                volumeInfo.PublishedDate ?? "Unknown",
                isbn10,
                isbn13
            );
        }
        catch (Exception)
        {
            return ISBNElement.NotFound(normalizedIsbn);
        }
    }

    public static async Task<ISBNElement> GetISBNSearchHtmlAsync(string isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn))
        {
            return ISBNElement.NotFound("*");
        }

        var normalizedIsbn = isbn.Trim();

        try
        {
            var html = await ISBNSearchResponse.GetHtmlFromIsbnSearch(normalizedIsbn);
            return await ISBNSearchResponse.ParseBookInfo(html, normalizedIsbn);
        }
        catch (Exception)
        {
            return ISBNElement.NotFound(normalizedIsbn);
        }
    }
}
