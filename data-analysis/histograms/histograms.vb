'=====================================================================
'
'  File: histograms.vb
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

' Illustrates how to create historgrams.
Module Histograms

    Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' Histograms are used to summarize the distribution of data.
        ' This QuickStart sample creates a histogram from data
        ' in a variety of ways.

        ' We use the test scores of students on a hypothetical national test.
        ' First we create a NumericalVariable that holds the test scores.
        Dim group1Results = Vector.Create(
                62, 77, 61, 94, 75, 82, 86, 83, 64, 84,
                68, 82, 72, 71, 85, 66, 61, 79, 81, 73)

        ' We can create a histogram with evenly spaced bins
        ' by specifying the lower bound, the upper bound,
        ' And the number of bins
        Dim histogram1 = Histogram.CreateEmpty(50, 100, 5)

        ' We can also provide the bounds explicitly
        Dim bounds = {50, 62, 74, 88, 100}
        Dim histogram2 = Histogram.CreateEmpty(bounds)

        ' Or we can first create an Index object
        Dim idx = Index.CreateBins(bounds)
        Dim histogram3 = Histogram.CreateEmpty(idx)

        ' To tally the results, we simply call the Tabulate method.
        ' The data can be supplied as a vector
        histogram1.Tabulate(group1Results)
        ' Or simply as any enumerable, including an array
        histogram2.Tabulate(group1Results.ToArray())

        ' You can add multiple data sets to the same histogram
        histogram2.Tabulate({74, 68, 89})
        ' Or you can add individual data points using the Increment method.
        ' This will increment the count of the bin that contains
        ' the specified value
        histogram2.Increment(83)
        histogram2.Increment(78)

        ' Histograms are just vectors, so the SetToZero method
        ' clears all the data
        histogram2.SetToZero()

        ' The Bins property returns an index of bins
        Dim bins = histogram1.Bins
        ' The Length property returns the total number of bins
        Console.WriteLine($"# bins: {bins.Length}")
        ' For binned histograms, the bins are of type Interval<T>
        Dim bin = bins(2)
        ' Interval structures have a lower bound, an upper bound:
        Console.WriteLine($"Bin 2 has lower bound {bin.LowerBound}.")
        Console.WriteLine($"Bin 2 has upper bound {bin.UpperBound}.")
        ' You can get the value at a specific bin using the Get method:
        Console.WriteLine($"Bin 2 has value {histogram1.Get(bin)}.")

        ' The histogram's FindBin method returns the Histogram bin
        ' that contains a specified value:
        bin = histogram1.FindBin(83)
        Console.WriteLine($"83 is in bin {bin}")

        ' You can use the BinsAndValues property to iterate through all the bins
        ' in a for-each loop:
        For Each pair In histogram1.BinsAndValues
            Console.WriteLine("Bin {0}: {1}", pair.Key, pair.Value)
        Next

        ' You can also create histograms for categorical data:
        Dim success = Vector.CreateCategorical(
                {True, False, True, True, False})
        Dim histogram4 = success.CreateHistogram()
        ' Bins for categorical histograms are just the categories:
        Dim successes = histogram4.Get(True)
        Console.WriteLine(successes)

    End Sub

End Module
