# Chebyshev Series

This sample illustrates the basic use of the ChebyshevSeries class  using Numerics.NET.

## Overview

This QuickStart sample demonstrates how to work with Chebyshev series for polynomial approximation in 
Numerics.NET. Chebyshev polynomials provide a robust alternative basis for representing and 
manipulating polynomials with superior numerical stability.

The sample covers:
- Creating Chebyshev series with explicit coefficients over specified intervals
- Generating Chebyshev approximations of arbitrary functions
- Examining the coefficients and error characteristics of Chebyshev approximations
- Performing least squares fitting using Chebyshev basis functions

The example specifically shows approximating the cosine function and demonstrates the high accuracy
achievable with relatively low-degree Chebyshev expansions. It includes error analysis at the
zeros of higher-degree Chebyshev polynomials and illustrates how to work with both interpolation
and least squares fitting approaches.

This approach is particularly valuable when high numerical precision is required or when working
with higher-degree polynomial approximations where traditional polynomial bases may become
unstable.


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
[C#](https://github.com/NumericsDotNet/quickstart-csharp/tree/net462/mathematics/curves/chebyshev-expansions), [F#](https://github.com/NumericsDotNet/quickstart-fsharp/tree/net462/mathematics/curves/chebyshev-expansions), [IronPython](https://github.com/NumericsDotNet/quickstart-ironpython/tree/net462/mathematics/curves/chebyshev-expansions).

You can find out more about the methods used in this sample from the Numerics.NET documentation.

### Numerics.NET User's Guide

- [Chebyshev Series](https://numerics.net/documentation/latest/mathematics/curves-and-interpolation/chebyshev-series)
- [Curve Basics](https://numerics.net/documentation/latest/mathematics/curves-and-interpolation/curve-basics)
- [Polynomials](https://numerics.net/documentation/latest/mathematics/curves-and-interpolation/polynomials)

### Numerics.NET API Reference

- [ChebyshevSeries class](https://numerics.net/documentation/latest/reference/numerics.net.curves.chebyshevseries)
- [ChebyshevBasis class](https://numerics.net/documentation/latest/reference/numerics.net.curves.chebyshevbasis)
- [Numerics.NET.Curves namespace](https://numerics.net/documentation/latest/reference/numerics.net.curves)


## Troubleshooting

### Common Issues

- **Missing dependencies**: Make sure to run `dotnet restore` before building
- **License verification failed**: Ensure you have a valid trial key from [https://numerics.net/trial-key](https://numerics.net/trial-key)
- **Runtime errors**: Check that you're targeting the correct .NET version (.NET 6 or .NET 8)

### Need Help?

- Check the [Numerics.NET documentation](https://numerics.net/documentation/)
- Contact support at [support@numerics.net](mailto:support@numerics.net?subject=ChebyshevExpansions%20QuickStart%20Sample%20%28Visual+Basic%29)

## About Numerics.NET

Numerics.NET is a powerful numerical library for .NET that provides advanced mathematical 
functions and algorithms for scientific computing, data analysis, and machine learning.
See [numerics.net](https://numerics.net) for details.

---

_Last updated on 2025-05-06 3:03:10 PM (version 9.1.3)._
