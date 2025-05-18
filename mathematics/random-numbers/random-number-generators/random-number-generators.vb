'=====================================================================
'
'  File: random-number-generators.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Imports Numerics.NET
Imports Numerics.NET.Random

    ' Illustrates the use of the classes that implement
    ' pseudo-random number generators.
    Module RandomNumberGenerators

        Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' This QuickStart Sample gives an overview of the pseudo-random
        ' number generators that provide an alternative for the
        ' System.Random class..

        '
        ' ExtendedRandom class
        '

        ' The ExtendedRandom class simply extends the functionality
        ' of the System.Random class:
        Dim extended As ExtendedRandom = New ExtendedRandom
        Dim intValues As Integer() = New Integer(100) {}
        Dim doubleValues As Double() = New Double(100) {}

        ' The fill method fills an array of integers with random numbers
        extended.Fill(intValues)
        Console.WriteLine($"integer(99) = {intValues(99)}")

        ' Or, it can generate uniform real values:
        extended.Fill(doubleValues)
        Console.WriteLine($"double(99) = {doubleValues(99)}")

        ' All random number generators can also produce variates
        ' from any user-specified probability distribution.
        ' The NonUniformRandomNumbers sample illustrates
        ' how to do this.

        '
        ' RANLUX Generators
        '

        ' The RANLUX generators are available with three different
        ' 'luxury levels.' Each level produces random numbers of
        ' increasing quality at a performance cost.
        '
        ' There are four constructors. The first constructor uses the
        ' default seed and the default (lowest) luxury level:
        Dim ranLux1 As RanLux = New RanLux

        ' We can specify a seed value as well:
        Dim ranLux2 As RanLux = New RanLux(99)

        ' We can specify the luxury level in the constructor:
        Dim ranLux3 As RanLux = New RanLux(RanLuxLuxuryLevel.Better)

        ' Finally, we can specify both a seed and the luxury level:
        Dim ranLux4 As RanLux = New RanLux(99, RanLuxLuxuryLevel.Best)

        ' All methods of System.Random and ExtendedRandom are available:
        ranLux1.Fill(intValues)
        ranLux2.Fill(doubleValues)
        Console.WriteLine($"Integer from RanLux(Best): {ranLux3.Next(100)}")

        '
        ' Generalized Feedback Shift Register Generator
        '

        ' This generator is implemented by the GfsrGenerator class.
        ' It has three constructors. A default constructor that uses
        ' a default seed value:
        Dim gfsr1 As GfsrGenerator = New GfsrGenerator

        ' A constructor that takes a single integer seed:
        Dim gfsr2 As GfsrGenerator = New GfsrGenerator(99)

        ' And a constructor that takes an array of integers
        ' as its seed. The maximum size of this seed array
        ' is 2^14-1 = 16383.
        Dim gfsr3 As GfsrGenerator = New GfsrGenerator(New Integer() {99, 17, Integer.MaxValue})

        ' Once again, all standard methods are available.
        Console.WriteLine($"Double from GFSR: {gfsr2.NextDouble()}")

        '
        ' Mersenne Twister
        '

        ' The Mersenne Twister is a variation on the GFSR generator and,
        ' not surprisingly, also has three constructors:
        Dim mersenne1 As MersenneTwister = New MersenneTwister
        Dim mersenne2 As MersenneTwister = New MersenneTwister(99)
        Dim mersenne3 As MersenneTwister = New MersenneTwister(New Integer() {99, 17, Integer.MaxValue})


    End Sub

End Module
