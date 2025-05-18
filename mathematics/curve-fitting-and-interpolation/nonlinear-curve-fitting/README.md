# Nonlinear Curve Fitting

This sample illustrates nonlinear least squares curve fitting of predefined and user-defined curves using the NonlinearCurveFitter class using Numerics.NET.

## Overview

This QuickStart sample demonstrates how to perform nonlinear curve fitting using the NonlinearCurveFitter class 
in Numerics.NET.

The sample shows two main approaches to nonlinear curve fitting:

1. Using a predefined curve type (FourParameterLogisticCurve) to fit a dose-response curve to data 
that includes measurement errors. This demonstrates how to:
   - Set up the curve fitter with x/y data points and measurement errors
   - Convert errors to weights for weighted least squares fitting
   - Obtain fit parameters and their standard deviations
   - Access diagnostic information about the fitting process

2. Creating and fitting a custom nonlinear curve using both manual implementation and automatic 
differentiation:
   - Shows how to create a custom curve class by inheriting from NonlinearCurve
   - Demonstrates implementing value, slope and partial derivative calculations
   - Shows how to use automatic differentiation as an alternative to manual implementation
   - Uses the NIST Statistical Reference Dataset MGH10 to validate the implementation
   - Demonstrates both unweighted and weighted fitting using built-in weight functions

The sample includes extensive error handling and provides examples of accessing diagnostic 
information such as residuals, iteration counts, and function evaluations.


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
[C#](https://github.com/NumericsDotNet/quickstart-csharp/tree/net8.0/mathematics/curve-fitting-and-interpolation/nonlinear-curve-fitting), [F#](https://github.com/NumericsDotNet/quickstart-fsharp/tree/net8.0/mathematics/curve-fitting-and-interpolation/nonlinear-curve-fitting), [IronPython](https://github.com/NumericsDotNet/quickstart-ironpython/tree/net8.0/mathematics/curve-fitting-and-interpolation/nonlinear-curve-fitting).

You can find out more about the methods used in this sample from the Numerics.NET documentation.

### Numerics.NET User's Guide

- [Nonlinear Curve Fitting](https://numerics.net/documentation/latest/mathematics/curve-fitting/nonlinear-curve-fitting)
- [Predefined Nonlinear Curves](https://numerics.net/documentation/latest/mathematics/curve-fitting/predefined-nonlinear-curves)
- [Minimizing Sums of Squares](https://numerics.net/documentation/latest/mathematics/optimization/minimizing-sums-of-squares)

### Numerics.NET API Reference

- [NonlinearCurveFitter class](https://numerics.net/documentation/latest/reference/numerics.net.curves.nonlinearcurvefitter)
- [NonlinearCurve class](https://numerics.net/documentation/latest/reference/numerics.net.curves.nonlinearCurve)
- [Numerics.NET.Curves.Nonlinear namespace](https://numerics.net/documentation/latest/reference/numerics.net.curves.nonlinear)


## Troubleshooting

### Common Issues

- **Missing dependencies**: Make sure to run `dotnet restore` before building
- **License verification failed**: Ensure you have a valid trial key from [https://numerics.net/trial-key](https://numerics.net/trial-key)
- **Runtime errors**: Check that you're targeting the correct .NET version (.NET 6 or .NET 8)

### Need Help?

- Check the [Numerics.NET documentation](https://numerics.net/documentation/)
- Contact support at [support@numerics.net](mailto:support@numerics.net?subject=NonlinearCurveFitting%20QuickStart%20Sample%20%28Visual+Basic%29)

## About Numerics.NET

Numerics.NET is a powerful numerical library for .NET that provides advanced mathematical 
functions and algorithms for scientific computing, data analysis, and machine learning.
See [numerics.net](https://numerics.net) for details.

---

_Last updated on 2025-05-06 3:03:10 PM (version 9.1.3)._
