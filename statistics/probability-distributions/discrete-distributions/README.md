# Discrete Distributions

This sample illustrates how to use the classes that represent discrete probability distributions in the Numerics.NET.Statistics.Distributions namespace using Numerics.NET.

## Overview

This QuickStart sample demonstrates how to work with discrete probability distributions in Numerics.NET, 
using the binomial distribution as a primary example.

The sample shows:
- How to construct discrete probability distributions with specified parameters
- How to access basic statistical properties like mean, variance, standard deviation, skewness and kurtosis
- How to calculate probability values using the probability density function (PDF) and cumulative 
  distribution function (CDF)
- How to generate random variates from the distribution using various sampling methods
- How to generate expected histograms for visualizing the distribution

The code uses the binomial distribution to illustrate these concepts, but the same principles apply to all
discrete distributions in Numerics.NET, including Bernoulli, geometric, hypergeometric, Poisson and other
distributions. The sample provides practical examples of common probability calculations and demonstrates
proper usage of the distribution classes.


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
[C#](https://github.com/NumericsDotNet/quickstart-csharp/tree/net8.0/statistics/probability-distributions/discrete-distributions), [F#](https://github.com/NumericsDotNet/quickstart-fsharp/tree/net8.0/statistics/probability-distributions/discrete-distributions), [IronPython](https://github.com/NumericsDotNet/quickstart-ironpython/tree/net8.0/statistics/probability-distributions/discrete-distributions).

You can find out more about the methods used in this sample from the Numerics.NET documentation.

### Numerics.NET User's Guide

- [Discrete Probability Distributions](https://numerics.netstatistics/discrete-distributions/discrete-probability-distributions)
- [Binomial Distribution](https://numerics.netstatistics/discrete-distributions/binomial-distribution)
- [Random Number Generators](https://numerics.netstatistics/random-numbers/random-number-generators)

### Numerics.NET API Reference

- [Distributions.DiscreteDistribution class](https://numerics.net/documentation/latest/reference/numerics.net.statistics.distributions.discretedistribution)
- [Distributions.BinomialDistribution class](https://numerics.net/documentation/latest/reference/numerics.net.statistics.distributions.binomialdistribution)
- [Distributions.PoissonDistribution class](https://numerics.net/documentation/latest/reference/numerics.net.statistics.distributions.poissondistribution)
- [Histogram class](https://numerics.net/documentation/latest/reference/numerics.net.dataanalysis.histogram)
- [Numerics.NET.Statistics.Distributions namespace](https://numerics.net/documentation/latest/reference/numerics.net.statistics.distributions)


## Troubleshooting

### Common Issues

- **Missing dependencies**: Make sure to run `dotnet restore` before building
- **License verification failed**: Ensure you have a valid trial key from [https://numerics.net/trial-key](https://numerics.net/trial-key)
- **Runtime errors**: Check that you're targeting the correct .NET version (.NET 6 or .NET 8)

### Need Help?

- Check the [Numerics.NET documentation](https://numerics.net/documentation/)
- Contact support at [support@numerics.net](mailto:support@numerics.net?subject=DiscreteDistributions%20QuickStart%20Sample%20%28Visual+Basic%29)

## About Numerics.NET

Numerics.NET is a powerful numerical library for .NET that provides advanced mathematical 
functions and algorithms for scientific computing, data analysis, and machine learning.
See [numerics.net](https://numerics.net) for details.

---

_Last updated on 2025-05-06 3:03:10 PM (version 9.1.3)._
