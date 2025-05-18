'=====================================================================
'
'  File: simple-regression.vb
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
Imports Numerics.NET
Imports Numerics.NET.Statistics

    ' Illustrates the use of the SimpleRegressionModel class for performing
    ' simple linear regression..
    Module SimpleRegression

        Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' Simple linear regression can be performed using
        ' the SimpleRegressionModel class. There are some special constructors
        ' for simple linear regression, with only one independent variable.
        '
        ' This QuickStart sample uses data from the National Institute
        ' for Standards and Technology's Statistical Reference Datasets
        ' library at http:'www.itl.nist.gov/div898/strd/.

        ' Note that, due to round-off error, the results here will not be exactly
        ' the same as the NIST results, which were calculated using 500 digits
        ' of precision!

        ' Model 1 uses the 'NoInt1' dataset. The model has no intercept.

        ' First, we construct Double arrays containing the data for
        ' the dependent and independent variables.
        Dim yData1 As Double() = {130, 131, 132, 133, 134, 135, 136, 137, 138, 139, 140}
        Dim xData1 As Double() = {60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70}

        ' Next, we create the regression model. We can pass the data arrays directly.
        Dim model1 As New SimpleRegressionModel(yData1, xData1)
        model1.NoIntercept = True
        model1.Fit()

        For Each param As Parameter(Of Double) In model1.Parameters
            Console.WriteLine(param.ToString())
        Next
        Console.WriteLine($"Residual standard error: {model1.StandardError:F2}")
        Console.WriteLine($"R-Squared: {model1.RSquared:F3}")
        Console.WriteLine($"Adjusted R-Squared: {model1.AdjustedRSquared:F3}")
        Console.WriteLine($"F-statistic: {model1.FStatistic:F3}")

        Console.WriteLine(model1.AnovaTable.ToString())

        ' Model 2 uses the 'Norris' dataset.

        Console.WriteLine(Environment.NewLine + "" + Environment.NewLine + "Model 2")
        Dim dependent2 = Vector.Create(0.1, 338.8, 118.1, 888.0, 9.2, 228.1, 668.5, 998.5,
                   449.1, 778.9, 559.2, 0.3, 0.1, 778.1, 668.8, 339.3,
                   448.9, 10.8, 557.7, 228.3, 998.0, 888.8, 119.6, 0.3,
                   0.6, 557.6, 339.3, 888.0, 998.5, 778.9, 10.2, 117.6,
                   228.9, 668.4, 449.2, 0.2)
        Dim independent2 = Vector.Create(0.2, 337.4, 118.2, 884.6, 10.1, 226.5, 666.3, 996.3,
                   448.6, 777.0, 558.2, 0.4, 0.6, 775.5, 666.9, 338.0,
                   447.5, 11.6, 556.0, 228.1, 995.8, 887.6, 120.2, 0.3,
                   0.3, 556.8, 339.1, 887.2, 999.0, 779.0, 11.1, 118.3,
                   229.2, 669.1, 448.9, 0.5)

        ' Next, we create the regression model, using the NumericalVariable objects
        ' we just created:
        Dim model2 As New SimpleRegressionModel(dependent2, independent2)
        model2.Fit()

        For Each param As Parameter(Of Double) In model2.Parameters
            Console.WriteLine(param.ToString())
        Next
        Console.WriteLine($"Residual standard error: {model2.StandardError:F8}")
        Console.WriteLine($"R-Squared: {model2.RSquared:F8}")
        Console.WriteLine($"Adjusted R-Squared: {model2.AdjustedRSquared:F8}")
        Console.WriteLine($"F-statistic: {model2.FStatistic:F3}")

        Console.WriteLine(model2.AnovaTable.ToString())

        ' The data can also be supplied as two Vector objects.
        ' This is not illustrated here.

    End Sub

End Module
