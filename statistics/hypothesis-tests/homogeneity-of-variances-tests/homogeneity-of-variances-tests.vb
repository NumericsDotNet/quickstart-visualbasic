'=====================================================================
'
'  File: homogeneity-of-variances-tests.vb
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

' Illustrates hypothesis tests for the homogeneity of variances
' using  classes from the Numerics.NET.Statistics.Tests namespace.
Module HomogeneityOfVariancesTests

    Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' One of the underlying assumptions of Analysis of Variance
        ' (ANOVA) is that the variances in the different groups are
        ' identical. This QuickStart Sample shows how to use
        ' the two tests are available that can verify this assumption.

        ' The data for this QuickStart Sample is measurements of
        ' the diameters of gears from 10 different batches.
        ' Two variables are provided:

        ' batchVariable contains the batch number of each measurement:
        Dim batch = Vector.Create(New Integer() _
            {
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
                3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
                4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
                5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
                6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
                7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
                8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
                9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
                10, 10, 10, 10, 10, 10, 10, 10, 10, 10
            }).AsCategorical()

        ' diameterVariable contains the actual measurements:
        Dim diameter = Vector.Create(New Double() _
            {
                1.006, 0.996, 0.998, 1.0, 0.992, 0.993, 1.002, 0.999, 0.994, 1.0,
                0.998, 1.006, 1.0, 1.002, 0.997, 0.998, 0.996, 1.0, 1.006, 0.988,
                0.991, 0.987, 0.997, 0.999, 0.995, 0.994, 1.0, 0.999, 0.996, 0.996,
                1.005, 1.002, 0.994, 1.0, 0.995, 0.994, 0.998, 0.996, 1.002, 0.996,
                0.998, 0.998, 0.982, 0.99, 1.002, 0.984, 0.996, 0.993, 0.98, 0.996,
                1.009, 1.013, 1.009, 0.997, 0.988, 1.002, 0.995, 0.998, 0.981, 0.996,
                0.99, 1.004, 0.996, 1.001, 0.998, 1.0, 1.018, 1.01, 0.996, 1.002,
                0.998, 1.0, 1.006, 1.0, 1.002, 0.996, 0.998, 0.996, 1.002, 1.006,
                1.002, 0.998, 0.996, 0.995, 0.996, 1.004, 1.004, 0.998, 0.999, 0.991,
                0.991, 0.995, 0.984, 0.994, 0.997, 0.997, 0.991, 0.998, 1.004, 0.997
            })

        ' To prepare the data, we create a vector of vectors,
        ' one for each batch. This is optional. (See below.)
        Dim variables = diameter.SplitBy(batch)

        '
        ' Bartlett's test
        '

        ' Bartlett's test is relatively fast, but has the drawback that
        ' it requires the data in the groups to be normally distributed,
        ' and it is not very robust against departures from normality.
        ' What this means in practice is that the test can't distinguish
        ' between rejection because of non-homogeneity of variances
        ' and violation of the normality assumption.

        Console.WriteLine("Bartlett's test.")

        ' We pass the array of variables to the constructor:
        Dim bartlett As New BartlettTest(variables)
        ' We could have also written
        Dim bartlett2 = New BartlettTest(diameter, batch)

        ' We can obtain the value of the test statistic through the Statistic property,
        ' and the corresponding P-value through the Probability property:
        Console.WriteLine($"Test statistic: {bartlett.Statistic:F4}")
        Console.WriteLine($"P-value:        {bartlett.PValue:F4}")

        Console.WriteLine("Critical value: {0:F4} at 90%",
                bartlett.GetUpperCriticalValue(0.1))
        Console.WriteLine("Critical value: {0:F4} at 95%",
                bartlett.GetUpperCriticalValue(0.05))
        Console.WriteLine("Critical value: {0:F4} at 99%",
                bartlett.GetUpperCriticalValue(0.01))

        ' We can now print the test results:
        Console.WriteLine("Reject null hypothesis? {0}",
                If(bartlett.Reject(), "yes", "no"))

        '
        ' Levene's Test
        '

        ' Levene's test is slower than Bartlett's test, but is generally more reliable.
        ' It comes in three variants, depending on the measure of location used.
        ' The default is that the group median is used.

        Console.WriteLine()
        Console.WriteLine("Levene's Test")

        ' Once again, we pass an array of Variable objects to the constructor.
        ' The LeveneTest constructor is overloaded: you can specify
        ' the type of mean (mean, median, or trimmed mean):
        Dim levene As LeveneTest = New LeveneTest(diameter, batch, LocationMeasure.Median)

        ' We can obtain the value of the test statistic through the Statistic property,
        ' and the corresponding P-value through the Probability property:
        Console.WriteLine($"Test statistic: {levene.Statistic:F4}")
        Console.WriteLine($"P-value:        {levene.PValue:F4}")

        ' We can obtain critical values for various significance levels:
        Console.WriteLine("Critical value: {0:F4} at 90%",
                levene.GetUpperCriticalValue(0.1))
        Console.WriteLine("Critical value: {0:F4} at 95%",
                levene.GetUpperCriticalValue(0.05))
        Console.WriteLine("Critical value: {0:F4} at 99%",
                levene.GetUpperCriticalValue(0.01))

        ' We can now print the test results:
        Console.WriteLine("Reject null hypothesis? {0}",
                If(levene.Reject(), "yes", "no"))

    End Sub

End Module
