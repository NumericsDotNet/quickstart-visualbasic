'=====================================================================
'
'  File: prime-numbers.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

' We use classes from the Numerics.NET.SpecialFunctions
' namespace.
Imports Numerics.NET
Imports Numerics.NET.Statistics.Multivariate

' Illustrates working with prime numbers using the
' IntegerMath class in the Numerics.NET.SpecialFunctions
' namespace of Numerics.NET.
Module PrimeNumbers
    Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        '
        ' Factoring numbers
        '

        ' The Factor method returns a sequence of pairs of the prime factors
        ' and their multiplicity:
        Dim index As Integer
        Dim n As Integer = 1001110110
        Dim factors = IntegerMath.Factor(n)

        Console.Write($"{n} = ")
        Dim separator As String = ""
        For Each factor in factors
            Console.Write($"{separator}{factor.Item1}")
            If (factor.Item2 > 1) Then
                Console.Write($"^{factor.Item2}")
            End If
            separator = " * "
        Next
        Console.WriteLine()

        n = 256 * 6157413
        factors = IntegerMath.Factor(n)
        Console.Write($"{n} = ")
        separator = ""
        For Each factor in factors
            Console.Write($"{separator}{factor.Item1}")
            If (factor.Item2 > 1) Then
                Console.Write($"^{factor.Item2}")
            End If
            separator = " * "
        Next
        Console.WriteLine()

        ' The 64bit version can safely factor numbers up to 48 bits long:
        Dim n2 As Long = 1296523 * 1177157L
        Dim factors2 = IntegerMath.Factor(n2)
        Console.Write($"{n2} = ")
        separator = ""
        For Each factor In factors2
            Console.Write($"{separator}{factor.Item1}")
            If (factor.Item2 > 1) Then
                Console.Write($"^{factor.Item2}")
            End If
            separator = " * "
        Next
        Console.WriteLine()

        '
        ' Prime numbers
        '

        ' The IsPrime method verifies if a number is prime or not.
        n = 801853937
        Console.WriteLine("{0} is prime? {1}!", n, IntegerMath.IsPrime(n))
        n = 801853939
        Console.WriteLine("{0} is prime? {1}!", n, IntegerMath.IsPrime(n))

        ' MextPrime gets the first prime after a specified number.
        ' You can call it repeatedly to get successive primes.
        ' Let's get the 10 smallest primes larger than one billion:
        Console.WriteLine("First 10 primes greater than 1 billion:")
        n = 1000000000
        For index = 1 To 10
            n = IntegerMath.NextPrime(n)
            Console.Write("{0,16}", n)
        Next
        Console.WriteLine()

        ' PreviousPrime gets the last prime before a specified number.
        Console.WriteLine("Last 10 primes less than 1 billion:")
        n = 1000000000
        For index = 1 To 10
            n = IntegerMath.PreviousPrime(n)
            Console.Write("{0,16}", n)
        Next
        Console.WriteLine()

    End Sub

End Module
