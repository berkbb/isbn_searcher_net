# ISBN Searcher for .NET

[![NuGet Version](https://img.shields.io/nuget/v/ISBNSearcher_NET?label=nuget&color=informational&logo=nuget)](https://www.nuget.org/packages/ISBNSearcher_NET/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/ISBNSearcher_NET?color=brightgreen&logo=nuget)](https://www.nuget.org/packages/ISBNSearcher_NET/)

ISBN ile kitap bilgisi aramak icin .NET kutuphanesi (`ISBNLibrary`) ve Blazor UI (`ShowCase`) icerir.

## Project Status

- `ISBNLibrary`: `net10.0`
- `ShowCase` (Blazor): `net10.0`
- Package version: `2.1.0`

## Features

- Google Books API ile ISBN arama
- isbnsearch.org uzerinden ISBN arama
- Tekli ve toplu arama yapabilen Blazor arayuz
- macOS / Windows / Linux tek tus baslaticilar

## Requirements

- .NET SDK 10+

## Quick Start (Blazor UI)

Proje klasoru:

```bash
cd /Users/berkbabadogan/Documents/GitHub/isbn_searcher_net
```

### macOS

- `start_showcase.command` dosyasina cift tikla.
- Log: `/tmp/isbn_showcase.log`

### Windows

- `start_showcase.bat` dosyasina cift tikla.
- Log: `%TEMP%\\isbn_showcase.log`

### Linux

```bash
./start_showcase.sh
```

- Log: `/tmp/isbn_showcase.log`


## Library Usage

```csharp
var book = await Rest.GetGoogleBookInfoAsync("9789750845987");
Console.WriteLine(book);

var fromIsbnSearch = await Rest.GetISBNSearchHtmlAsync("9789750845987");
Console.WriteLine(fromIsbnSearch);
```

