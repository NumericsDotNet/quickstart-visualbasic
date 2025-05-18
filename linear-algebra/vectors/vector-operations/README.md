# Vector Operations

This sample illustrates how to perform operations on Vector objects, including construction, element access, arithmetic operations using Numerics.NET.

## Overview

This QuickStart sample demonstrates the fundamental operations available for working with vector objects in 
Numerics.NET. It shows how to perform common mathematical operations on vectors efficiently and elegantly.

The sample covers:
- Basic vector arithmetic including addition, subtraction, and scalar multiplication
- Computing vector norms (1-norm, 2-norm, infinity norm)
- Calculating dot products between vectors
- Finding maximum and minimum elements and their indices
- Applying mathematical functions to vectors using Map operations
- In-place operations for better performance
- Different ways to construct and manipulate vectors

Each operation is demonstrated with clear examples and shows both operator syntax and equivalent method
calls. The sample illustrates how to chain operations efficiently and how to use both static and
instance methods for vector manipulation. Special attention is paid to performance considerations,
such as when to use in-place operations versus creating new vector instances.


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
[C#](https://github.com/NumericsDotNet/quickstart-csharp/tree/net462/linear-algebra/vectors/vector-operations), [F#](https://github.com/NumericsDotNet/quickstart-fsharp/tree/net462/linear-algebra/vectors/vector-operations), [IronPython](https://github.com/NumericsDotNet/quickstart-ironpython/tree/net462/linear-algebra/vectors/vector-operations).

You can find out more about the methods used in this sample from the Numerics.NET documentation.

### Numerics.NET User's Guide

- [Vector Basics](https://numerics.netvector-and-matrix/vectors/vector-basics)
- [Operations on Vectors](https://numerics.netvector-and-matrix/vectors/operations-on-vectors)
- [Basic Concepts](https://numerics.netvector-and-matrix/basic-concepts)

### Numerics.NET API Reference

- [Vector class](https://numerics.net/documentation/latest/reference/numerics.net.vector)
- [Vector&lt;T&gt; class](https://numerics.net/documentation/latest/reference/numerics.net.vector-1)
- [Numerics.NET.LinearAlgebra namespace](https://numerics.net/documentation/latest/reference/numerics.net.linearalgebra)


## Troubleshooting

### Common Issues

- **Missing dependencies**: Make sure to run `dotnet restore` before building
- **License verification failed**: Ensure you have a valid trial key from [https://numerics.net/trial-key](https://numerics.net/trial-key)
- **Runtime errors**: Check that you're targeting the correct .NET version (.NET 6 or .NET 8)

### Need Help?

- Check the [Numerics.NET documentation](https://numerics.net/documentation/)
- Contact support at [support@numerics.net](mailto:support@numerics.net?subject=VectorOperations%20QuickStart%20Sample%20%28Visual+Basic%29)

## About Numerics.NET

Numerics.NET is a powerful numerical library for .NET that provides advanced mathematical 
functions and algorithms for scientific computing, data analysis, and machine learning.
See [numerics.net](https://numerics.net) for details.

---

_Last updated on 2025-05-06 3:03:10 PM (version 9.1.3)._
