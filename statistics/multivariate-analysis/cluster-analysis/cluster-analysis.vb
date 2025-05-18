'=====================================================================
'
'  File: cluster-analysis.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

Imports Numerics.NET.Data.Stata
Imports Numerics.NET
Imports Numerics.NET.Statistics
Imports Numerics.NET.Statistics.Multivariate

' <summary>
' Demonstrates how to use classes that implement
' hierarchical and K-means clustering.
' </summary>
Module ClusterAnalysisExample

    Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' This QuickStart Sample demonstrates how to run two
        ' common multivariate analysis techniques:
        ' hierarchical cluster analysis and K-means cluster analysis.
        '
        ' The classes used in this sample reside in the
        ' Numerics.NET.Statistics.Multivariate namespace..

        ' First, our dataset, which is from
        '     Computer-Aided Multivariate Analysis, 4th Edition
        '     by A. A. Afifi, V. Clark and S. May, chapter 16
        '     See http:'www.ats.ucla.edu/stat/Stata/examples/cama4/default.htm
        Dim frame = StataFile.ReadDataFrame("..\..\..\..\..\Data\companies.dta")

        '
        ' Hierarchical cluster analysis
        '

        Console.WriteLine("Hierarchical clustering")

        ' Create the model:
        Dim columns = {"ror5", "de", "salesgr5", "eps5", "npm1", "pe", "payoutr1"}
        Dim hc = New HierarchicalClusterAnalysis(frame, columns)
        ' Alternatively, we could use a formula to specify the variables
        Dim formula = "ror5 + de + salesgr5 + eps5 + npm1 + pe + payoutr1"
        hc = New HierarchicalClusterAnalysis(frame, formula)

        ' Rescale the variables to their Z-scores before doing the analysis:
        hc.Standardize = True
        ' The linkage method defaults to centroid:
        hc.LinkageMethod = LinkageMethod.Centroid
        ' We could set the distance measure. We use the default:
        hc.DistanceMeasure = DistanceMeasures.SquaredEuclideanDistance

        ' Fit the model:
        hc.Fit()

        ' We can partition the cases into clusters:
        Dim partition As HierarchicalClusterCollection = hc.GetClusterPartition(5)
        ' Individual clusters are accessed through an index, or through enumeration.
        For Each cluster As HierarchicalCluster In partition
            Console.WriteLine("Cluster {0} has {1} members.", cluster.Index, cluster.Size)
        Next

        ' And get the indexes of the observations in a single cluster:
        Dim indexes = partition(3).MemberIndexes
        Console.WriteLine($"Number of items in the partition: {indexes.Length}")

        ' Get a variable that shows memberships:
        Dim memberships = partition.GetMemberships()
        For i As Integer = 15 To memberships.Length - 1
            Console.WriteLine("Observation {0} belongs to cluster {1}", i, memberships.GetLevelIndex(i))
        Next i

        ' A dendrogram is a graphical representation of the clustering in the form of a tree.
        ' You can get all the information you need to draw a dendrogram starting from
        ' the root node of the dendrogram:
        Dim root As DendrogramNode = hc.DendrogramRoot
        ' Position and DistanceMeasure give the x and y coordinates:
        Console.WriteLine("Root position: ({0:F4}, {1:F4})", root.Position, root.DistanceMeasure)
        ' The left and right children:
        Console.WriteLine($"Position of left child: {root.LeftChild.Position:F4}")
        Console.WriteLine($"Position of right child: {root.RightChild.Position:F4}")

        ' You can also get a filter that defines a sort order suitable for
        ' drawing the dendrogram:
        Dim sortOrder = hc.GetDendrogramOrder()
        Console.WriteLine()

        '
        ' K-Means Clustering
        '

        Console.WriteLine("K-means clustering")

        ' Create the model. We need to specify the number of clusters up front:
        Dim kmc As New KMeansClusterAnalysis(frame, columns, 3)
        ' Rescale the variables to their Z-scores before doing the analysis:
        kmc.Standardize = True

        ' Fit the model:
        kmc.Fit()

        ' The Predictions property Is a categorical vector that contains
        ' the cluster assignments
        Dim predictions = kmc.Predictions
        ' The GetDistancesToCenters method returns a vector containing
        ' the distance of each observations to its center.
        Dim distances = kmc.GetDistancesToCenters()

        ' For example
        For i = 18 To predictions.Length - 1
            Console.WriteLine("Observation {0} belongs to cluster {1}, distance: {2:F4}.",
                    i, predictions(i), distances(i))
        Next
        ' You can use this to compute several statistics
        Dim Descriptives = distances.SplitBy(predictions).
                Map(Function(x) New Descriptives(Of Double)(x))

        ' Individual clusters are accessed through an index, Or through enumeration.
        For i = 0 To Descriptives.Length - 1
            Console.WriteLine("Cluster {0} has {1} members. Sum of squares: {2:F4}",
                    i, Descriptives(i).Count, Descriptives(i).SumOfSquares)
            Console.WriteLine($"Center: {kmc.Clusters(i):F4}")
        Next

        ' The distances between clusters are also available
        Console.WriteLine(kmc.GetClusterDistances().ToString("F4"))

        ' You can get a filter for the observations in a single cluster.
        ' This uses the GetIndexes method of categorical vectors.
        Dim level1Indexes = kmc.Predictions.GetIndexes(1).ToArray()
        Console.WriteLine($"Number of items in cluster 1: {level1Indexes.Length}")

    End Sub

End Module
