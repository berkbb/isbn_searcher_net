

using System.Net;
using GoogleBooksResponse;

public static class Rest
{



    /// <summary>
    ///  Get ISBN data from REST API with given isbn from web.
    /// </summary>
    /// <param name="isbn">ISBN string.</param>
    /// <returns>ISBNElement</returns>
    public static async Task<ISBNElement> GetBookInfoAsync(string isbn)
    {
        try
        {
            using (HttpClient httpClient = new HttpClient())
            {

                var response = await httpClient.GetAsync($"https://www.googleapis.com/books/v1/volumes?q=isbn:{isbn}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var welcome = Welcome.FromJson(result);



                    if (welcome.TotalItems >= 1 && welcome.Items != null)
                    {
                        var info = welcome.Items.FirstOrDefault()!.VolumeInfo;

                        return new ISBNElement(info.Title, info.Authors.compileArrayInString(), info.PublishedDate, info.IndustryIdentifiers[0].Identifier, isbn);
                    }

                    else
                    {
                        throw new ArgumentNullException("result", "is null");

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
            return new ISBNElement("Cannot find ISBN !", "*", "*", "*", isbn);
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"Error: {ex.ParamName} {ex.Message} !");
            return new ISBNElement("Cannot find ISBN !","*", "*", "*", isbn);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message} !");
            return new ISBNElement("Cannot find ISBN !", "*", "*", "*", isbn);
        }

    }

    /// <summary>
    /// Crates REST Get() URI scheme.
    /// </summary>
    /// <param name="isbn">ISBN string</param>
    /// <returns>Get() URI for REST operations (string)</returns>
    private static string GetSpecialUrlWithISBN(string isbn)
    {
        return $"https://www.googleapis.com/books/v1/volumes?q=isbn:{isbn}";
    }
}