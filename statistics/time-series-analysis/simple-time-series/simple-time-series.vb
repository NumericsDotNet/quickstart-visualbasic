'=====================================================================
'
'  File: simple-time-series.vb
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

    ' Illustrates the use of the TimeSeriesCollection class to represent
    ' and manipulate time series data.
    Module SimpleTimeSeries

        Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' Time series data frames can be created in a variety of ways.
        ' Here we read from a CSV file And specify the column to use as the index
        Dim timeSeries = DelimitedTextFile.ReadDataFrame(Of DateTime)(
                "..\..\..\..\..\Data\MicrosoftStock.csv", "Date")

        ' The RowCount property returns the number of
        ' observations:
        Console.WriteLine($"# observations: {timeSeries.RowCount}")

        '
        ' Accessing variables
        '

        ' Variables are accessed by name or numeric index.
        ' They need to be cast to the appropriate specialized
        ' type (NumericalVariable, DateTimeVariable, etc.)
        Dim close = timeSeries("Close").As(Of Double)
        Console.WriteLine($"Average close price: ${close.Mean():F2}")

        ' Variables can also be accessed by numeric index:
        Console.WriteLine($"3rd variable: {timeSeries(2).Name}")

        ' The GetSubset method returns the data from the specified range.
        Dim y2004 As DateTime = New DateTime(2004, 1, 1)
        Dim y2005 As DateTime = New DateTime(2005, 1, 1)
        Dim series2004 = timeSeries.GetRows(y2004, y2005)
        Console.WriteLine("Opening price on the first trading day of 2004: {0}",
                series2004("Open").GetValue(0))

        '
        ' Transforming the Frequency
        '

        ' The first step is to define the aggregator function
        ' for each variable. This function specifies how each
        ' observation in the new time series is calculated
        ' from the observations in the original series.

        ' The Aggregator class has a number of
        ' pre-defined aggregator functions:
        Dim allAggregators = New Dictionary(Of String, AggregatorGroup)() From
            {
                {"Open", Aggregators.First},
                {"Close", Aggregators.Last},
                {"High", Aggregators.Max},
                {"Low", Aggregators.Min},
                {"Volume", Aggregators.Sum}
            }

        ' We can specify a subset of the series by providing
        ' the start and end dates.

        ' The TransformFrequency method returns a new series
        ' containing the aggregated data:

        Dim monthlySeries = timeSeries.GetRows(y2004, y2005).
                Resample(Recurrence.Monthly, allAggregators)

        ' We can now print the results:
        Console.WriteLine("Monthly statistics for Microsoft Corp. (MSFT)")
        Console.WriteLine(monthlySeries.Summarize())

    End Sub

End Module
