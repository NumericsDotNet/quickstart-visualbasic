# Data Wrangling

This sample illustrates how to perform basic data wrangling or data munging operations on data frames using classes in the Numerics.NET.DataAnalysis namespace using Numerics.NET.

## Overview

This QuickStart sample demonstrates essential data wrangling operations using Numerics.NET's DataFrame class. 
Data wrangling, also known as data munging, is the process of transforming and mapping data from one format 
into another to make it more suitable for analysis.

The sample covers several key data wrangling operations:

- Joining data frames using different join types (outer, inner, left, right)
- Working with different data sources and combining them into a single data frame
- Handling missing data during joins
- Performing one-to-one and one-to-many joins using indexes and keys
- Time series data alignment using nearest-neighbor joins
- Sorting data frames by index or column values

The example uses realistic scenarios like combining presidential data with state information and aligning 
time series data with different timestamps. It demonstrates both exact matching joins and approximate 
matching for time series data that may be offset by a few hours.


## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (.NET 6.0 or .NET 8.0)
- A code editor like [Visual Studio](https://visualstudio.microsoft.com/), [VS Code](https://code.visualstudio.com/), or [JetBrains Rider](https://www.jetbrains.com/rider/)
- Required NuGet packages: Numerics.NET.Core, Version 9.1.0

### Running the Sample

#### In Visual Studio
1. Open the solution file (.sln) in Visual Studio
2. Restore NuGet packages: Right-click on the solution → Restore NuGet Packages
3. Update the trial key in the code:
4. Press F5 to build and run the sample

#### In VS Code

1. Open the project folder in VS Code
2. Make sure you have the [C# Dev Kit extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit) installed
3. Open a terminal and run: `dotnet restore`
4. Update the trial key in the code 
5. Run the sample with: `dotnet run`

#### From the Command Line

1. Open a terminal and navigate to the project directory
2. Run `dotnet restore` to restore the required NuGet packages
3. Update the trial key in the code
4. Run the sample with `dotnet run`

### Getting a Trial Key

To run this sample, you'll need a trial key for Numerics.NET:

1. Visit [https://numerics.net/trial-key](https://numerics.net/trial-key)
2. Sign in using the provider of your choice to receive your 30-day trial key
3. Replace the placeholder in `License.Verify("your-trial-key-here")` with your actual trial key

## Related Content

This sample is also available in the following languages: 
[C#](https://github.com/NumericsDotNet/quickstart-csharp/tree/net462/data-analysis/data-wrangling), [F#](https://github.com/NumericsDotNet/quickstart-fsharp/tree/net462/data-analysis/data-wrangling).

You can find out more about the methods used in this sample from the Numerics.NET documentation.

### Numerics.NET User's Guide

- [Constructing data frames](https://numerics.net/documentation/latest/data-analysis/data-frames/constructing-data-frames)
- [Basic Operations on Data Frames](https://numerics.net/documentation/latest/data-analysis/data-frames/basic-operations-on-data-frames)
- [Joining and reshaping data frames](https://numerics.net/documentation/latest/data-analysis/data-wrangling/joining-and-reshaping-data-frames)
- [Sorting And Filtering](https://numerics.net/documentation/latest/data-analysis/data-wrangling/sorting-and-filtering)

### Numerics.NET API Reference

- [DataFrame&lt;R, C&gt; class](https://numerics.net/documentation/latest/reference/numerics.net.dataanalysis.dataframe-2)
- [Index&lt;T&gt; class](https://numerics.net/documentation/latest/reference/numerics.net.dataanalysis.index-1)
- [Grouping&lt;T&gt; class](https://numerics.net/documentation/latest/reference/numerics.net.dataanalysis.grouping-1)
- [Pivot&lt;R, C&gt; class](https://numerics.net/documentation/latest/reference/numerics.net.dataanalysis.pivot-2)
- [Numerics.NET.DataAnalysis namespace](https://numerics.net/documentation/latest/reference/numerics.net.dataanalysis)


## Troubleshooting

### Common Issues

- **Missing dependencies**: Make sure to run `dotnet restore` before building
- **License verification failed**: Ensure you have a valid trial key from [https://numerics.net/trial-key](https://numerics.net/trial-key)
- **Runtime errors**: Check that you're targeting the correct .NET version (.NET 6 or .NET 8)

### Need Help?

- Check the [Numerics.NET documentation](https://numerics.net/documentation/)
- Contact support at [support@numerics.net](mailto:support@numerics.net?subject=DataWrangling%20QuickStart%20Sample%20%28Visual+Basic%29)

## About Numerics.NET

Numerics.NET is a powerful numerical library for .NET that provides advanced mathematical 
functions and algorithms for scientific computing, data analysis, and machine learning.
See [numerics.net](https://numerics.net) for details.

---

_Last updated on 2025-05-06 3:03:10 PM (version 9.1.3)._
