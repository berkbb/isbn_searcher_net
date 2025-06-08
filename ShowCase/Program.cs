// See https://aka.ms/new-console-template for more information
using System.Text.Json;

// Read the JSON file for gathering barcodes with Google
string vscode_fileName = "barcodes.json";
// string vsmac_fileName = "/Users/berkbabadogan/Documents/GitHub/isbn_searcher_net/ShowCase/barcodes.json";
string jsonString = File.ReadAllText(vscode_fileName);
List<Barcode> barcodesList = JsonSerializer.Deserialize<List<Barcode>>(jsonString)!;

// Print the gathered ISBN item with GetBookInfo() with given barcode.
foreach (var item in barcodesList)
{
    if (!string.IsNullOrEmpty(item.barcode))
    {
        // var book_item = await Rest.GetGoogleBookInfoAsync(item.barcode);
        var book_item = await Rest.GetISBNSearchHtmlAsync(item.barcode);
        // Add the item to the ISBNElementExtensions.elements list.
        ISBNElementExtensions.elements.Add(book_item);
        // Console.WriteLine(book_item.ToString());
    }
    else
    {
        Console.WriteLine("Warning: Encountered an empty or null barcode, skipping.");
    }
}
// Make HTML table from the gathered element list.
var dataTable = ISBNElementExtensions.elements.makeHTMLTable();

// Write the HTML table to the file.
var dateTimePostfix = DateTime.Now.ToString("yyyyMMddHHmmss");
var outputName = $"datatable_{dateTimePostfix}.html";
var path = $"{outputName}";

await File.WriteAllTextAsync(path, dataTable);
Console.WriteLine($"Data table has been written to HTML file. You can find it at Showcase/{path}.");

