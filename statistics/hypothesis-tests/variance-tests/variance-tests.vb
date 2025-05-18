'=====================================================================
'
'  File: variance-tests.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

Imports Numerics.NET
Imports Numerics.NET.Statistics
Imports Numerics.NET.Statistics.Tests

    ' Demonstrates how to use hypothesis tests for the variance
    ' of one or two distributions.
    Module VarianceTests

        Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' This QuickStart Sample uses the scores obtained by the students
        ' in two groups of students on a national test.
        '
        ' We want to know if the variance of the scores is greater than
        ' a specific value. We use the one sample Chi-square test for this
        ' purpose.

        Console.WriteLine("Tests for class 1")

        ' First we create a NumericalVariable that holds the test results.
        Dim group1Results = Vector.Create(Of Double)(
                62, 77, 61, 94, 75, 82, 86, 83, 64, 84,
                68, 82, 72, 71, 85, 66, 61, 79, 81, 73)

        ' We can get the mean and standard deviation of the class right away:
        Console.WriteLine($"Mean for the class: {group1Results.Mean:F1}")
        Console.WriteLine($"Standard deviation: {group1Results.StandardDeviation:F1}")

        '
        ' One Sample Chi-square Test
        '

        Console.WriteLine(Environment.NewLine + "Using chi-square test:")

        ' We want to know if the variance is larger than 5.
        ' Therefore, we use a one-tailed chi-square test:
        Dim chiSquareTest As OneSampleChiSquareTest =
                New OneSampleChiSquareTest(group1Results, 5, HypothesisType.OneTailedUpper)

        ' We can obtan the value of the test statistic through the Statistic property,
        ' and the corresponding P-value through the Probability property:
        Console.WriteLine($"Test statistic: {chiSquareTest.Statistic:F4}")
        Console.WriteLine($"P-value:        {chiSquareTest.PValue:F4}")

        ' The significance level is the default value of 0.05:
        Console.WriteLine("Significance level:     {0:F2}",
                chiSquareTest.SignificanceLevel)
        ' We can now print the test results:
        Console.WriteLine("Reject null hypothesis? {0}",
                If(chiSquareTest.Reject(), "yes", "no"))
        ' We can get a confidence interval for the current significance level:
        Dim varianceInterval As Interval = chiSquareTest.GetConfidenceInterval()
        Console.WriteLine("95% Confidence interval for the variance: {0:F1} - {1:F1}",
                varianceInterval.LowerBound, varianceInterval.UpperBound)

        ' We can get the same results for the 0.01 significance level by explicitly
        ' passing the significance level as a parameter to these methods:
        Console.WriteLine($"Significance level:     {0.01:F2}")
        Console.WriteLine("Reject null hypothesis? {0}",
                If(chiSquareTest.Reject(0.01), "yes", "no"))

        ' The GetConfidenceInterval method needs the confidence level, which equals
        ' 1 - the significance level:
        varianceInterval = chiSquareTest.GetConfidenceInterval(0.99)
        Console.WriteLine("99% Confidence interval for the variance: {0:F1} - {1:F1}",
                varianceInterval.LowerBound, varianceInterval.UpperBound)

        '
        ' Two sample F-test
        '

        Console.WriteLine(Environment.NewLine + "Using F-test:")
        ' We want to compare the scores of the first group to the scores
        ' of a second group from another school. We want to verify that the
        ' variances of the scores from the two schools are equal. Once again,
        ' we start by creating a NumericalVariable, this time containing
        ' the scores for the second group:
        Dim group2Results = Vector.Create(Of Double)(
                61, 80, 98, 90, 94, 65, 79, 75, 74, 86,
                76, 85, 78, 72, 76, 79, 65, 92, 76, 80)

        ' To compare the variances of the two groups, we need the two sample
        ' F test, implemented by the FTest class:
        Dim fTest As FTest = New FTest(group1Results, group2Results)
        ' We can obtan the value of the test statistic through the Statistic property,
        ' and the corresponding P-value through the Probability property:
        Console.WriteLine($"Test statistic: {fTest.Statistic:F4}")
        Console.WriteLine($"P-value:        {fTest.PValue:F4}")

        ' The significance level is the default value of 0.05:
        Console.WriteLine("Significance level:     {0:F2}",
                fTest.SignificanceLevel)
        ' We can now print the test results:
        Console.WriteLine("Reject null hypothesis? {0}",
                If(fTest.Reject(), "yes", "no"))


    End Sub

End Module
