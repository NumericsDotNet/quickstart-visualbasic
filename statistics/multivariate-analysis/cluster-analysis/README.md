# Cluster Analysis

This sample illustrates how to use the classes in the Numerics.NET.Statistics.Multivariate namespace to perform hierarchical clustering and K-means clustering using Numerics.NET.

## Overview

This QuickStart sample demonstrates how to perform cluster analysis using both hierarchical and K-means 
clustering methods in C#. It shows how to analyze multivariate data to discover natural groupings 
within the data.

The sample uses a real-world dataset of company financial metrics to demonstrate:

- How to perform hierarchical cluster analysis, including:
  - Standardizing variables using Z-scores
  - Selecting linkage methods and distance measures
  - Creating cluster partitions
  - Working with dendrograms
  - Accessing cluster memberships and ordering

- How to perform K-means clustering, including:
  - Initializing the model with a specified number of clusters
  - Standardizing variables
  - Accessing cluster assignments and distances
  - Computing cluster statistics
  - Working with cluster centers and inter-cluster distances
  
The sample includes detailed comments explaining each step and demonstrates best practices for working
with both clustering methods.


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
[C#](https://github.com/NumericsDotNet/quickstart-csharp/tree/net462/statistics/multivariate-analysis/cluster-analysis), [F#](https://github.com/NumericsDotNet/quickstart-fsharp/tree/net462/statistics/multivariate-analysis/cluster-analysis), [IronPython](https://github.com/NumericsDotNet/quickstart-ironpython/tree/net462/statistics/multivariate-analysis/cluster-analysis).

You can find out more about the methods used in this sample from the Numerics.NET documentation.

### Numerics.NET User's Guide

- [Hierarchical Cluster Analysis](https://numerics.net/documentation/latest/statistics/multivariate-analysis/hierarchical-cluster-analysis)
- [K-Means Cluster Analysis](https://numerics.net/documentation/latest/statistics/multivariate-analysis/k-means-cluster-analysis)
- [Stata Files](https://numerics.net/documentation/latest/data-access/stata-files)

### Numerics.NET API Reference

- [HierarchicalClusterAnalysis class](https://numerics.net/documentation/latest/reference/numerics.net.statistics.multivariate.hierarchicalclusteranalysis)
- [KMeansClusterAnalysis class](https://numerics.net/documentation/latest/reference/numerics.net.statistics.multivariate.kmeansclusteranalysis)
- [StataFile class](https://numerics.net/documentation/latest/reference/numerics.net.data.stata.statafile)
- [Numerics.NET.Statistics.Multivariate namespace](https://numerics.net/documentation/latest/reference/numerics.net.statistics.multivariate)


## Troubleshooting

### Common Issues

- **Missing dependencies**: Make sure to run `dotnet restore` before building
- **License verification failed**: Ensure you have a valid trial key from [https://numerics.net/trial-key](https://numerics.net/trial-key)
- **Runtime errors**: Check that you're targeting the correct .NET version (.NET 6 or .NET 8)

### Need Help?

- Check the [Numerics.NET documentation](https://numerics.net/documentation/)
- Contact support at [support@numerics.net](mailto:support@numerics.net?subject=ClusterAnalysis%20QuickStart%20Sample%20%28Visual+Basic%29)

## About Numerics.NET

Numerics.NET is a powerful numerical library for .NET that provides advanced mathematical 
functions and algorithms for scientific computing, data analysis, and machine learning.
See [numerics.net](https://numerics.net) for details.

---

_Last updated on 2025-05-06 3:03:10 PM (version 9.1.3)._
