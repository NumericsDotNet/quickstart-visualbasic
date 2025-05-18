'=====================================================================
'
'  File: principal-component-analysis.vb
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
Imports Numerics.NET.Statistics.Multivariate

    ' Demonstrates how to use classes that implement
    ' Principal Component Analysis (PCA).
    Module PCAnalysis

    Sub Main()

        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' This QuickStart Sample demonstrates how to perform
        ' a principal component analysis on a set of data.
        '
        ' The classes used in this sample reside in the
        ' Numerics.NET.Statistics.Multivariate namespace..

        ' First, our dataset, 'depress.txt', which is from
        '     Computer-Aided Multivariate Analysis, 4th Edition
        '     by A. A. Afifi, V. Clark and S. May, chapter 16
        '     See http:'www.ats.ucla.edu/stat/Stata/examples/cama4/default.htm

        ' The data is in delimited text format. Use a matrix reader to load it into a matrix.
        Dim m = DelimitedTextFile.ReadMatrix(Of Double)("..\..\..\..\..\Data\Depress.txt",
                New DelimitedTextOptions(Nothing, False, False, 0, 100, GetType(Double),
                columnDelimiter:=" "c, mergeConsecutiveDelimiters:=True))

        ' The data we want is in columns 8 through 27:
        m = m.GetSubmatrix(0, m.RowCount - 1, 8, 27)

        '
        ' Principal component analysis
        '
        ' We can construct PCA objects in many ways. Since we have the data in a matrix,
        ' we use the constructor that takes a matrix as input.
        Dim pca As New PrincipalComponentAnalysis(m)
        ' and immediately perform the analysis:
        pca.Fit()

        ' We can get the contributions of each component:
        Console.WriteLine(" #    Eigenvalue Difference Contribution Contrib. %")
        For i As Integer = 0 To 4
            ' We get the ith component from the model...
            Dim component As PrincipalComponent = pca.Components(i)
            ' and write out its properties
            Console.WriteLine("{0,2}{1,12:F4}{1,11:F4}{2,14:F3}%{3,10:F3}%",
                    i, component.Eigenvalue, component.EigenvalueDifference,
                    100 * component.ProportionOfVariance,
                    100 * component.CumulativeProportionOfVariance)
        Next

        ' To get the proportions for all components, use the
        ' properties of the PCA object:
        Dim proportions = pca.VarianceProportions

        ' To get the number of components that explain a given proportion
        ' of the variation, use the GetVarianceThreshold method:
        Dim count As Integer = pca.GetVarianceThreshold(0.9)
        Console.WriteLine($"Components needed to explain 90% of variation: {count}")
        Console.WriteLine()

        ' The value property gives the components themselves:
        Console.WriteLine("Components:")
        Console.WriteLine("Var.      1       2       3       4       5")
        Dim pcs As PrincipalComponentCollection = pca.Components
        For i As Integer = 0 To pcs.Count - 1

            Console.WriteLine("{0,4}{1,8:F4}{2,8:F4}{3,8:F4}{4,8:F4}{5,8:F4}",
                    i, pcs(0).Value(i), pcs(1).Value(i), pcs(2).Value(i), pcs(3).Value(i), pcs(4).Value(i))
        Next
        Console.WriteLine()

        ' The scores are the coefficients of the observations expressed as a combination
        ' of principal components.
        Dim scores = pca.ScoreMatrix

        ' To get the predicted observations based on a specified number of components,
        ' use the GetPredictions method.
        Dim prediction = pca.GetPredictions(count)
        Console.WriteLine($"Predictions imports {count} components:")
        Console.WriteLine("   Pr. 1  Act. 1   Pr. 2  Act. 2   Pr. 3  Act. 3   Pr. 4  Act. 4", count)
        For i As Integer = 0 To 9
            Console.WriteLine("{0,8:F4}{1,8:F4}{2,8:F4}{3,8:F4}{4,8:F4}{5,8:F4}{6,8:F4}{7,8:F4}",
                    prediction(i, 0), m(i, 0),
                    prediction(i, 1), m(i, 1),
                    prediction(i, 2), m(i, 2),
                    prediction(i, 3), m(i, 3))
        Next

    End Sub

End Module
