'=====================================================================
'
'  File: basic-polynomials.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

' The Polynomial class resides in the Numerics.NET.Curves namespace.
Imports Numerics.NET.Curves

Module BasicPolynomials

    ' Illustrates the basic use of the Polynomial class in the
    ' Numerics.NET.Curve namespace of Numerics.NET.
    Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' All curves inherit from the Curve abstract base
        ' class. The Polynomial class overrides implements all
        ' the methods and properties of the Curve class,
        ' and adds a few more.

        ' Index variable.
        Dim index As Int32

        '
        ' Polynomial constructors
        '

        ' The Polynomial class has multiple constructors. Each
        ' constructor derives from a different way to define
        ' a polynomial or parabola.

        ' 1st option: a polynomial of a specified degree.
        Dim polynomial1 As New Polynomial(3)
        ' Now set the coefficients individually.
        ' The constant term has index 0:
        polynomial1.Coefficient(3) = 1
        polynomial1.Coefficient(2) = 1
        polynomial1.Coefficient(1) = 0
        polynomial1.Coefficient(0) = -2
        ' 2nd option: specify the coefficients in the constructor
        ' as an array of doubles:
        Dim coefficients = New Double() {1, 1, 0, -2}
        Dim polynomial2 As New Polynomial(coefficients)

        ' In addition, you can create a polynomial that
        ' has certain roots using the static FromRoots
        ' method:
        Dim roots = New Double() {1, 2, 3, 4}
        Dim polynomial3 As Polynomial = Polynomial.FromRoots(roots)
        ' Or you can construct the interpolating polynomial
        ' by calling the static GetInterpolatingPolynomial
        ' method. The parameters are two Double arrays
        ' containing the x values and y values respectively.
        Dim xValues = New Double() {1, 2, 3, 4}
        Dim yValues = New Double() {1, 4, 10, 8}
        Dim polynomial4 = Polynomial.GetInterpolatingPolynomial(xValues, yValues)

        ' The ToString method gives a common string
        ' representation of the polynomial:
        Console.WriteLine($"polynomial3 = {polynomial3.ToString()}")

        '
        ' Curve Parameters
        '

        ' The shape of any curve is determined by a set of parameters.
        ' These parameters can be retrieved and set through the
        ' Parameters collection. The number of parameters for a curve
        ' is given by this collection's Count property.
        '
        ' For polynomials, the parameters are the coefficients
        ' of the polynomial. The constant term has index 0:
        Console.WriteLine("polynomial1.ParameterCount = {0}",
                polynomial1.Parameters.Count)
        ' Parameters can easily be retrieved:
        Console.Write("polynomial1 parameters:")
        For index = 0 To polynomial1.Parameters.Count - 1
            Console.Write("{0} ", polynomial1.Parameters(index))
        Next
        Console.WriteLine()
        ' We can see that polynomial2 defines the same polynomial
        ' curve as polynomial1, but polynomial2 is different:
        Console.Write("polynomial2 parameters:")
        For index = 0 To polynomial2.Parameters.Count - 1
            Console.Write("{0} ", polynomial2.Parameters(index))
        Next
        Console.WriteLine()
        ' Parameters can also be set:
        polynomial1.Parameters(0) = 1

        ' The degree of the polynomial is returned by
        ' the Degree property:
        Console.WriteLine("Degree of polynomial3 = {0}",
                polynomial3.Degree)

        '
        ' Curve Methods
        '

        ' The ValueAt method returns the y value of the
        ' curve at the specified x value:
        Console.WriteLine("polynomial1.ValueAt(2) = {0}",
                polynomial1.ValueAt(2))

        ' The SlopeAt method returns the slope of the curve
        ' a the specified x value:
        Console.WriteLine("polynomial1.SlopeAt(2) = {0}",
                polynomial1.SlopeAt(2))

        ' You can also create a new curve that is the
        ' derivative of the original:
        Dim derivative = polynomial1.GetDerivative()
        Console.WriteLine("Slope at 2 (derivative) = {0}",
                derivative.ValueAt(2))
        ' For a polynomial, the derivative is a Quadratic curve
        ' if the degree is equal to three:
        Console.WriteLine("Type of derivative: {0}",
                derivative.GetType().FullName)
        Console.Write("Derivative parameters: ")
        For index = 0 To derivative.Parameters.Count - 1
            Console.Write("{0} ", derivative.Parameters(index))
        Next
        Console.WriteLine()
        ' If the degree is 4 or higher, the derivative is
        ' once again a polynomial:
        Console.WriteLine("Type of derivative for polynomial3: {0}",
                polynomial3.GetDerivative().GetType().FullName)

        ' You can get a Line that is the tangent to a curve
        ' at a specified x value using the TangentAt method:
        Dim tangent = polynomial1.TangentAt(2)
        Console.WriteLine("Tangent line at 2:")
        Console.WriteLine($"  Y-intercept = {tangent.Parameters(0)}")
        Console.WriteLine($"  Slope = {tangent.Parameters(1)}")

        ' For many curves, you can evaluate a definite
        ' integral exactly:
        Console.WriteLine("Integral of polynomial1 between 0 and 1 = {0}",
                polynomial1.Integral(0, 1))

        ' You can find the zeroes or roots of the curve
        ' by calling the FindRoots method. Note that this
        ' method only returns the real roots.
        roots = polynomial1.FindRoots()
        Console.WriteLine("Number of roots of polynomial1 = {0}",
                roots.Length)
        Console.WriteLine($"Value of root 1 = {roots(0)}")
        ' Let's find polynomial3's roots again:
        roots = polynomial3.FindRoots()
        Console.WriteLine("Number of roots of polynomial3: {0}",
                roots.Length)
        Console.WriteLine($"Value of root = {roots(0)}")
        Console.WriteLine($"Value of root = {roots(1)}")
        ' Root finding isn't an exact science. Note the
        ' round-off error in these values:
        Console.WriteLine($"Value of root = {roots(2)}")
        Console.WriteLine($"Value of root = {roots(3)}")

        ' For more advanced uses of the Polynomial class,
        ' see the AdvancedPolynomials QuickStart sample.


    End Sub

End Module
