# Advent of Code 2021
This repository contains the code for the practice challenges for the [Advent of Code](https://adventofcode.com/2021). I used C#8.0 to through all of them and [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) as the IDE.

## How to run the solution
The solution is organized by projects. There is library one project called `Common` which contains an `Utils` class to retrieve the input data directly from the Advent od Code website. It is required to have an `appsettings.json` file with the following arguments:
```json
{
  "Year": 2021,
  "SessionCookie": SESSION_COOKIE_VALUE
}
``` 

To get the `SESSION_COOKIE_VALUE`:
1. Login on Advent of Code with your account.
Open your browser’s developer console. This is usually done by right-clicking on the page and selecting “Inspect” or "Inspect Element".
1. Navigate to the Network tab in the developer console.
2. Refresh the page or navigate to any input page, such as https://adventofcode.com/2021/day/1/input.
3. In the list of network requests, click on the request for the page you just loaded. This will open a panel with details about the request.
4. Look for the “Cookie” header in the request headers section. The value of this header is your session cookie.

The rest of the projects are named with the format `Day00`, according to the challenge day. They are console projects that may be launched directly from the IDE or using:

```bash
dotnet run --project NAME_OF_PROJECT
```

> [!NOTE]  
> The [.NET CLI](https://learn.microsoft.com/en-us/dotnet/core/tools/) must be installed to run the previous command.