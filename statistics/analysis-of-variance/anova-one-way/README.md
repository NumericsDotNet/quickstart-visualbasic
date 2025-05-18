# One-Way Anova

This sample illustrates how to use the OneWayAnovaModel class to perform a one-way analysis of variance using Numerics.NET.

## Overview

This QuickStart sample demonstrates how to perform a one-way analysis of variance (ANOVA) using the 
OneWayAnovaModel class in Numerics.NET.

The sample analyzes a marketing dataset examining the effect of package color on product sales 
across 12 stores. The data includes sales figures for packages in three colors (red, blue, and 
green). Using one-way ANOVA, the sample shows how to:

- Create a DataFrame from a collection of anonymous objects containing the sales data
- Construct and fit a OneWayAnovaModel using both direct variable specification and formula syntax
- Verify the balance of the experimental design
- Generate and display a classic ANOVA table
- Access group means and other statistics for each color group (treatment level)
- Calculate overall summary statistics like the grand mean

The code demonstrates proper model construction, checking model assumptions, and extracting both 
detailed and summary statistics from the analysis. This example is particularly relevant for 
researchers and analysts conducting experiments with a single categorical factor.


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
[C#](https://github.com/NumericsDotNet/quickstart-csharp/tree/net462/statistics/analysis-of-variance/anova-one-way), [F#](https://github.com/NumericsDotNet/quickstart-fsharp/tree/net462/statistics/analysis-of-variance/anova-one-way), [IronPython](https://github.com/NumericsDotNet/quickstart-ironpython/tree/net462/statistics/analysis-of-variance/anova-one-way).

You can find out more about the methods used in this sample from the Numerics.NET documentation.

### Numerics.NET User's Guide

- [ANOVA Models](https://numerics.net/documentation/latest/statistics/analysis-of-variance/anova-models)
- [One-Way ANOVA](https://numerics.net/documentation/latest/statistics/analysis-of-variance/one-way-anova)
- [Constructing data frames](https://numerics.net/documentation/latest/data-analysis/data-frames/constructing-data-frames)

### Numerics.NET API Reference

- [OneWayAnovaModel class](https://numerics.net/documentation/latest/reference/numerics.net.statistics.onewayanovamodel)
- [DataFrame&lt;R, C&gt; class](https://numerics.net/documentation/latest/reference/numerics.net.dataanalysis.dataframe-2)
- [Cell structure](https://numerics.net/documentation/latest/reference/numerics.net.statistics.cell)
- [AnovaTable class](https://numerics.net/documentation/latest/reference/numerics.net.statistics.anovatable)
- [Numerics.NET.Statistics namespace](https://numerics.net/documentation/latest/reference/numerics.net.statistics)


## Troubleshooting

### Common Issues

- **Missing dependencies**: Make sure to run `dotnet restore` before building
- **License verification failed**: Ensure you have a valid trial key from [https://numerics.net/trial-key](https://numerics.net/trial-key)
- **Runtime errors**: Check that you're targeting the correct .NET version (.NET 6 or .NET 8)

### Need Help?

- Check the [Numerics.NET documentation](https://numerics.net/documentation/)
- Contact support at [support@numerics.net](mailto:support@numerics.net?subject=AnovaOneWay%20QuickStart%20Sample%20%28Visual+Basic%29)

## About Numerics.NET

Numerics.NET is a powerful numerical library for .NET that provides advanced mathematical 
functions and algorithms for scientific computing, data analysis, and machine learning.
See [numerics.net](https://numerics.net) for details.

---

_Last updated on 2025-05-06 3:03:10 PM (version 9.1.3)._
