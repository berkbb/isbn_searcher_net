
# ISBN Searcher for .NET. 

[![NuGet Version](https://img.shields.io/nuget/v/ISBNSearcher_NET?&label=nuget&color=informational&logo=nuget)](https://www.nuget.org/packages/ISBNSearcher_NET/) 
[![NuGet Downloads](https://img.shields.io/nuget/dt/ISBNSearcher_NET?color=brightgreen&logo=nuget)](https://www.nuget.org/packages/ISBNSearcher_NET/)
[![License](https://img.shields.io/github/license/berkbb/tcid_checker_net?color=important)](https://www.nuget.org/packages/ISBNSearcher_NET/)


   

## Features

* Searches ISBN metadata with given local JSON via API supplied by Google.

* Searches ISBN metadata with given local JSON via API supplied by isbnsearch.org.





## Usage

### With Google Books API

```c#


// Read the JSON file for gathering barcodes with Google
string vscode_fileName = "barcodes.json";
string jsonString = File.ReadAllText(vscode_fileName);
List<Barcode> barcodesList = JsonSerializer.Deserialize<List<Barcode>>(jsonString)!;

// Print the gathered ISBN item with GetBookInfo() with given barcode.
foreach (var item in barcodesList)
{
    if (!string.IsNullOrEmpty(item.barcode))
    {
        var book_item = await Rest.GetGoogleBookInfoAsync(item.barcode);
        // Add the item to the ISBNElementExtensions.elements list.
        ISBNElementExtensions.elements.Add(book_item);
        Console.WriteLine(book_item.ToString());
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

// Get object from the gathered element list.
Console.WriteLine(ISBNElementExtensions.elements.LastOrDefault());


```


