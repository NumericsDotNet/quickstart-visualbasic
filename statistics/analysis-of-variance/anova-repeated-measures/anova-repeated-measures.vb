'=====================================================================
'
'  File: anova-repeated-measures.vb
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

' Illustrates the use of the OneWayRAnovaModel class for performing
' a one-way analysis of variance with repeated measures.
Module AnovaRepeatedMeasuresWay

    Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' This QuickStart Sample investigates the effect of the color of packages
        ' on the sales of the product. The data comes from 12 stores.
        ' Packages can be either red, green or blue.

        ' Set up the data as anonymous records
        Dim data = {
                New With {.Person = 1, .Drug = 1, .Score = 30},
                New With {.Person = 1, .Drug = 2, .Score = 28},
                New With {.Person = 1, .Drug = 3, .Score = 16},
                New With {.Person = 1, .Drug = 4, .Score = 34},
                New With {.Person = 2, .Drug = 1, .Score = 14},
                New With {.Person = 2, .Drug = 2, .Score = 18},
                New With {.Person = 2, .Drug = 3, .Score = 10},
                New With {.Person = 2, .Drug = 4, .Score = 22},
                New With {.Person = 3, .Drug = 1, .Score = 24},
                New With {.Person = 3, .Drug = 2, .Score = 20},
                New With {.Person = 3, .Drug = 3, .Score = 18},
                New With {.Person = 3, .Drug = 4, .Score = 30},
                New With {.Person = 4, .Drug = 1, .Score = 38},
                New With {.Person = 4, .Drug = 2, .Score = 34},
                New With {.Person = 4, .Drug = 3, .Score = 20},
                New With {.Person = 4, .Drug = 4, .Score = 44},
                New With {.Person = 5, .Drug = 1, .Score = 26},
                New With {.Person = 5, .Drug = 2, .Score = 28},
                New With {.Person = 5, .Drug = 3, .Score = 14},
                New With {.Person = 5, .Drug = 4, .Score = 30}}
        Dim frame = DataFrame.FromObjects(data)

        ' Construct the OneWayAnova object.
        Dim anova = New OneWayRAnovaModel(frame, "Score", "Drug", "Person")
        ' Alternatively, we can use a formula to specify the variables
        ' in the model
        anova = New OneWayRAnovaModel(frame, "Score ~ Drug + Person")

        ' Perform the calculation.
        anova.Fit()

        ' Verify that the design is balanced:
        If (Not anova.IsBalanced) Then
            Console.WriteLine("The design is not balanced.")
        End If

        ' The AnovaTable property gives us a classic anova table.
        ' We can write the table directly to the console:
        Console.WriteLine(anova.AnovaTable.ToString())
        Console.WriteLine()

        ' A Cell object represents the data in a cell of the model,
        ' i.e. the data related to one level of the factor.
        ' We can use it to access the group means for each drug.

        ' We need two indices here: the second index corresponds
        ' to the person factor.

        ' First we get the IIndex object so we can easily iterate
        ' through the levels:
        Dim drugFactor = anova.GetFactor(Of Integer)(0)
        For Each level In drugFactor
            Console.WriteLine("Mean for group '{0}': {1:F4}",
                    level, anova.TreatmentTotals.Get(level).Mean)
        Next

        ' We could have accessed the cells directly as well:
        Console.WriteLine("Variance for second drug: {0}",
                anova.TreatmentTotals.Get(2).Variance)
        Console.WriteLine()

        ' We can get the summary data for the entire model
        ' by using the TotalCell property:
        Dim totalSummary As Cell = anova.TotalCell
        Console.WriteLine("Summary data:")
        Console.WriteLine($"# observations: {totalSummary.Count}")
        Console.WriteLine($"Grand mean:     {totalSummary.Mean:F4}")

    End Sub

End Module
