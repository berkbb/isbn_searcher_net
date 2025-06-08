using System;

public class ISBNElement
{
    /// <summary>
    /// Title
    /// </summary>
    public string title;

    /// <summary>
    /// Author
    /// </summary>
    public string author;

    /// <summary>
    /// Published Date
    /// </summary>
    public string publishDate;

    /// <summary>
    /// ISBN_10
    /// </summary>
    public string ISBN_10;

    /// <summary>
    /// ISBN_13
    /// </summary>
    public string ISBN_13;

    /// <summary>
    /// ISBN element.
    /// </summary>
    /// <param name="title">Book Title</param>
    /// <param name="author">Author</param>
    /// <param name="publishDate">Publish date.</param>
    /// <param name="ISBN_ten"> ISBN 10</param>
    /// <param name="isbn_thirteen">ISBN 13</param>
    public ISBNElement(string title, string author, string publishDate, string ISBN_ten, string isbn_thirteen)
    {
        this.title = title;
        this.author = author;
        this.publishDate = publishDate;
        ISBN_10 = ISBN_ten;
        ISBN_13 = isbn_thirteen;
    }
    /// <summary>
    /// ToString() overload.
    /// </summary>
    /// <returns>string</returns>
    public override string ToString()
    {
        return
            "╔════════════════════════════════════╗\n" +
            "║           Book Details            ║\n" +
            "╠════════════════════════════════════╣\n" +
            $"║ Title     : {title,-22}║\n" +
            $"║ Author    : {author,-22}║\n" +
            $"║ ISBN-13   : {ISBN_13,-22}║\n" +
            $"║ ISBN-10   : {ISBN_10,-22}║\n" +
            $"║ Published : {publishDate,-22}║\n" +
            "╚════════════════════════════════════╝\n";
    }


    /// <summary>
    /// Make table row from class item.
    /// </summary>
    /// <returns>string (HTML row)</returns>
    public String ToTableRow()
    {
        return $"<tr><td>{ISBN_13}</td><td>{title}</td><td>{author}</td></tr>";
    }


}

