'=====================================================================
'
'  File: polynomial-regression.vb
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

    ' Illustrates the use of the PolynomialRegressionModel class
    ' to perform polynomial regression.
    Module PolynomialRegression

        Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' Polynomial regression can be performed using
        ' the PolynomialRegressionModel class.
        '
        ' This QuickStart sample uses data from the National Institute
        ' for Standards and Technology's Statistical Reference Datasets
        ' library at http:'www.itl.nist.gov/div898/strd/.

        ' Note that, due to round-off error, the results here will not be exactly
        ' the same as the NIST results, which were calculated using 500 digits
        ' of precision!

        ' We use the 'Pontius' dataset, which contains measurement data
        ' from the calibration of load cells. The independent variable is the load.
        ' The dependent variable is the deflection.
        Dim deflection = Vector.Create(New Double() _
            {
                0.11019, 0.21956, 0.32949, 0.43899, 0.54803, 0.65694, 0.76562,
                0.87487, 0.98292, 1.09146, 1.20001, 1.30822, 1.41599, 1.52399,
                1.63194, 1.73947, 1.84646, 1.95392, 2.06128, 2.16844, 0.11052,
                0.22018, 0.32939, 0.43885999999999997, 0.54798, 0.65739, 0.76596, 0.87474, 0.983,
                1.0915, 1.20004, 1.30818, 1.41613, 1.52408, 1.63159, 1.73965,
                1.84696, 1.95445, 2.06177, 2.16829
            })
        Dim load = Vector.Create(New Double() _
            {
                150000.0, 300000, 450000, 600000, 750000, 900000,
                1050000, 1200000, 1350000, 1500000, 1650000, 1800000,
                1950000, 2100000, 2250000, 2400000, 2550000, 2700000,
                2850000, 3000000, 150000, 300000, 450000, 600000,
                750000, 900000, 1050000, 1200000, 1350000, 1500000,
                1650000, 1800000, 1950000, 2100000, 2250000, 2400000,
                2550000, 2700000, 2850000, 3000000
            })

        ' Now create the regression model. We supply the dependent and independent
        ' variable, and the degree of the polynomial:
        Dim model = New PolynomialRegressionModel(deflection, load, 2)

        ' The Fit method performs the actual regression analysis.
        model.Fit()

        ' The Parameters collection contains information about the regression
        ' parameters.
        Console.WriteLine("Variable                  Value    Std.Error  t-stat  p-Value")
        For Each param As Parameter(Of Double) In model.Parameters
            ' Parameter objects have the following properties:
            ' - Name, usually the name of the variable:
            ' - Estimated value of the parameter:
            ' - Standard error:
            ' - The value of the t statistic for the hypothesis that the parameter
            '    is zero.
            ' - Probability corresponding to the t statistic.
            Console.WriteLine("{0,-19}{1,12:E4}{2,12:E2}{3,8:F2} {4,7:F4}",
                    param.Name,
                    param.Value,
                    param.StandardError,
                    param.Statistic,
                    param.PValue)
        Next
        Console.WriteLine()

        ' In addition to these properties, Parameter objects have a GetConfidenceInterval
        ' method that returns a confidence interval at a specified confidence level.
        ' Notice that individual parameters can be accessed using their numeric index.
        ' Parameter 0 is the intercept, if it was included.
        Dim confidenceInterval As Interval = model.Parameters(0).GetConfidenceInterval(0.95)
        Console.WriteLine("95% confidence interval for constant term: {0:E4} - {1:E4}",
                confidenceInterval.LowerBound, confidenceInterval.UpperBound)
        Console.WriteLine()

        ' There is also a wealth of information about the analysis available
        ' through various properties of the LinearRegressionModel object:
        Console.WriteLine($"Residual standard error: {model.StandardError:E3}")
        Console.WriteLine($"R-Squared:               {model.RSquared:F4}")
        Console.WriteLine($"Adjusted R-Squared:      {model.AdjustedRSquared:F4}")
        Console.WriteLine($"F-statistic:             {model.FStatistic:F4}")
        Console.WriteLine($"Corresponding p-value:   {model.PValue:E5}")
        Console.WriteLine()

        ' Much of this data can be summarized in the form of an ANOVA table:
        Console.WriteLine(model.AnovaTable.ToString())

    End Sub

End Module
