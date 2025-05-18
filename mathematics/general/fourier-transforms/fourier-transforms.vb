'=====================================================================
'
'  File: fourier-transforms.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

Imports Numerics.NET
' The FFT classes reside in the Numerics.NET.SignalProcessing
' namespace.
Imports Numerics.NET.SignalProcessing

' Illustrates the use of the FftProvider and Fft classes for computing
' the Fourier transform of real and complex signals.
Module FourierTransforms

    Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' This QuickStart sample shows how to compute the Fouier
        ' transform of real and complex signals.

        ' Some vectors to play with:
        Dim r1 = Vector.Create(Of Double)(1000)
        For i As Integer = 0 To r1.Length - 1
            r1(i) = 1.0 / (1 + i)
        Next
        Dim c1 = Vector.Create(Of Complex(Of Double))(1000)
        For i As Integer = 0 To c1.Length - 1
            c1(i) = New Complex(Of Double)(Math.Sin(0.03 * i), Math.Cos(0.07 * i))
        Next

        Dim r2 = Vector.Create(1.0, 2.0, 3.0, 4.0)
        Dim c2 = Vector.Create(
                New Complex(Of Double)(1, 2),
                New Complex(Of Double)(3, 4),
                New Complex(Of Double)(5, 6),
                New Complex(Of Double)(7, 8))

        '
        ' One-time FFT's
        '

        ' The Vector and ComplexVector classes have static methods to compute FFT's:
        Dim c3 = Vector.FourierTransform(r2)
        Dim r3 = Vector.InverseFourierTransform(c3)
        Console.WriteLine($"fft(r2) = {c3:F3}")
        Console.WriteLine($"ifft(fft(r2)) = {r3:F3}")
        ' The ComplexConjugateSignalVector type represents a complex vector
        ' that is the Fourier transform of a real signal.
        ' It enforces certain symmetry properties:
        Console.WriteLine($"c3(i) == conj(c3(N-i)): {c3(1)} == conj({c3(3)})")

        '
        ' FFT Providers
        '

        ' FFT's require a fair bit of pre-computation. Using the FftProvider class,
        ' you can get an Fft object that caches these computations.

        ' Here, we create an FFT implementation for a real signal:
        Dim realFft = Fft(Of Double).CreateReal(r1.Length)
        ' For a complex to complex transform:
        Dim complexFft = Fft(Of Double).CreateComplex(c1.Length)

        ' You can set the scale factor for the forward transform.
        ' The default is 1/N.
        realFft.ForwardScaleFactor = 1.0 / Math.Sqrt(c1.Length)
        ' and the backward transform, with default 1:
        realFft.BackwardScaleFactor = realFft.ForwardScaleFactor

        ' The ForwardTransform method performs a forward transform:
        Dim c4 As Vector(Of Complex(Of Double)) = realFft.ForwardTransform(r1)
        Console.WriteLine("First 5 terms of fft(r1):")
        For i As Integer = 0 To 4
            Console.WriteLine("   {0}: {1}", i, c4(i))
        Next i
        c4 = complexFft.ForwardTransform(c1)
        Console.WriteLine("First 5 terms of fft(c1):")
        For i As Integer = 0 To 4
            Console.WriteLine("   {0}: {1}", i, c4(i))
        Next i
        ' ForwardTransform has many overloads for real to complex and
        ' complex to complex transforms.

        ' A one-sided transform returns only the first half of the FFT of
        ' a real signal. The rest can be deduced from the symmetry properties.
        ' Here's how to compute a one-sided FFT:
        Dim halfLength As Integer = r1.Length \ 2 + 1
        Dim c5 = Vector.Create(Of Complex(Of Double))(halfLength)
        realFft.ForwardTransform(r1, c5, RealFftFormat.OneSided)

        ' The BackwardTransform method has a similar set of overloads:
        Dim r4 = Vector.Create(Of Double)(r1.Length)
        realFft.BackwardTransform(c5, r4, RealFftFormat.OneSided)

        ' As the last step, we need to dispose the two FFT implementations.
        realFft.Dispose
        complexFft.Dispose

        '
        ' 2D transforms
        '

        ' 2D transforms are handled in a completely analogous way.
        Dim m = Matrix.Create(Of Double)(36, 56)
        For i As Integer = 0 To m.RowCount - 1
            For j As Integer = 0 To m.ColumnCount - 1
                m(i, j) = Math.Exp(-0.1 * i) * Math.Sin(0.01 * (i * i + j * j - i * j))
            Next
        Next
        Dim mFft = Matrix.Create(Of Complex(Of Double))(m.RowCount, m.ColumnCount)

        Dim fft2 = Fft2D(Of Double).CreateReal(m.RowCount, m.ColumnCount)
        fft2.ForwardTransform(m, mFft)

        Console.WriteLine("First few terms of fft(m):")
        For i As Integer = 0 To 3
            Dim comma As String = String.Empty
            For j As Integer = 0 To 3
                Console.Write(comma)
                Console.Write("{0}", mFft(i, j).ToString("F4"))
                comma = ", "
            Next
            Console.WriteLine()
        Next

        ' and the backward transform:
        fft2.BackwardTransform(mFft, m)

        ' Once again, we need to dispose the FFT implementation:
        fft2.Dispose


    End Sub

End Module
