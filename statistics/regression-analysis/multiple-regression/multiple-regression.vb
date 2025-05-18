'=====================================================================
'
'  File: multiple-regression.vb
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
Imports Numerics.NET.Statistics
Imports Numerics.NET.Data.Text

' Illustrates the use of the LinearRegressionModel class
' to perform multiple linear regression.
Module MultipleRegression

    Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' Multiple linear regression can be performed using
        ' the LinearRegressionModel class.
        '
        ' This QuickStart sample uses data test scores of 200 high school
        ' students, including science, math, and reading.

        ' First, read the data from a file into a data frame.
        Dim frame = DelimitedTextFile.ReadDataFrame("..\..\..\..\..\Data\hsb2.csv")

        ' Now create the regression model. Parameters are the data frame,
        ' the name of the dependent variable, and a string array containing
        ' the names of the independent variables.
        Dim lm As New LinearRegressionModel(frame,
                "science", {"math", "female", "socst", "read"})

        ' Alternatively, we can use a formula to describe the variables
        ' in the model. The dependent variable goes on the left, the
        ' independent variables on the right of the ~
        Dim lm2 = New LinearRegressionModel(frame,
                "science ~ math + female + socst + read")

        ' We can set model options now, such as whether to exclude
        ' the constant term:
        ' model.NoIntercept = false

        ' The Fit method performs the actual regression analysis.
        lm.Fit()

        ' The Parameters collection contains information about the regression
        ' parameters.
        Console.WriteLine("Variable              Value    Std.Error  t-stat  p-Value")
        For Each param As Parameter(Of Double) In lm.Parameters
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

        ' In addition to these properties, Parameter objects have
        ' a GetConfidenceInterval method that returns
        ' a confidence interval at a specified confidence level.
        ' Notice that individual parameters can be accessed
        ' using their numeric index. Parameter 0 is the intercept,
        ' if it was included.
        Dim confidenceInterval = lm.Parameters(0).GetConfidenceInterval(0.95)
        Console.WriteLine("95% confidence interval for intercept: {0:F4} - {1:F4}",
                confidenceInterval.LowerBound, confidenceInterval.UpperBound)

        ' Parameters can also be accessed by name:
        confidenceInterval = lm.Parameters.Get("math").GetConfidenceInterval(0.95)
        Console.WriteLine("95% confidence interval for 'math': {0:F4} - {1:F4}",
                confidenceInterval.LowerBound, confidenceInterval.UpperBound)
        Console.WriteLine()

        ' There is also a wealth of information about the analysis available
        ' through various properties of the LinearRegressionModel object:
        Console.WriteLine($"Residual standard error: {lm.StandardError:F3}")
        Console.WriteLine($"R-Squared:               {lm.RSquared:F4}")
        Console.WriteLine($"Adjusted R-Squared:      {lm.AdjustedRSquared:F4}")
        Console.WriteLine($"F-statistic:             {lm.FStatistic:F4}")
        Console.WriteLine($"Corresponding p-value:   {lm.PValue:F5}")
        Console.WriteLine()

        ' Much of this data can be summarized in the form of an ANOVA table:
        Console.WriteLine(lm.AnovaTable.ToString())

        ' All this information can be printed using the Summarize method.
        ' You will also see summaries using the library in C# interactive.
        Console.WriteLine(lm.Summarize())

    End Sub

End Module
