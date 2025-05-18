'=====================================================================
'
'  File: data-frames.vb
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

' <summary>
' Illustrates how to create And manipulate data frames.
' </summary>
Module DataFrames

    Sub Main()

        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' Data frames can be constructed in a variety of ways.
        ' This example will use mostly static methods of the
        ' static DataFrame class.

        ' From a dictionary of column keys that map to collections:
        Dim dict = New Dictionary(Of String, Object)() From {
                {"state", {"Ohio", "Ohio", "Ohio", "Nevada", "Nevada"}},
                {"year", {2000, 2001, 2002, 2001, 2002}},
                {"pop", {1.5, 1.7, 3.6, 2.4, 2.9}}}
        Dim df1 = DataFrame.FromColumns(dict)
        Console.WriteLine(df1)

        ' The data frame has a default index of row numbers.
        ' A row index can be specified as well:
        Dim df2 = DataFrame.FromColumns(New Dictionary(Of String, Object)() From {
                {"first", {11.0, 14.0, 17.0, 93.0, 55.0}},
                {"second", {22.0, 33.0, 43.0, 51.0, 69.0}}},
                Index.CreateDateRange(New DateTime(2015, 4, 1), 5))
        Console.WriteLine(df2)

        ' Alternatively, the columns can be a list of collections.
        Dim rowIndex = Index.Create({"one", "two", "three", "four", "five"})
        Dim df3 = DataFrame.FromColumns(dict, rowIndex)
        Console.WriteLine(df3)
        ' If you supply a column index, only the columns with
        ' keys in the index will be retained:
        Dim columnIndex = Index.Create({"pop", "year"})
        Dim df4 = DataFrame.FromColumns(dict, rowIndex, columnIndex)
        Console.WriteLine(df4)

        ' Yet another way Is to use tuples:
        Dim CreateTuple As Func(Of String, Object, Tuple(Of String, Object)) =
            Function(x, y) Tuple.Create(x, y)
        Dim df5 = DataFrame.FromColumns(
            ("state", {"Ohio", "Ohio", "Ohio", "Nevada", "Nevada"}),
            ("year", {2000, 2001, 2002, 2001, 2002}),
            ("pop", {1.5, 1.7, 3.6, 2.4, 2.9}))
        Console.WriteLine(df5)

        ' Data frames can be created from a sequence of objects.
        ' By default, all public properties are included as columns
        ' in the resulting data frame:
        Dim points =
        {
            New With {.X = 1, .Y = 5, .Z = 9},
            New With {.X = 2, .Y = 6, .Z = 10},
            New With {.X = 3, .Y = 7, .Z = 11},
            New With {.X = 4, .Y = 8, .Z = 12}
        }
        Dim df6 = DataFrame.FromObjects(points)
        Console.WriteLine(df6)
        ' It Is possible to select the properties:
        Dim df7 = DataFrame.FromObjects(points, {"Z", "X"})
        Console.WriteLine(df7)

        ' Vectors And matrices can be converted to data frames
        ' using their ToDataFrame method:
        Dim m = Matrix.CreateRandom(10, 2)
        Dim df8 = m.ToDataFrame(Index.Default(10), Index.Create({"A", "B"}))
        Dim v = Vector.CreateRandom(3)
        Dim df9 = v.ToDataFrame(Index.Create({"a", "b", "c"}), "values")

        '
        ' Import / export
        '

        ' Several methods exist for importing data frames directly
        ' from data sources Like text files, R data files, And databases.
        Dim dt = New System.Data.DataTable()
        dt.Columns.Add("x1", GetType(Double))
        dt.Columns.Add("x2", GetType(Double))
        dt.Rows.Add(New Object() {1.0, 2.0})
        dt.Rows.Add(New Object() {3.0, 4.0})
        dt.Rows.Add(New Object() {5.0, 6.0})
        Dim df11 = DataFrame.FromDataTable(dt)

        Dim df12 = DelimitedTextFile.ReadDataFrame("..\..\..\..\data\iris.csv",
            DelimitedTextOptions.CsvWithoutHeader)
        ' By default, these methods return a data frame with a default
        ' index (row numbers). You can specify the column(s) to use
        ' for the index, And the data frame will use that column.
        Dim df13 = DelimitedTextFile.ReadDataFrame(Of Integer)("..\..\..\..\data\titanic.csv",
            "PassengerId", options:=DelimitedTextOptions.Csv)
        DelimitedTextFile.Write("irisCopy.csv", df12, DelimitedTextOptions.Csv)

        '
        ' Setting row And column indexes
        '

        ' You can use specific columns as the row index.
        ' Here we have a 2 level hierarchical index:
        Dim df1a = df1.WithRowIndex(Of String, Integer)("state", "year")

        ' Column indexes can be changed as well:
        Dim df2b = df2.WithColumnIndex(New String() {"A", "B"})


    End Sub

End Module
