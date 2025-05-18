'=====================================================================
'
'  File: data-wrangling.vb
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

' <summary>
' Illustrates how to perform basic data wrangling operations
' on data frames.
' </summary>
Module DataWrangling

    Sub Main()

        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        '
        ' Joining And reshaping
        '

        ' When data comes from different sources,
        ' the Append method lets you join the two
        ' data frames
        Dim frame = DataFrame.FromColumns(New Dictionary(Of String, Object)() From {
                {"A", {"A0", "A1", "A2", "A3"}},
                {"B", {"B0", "B1", "B2", "B3"}},
                {"C", {"C0", "C1", "C2", "C3"}},
                {"D", {"D0", "D1", "D2", "D3"}}},
                Index.Default(0, 3))
        Dim df2 = DataFrame.FromColumns(New Dictionary(Of String, Object)() From {
                {"A", {"A4", "A5", "A6", "A7"}},
                {"B", {"B4", "B5", "B6", "B7"}},
                {"C", {"C4", "C5", "C6", "C7"}},
                {"D", {"D4", "D5", "D6", "D7"}}},
                Index.Default(4, 7))
        Dim df12 = frame.Append(df2)
        ' It Is possible to join more than 2 data frames
        Dim df3 = DataFrame.FromColumns(New Dictionary(Of String, Object)() From {
                {"A", {"A8", "A9", "A10", "A11"}},
                {"B", {"B8", "B9", "B10", "B11"}},
                {"C", {"C8", "C9", "C10", "C11"}},
                {"D", {"D8", "D9", "D10", "D11"}}},
                Index.Default(8, 11))
        Dim df123 = DataFrame.Append(frame, df2, df3)

        ' When the columns don't match, you can specify
        ' a join operation which determines which columns
        ' to keep in the result. If a column Is missing
        ' in a data frame And present in the result,
        ' missing values are inserted.
        frame = DataFrame.FromColumns(New Dictionary(Of String, Object)() From {
                {"A", {"A0", "A1", "A2", "A3"}},
                {"B", {"B0", "B1", "B2", "B3"}},
                {"C", {"C0", "C1", "C2", "C3"}}},
                Index.Default(0, 3))
        df2 = DataFrame.FromColumns(New Dictionary(Of String, Object)() From {
                {"A", {"A4", "A5", "A6", "A7"}},
                {"B", {"B4", "B5", "B6", "B7"}},
                {"D", {"D4", "D5", "D6", "D7"}}},
                Index.Default(4, 7))
        Dim df12outer = frame.Append(df2, JoinType.Outer)
        Dim df12Inner = frame.Append(df2, JoinType.Inner)
        ' Left column join Is equivalent to using the left column index
        Dim df12Left = frame.Append(df2, JoinType.Left)
        Dim df12Left2 = frame.Append(df2, frame.ColumnIndex)
        ' Again, these are equivalent
        Dim df12Right = frame.Append(df2, JoinType.Right)
        Dim df12Right2 = frame.Append(df2, df2.ColumnIndex)

        ' One to one joins match rows on their keys
        Dim dates1 = Index.CreateDateRange(New DateTime(2015, 11, 11), 5, Recurrence.Daily)
        Dim df4 = Vector.CreateRandom(5).ToDataFrame(dates1, "values1")
        Dim dates2 = Index.CreateDateRange(dates1(2), 5, Recurrence.Daily)
        Dim df5 = Vector.CreateRandom(5).ToDataFrame(dates2, "values2")
        Dim df6 = DataFrame.Join(df4, JoinType.Outer, df5)
        Console.WriteLine(df6)

        ' One to many joins match one data frame's index to another's
        ' column.
        ' Create a list of presidents
        Dim numbers = Index.Create({44, 43, 42, 41, 40})
        Dim names = Vector.Create("Barack Obama", "George W. Bush", "Bill Clinton",
                "George H.W. Bush", "Ronald Reagan")
        Dim homeStates = Vector.Create("IL", "TX", "AR", "TX", "CA")
        Dim presidents = DataFrame.FromColumns(New Dictionary(Of String, Object)() From {
                {"Name", names}, {"Home state", homeStates}}, numbers)
        ' And a list of states indexed by their abbreviations
        Dim abbreviations = Index.Create({"AR", "CA", "GA", "MI", "IL", "TX"})
        Dim stateNames = Vector.Create("Arkansas", "California", "Georgia",
                "Michigan", "Illinois", "Texas")
        Dim states = DataFrame.FromColumns(New Dictionary(Of String, Object)() From {
                    {"Full name", stateNames}}, abbreviations)
        ' Now get the full names of states in the list
        Dim presidentsWithState = DataFrame.Join(presidents, JoinType.Left, states, key:="Home state")
        Console.WriteLine(presidentsWithState)

        ' When the indexes are sorted, it Is possible
        ' to do an inexact join to the nearest value.
        ' This Is useful for time series where one series
        ' if offset by a few hours relative to the other
        Dim dates7 = Index.CreateDateRange(New DateTime(2015, 11, 11), 5, Recurrence.Daily)
        Dim df7 = Vector.CreateRandom(5).ToDataFrame(dates7, "values1")
        Dim dates8 = Index.CreateDateRange(dates7(0).AddHours(3), 5, Recurrence.Daily)
        Dim df8 = Vector.CreateRandom(5).ToDataFrame(dates8, "values2")
        Dim df9 = df7.JoinOnNearest(df8, Direction.Backward)
        Console.WriteLine(df9)

        '
        ' Sorting And filtering
        '

        ' Data frames can be sorted by their index Or by
        ' a column. The sort methods always return a New data frame.
        dates2 = Index.CreateDateRange(New DateTime(2015, 11, 11), 15, Recurrence.Daily)
        Dim frame2 = DataFrame.FromColumns(New Dictionary(Of String, Object)() From {
                {"values1", Vector.CreateRandom(dates2.Length)},
                {"values2", Vector.CreateRandom(dates2.Length)},
                {"values3", Vector.CreateRandom(dates2.Length)}}, dates2)
        Dim frame3 = frame2.SortByIndex(SortOrder.Descending)
        Dim frame4 = frame2.SortBy("values1", SortOrder.Ascending)


    End Sub

End Module
