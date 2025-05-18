# Linear Programming

This sample illustrates solving linear programming (LP) problems using classes in the Numerics.NET.Optimization.LinearProgramming namespace using Numerics.NET.

## Overview

This QuickStart sample demonstrates how to solve linear programming problems using Numerics.NET's 
optimization capabilities. Linear programming is a method for achieving the best outcome in a 
mathematical model whose requirements are represented by linear relationships.

The sample illustrates three different approaches to creating and solving linear programs:

1. Matrix-based approach - Shows how to construct a linear program by specifying the cost vector, 
   constraint coefficients matrix, and right-hand side vector directly. This is useful when working 
   with problems that are naturally expressed in matrix form.

2. Incremental construction - Demonstrates building a linear program step by step by adding variables 
   and constraints individually. This approach provides more clarity and is often more maintainable 
   for smaller problems.

3. MPS file input - Shows how to read a linear program from an MPS format file, which is an industry 
   standard format for representing linear and integer programming problems.

For each approach, the sample shows how to:
- Set up the linear program
- Solve it using the Revised Simplex algorithm
- Retrieve both the primal and dual solutions
- Access the optimal objective value

The sample includes proper error handling and demonstrates best practices for working with the 
LinearProgram class.


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
[C#](https://github.com/NumericsDotNet/quickstart-csharp/tree/net462/mathematics/optimization/linear-programming), [F#](https://github.com/NumericsDotNet/quickstart-fsharp/tree/net462/mathematics/optimization/linear-programming), [IronPython](https://github.com/NumericsDotNet/quickstart-ironpython/tree/net462/mathematics/optimization/linear-programming).

You can find out more about the methods used in this sample from the Numerics.NET documentation.

### Numerics.NET User's Guide

- [Optimization Model Basics](https://numerics.net/documentation/latest/mathematics/optimization/optimization-model-basics)
- [Linear Programming](https://numerics.net/documentation/latest/mathematics/optimization/linear-programming)
- [Solving Systems of Linear Equations](https://numerics.net/documentation/latest/mathematics/solving-equations/solving-systems-of-linear-equations)

### Numerics.NET API Reference

- [LinearProgram class](https://numerics.net/documentation/latest/reference/numerics.net.optimization.linearprogram)
- [LinearProgramConstraint class](https://numerics.net/documentation/latest/reference/numerics.net.optimization.linearprogramconstraint)
- [LinearProgramVariable class](https://numerics.net/documentation/latest/reference/numerics.net.optimization.linearprogramvariable)
- [MpsReader class](https://numerics.net/documentation/latest/reference/numerics.net.optimization.mpsreader)
- [Numerics.NET.Optimization namespace](https://numerics.net/documentation/latest/reference/numerics.net.optimization)


## Troubleshooting

### Common Issues

- **Missing dependencies**: Make sure to run `dotnet restore` before building
- **License verification failed**: Ensure you have a valid trial key from [https://numerics.net/trial-key](https://numerics.net/trial-key)
- **Runtime errors**: Check that you're targeting the correct .NET version (.NET 6 or .NET 8)

### Need Help?

- Check the [Numerics.NET documentation](https://numerics.net/documentation/)
- Contact support at [support@numerics.net](mailto:support@numerics.net?subject=LinearProgramming%20QuickStart%20Sample%20%28Visual+Basic%29)

## About Numerics.NET

Numerics.NET is a powerful numerical library for .NET that provides advanced mathematical 
functions and algorithms for scientific computing, data analysis, and machine learning.
See [numerics.net](https://numerics.net) for details.

---

_Last updated on 2025-05-06 3:03:10 PM (version 9.1.3)._
