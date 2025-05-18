'=====================================================================
'
'  File: discrete-distributions.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

Imports Numerics.NET.DataAnalysis
Imports Numerics.NET.Random
Imports Numerics.NET.Statistics.Distributions

' Demonstrates how to use classes that implement
' discrete probabililty distributions.
Module DiscreteDistributions

    Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' This QuickStart Sample demonstrates the capabilities of
        ' the classes that implement discrete probability distributions.
        ' These classes inherit from the DiscreteDistribution class.
        '
        ' For an illustration of classes that implement discrete probability
        ' distributions, see the ContinuousDistributions QuickStart Sample.
        '
        ' We illustrate the properties and methods of discrete distribution
        ' using a binomial distribution. The same properties and methods
        ' apply to all other discrete distributions.

        '
        ' Constructing distributions
        '

        ' Many discrete probability distributions are related to Bernoulli trials,
        ' events with a certain probability, p, of success. The number of trials
        ' is often one of the distribution's parameters.

        ' The binomial distribution has two constructors. Here, we create a
        ' binomial distribution for 6 trials with a probability of success of 0.6:
        Dim binomial As New BinomialDistribution(6, 0.6)

        ' The distribution's parameters are available through the
        ' NumberOfTrials and ProbabilityOfSuccess properties:
        Console.WriteLine($"# of trials:          {binomial.NumberOfTrials:F5}")
        Console.WriteLine($"Prob. of success:     {binomial.ProbabilityOfSuccess:F5}")

        '
        ' Basic statistics
        '

        ' The Mean property returns the mean of the distribution:
        Console.WriteLine($"Mean:                 {binomial.Mean:F5}")

        ' The Variance and StandardDeviation are also available:
        Console.WriteLine($"Variance:             {binomial.Variance:F5}")
        Console.WriteLine($"Standard deviation:   {binomial.StandardDeviation:F5}")

        ' As are the skewness:
        Console.WriteLine($"Skewness:             {binomial.Skewness:F5}")

        ' The Kurtosis property returns the kurtosis supplement.
        ' The Kurtosis property for the normal distribution returns zero.
        Console.WriteLine($"Kurtosis:             {binomial.Kurtosis:F5}")
        Console.WriteLine()

        '
        ' Distribution functions
        '

        ' The (cumulative) distribution function (CDF) is implemented by the
        ' DistributionFunction method:
        Console.WriteLine($"CDF(4) =            {binomial.DistributionFunction(4):F5}")

        ' The probability density function (PDF) is available as the
        ' Probability method:
        Console.WriteLine($"PDF(4) =            {binomial.Probability(4):F5}")

        ' The Probability method has an overload that returns the probability
        ' that a variate lies between two values:
        Console.WriteLine("Probability(3, 5) = {0:F5}", binomial.Probability(3, 5))
        Console.WriteLine()

        '
        ' Random variates
        '

        ' The Sample method returns a single random variate
        ' using the specified random number generator:
        Dim rng As New MersenneTwister
        Dim x As Integer = binomial.Sample(rng)
        ' The Sample method fills an array or vector with
        ' random variates. It has several overloads:
        Dim xArray As Integer() = New Integer(100) {}
        ' 1. Fill all values:
        binomial.Sample(rng, xArray)
        ' 2. Fill only a range (start index and length are supplied)
        binomial.Sample(rng, xArray, 20, 50)

        ' The GetExpectedHistogram method returns a Histogram that contains the
        ' expected number of samples in each bin:
        Dim h = binomial.GetExpectedHistogram(100)
        Console.WriteLine("Expected distribution of 100 samples:")
        For i = 0 To h.Length - 1
            Console.WriteLine("{0} success(es) -> {1}", i, h(i))
        Next
        Console.WriteLine()

    End Sub

End Module
