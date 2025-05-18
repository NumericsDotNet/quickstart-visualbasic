'=====================================================================
'
'  File: anova-two-way.vb
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

' Illustrates the use of the TwoWayAnovaModel class for performing
' a two-way analysis of variance.
Module AnovaTwoWay

    Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' This example investigates the effect of the color and shape
        ' of packages on the sales of the product. The data comes from
        ' 12 stores. Packages can be either red, green or blue in color.
        ' The shape can be either square or rectangular.

        ' Set up the data as anonymous records
        Dim data = {
                New With {.Store = 1, .Color = "Blue", .Shape = "Square", .Sales = 6},
                New With {.Store = 2, .Color = "Blue", .Shape = "Square", .Sales = 14},
                New With {.Store = 3, .Color = "Blue", .Shape = "Rectangle", .Sales = 19},
                New With {.Store = 4, .Color = "Blue", .Shape = "Rectangle", .Sales = 17},
                New With {.Store = 5, .Color = "Red", .Shape = "Square", .Sales = 18},
                New With {.Store = 6, .Color = "Red", .Shape = "Square", .Sales = 11},
                New With {.Store = 7, .Color = "Red", .Shape = "Rectangle", .Sales = 20},
                New With {.Store = 8, .Color = "Red", .Shape = "Rectangle", .Sales = 23},
                New With {.Store = 9, .Color = "Green", .Shape = "Square", .Sales = 7},
                New With {.Store = 10, .Color = "Green", .Shape = "Square", .Sales = 11},
                New With {.Store = 11, .Color = "Green", .Shape = "Rectangle", .Sales = 18},
                New With {.Store = 12, .Color = "Green", .Shape = "Rectangle", .Sales = 10}}
        Dim frame = DataFrame.FromObjects(data)

        ' Construct the OneWayAnova object.
        Dim anova As New TwoWayAnovaModel(frame, "Sales", "Color", "Shape")
        ' Alternatively, you can use a formula to specify the variables
        anova = New TwoWayAnovaModel(frame, "Sales ~ Color + Shape")

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
        ' i.e. the data related to one combination of levels of each factor.
        ' We can use it to access the group means of our color groups.

        ' First we get the IIndex object so we can easily iterate
        ' through the levels:
        Dim colorFactor = anova.GetFactor(Of String)(0)
        For Each level In colorFactor
            Console.WriteLine("Mean for square boxes group '{0}': {1:F4}",
                    level, anova.Cells.Get(level, "Square").Mean)
        Next

        ' We could have accessed the cells directly as well:
        Console.WriteLine("Variance for red, rectangular packages: {0}",
                anova.Cells.Get("Red", "Rectangle").Variance)
        Console.WriteLine()

        ' The RowTotals And ColumnTotals properties permits us to
        ' summarize the data over all levels of a factor. For example,
        ' to get the means of the shape groups, we use
        Dim shapeFactor = anova.GetFactor(Of String)(1)
        For Each level In shapeFactor
            Console.WriteLine("Mean for group '{0}': {1:F4}",
                    level, anova.ColumnTotals.Get(level).Mean)
        Next
        Console.WriteLine()

        ' We can get the summary data for the entire model
        ' by using the TotalCell property:
        Dim totalSummary As Cell = anova.TotalCell
        Console.WriteLine("Summary data:")
        Console.WriteLine($"# observations: {totalSummary.Count}")
        Console.WriteLine($"Grand mean:     {totalSummary.Mean:F4}")

    End Sub

End Module
