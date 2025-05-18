'=====================================================================
'
'  File: grouping-and-aggregation.vb
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
Imports Numerics.NET.Data.Text

' <summary>
' Illustrates how to group data And how to compute aggregates
' over groups And entire datasets.
' </summary>
Module GroupingAndAggregation

    Sub Main()

        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' We work with the Titanic dataset
        Dim titanic = DelimitedTextFile.ReadDataFrame("..\..\..\..\data\titanic.csv")
        ' We'll use these columns often:
        Dim age = titanic.GetColumn("Age")
        Dim survived = titanic("Survived").As(Of Boolean)()
        ' We want to group by the passenger class,
        ' so we make this a categorical vector.
        Dim pclass = titanic("Pclass").AsCategorical()

        '
        ' Aggregators And Aggregation
        '

        ' The Aggregators class defines all common aggregator functions.
        ' Here we compute the mean and do the computations using the double
        ' type. The Aggregate method applies the aggregator
        ' to every column in the data frame
        Dim means = titanic.Aggregate(Aggregators.Mean.As(Of Double))
        Console.WriteLine(means.Summarize())

        ' We can create custom aggregators. Here we compute
        ' the fraction of true values of a boolean vector
        Dim trueFraction = Aggregators.Create(
                Function(b As Vector(Of Boolean)) CDbl(b.CountTrue()) / b.Count)
        Dim pctSurvived = survived.Aggregate(trueFraction)

        ' We can also compute more than one aggregate
        Dim descriptives = titanic.Aggregate(
                Aggregators.Count,
                Aggregators.Mean.As(Of Double),
                Aggregators.StandardDeviation.As(Of Double))
        Console.WriteLine(descriptives.Summarize())

        ' Aggregations can be applied to individual vectors
        Dim meanAge = age.Aggregate(Aggregators.Mean)

        ' Or to rows Or columns of a matrix
        Dim m = Matrix.CreateRandom(5, 8)
        Dim meanByRow = m.AggregateRows(Aggregators.Mean)
        Dim meanByColumn = m.AggregateColumns(Aggregators.Mean)

        '
        ' Groupings
        '

        ' By defining a grouping, we can compute the aggregate
        ' for each group.

        ' The simplest grouping Is by value, similar to
        ' GROUP BY clauses in database queries.

        ' Let's get the average age by class:
        Dim ageByClass = age.AggregateBy(pclass, Aggregators.Mean)

        ' Grouping by quantile means we sort the values
        ' And divide the result into groups of the same size.
        Dim byQuantile = Grouping.ByQuantile(age, 5)
        Dim survivedByAgeGroup = survived.AggregateBy(byQuantile, trueFraction)
        Console.WriteLine("Survival rate by age group:")
        Console.WriteLine(survivedByAgeGroup.Summarize())

        ' For the remainder we will use a vector with a DateTime index
        Dim x = Vector.CreateRandom(200)
        Dim dates = Index.CreateDateRange(New DateTime(2016, 1, 1), x.Length)
        x.Index = dates

        ' A partition Is a straight division of the data into equal groups
        Dim partition = Grouping.Partition(dates, 10,
                alignToEnd:=True, skipIncomplete:=True)
        Dim partitionAvg = x.AggregateBy(partition, Aggregators.Mean)
        Console.WriteLine("Avg. by partition:")
        Console.WriteLine(partitionAvg)

        '
        ' Moving And expanding windows
        '

        ' Moving Or rolling averages And related statistics
        ' can be computed efficiently by using moving windows
        Dim window = Grouping.Window(dates, 20)
        Dim ma20 = x.AggregateBy(window, Aggregators.Mean)
        Console.WriteLine("ma20:")
        Console.WriteLine(ma20.GetSlice(0, 20))
        ' Moving standard deviation Is just as simple
        Dim mstd20 = x.AggregateBy(window, Aggregators.StandardDeviation)
        Console.WriteLine("mstd20:")
        Console.WriteLine(mstd20.GetSlice(0, 20))

        ' Moving windows can have a fixed number of elements, as above,
        ' Or a fixed maximum width
        Dim window2 = Grouping.RangeWindow(dates, TimeSpan.FromDays(20))
        Dim ma20_2 = x.AggregateBy(window2, Aggregators.Mean)

        ' Expanding windows keep the starting point And move the end point
        ' forward in time
        Dim expanding = Grouping.ExpandingWindow(dates)
        Dim expAvg = x.AggregateBy(expanding, Aggregators.Mean)
        Console.WriteLine("expAvg:")
        Console.WriteLine(expAvg.GetSlice(0, 10))

        '
        ' Resampling
        '

        ' Resampling means computing values for a series
        ' with longer periods by aggregating over the values
        ' for shorter periods.

        ' We start by creating an index with the boundaries,
        ' in this case the 10th of each month.
        Dim months = Index.CreateDateRange(New DateTime(2016, 1, 10),
                12, Recurrence.Monthly)
        ' We then create the resampling grouping from this:
        ' Giving the Direction argument as Backward means that
        ' the last value in the time period Is used as the key
        ' for the group.
        Dim resampling1 = Grouping.Resample(dates, months, Direction.Backward)
        ' We can also obtain this grouping in one step
        Dim resampling2 = Grouping.Resample(dates,
                Recurrence.Monthly.Day(10), Direction.Backward)
        Dim resampled = x.AggregateBy(resampling2, Aggregators.Mean)

        '
        ' Pivot tables
        '

        ' A pivot table Is a 2-dimensional grouping on two key columns.
        ' For this, we go back to the Titanic dataset, And we compute
        ' the survival rate per class in a different way. We group
        ' by class And by whether the passenger survived
        Dim Pivot = Grouping.Pivot(
                titanic("Pclass").As(Of Integer)(),
                titanic("Survived").As(Of Boolean)())
        ' We can then get the # of elements in each group
        ' as a matrix, with rows indexed by class And columns
        ' indexed by survived
        Dim counts = Pivot.CountsMatrix()
        ' Scaling by the row sums gives us the fraction
        ' of survived/did Not survive for each class
        Dim fractions = counts.UnscaleRowsInPlace(counts.GetRowSums())
        Console.WriteLine(fractions.Summarize())


    End Sub

End Module
