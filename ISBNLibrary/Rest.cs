using System.Net;

public static class Rest
{
    /// <summary>
    ///  Get ISBN data from REST API with given isbn from web. Google Books API is used.
    ///  If the ISBN is not found, it returns an ISBNElement with default values.
    ///  If the ISBN is found, it returns an ISBNElement with the book's title, author, published date, ISBN_10, and ISBN_13.
    ///  If there is an error, it returns an ISBNElement with "Cannot find ISBN !" message.
    ///  The ISBN_10 and ISBN_13 are the same as the given isbn.
    ///  The author is a string that contains the authors of the book, separated by commas.
    ///  The published date is a string that contains the published date of the book.
    ///  The title is a string that contains the title of the book.
    ///  The ISBNElement is a class that contains the book's information.
    /// <summary>
    /// <param name="isbn">ISBN string.</param>
    /// <returns>ISBNElement</returns>
    /// <exception cref="HttpRequestException">Thrown when there is an error in the HTTP request.</exception>
    /// <exception cref="ArgumentNullException">Thrown when the result is null.</exception>
    /// <exception cref="Exception">Thrown when there is an error in the process.</exception>

    public static async Task<ISBNElement> GetGoogleBookInfoAsync(string isbn)
    {
        try
        {
            using (HttpClient httpClient = new())
            {
                var response = await httpClient.GetAsync($"https://www.googleapis.com/books/v1/volumes?q=isbn:{isbn}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var welcome = Welcome.FromJson(result);

                    if (welcome.TotalItems >= 1 && welcome.Items != null)
                    {
                        var info = welcome.Items.FirstOrDefault()!.VolumeInfo;

                        return new ISBNElement(
                            info.Title,
                            info.Authors.compileArrayInString(),
                            info.PublishedDate,
                            info.IndustryIdentifiers[0].Identifier,
                            isbn
                        );
                    }
                    else
                    {
                        throw new ArgumentNullException("result", "Result is null");
                    }
                }
                else
                {
                    throw new HttpRequestException($"{response.StatusCode} - {response.ReasonPhrase}");
                }
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error: {ex.Message} !");
            return new ISBNElement("Cannot find ISBN!", "*", "*", "*", isbn);
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"Error: {ex.ParamName} {ex.Message} !");
            return new ISBNElement("Cannot find ISBN!", "*", "*", "*", isbn);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message} !");
            return new ISBNElement("Cannot find ISBN!", "*", "*", "*", isbn);
        }
    }
    public static async Task<ISBNElement> GetISBNSearchHtmlAsync(string isbn)
    {
        var html = await ISBNSearchResponse.GetHtmlFromIsbnSearch(isbn);
        Console.WriteLine($"HTML content retrieved for ISBN: {html}");
        if (string.IsNullOrWhiteSpace(html))
        {
            throw new ArgumentNullException(nameof(html), "HTML content cannot be null or empty.");

        }
        else
        {
            try
            {
                return await ISBNSearchResponse.ParseBookInfo(html);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing HTML: {ex.Message}");
                return new ISBNElement("Cannot find ISBN!", "*", "*", "*", isbn);
            }
        }
  
      
    }
}

