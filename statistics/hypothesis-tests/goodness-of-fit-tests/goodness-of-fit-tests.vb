'=====================================================================
'
'  File: goodness-of-fit-tests.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

Imports System.Data
Imports Numerics.NET.Statistics
Imports Numerics.NET.Statistics.Tests
Imports Numerics.NET.Statistics.Distributions
Imports Numerics.NET

' Illustrates the Chi Square, Kolmogorov-Smirnov and Anderson-Darling
' tests for goodness-of-fit.
Module GoodnessOfFitTests

    Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' This QuickStart Sample illustrates the wide variety of goodness-of-fit
        ' tests available.

        '
        ' Chi-square Test
        '

        Console.WriteLine("Chi-square test.")

        ' The Chi-square test is the simplest of the goodness-of-fit tests.
        ' The results follow a binomial distribution with 3 trials (rolls of the dice):
        Dim sixesDistribution As BinomialDistribution = New BinomialDistribution(3, 1 / 6.0)

        ' First, create a histogram with the expected results.
        Dim expected = sixesDistribution.GetExpectedHistogram(100)

        ' And a histogram with the actual results
        Dim actual = Vector.Create(New Double() {51, 35, 12, 2})
        Dim chiSquare As New ChiSquareGoodnessOfFitTest(actual, expected)

        ' We can obtan the value of the test statistic through the Statistic property,
        ' and the corresponding P-value through the Probability property:
        Console.WriteLine($"Test statistic: {chiSquare.Statistic:F4}")
        Console.WriteLine($"P-value:        {chiSquare.PValue:F4}")

        ' We can now print the test results:
        Console.WriteLine("Reject null hypothesis? {0}",
                If(chiSquare.Reject(), "yes", "no"))

        '
        ' One-sample Kolmogorov-Smirnov Test
        '

        Console.WriteLine(Environment.NewLine + "One-sample Kolmogorov-Smirnov Test")

        ' We will investigate a sample of 25 random numbers from a lognormal distribution
        ' and investigate how well it matches a similar looking Weibull distribution.

        ' We first create the two distributions:
        Dim logNormal As LognormalDistribution = New LognormalDistribution(-0.5, 0.8)
        Dim weibull As New WeibullDistribution(2, 1)

        ' Then we generate the samples from the lognormal distribution:
        Dim logNormalSample = logNormal.Sample(25)

        ' Finally, we construct the Kolmogorov-Smirnov test:
        Dim ksTest As New OneSampleKolmogorovSmirnovTest(logNormalSample, weibull)

        ' We can obtan the value of the test statistic through the Statistic property,
        ' and the corresponding P-value through the Probability property:
        Console.WriteLine($"Test statistic: {ksTest.Statistic:F4}")
        Console.WriteLine($"P-value:        {ksTest.PValue:F4}")

        ' We can now print the test results:
        Console.WriteLine("Reject null hypothesis? {0}",
                If(ksTest.Reject(), "yes", "no"))

        '
        ' Two-sample Kolmogorov-Smirnov Test
        '

        Console.WriteLine(Environment.NewLine + "Two-sample Kolmogorov-Smirnov Test")

        ' We once again investigate the similarity between a lognormal and
        ' a Weibull distribution. However, this time, we use 25 random
        ' samples from each distribution.

        ' We already have the lognormal samples.
        ' Generate the samples from the Weibull distribution:
        Dim weibullSample = weibull.Sample(25)

        ' Finally, we construct the Kolmogorov-Smirnov test:
        Dim ksTest2 As TwoSampleKolmogorovSmirnovTest =
                New TwoSampleKolmogorovSmirnovTest(logNormalSample, weibullSample)

        ' We can obtan the value of the test statistic through the Statistic property,
        ' and the corresponding P-value through the Probability property:
        Console.WriteLine($"Test statistic: {ksTest2.Statistic:F4}")
        Console.WriteLine($"P-value:        {ksTest2.PValue:F4}")

        ' We can now print the test results:
        Console.WriteLine("Reject null hypothesis? {0}",
                If(ksTest2.Reject(), "yes", "no"))

        '
        ' Anderson-Darling Test
        '

        Console.WriteLine(Environment.NewLine + "Anderson-Darling Test")

        ' The Anderson-Darling is defined for a small number of
        ' distributions. Currently, only the normal distribution
        ' is supported.

        ' We will investigate the distribution of the strength
        ' of polished airplane windows. The data comes from
        ' Fuller, e.al. (NIST, 1993) and represents the pressure
        ' (in psi).

        ' First, create a numerical variable:
        Dim strength = Vector.Create(New Double() _
                {18.83, 20.8, 21.657, 23.03, 23.23, 24.05,
                    24.321, 25.5, 25.52, 25.8, 26.69, 26.77,
                    26.78, 27.05, 27.67, 29.9, 31.11, 33.2,
                    33.73, 33.76, 33.89, 34.76, 35.75, 35.91,
                    36.98, 37.08, 37.09, 39.58, 44.045, 45.29,
                    45.381})

        ' Let's print some summary statistics:
        Console.WriteLine($"Number of observations: {strength.Length}")
        Console.WriteLine($"Mean:                   {strength.Mean:F3}")
        Console.WriteLine($"Standard deviation:     {strength.StandardDeviation:F3}")

        ' The most refined test of normality is the Anderson-Darling test.
        Dim adTest As AndersonDarlingTest =
                New AndersonDarlingTest(strength, 30.81, 7.38)

        ' We can obtan the value of the test statistic through the Statistic property,
        ' and the corresponding P-value through the Probability property:
        Console.WriteLine($"Test statistic: {adTest.Statistic:F4}")
        Console.WriteLine($"P-value:        {adTest.PValue:F4}")

        ' We can now print the test results:
        Console.WriteLine("Reject null hypothesis? {0}",
                If(adTest.Reject(), "yes", "no"))

    End Sub

End Module
