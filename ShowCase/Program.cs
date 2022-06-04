// See https://aka.ms/new-console-template for more information


using System;
using System.Text.Json;

//Read the JSON file for gathering barcodes.
string vscode_fileName = "barcodes.json";
string vsmac_fileName = "/Users/berkbabadogan/Documents/GitHub/isbn_searcher_net/ShowCase/barcodes.json";
string jsonString = File.ReadAllText(vsmac_fileName);
List<Barcode> barcodesList = JsonSerializer.Deserialize<List<Barcode>>(jsonString)!;

//Print the gathered ISBN item with GetBookInfo() with given barcode .
foreach (var item in barcodesList)
{
    var book_item = await Rest.GetBookInfoAsync(item.barcode);
    ISBNElementExtensions.elements.Add(book_item);
    Console.WriteLine(book_item.ToString());
}
//Make HTML table from the gathered element list.
var dataTable = ISBNElementExtensions.elements.makeHTMLTable();

//Write the HTML table to the file.
var dateTimePostfix = DateTime.Now.ToString("yyyyMMddHHmmss");
var outputName = $"datatable_{dateTimePostfix}.html";
var path = $"{outputName}";

await File.WriteAllTextAsync(path, dataTable);
Console.WriteLine($"The data table is printed to HTML document, you can find it in the Showcase/{path}.");

//Get object from the gathered element list.
Console.WriteLine(ISBNElementExtensions.elements.LastOrDefault());

