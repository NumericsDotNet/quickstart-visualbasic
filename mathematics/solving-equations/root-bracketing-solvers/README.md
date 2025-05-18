# Root Bracketing Solvers

This sample illustrates the use of the root bracketing solvers for solving equations in one variable using Numerics.NET.

## Overview

This QuickStart sample demonstrates how to use root bracketing solvers to find solutions to nonlinear equations 
in one variable using Numerics.NET.

Root bracketing solvers are numerical algorithms that find solutions to equations of the form f(x)=0 
by repeatedly narrowing down an interval known to contain a root. The sample shows three different 
methods:

- The Bisection method, which simply halves the interval at each step
- The Regula Falsi (False Position) method, which uses linear interpolation but can be slow to 
  converge in some cases
- The Dekker-Brent method, which combines the reliability of bisection with faster convergence
  techniques

The sample demonstrates how to:
- Set up and configure different root bracketing solvers
- Specify the target function to solve
- Control convergence criteria and iteration limits
- Handle the results and check solution status
- Access error estimates and iteration counts

Examples use simple trigonometric functions to illustrate the behavior and relative performance of 
each method. The code shows how to handle both well-behaved and more challenging cases.


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
[C#](https://github.com/NumericsDotNet/quickstart-csharp/tree/net462/mathematics/solving-equations/root-bracketing-solvers), [F#](https://github.com/NumericsDotNet/quickstart-fsharp/tree/net462/mathematics/solving-equations/root-bracketing-solvers), [IronPython](https://github.com/NumericsDotNet/quickstart-ironpython/tree/net462/mathematics/solving-equations/root-bracketing-solvers).

You can find out more about the methods used in this sample from the Numerics.NET documentation.

### Numerics.NET User's Guide

- [The EquationSolver Class](https://numerics.net/documentation/latest/mathematics/solving-equations/equationsolver-class)
- [Methods that do not use the derivative](https://numerics.net/documentation/latest/mathematics/solving-equations/methods-that-do-not-use-the-derivative)

### Numerics.NET API Reference

- [BisectionSolver class](https://numerics.net/documentation/latest/reference/numerics.net.equationsolvers.bisectionsolver)
- [RegulaFalsiSolver class](https://numerics.net/documentation/latest/reference/numerics.net.equationsolvers.regulafalsisolver)
- [DekkerBrentSolver class](https://numerics.net/documentation/latest/reference/numerics.net.equationsolvers.dekkerbrentsolver)
- [Numerics.NET.EquationSolvers namespace](https://numerics.net/documentation/latest/reference/numerics.net.equationsolvers)


## Troubleshooting

### Common Issues

- **Missing dependencies**: Make sure to run `dotnet restore` before building
- **License verification failed**: Ensure you have a valid trial key from [https://numerics.net/trial-key](https://numerics.net/trial-key)
- **Runtime errors**: Check that you're targeting the correct .NET version (.NET 6 or .NET 8)

### Need Help?

- Check the [Numerics.NET documentation](https://numerics.net/documentation/)
- Contact support at [support@numerics.net](mailto:support@numerics.net?subject=RootBracketingSolvers%20QuickStart%20Sample%20%28Visual+Basic%29)

## About Numerics.NET

Numerics.NET is a powerful numerical library for .NET that provides advanced mathematical 
functions and algorithms for scientific computing, data analysis, and machine learning.
See [numerics.net](https://numerics.net) for details.

---

_Last updated on 2025-05-06 3:03:10 PM (version 9.1.3)._
