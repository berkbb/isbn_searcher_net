using System.Net;

public sealed class ISBNElement
{
    public string Title { get; }
    public string Author { get; }
    public string PublishDate { get; }
    public string Isbn10 { get; }
    public string Isbn13 { get; }

    // Legacy field aliases kept for backward compatibility.
    public string title => Title;
    public string author => Author;
    public string publishDate => PublishDate;
    public string ISBN_10 => Isbn10;
    public string ISBN_13 => Isbn13;

    public ISBNElement(string title, string author, string publishDate, string isbnTen, string isbnThirteen)
    {
        Title = string.IsNullOrWhiteSpace(title) ? "Unknown" : title.Trim();
        Author = string.IsNullOrWhiteSpace(author) ? "Unknown" : author.Trim();
        PublishDate = string.IsNullOrWhiteSpace(publishDate) ? "Unknown" : publishDate.Trim();
        Isbn10 = string.IsNullOrWhiteSpace(isbnTen) ? "N/A" : isbnTen.Trim();
        Isbn13 = string.IsNullOrWhiteSpace(isbnThirteen) ? "N/A" : isbnThirteen.Trim();
    }

    public static ISBNElement NotFound(string isbn)
    {
        return new ISBNElement("Cannot find ISBN!", "*", "*", "*", isbn);
    }

    public override string ToString()
    {
        return
            "╔════════════════════════════════════╗\n" +
            "║           Book Details            ║\n" +
            "╠════════════════════════════════════╣\n" +
            $"║ Title     : {Title,-22}║\n" +
            $"║ Author    : {Author,-22}║\n" +
            $"║ ISBN-13   : {Isbn13,-22}║\n" +
            $"║ ISBN-10   : {Isbn10,-22}║\n" +
            $"║ Published : {PublishDate,-22}║\n" +
            "╚════════════════════════════════════╝\n";
    }

    public string ToTableRow()
    {
        return $"<tr><td>{WebUtility.HtmlEncode(Isbn13)}</td><td>{WebUtility.HtmlEncode(Title)}</td><td>{WebUtility.HtmlEncode(Author)}</td></tr>";
    }
}
