
# ISBN Searcher for .NET. 

[![NuGet Version](https://img.shields.io/nuget/v/ISBNSearcher_NET?&label=nuget&color=informational&logo=nuget)](https://www.nuget.org/packages/ISBNSearcher_NET/) 
[![NuGet Downloads](https://img.shields.io/nuget/dt/ISBNSearcher_NET?color=brightgreen&logo=nuget)](https://www.nuget.org/packages/ISBNSearcher_NET/)
[![License](https://img.shields.io/github/license/berkbb/tcid_checker_net?color=important)](https://www.nuget.org/packages/ISBNSearcher_NET/)


   

## Features

* Searches ISBN metadata with given local JSON via API supplied by Google.





## Usage
 

```c#


//Read the JSON file for gathering barcodes.
string fileName = "barcodes.json";
string jsonString = File.ReadAllText(fileName);
List<Barcode> barcodesList = JsonSerializer.Deserialize<List<Barcode>>(jsonString)!;

//Print the gathered ISBN item with GetBookInfo() with given barcode .
foreach (var item in barcodesList)
{
    var book_item = await Rest.GetBookInfoAsync(item.barcode);
    ISBNElementExtensions.elements.Add(book_item);
    Console.WriteLine(book_item.ToString());
}
//Make HTML table from the gathered element list.
var dataTable = ISBNElementExtensions.elements.makeTable();

//Write the HTML table to the file.
var dateTimePostfix = DateTime.Now.ToString("yyyyMMddHHmmss");
var outputName = $"datatable_{dateTimePostfix}.html";

await File.WriteAllTextAsync(outputName, dataTable);
Console.WriteLine($"The data table is printed to HTML document, you can find it in the ShowCase/{outputName}.");
```



