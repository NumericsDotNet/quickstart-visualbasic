'=====================================================================
'
'  File: continuous-distributions.vb
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
Imports Numerics.NET.Statistics
Imports Numerics.NET.Statistics.Distributions

' Demonstrates how to use classes that implement
' continuous probabililty distributions.
Module ContinuousDistributions

    Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' This QuickStart Sample demonstrates the capabilities of
        ' the classes that implement continuous probability distributions.
        ' These classes inherit from the ContinuousDistribution class.
        '
        ' For an illustration of classes that implement discrete probability
        ' distributions, see the DiscreteDistributions QuickStart Sample.
        '
        ' We illustrate the properties and methods of continuous distribution
        ' using a Weibull distribution. The same properties and methods
        ' apply to all other continuous distributions.

        '
        ' Constructing distributions
        '

        ' Most distributions have one or more parameters with different definitions.
        '
        ' The location parameter is always related to the mean of the distribution.
        ' When omitted, its default value is zero.
        '
        ' The scale parameter is always directly related to the standard deviation.
        ' A larger scale parameter means that the distribution is wider.
        ' When omitted, its default value is one.

        ' The Weibull distribution has three constructors. The most complete
        ' constructor takes a location, scale, and shape parameter.
        Dim weibull As New WeibullDistribution(3, 2, 3)

        '
        ' Basic statistics
        '

        ' The Mean property returns the mean of the distribution:
        Console.WriteLine($"Mean:                 {weibull.Mean:F5}")

        ' The Variance and StandardDeviation are also available:
        Console.WriteLine($"Variance:             {weibull.Variance:F5}")
        Console.WriteLine($"Standard deviation:   {weibull.StandardDeviation:F5}")
        ' The inter-quartile range is another measure of scale:
        Console.WriteLine($"Inter-quartile range: {weibull.InterQuartileRange:F5}")

        ' As are the skewness:
        Console.WriteLine($"Skewness:             {weibull.Skewness:F5}")

        ' The Kurtosis property returns the kurtosis supplement.
        ' The Kurtosis property for the normal distribution returns zero.
        Console.WriteLine($"Kurtosis:             {weibull.Kurtosis:F5}")
        Console.WriteLine()

        '
        ' Distribution functions
        '

        ' The (cumulative) distribution function (CDF) is implemented by the
        ' DistributionFunction method:
        Console.WriteLine($"CDF(4.5) =            {weibull.DistributionFunction(4.5):F5}")

        ' Its complement is the survivor function:
        Console.WriteLine($"SDF(4.5) =            {weibull.SurvivorDistributionFunction(4.5):F5}")

        ' While its inverse is given by the InverseDistributionFunction method:
        Console.WriteLine($"Inverse CDF(0.4) =    {weibull.InverseDistributionFunction(0.4):F5}")

        ' The probability density function (PDF) is also available:
        Console.WriteLine($"PDF(4.5) =            {weibull.ProbabilityDensityFunction(4.5):F5}")

        ' The Probability method returns the probability that a variate lies between two values:
        Console.WriteLine("Probability(4.5, 5.5) = {0:F5}", weibull.Probability(4.5, 5.5))
        Console.WriteLine()

        '
        ' Random variates
        '

        ' The Sample method returns a single random variate
        ' using the specified random number generator:
        Dim rng As New MersenneTwister
        Dim x As Double = weibull.Sample(rng)
        ' The Sample method fills an array or vector with
        ' random variates. It has several overloads:
        Dim xArray As Double() = New Double(100) {}
        ' 1. Fill all values:
        weibull.SampleInto(rng, xArray)
        ' 2. Fill only a range (start index and length are supplied)
        weibull.SampleInto(rng, xArray, 20, 50)
        ' The same two options are available with a DenseVector
        ' instead of a double array.

        ' The GetExpectedHistogram method returns a Histogram that contains the
        ' expected number of samples in each bin, given the total number of samples.
        ' The bins are specified by lower and upper bounds and number of bins:
        Dim h = weibull.GetExpectedHistogram(3.0, 10.0, 5, 100)
        Dim bins = CType(h.Index, IntervalIndex(Of Double))
        Console.WriteLine("Expected distribution of 100 samples:")
        For i = 0 To h.Length - 1
            Console.WriteLine("Between {0} and {1} -> {2}",
                    bins(i).LowerBound, bins(i).UpperBound, h(i))
        Next
        Console.WriteLine()

        ' or by supplying an array of boundaries:
        h = weibull.GetExpectedHistogram(New Double() {3.0, 5.2, 7.4, 9.6, 11.8}, 100)
        bins = CType(h.Index, IntervalIndex(Of Double))
        Console.WriteLine("Expected distribution of 100 samples:")
        For i = 0 To h.Length - 1
            Console.WriteLine("Between {0} and {1} -> {2}",
                    bins(i).LowerBound, bins(i).UpperBound, h(i))
        Next

    End Sub

End Module
