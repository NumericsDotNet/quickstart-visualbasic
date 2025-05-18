'=====================================================================
'
'  File: sorting-and-filtering.vb
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
Imports Numerics.NET
Imports Numerics.NET.Statistics

    ' Illustrates sorting and filtering of data sets and variables.
    Module SortingAndFiltering

        Sub Main()

        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' We load the data into a data frame with a DateTime row index
        Dim timeSeries = DelimitedTextFile.ReadDataFrame(Of DateTime)(
                "..\..\..\..\Data\MicrosoftStock.csv", "Date")
        Dim dates = timeSeries.RowIndex

        ' The following are all equivalent ways of getting
        ' a strongly typed vector from a data frame
        Dim open = timeSeries("Open").As(Of Double)()
        Dim close = timeSeries.GetColumn("Close")
        Dim high = timeSeries.GetColumn(Of Double)("High")
        Dim low = CType(timeSeries("Low"), Vector(Of Double))

        Dim volume = timeSeries("Volume").As(Of Double)()

        ' Let's print some basic statistics for the full data set:
        Console.WriteLine($"Total # observations: {timeSeries.RowCount}")
        Console.WriteLine($"Average volume: {volume.Mean():F0}")
        Console.WriteLine($"Total volume: {volume.Sum():F0}")

        '
        ' Filtering
        '

        ' Use the GetRows method to select subsets of rows.

        ' You can use a sequence of keys
        Dim subset = timeSeries.GetRows(
                {New DateTime(2000, 3, 1), New DateTime(2000, 3, 2)})

        ' When the index Is sorted, you can use a range
        subset = timeSeries.GetRows(
                New DateTime(2000, 1, 1), New DateTime(2010, 1, 1))

        ' Another option Is to use a boolean mask. Here we select
        ' observations where the close price was greater
        ' than the open price
        Dim Filter = Vector.GreaterThan(close, open)
        ' Then we can use the GetRows method
        subset = timeSeries.GetRows(Filter)
        ' Data Is now filtered
        Console.WriteLine($"Filtered # observations: {subset.RowCount}")

        ' Masks can be combined using logical operations
        Dim volumeFilter = volume.Map(Function(x) 200000000.0 <= x And x < 300000000.0)
        Console.WriteLine($"Volume filtered #: {volumeFilter.CountTrue()}")
        Dim intersection = Vector.And(volumeFilter, Filter)
        Dim union = Vector.Or(volumeFilter, Filter)
        Dim negation = Vector.Not(Filter)

        Console.WriteLine($"Combined filtered #: {intersection.CountTrue()}")
        subset = timeSeries.GetRows(intersection)

        ' When the row index Is ordered, it Is possible
        ' to get the rows with the key nearest to the
        ' supplied keys
        Dim startDate = New DateTime(2001, 1, 1, 3, 0, 0)
        Dim offsetDates = Index.CreateDateRange(startDate,
                100, Recurrence.Daily)
        subset = timeSeries.GetNearestRows(offsetDates, Direction.Forward)

        '
        ' Sorting
        '

        ' The simplest way to sort data Is calling the Sort method
        ' with the name of the variable to sort on
        Dim sortedSeries = timeSeries.SortBy("High", SortOrder.Descending)
        Dim highSorted = sortedSeries.GetColumn("High")(New Range(0, 4))
        Console.WriteLine("Largest 'High' values:")
        Console.WriteLine(highSorted.ToString("F2"))

        ' If you just want the largest few items in a series,
        ' you can use the Top Or Bottom method
        Console.WriteLine(high.Top(5).ToString("F2"))

    End Sub

End Module
