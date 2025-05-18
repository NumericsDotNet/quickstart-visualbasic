'=====================================================================
'
'  File: non-parametric-tests.vb
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

' Demonstrates how to use non-parametric hypothesis tests
' like the Mann-Whitney (Wilcoxon) rank sum test and the
' Kruskal-Wallis test.
Module MeanTests

    Sub Main()

        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        '
        ' Mann-Whitney test
        '

        Console.WriteLine("Mann-Whitney Test")
        Console.WriteLine()

        ' The Mann-Whitney test compares to samples to see if they were
        ' drawn from the same distribution.

        ' We use an example from McDonald, et.al. (1996), who compared
        ' the geographic variation in oyster DNA to the variation in
        ' proteins. A significant difference in the samples would suggest
        ' that natural selection played a role in the oyster diversification.

        ' There are two ways to create a test with multiple samples.

        ' The first is to put all the data in one variable,
        ' and use a second variable to group the data in the first.
        Console.WriteLine("Using grouping variable:")

        Dim values = Vector.Create(
                -0.005, 0.116, -0.006, 0.095, 0.053, 0.003,
                -0.005, 0.016, 0.041, 0.016, 0.066,
                 0.163, 0.004, 0.049, 0.006, 0.058,
                -0.002, 0.015, 0.044, 0.024)
        Dim groups = Vector.CreateCategorical({
                Group.Dna, Group.Dna, Group.Dna, Group.Dna, Group.Dna, Group.Dna,
                Group.Protein, Group.Protein, Group.Protein, Group.Protein, Group.Protein,
                Group.Protein, Group.Protein, Group.Protein, Group.Protein, Group.Protein,
                Group.Protein, Group.Protein, Group.Protein, Group.Protein})

        ' With this data, we can create the test:
        Dim mw As New MannWhitneyTest(Of Double)(values, groups)

        ' We can obtan the value of the test statistic through the Statistic property,
        ' and the corresponding P-value through the PValue property:
        Console.WriteLine($"Test statistic: {mw.Statistic:F4}")
        Console.WriteLine($"P-value:        {mw.PValue:F4}")

        ' The significance level is the default value of 0.05:
        Console.WriteLine($"Significance level:     {mw.SignificanceLevel:F2}")
        ' We can now print the test scores:
        Console.WriteLine($"Reject null hypothesis? {If(mw.Reject(), "yes", "no")}")

        ' We can get the same scores for the 0.01 significance level by explicitly
        ' passing the significance level as a parameter to these methods:
        Console.WriteLine($"Significance level:     {0.01:F2}")
        Console.WriteLine($"Reject null hypothesis? {If(mw.Reject(), "yes", "no")}")
        Console.WriteLine()

        ' The second method is to put the data in different variables
        Console.WriteLine("Using multiple variables:")

        Dim dnaValues = Vector.Create(-0.005, 0.116, -0.006, 0.095, 0.053, 0.003)
        Dim proteinValues = Vector.Create(
                -0.005, 0.016, 0.041, 0.016, 0.066,
                 0.163, 0.004, 0.049, 0.006, 0.058,
                -0.002, 0.015, 0.044, 0.024)

        ' With this data, we can create the test:
        mw = New MannWhitneyTest(Of Double)(dnaValues, proteinValues)

        ' We can obtan the value of the test statistic through the Statistic property,
        ' and the corresponding P-value through the PValue property:
        Console.WriteLine($"Test statistic: {mw.Statistic:F4}")
        Console.WriteLine($"P-value:        {mw.PValue:F4}")

        ' The significance level is the default value of 0.05:
        Console.WriteLine($"Significance level:     {mw.SignificanceLevel:F2}")
        ' We can now print the test scores:
        Console.WriteLine($"Reject null hypothesis? {If(mw.Reject(), "yes", "no")}")

        '
        ' Kruskal-Wallis test
        '

        Console.WriteLine()
        Console.WriteLine("Kruskal-Wallis Test")
        Console.WriteLine()

        ' The Kruskal-Wallis test is a generalization of the Mann-Whitney test
        ' to more than 2 groups.

        ' The following example was taken from the NIST Engineering Statistics Handbook
        ' at http:'www.itl.nist.gov/div898/handbook/prc/section4/prc41.htm

        ' The data represents percentage quarterly growth
        ' in 4 investment funds:
        Dim aValues = Vector.Create(4.2, 4.6, 3.9, 4.0)
        Dim bValues = Vector.Create(3.3, 2.4, 2.6, 3.8, 2.8)
        Dim cValues = Vector.Create(1.9, 2.4, 2.1, 2.7, 1.8)
        Dim dValues = Vector.Create(3.5, 3.1, 3.7, 4.1, 4.4)

        ' We simply pass these variables to the constructor:
        Dim kw As New KruskalWallisTest(aValues, bValues, cValues, dValues)

        ' We can obtan the value of the test statistic through the Statistic property,
        ' and the corresponding P-value through the PValue property:
        Console.WriteLine($"Test statistic: {kw.Statistic:F4}")
        Console.WriteLine($"P-value:        {kw.PValue:F4}")

        ' The significance level is the default value of 0.05:
        Console.WriteLine($"Significance level:     {kw.SignificanceLevel:F2}")
        ' We can now print the test scores:
        Console.WriteLine($"Reject null hypothesis? {If(kw.Reject(), "yes", "no")}")

        '
        ' Runs test
        '

        Console.WriteLine()
        Console.WriteLine("Runs Test")
        Console.WriteLine()

        ' The runs test is a test of randomness.

        ' It compares the lengths of runs of the same value
        ' in a sample to what would be expected.

        ' In numerical data, it uses the runs of successively
        ' increasing or decreasing values

        Dim genders = Vector.Create(
                Gender.Male, Gender.Male, Gender.Male, Gender.Female, Gender.Female,
                Gender.Female, Gender.Male, Gender.Male, Gender.Male, Gender.Male,
                Gender.Female, Gender.Female, Gender.Male, Gender.Male, Gender.Male,
                Gender.Female, Gender.Female, Gender.Female, Gender.Female, Gender.Female,
                Gender.Female, Gender.Female, Gender.Male, Gender.Male, Gender.Female,
                Gender.Male, Gender.Male, Gender.Female, Gender.Female, Gender.Female,
                Gender.Female).AsCategorical()

        Dim rt As New RunsTest(Of Gender)(genders)

        ' We can obtan the value of the test statistic through the Statistic property,
        ' and the corresponding P-value through the PValue property:
        Console.WriteLine($"Test statistic: {rt.Statistic:F4}")
        Console.WriteLine($"P-value:        {rt.PValue:F4}")

        ' The significance level is the default value of 0.05:
        Console.WriteLine($"Significance level:     {rt.SignificanceLevel:F2}")
        ' We can now print the test scores:
        Console.WriteLine($"Reject null hypothesis? {If(rt.Reject(), "yes", "no")}")

    End Sub

    Enum Group
        Dna
        Protein
    End Enum

    Enum Gender
        Male
        Female
    End Enum

End Module
