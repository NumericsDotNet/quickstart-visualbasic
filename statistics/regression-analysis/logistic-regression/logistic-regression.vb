'=====================================================================
'
'  File: logistic-regression.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

Imports Numerics.NET.Data.Text
Imports Numerics.NET.DataAnalysis
Imports Numerics.NET.Statistics
Imports Numerics.NET.Statistics.Tests

' Illustrates building logistic regression models using
' the LogisticRegressionModel class in the
' Numerics.NET.Statistics namespace of Numerics.NET.
Module LogisticRegression

    Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' Logistic regression can be performed using
        ' the LogisticRegressionModel class.
        '
        ' This QuickStart sample uses data from a study of factors
        ' that determine low birth weight at Baystate Medical Center.
        ' from Belsley, Kuh and Welsch. The fields are as follows:
        '   AGE:  Mother's age.
        '   LWT:  Mother's weight.
        '   RACE: 1=white, 2=black, 3=other.
        '   FVT:  Number of physician visits during the 1st trimester.
        '   LOW:  Low birth weight indicator.

        ' First, read the data from a file into an ADO.NET DataTable.
        ' For the sake of clarity, we put this code in its own method.
        Dim data = FixedWidthTextFile.ReadDataFrame(
                "..\..\..\..\..\Data\lowbwt.txt",
                {4, 11, 18, 25, 33, 42, 49, 55, 61, 68})

        ' We need indicator variables for the race. All we need to do
        ' is mark the variable as categorical:
        data.MakeCategorical("RACE", Index.Create({1, 2, 3}))

        ' Now create the regression model. Parameters are the name
        ' of the dependent variable, a string array containing
        ' the names of the independent variables, and the data frame
        ' containing all variables.

        ' Note that RACE, which is a categorical variable, is automatically
        ' expanded into indicator variables.
        Dim model As LogisticRegressionModel = New LogisticRegressionModel(data, "LOW",
                New String() {"AGE", "LWT", "RACE", "FTV"})

        ' Alternatively, we can use a formula to describe the variables
        ' in the model. The dependent variable goes on the left, the
        ' independent variables on the right of the ~
        model = New LogisticRegressionModel(data, "LOW ~ AGE + LWT + RACE + FTV")

        ' The Fit method performs the actual regression analysis.
        model.Fit()

        ' The Parameters collection contains information about the regression
        ' parameters.
        Console.WriteLine("Variable              Value    Std.Error  t-stat  p-Value")
        For Each parameter In model.Parameters
            ' Parameter objects have the following properties:
            ' Name, usually the name of the variable:
            ' Estimated value of the parameter:
            ' Standard error:
            ' The value of the t statistic for the hypothesis that the parameter is zero.
            ' Probability corresponding to the t statistic.
            Console.WriteLine("{0,-20}{1,10:F5}{2,10:F5}{3,8:F2} {4,7:F4}",
                    parameter.Name,
                    parameter.Value,
                    parameter.StandardError,
                    parameter.Statistic,
                    parameter.PValue)
        Next

        ' The log-likelihood of the computed solution is also available:
        Console.WriteLine($"Log-likelihood: {model.LogLikelihood:F4}")

        ' We can test the significance by looking at the results
        ' of a log-likelihood test, which compares the model to
        ' a constant-only model:
        Dim lrt As SimpleHypothesisTest = model.GetLikelihoodRatioTest()
        Console.WriteLine("Likelihood-ratio test: chi-squared={0:F4}, p={1:F4}", lrt.Statistic, lrt.PValue)

        ' We can compute a model with fewer parameters:
        Dim model2 As LogisticRegressionModel = New LogisticRegressionModel(data, "LOW",
                New String() {"LWT", "RACE"})
        model2.Fit()

        ' Print the results...
        Console.WriteLine("Variable              Value    Std.Error  t-stat  p-Value")
        For Each parameter In model2.Parameters
            Console.WriteLine("{0,-20}{1,10:F5}{2,10:F5}{3,8:F2} {4,7:F4}",
                    parameter.Name, parameter.Value, parameter.StandardError,
                    parameter.Statistic, parameter.PValue)
            ' ...including the log-likelihood:
        Next

        Console.WriteLine($"Log-likelihood: {model2.LogLikelihood:F4}")

        ' We can now compare the original model to this one, once again
        ' using the likelihood ratio test:
        lrt = model.GetLikelihoodRatioTest(model2)
        Console.WriteLine("Likelihood-ratio test: chi-squared={0:F4}, p={1:F4}", lrt.Statistic, lrt.PValue)

        '
        ' Multinomial (polytopous) logistic regression
        '

        ' The LogisticRegressionModel class can also be used
        ' for logistic regression with more than 2 responses.
        ' The following example is from "Applied Linear Statistical
        ' Models."

        ' Load the data into a matrix
        Dim columnNames = {"id", "duration", "x2", "x3", "x4",
                "nutritio", "agecat1", "agecat3", "alcohol", "smoking"}
        Dim frame = FixedWidthTextFile.ReadDataFrame(
                "..\..\..\..\..\Data\mlogit.txt",
                New FixedWidthTextOptions(
                    {5, 10, 15, 20, 25, 32, 37, 42, 47},
                    columnHeaders:=False)).WithColumnIndex(columnNames)

        ' For multinomial regression, the response variable must be
        ' a CategoricalVariable:
        frame.MakeCategorical("duration")

        ' The constructor takes an extra argument of type
        ' LogisticRegressionMethod:
        Dim model3 As New LogisticRegressionModel(frame, "duration",
                {"nutritio", "agecat1", "agecat3", "alcohol", "smoking"})
        model3.Method = LogisticRegressionMethod.Nominal

        ' When using a formula, we can use '.' as a shortcut
        ' for all unused variables in the data frame.
        ' Because duration has 3 levels, nominal logistic regression
        ' Is automatically inferred.
        model3 = New LogisticRegressionModel(frame,
                "duration ~ nutritio + agecat1 + agecat3 + alcohol + smoking")

        ' Everything else is the same:
        model3.Fit()

        ' There is a set of parameters for each level of the
        ' response variable. The highest level is the reference
        ' level and has no associated parameters.
        For Each p In model3.Parameters
            Console.WriteLine(p.ToString())
        Next

        Console.WriteLine($"Log likelihood:  {model3.LogLikelihood:F4}")

        ' To test the hypothesis that all the slopes are zero,
        ' use the GetLikelihoodRatioTest method.
        lrt = model3.GetLikelihoodRatioTest()
        Console.WriteLine("Test that all slopes are zero: chi-squared={0:F4}, p={1:F4}", lrt.Statistic, lrt.PValue)

    End Sub

End Module
