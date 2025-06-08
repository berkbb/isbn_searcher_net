using HtmlAgilityPack;

public partial class ISBNSearchResponse
{
    public static async Task<string> GetHtmlFromIsbnSearch(string isbn)
    {
        using var client = new HttpClient();
        string url = $"https://isbnsearch.org/isbn/{isbn}";
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var html = await response.Content.ReadAsStringAsync();
        return html;
    }
    public static Task<ISBNElement> ParseBookInfo(string html)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(html))
            {
                throw new ArgumentNullException(nameof(html), "HTML content cannot be null or empty.");
            }
            else

            {
                  try
                  {
                      var doc = new HtmlDocument();
                     doc.LoadHtml(html);
                     // Example: Extract title and author using HtmlAgilityPack
                    var titleNode = doc.DocumentNode.SelectSingleNode("//div[@class='bookinfo']/h1")?.InnerText;
                    var authorNode = doc.DocumentNode.SelectSingleNode("//div[@class='bookinfo']//p[strong[text()='Author:']]")?.InnerText;
                    var isbn13Node = doc.DocumentNode.SelectSingleNode("//div[@class='bookinfo']//p[strong[text()='ISBN-13:']]/a")?.InnerText;
                    var isbn10Node = doc.DocumentNode.SelectSingleNode("//div[@class='bookinfo']//p[strong[text()='ISBN-10:']]/a")?.InnerText;
                    var publishedNode = doc.DocumentNode.SelectSingleNode("//div[@class='bookinfo']//p[strong[text()='Published:']]")?.InnerText;

                    // author == "David Gibbins"

                if (titleNode != null && authorNode != null && isbn13Node != null && isbn10Node != null && publishedNode != null)
                {
                    string new_title = titleNode.Trim();
                    string new_author = authorNode.Trim();
                    string new_isbn13 = isbn13Node.Trim();
                    string new_isbn10 = isbn10Node.Trim();
                    string new_published = publishedNode.Trim();

                    var element = new ISBNElement(
                        new_title,
                        new_author,
                        new_isbn13,
                        new_isbn10,
                        new_published
                    );

                    return Task.FromResult(element);
                }
                else
                {
                    throw new ArgumentNullException("titleNode or authorNode", "Could not find title or author in the HTML content.");
                }
                  }
                  catch (Exception ex)
                  {
                      Console.WriteLine($"Error parsing HTML: {ex.Message}");
                      return Task.FromException<ISBNElement>(ex);
                  }
              
            }
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return Task.FromException<ISBNElement>(ex);
        }
  

      
    }
}
