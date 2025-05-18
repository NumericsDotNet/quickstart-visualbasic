'=====================================================================
'
'  File: linear-curve-fitting.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

' The curve fitting classes reside in the
' Numerics.NET.Curves namespace.
Imports Numerics.NET.Curves
' Vectors and function delegates reside in the Numerics.NET.Mathemaics
' namespace
Imports Numerics.NET

' Illustrates least squares curve fitting of polynomials and
' other linear functions using the LinearCurveFitter class in the
' Numerics.NET.Curves namespace of Numerics.NET.
Module LinearCurveFitting

    Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' This QuickStart sample illustrates linear least squares
        ' curve fitting using polynomials and linear combinations
        ' of arbitrary functions.

        ' Linear least squares fits are calculated using the
        ' LinearCurveFitter class:
        Dim fitter As New LinearCurveFitter

        ' We use data from the National Institute for Standards
        ' and Technology's Statistical Reference Datasets library
        ' at http:'www.itl.nist.gov/div898/strd/.

        ' Note that, due to round-off error, the results here will not be exactly
        ' the same as the NIST results, which were calculated using 500 digits
        ' of precision!

        ' We use the 'Pontius' dataset, which contains measurement data
        ' from the calibration of load cells. The independent variable is the load.
        ' The dependent variable is the deflection.
        Dim deflectionData = Vector.Create(0.11019, 0.21956,
                0.32949, 0.43899, 0.54803, 0.65694, 0.76562, 0.87487, 0.98292,
                1.09146, 1.20001, 1.30822, 1.41599, 1.52399, 1.63194, 1.73947,
                1.84646, 1.95392, 2.06128, 2.16844, 0.11052, 0.22018, 0.32939,
                0.43885999999999997, 0.54798, 0.65739, 0.76596, 0.87474, 0.983, 1.0915, 1.20004,
                1.30818, 1.41613, 1.52408, 1.63159, 1.73965,
                1.84696, 1.95445, 2.06177, 2.16829)
        Dim loadData = Vector.Create(150.0, 300, 450, 600, 750, 900,
                1050, 1200, 1350, 1500, 1650, 1800,
                1950, 2100, 2250, 2400, 2550, 2700,
                2850, 3000, 150, 300, 450, 600,
                750, 900, 1050, 1200, 1350, 1500,
                1650, 1800, 1950, 2100, 2250, 2400,
                2550, 2700, 2850, 3000)

        ' You must supply the curve whose parameters will be
        ' fit to the data. The curve must inherit from LinearCombination.
        '
        ' Here, we use a quadratic polynomial:
        fitter.Curve = New Polynomial(2)

        ' The X values go into the XValues property:
        fitter.XValues = loadData
        ' ...and Y values go into the YValues property:
        fitter.YValues = deflectionData

        ' The Fit method performs the actual calculation:
        fitter.Fit()

        ' A Vector containing the parameters of the best fit
        ' can be obtained through the
        ' BestFitParameters property.
        Dim solution = fitter.BestFitParameters
        ' The standard deviations associated with each parameter
        ' are available through the GetStandardDeviations method.
        Dim s = fitter.GetStandardDeviations()

        Console.WriteLine("Calibration of load cells")
        Console.WriteLine("    deflection = c1 + c2*load + c3*load^2 ")
        Console.WriteLine("Solution:")
        Console.WriteLine($"c1: {solution(0),20:E10} {s(0),20:E10}")
        Console.WriteLine($"c2: {solution(1),20:E10} {s(1),20:E10}")
        Console.WriteLine($"c3: {solution(2),20:E10} {s(2),20:E10}")

        Console.WriteLine($"Residual sum of squares: {fitter.Residuals.Norm()}")

        ' Now let's redo the same operation, but with observations weighted
        ' by 1/Y^2. To do this, we set the WeightFunction property.
        ' The WeightFunctions class defines a set of ready-to-use weight functions.
        fitter.WeightFunction = WeightFunctions.OneOverYSquared
        ' Refit the curve:
        fitter.Fit()
        solution = fitter.BestFitParameters
        s = fitter.GetStandardDeviations()

        ' The solution is slightly different:
        Console.WriteLine("Solution (weighted observations):")
        Console.WriteLine($"c1: {solution(0),20:E10} {s(0),20:E10}")
        Console.WriteLine($"c2: {solution(1),20:E10} {s(1),20:E10}")
        Console.WriteLine($"c3: {solution(2),20:E10} {s(2),20:E10}")
        Console.WriteLine()

        '
        ' Fitting combinations of arbitrary functions
        '

        ' The following example estimates the two parameters, c1 and c2
        ' in the theoretical model for conductance:
        '     k(T) = 1 / (c1 / T + c2 * T*T)

        Dim temperature = Vector.Create(12.29, 13.75, 14.82,
                16.12, 18.04, 18.67, 20.52, 22.68, 25.15,
                27.72, 30.24, 33.21, 36.48, 39.86, 50.4)
        Dim conductance = Vector.Create(25.35, 27.88, 29.93,
                30.42, 31.0, 31.96, 32.47, 30.33, 31.14,
                27.46, 23.29, 20.72, 17.24, 14.71, 9.5)

        ' First, we transform the dependent variable:
        Dim y = Vector.Reciprocal(conductance)

        ' y is a linear combination of basis functions 1/T and T*T.
        ' Create a function basis object:
        Dim basisFunctions As Func(Of Double, Double)() = {AddressOf f1, AddressOf f2}
        Dim basis As New GeneralFunctionBasis(basisFunctions)

        ' Create a LinearCombination curve using this function basis:
        Dim myCurve As New LinearCombination(basis)

        ' Set the curve fitter properties:
        fitter.Curve = myCurve
        fitter.XValues = temperature
        fitter.YValues = y
        ' Reset the weights
        fitter.WeightFunction = Nothing
        fitter.WeightVector = Nothing

        ' Now compute the solution:
        fitter.Fit()
        solution = fitter.BestFitParameters
        s = fitter.GetStandardDeviations()

        ' Print the results
        Console.WriteLine("Conductance of copper: k(T) = 1 / (c1/T + c2*T^2)")
        Console.WriteLine("Solution:")
        Console.WriteLine($"c1: {solution(0),20:E10} {s(0),20:E10}")
        Console.WriteLine($"c2: {solution(1),20:E10} {s(1),20:E10}")

        Console.WriteLine($"Residual sum of squares: {fitter.Residuals.Norm()}")

    End Sub

    ' First basis function for the conductance sample.
    Function f1(x As Double) As Double
        Return 1 / x
    End Function

    ' Second basis function for the conductance sample.
    Function f2(x As Double) As Double
        Return x * x
    End Function

End Module
