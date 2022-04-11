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
        this.ISBN_10 = ISBN_ten;
        this.ISBN_13 = isbn_thirteen;
    }
    /// <summary>
    /// ToString() overload.
    /// </summary>
    /// <returns>string</returns>
    public override string ToString()
    {
        return $"Book -- > Title: {this.title} - Author: {this.author} -  Publish Date: {this.publishDate} - ISBN 10: {this.ISBN_10} - ISBN 13: {this.ISBN_13}";
    }


    /// <summary>
    /// Make table row from class item.
    /// </summary>
    /// <returns>string (HTML row)</returns>
    public String ToTableRow()
    {
        return $"<tr><td>{this.ISBN_13}</td><td>{this.title}</td><td>{this.author}</td><td>{this.publishDate}</td><td>{this.ISBN_10}</td></tr>";
    }


}

