'=====================================================================
'
'  File: non-uniform-random-numbers.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Imports Numerics.NET.Statistics.Distributions
Imports Numerics.NET.Random

    ' Illustrates generating non-uniform random numbers
    ' using the classes in the Numerics.NET.Statistics.Random
    Module NonUniformRandomNumbers

        Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' Random number generators and the generation
        ' of uniform pseudo-random numbers are illustrated
        ' in the UniformRandomNumbers QuickStart Sample.

        ' In this sample, we will generate numbers from
        ' an exponential distribution, and compare summary
        ' results to what would be expected from
        ' the corresponding Poisson distribution.

        Dim meanTimeBetweenEvents As Double = 0.42

        ' We will use the exponential distribution to generate the time
        ' between events. The number of events per unit time follows
        ' a Poisson distribution.

        ' The parameter of the exponential distribution is the time between events.
        Dim exponential As ExponentialDistribution =
                New ExponentialDistribution(meanTimeBetweenEvents)
        ' The parameter of the Poisson distribution is the mean number of events
        ' per unit time, which is the reciprocal of the time between events:
        Dim poisson As PoissonDistribution = New PoissonDistribution(1 / meanTimeBetweenEvents)

        ' We use a MersenneTwister to generate the random numbers:
        Dim random As New MersenneTwister

        ' The totals array will track the number of events per time unit.
        Dim totals As Integer() = New Integer(14) {}

        Dim currentTime As Double = 0
        Dim endOfCurrentTimeUnit As Double = 1
        Dim eventsInUnit As Integer = 0
        Do While (currentTime < 100000)
            Dim timeBetween As Double = exponential.Sample(random)
            ' Alternatively, we could have written
            '   timeBetween = random.NextDouble(exponential)
            ' which would give an identical result.
            currentTime += timeBetween
            Do While (currentTime > endOfCurrentTimeUnit)
                If (eventsInUnit >= totals.Length) Then
                    eventsInUnit = totals.Length - 1
                End If
                totals(eventsInUnit) = totals(eventsInUnit) + 1
                eventsInUnit = 0
                endOfCurrentTimeUnit = endOfCurrentTimeUnit + 1
            Loop
            eventsInUnit = eventsInUnit + 1
        Loop

        ' Now print the totals
        Console.WriteLine("# Events    Actual  Expected")
        For i As Integer = 0 To totals.Length - 1
            Dim expected As Integer = Convert.ToInt32(100000 * poisson.Probability(i))
            Console.WriteLine("{0,8}  {1,8}  {2,8}", i, totals(i), expected)
        Next


    End Sub

End Module
