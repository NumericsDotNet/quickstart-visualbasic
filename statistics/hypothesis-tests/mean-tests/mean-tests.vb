'=====================================================================
'
'  File: mean-tests.vb
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
Imports Numerics.NET
Imports Numerics.NET.Statistics
Imports Numerics.NET.Statistics.Tests

' Illustrates hypothesis tests for the mean of a sample using classes
' from the Numerics.NET.Statistics.Tests namespace.
Module MeanTests

    Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' This QuickStart Sample uses the scores obtained by the students
        ' in two groups of students on a national test.
        '
        ' We want to know if the scores for these two groups of students
        ' are significantly different from the national average, and
        ' from each other.

        ' The mean and standard deviation of the complete population:
        Const nationalMean As Double = 79.3
        Const nationalStandardDeviation As Double = 7.3

        Console.WriteLine("Tests for group 1")

        ' First we create a NumericalVariable that holds the test scores.
        Dim group1Results = Vector.Create(New Double() _
                {62, 77, 61, 94, 75, 82, 86, 83, 64, 84, 68, 82, 72, 71, 85, 66, 61, 79, 81, 73})

        ' We can get the mean and standard deviation of the group right away:
        Console.WriteLine($"Mean for the group: {group1Results.Mean:F1}")
        Console.WriteLine($"Standard deviation: {group1Results.StandardDeviation:F1}")

        '
        ' One Sample z-test
        '

        Console.WriteLine(Environment.NewLine + "Using z-test:")
        ' We know the population standard deviation, so we can use the z-test,
        ' implemented by the OneSampleZTest group. We pass the sample variable
        ' and the population parameters to the constructor.
        Dim zTest As OneSampleZTest =
                New OneSampleZTest(group1Results, nationalMean, nationalStandardDeviation)
        ' We can obtan the value of the test statistic through the Statistic property,
        ' and the corresponding P-value through the Probability property:
        Console.WriteLine($"Test statistic: {zTest.Statistic:F4}")
        Console.WriteLine($"P-value:        {zTest.PValue:F4}")

        ' The significance level is the default value of 0.05:
        Console.WriteLine($"Significance level:     {zTest.SignificanceLevel:F2}")
        ' We can now print the test scores:
        Console.WriteLine($"Reject null hypothesis? {If(zTest.Reject(), "yes", "no")}")
        ' We can get a confidence interval for the current significance level:
        Dim meanInterval As Interval = zTest.GetConfidenceInterval()
        Console.WriteLine("95% Confidence interval for the mean: {0:F1} - {1:F1}",
                meanInterval.LowerBound, meanInterval.UpperBound)

        ' We can get the same scores for the 0.01 significance level by explicitly
        ' passing the significance level as a parameter to these methods:
        Console.WriteLine($"Significance level:     {0.01:F2}")
        Console.WriteLine($"Reject null hypothesis? {If(zTest.Reject(0.01), "yes", "no")}")
        ' The GetConfidenceInterval method needs the confidence level, which equals
        ' 1 - the significance level:
        meanInterval = zTest.GetConfidenceInterval(0.99)
        Console.WriteLine("99% Confidence interval for the mean: {0:F1} - {1:F1}",
                meanInterval.LowerBound, meanInterval.UpperBound)

        '
        ' One sample t-test
        '

        Console.WriteLine(Environment.NewLine + "Using t-test:")
        ' Suppose we only know the mean of the national scores,
        ' not the standard deviation. In this case, a t-test is
        ' the appropriate test to use.
        Dim tTest As OneSampleTTest = New OneSampleTTest(group1Results, nationalMean)
        ' We can obtan the value of the test statistic through the Statistic property,
        ' and the corresponding P-value through the Probability property:
        Console.WriteLine($"Test statistic: {tTest.Statistic:F4}")
        Console.WriteLine($"P-value:        {tTest.PValue:F4}")

        ' The significance level is the default value of 0.05:
        Console.WriteLine($"Significance level:     {tTest.SignificanceLevel:F2}")
        ' We can now print the test scores:
        Console.WriteLine($"Reject null hypothesis? {If(tTest.Reject(), "yes", "no")}")
        ' We can get a confidence interval for the current significance level:
        meanInterval = tTest.GetConfidenceInterval()
        Console.WriteLine("95% Confidence interval for the mean: {0:F1} - {1:F1}",
                meanInterval.LowerBound, meanInterval.UpperBound)

        '
        ' Two sample t-test
        '

        Console.WriteLine(Environment.NewLine + "Using two-sample t-test:")
        ' We want to compare the scores of the first group to the scores
        ' of a second group from the same school. Once again, we start
        ' by creating a NumericalVariable containing the scores:
        Dim group2Results = Vector.Create(New Double() _
                {61, 80, 98, 90, 94, 65, 79, 75, 74, 86, 76, 85, 78, 72, 76, 79, 65, 92, 76, 80})

        ' To compare the means of the two groups, we need the two sample
        ' t test, implemented by the TwoSampleTTest group:
        Dim tTest2 As TwoSampleTTest = New TwoSampleTTest(group1Results, group2Results,
                SamplePairing.Paired, assumeEqualVariances:=False)
        ' We can obtan the value of the test statistic through the Statistic property,
        ' and the corresponding P-value through the Probability property:
        Console.WriteLine($"Test statistic: {tTest2.Statistic:F4}")
        Console.WriteLine($"P-value:        {tTest2.PValue:F4}")

        ' The significance level is the default value of 0.05:
        Console.WriteLine($"Significance level:     {tTest2.SignificanceLevel:F2}")
        ' We can now print the test scores:
        Console.WriteLine($"Reject null hypothesis? {If(tTest2.Reject(), "yes", "no")}")

    End Sub

End Module
