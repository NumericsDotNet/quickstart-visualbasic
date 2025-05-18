'=====================================================================
'
'  File: nonlinear-curve-fitting.vb
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
' The predefined non-linear curves reside in the
' Numerics.NET.Curves namespace.
Imports Numerics.NET.Curves.Nonlinear
' Vectors reside in the Numerics.NET.Mathemaics.LinearAlgebra
' namespace
Imports Numerics.NET
Imports Numerics.NET.Algorithms
Imports Numerics.NET.LinearAlgebra

    ' Illustrates nonlinear least squares curve fitting using the
    ' NonlinearCurveFitter class in the
    ' Numerics.NET.Curves namespace of Numerics.NET.
    Module NonlinearCurveFitting

        Sub Main()

        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' Nonlinear least squares fits are calculated using the
        ' NonlinearCurveFitter class:
        Dim fitter As New NonlinearCurveFitter

        ' In the first example, we fit a dose response curve
        ' to a data set that includes error information.

        ' The data points must be supplied as Vector objects:
        Dim dose = Vector.Create(1.46247, 2.3352,
                4, 7, 12, 18, 23, 30, 40, 60, 90, 160, 290, 490, 860)
        Dim response = Vector.Create(95.49073, 95.14551, 94.86448,
                92.66762, 85.36377, 74.72183, 62.76747, 51.04137, 38.20257,
                28.01712, 19.40086, 13.18117, 9.87161, 7.64622, 7.21826)
        Dim errors = Vector.Create(4.74322, 4.74322, 4.74322,
                4.63338, 4.26819, 3.73609, 3.13837, 3.55207, 3.91013,
                2.40086, 2.6, 3.65906, 2.49358, 2.38231, 2.36091)

        ' You must supply the curve whose parameters will be
        ' fit to the data. The curve must inherit from NonlinearCurve.
        ' The FourParameterLogistic curve is one of several
        ' predefined nonlinear curves:
        Dim doseResponseCurve As FourParameterLogisticCurve _
                = New FourParameterLogisticCurve

        ' Now we set the curve fitter's Curve property:
        fitter.Curve = doseResponseCurve
        ' The GetInitialFitParameters method computes
        ' initial values appropriate for the data:
        fitter.InitialGuess = doseResponseCurve.GetInitialFitParameters(dose, response)

        ' and the data values:
        fitter.XValues = dose
        fitter.YValues = response
        ' The GetWeightVectorFromErrors method of the WeightFunctions
        ' class lets us convert the error values to weights:
        fitter.WeightVector = WeightFunctions.GetWeightVectorFromErrors(errors)

        ' The Fit method performs the actual calculation.
        fitter.Fit()
        ' The standard deviations associated with each parameter
        ' are available through the GetStandardDeviations method.
        Dim s = fitter.GetStandardDeviations()

        ' We can now print the results:
        Console.WriteLine("Dose response curve")

        Console.WriteLine("Initial value: {0,10:F6} +/- {1:F4}", doseResponseCurve.InitialValue, s(0))
        Console.WriteLine("Final value:   {0,10:F6} +/- {1:F4}", doseResponseCurve.FinalValue, s(1))
        Console.WriteLine("Center:        {0,10:F6} +/- {1:F4}", doseResponseCurve.Center, s(2))
        Console.WriteLine("Hill slope:    {0,10:F6} +/- {1:F4}", doseResponseCurve.HillSlope, s(3))

        ' We can also show some statistics about the calculation:
        Console.WriteLine($"Residual sum of squares: {fitter.Residuals.Norm()}")
        ' The Optimizer property returns the MultidimensionalOptimization object
        ' used to perform the calculation:
        Console.WriteLine($"# iterations: {fitter.Optimizer.IterationsNeeded}")
        Console.WriteLine($"# function evaluations: {fitter.Optimizer.EvaluationsNeeded}")

        Console.WriteLine()

        '
        ' Defining your own nonlinear curve
        '

        ' In this example, we use one of the datasets (MGH10)
        ' from the National Institute for Statistics and Technology
        ' (NIST) Statistical Reference Datasets.
        ' See http://www.itl.nist.gov/div898/strd for details

        ' Here, we need to define our own curve.
        ' The MyCurve class is defined below.
        fitter.Curve = New MyCurve

        ' For simple functions, you can use Automatic Differentiation
        ' to compute the derivative of the function and the partial
        ' derivatives with respect to the curve parameters.

        ' To do so, call the FromExpression method of the NonlinearCurve
        ' class with a lambda expression for the value of the function.
        ' The first argument of the lambda is the x value. The remaining
        ' arguments correspond to the curve parameters:
        fitter.Curve = NonlinearCurve.FromExpression(Function(x, a, b, c) a * Math.Exp(b / (x + c)))

        ' In this case, we have to specify the initial values
        ' for the parameters:
        fitter.InitialGuess = Vector.Create(0.2, 40000, 2500)

        ' The data is provided as Vector objects.
        ' X values go into the XValues property...
        fitter.XValues = Vector.Create(
                50.0, 55.0, 60.0, 65.0,
                70.0, 75.0, 80.0, 85.0,
                90.0, 95.0, 100.0, 105.0,
                110.0, 115.0, 120.0, 125.0)
        ' ...and Y values go into the YValues property:
        fitter.YValues = Vector.Create(
                34780.0, 28610.0, 23650.0, 19630.0,
                16370.0, 13720.0, 11540.0, 9744.0,
                8261.0, 7030.0, 6005.0, 5147.0,
                4427.0, 3820.0, 3307.0, 2872.0)

        fitter.WeightVector = Nothing
        ' The Fit method performs the actual calculation:
        fitter.Fit()

        ' A Vector containing the parameters of the best fit
        ' can be obtained through the
        ' BestFitParameters property.
        Dim solution = fitter.BestFitParameters
        s = fitter.GetStandardDeviations()

        Console.WriteLine("NIST Reference Data Set")
        Console.WriteLine("Solution:")
        Console.WriteLine($"b1: {solution(0),20} {s(0),20}")
        Console.WriteLine($"b2: {solution(1),20} {s(1),20}")
        Console.WriteLine($"b3: {solution(2),20} {s(2),20}")

        Console.WriteLine("Certified values:")
        Console.WriteLine($"b1: {0.005609636471,20} {0.00015687892471,20}")
        Console.WriteLine($"b2: {6181.3463463,20} {23.309021107,20}")
        Console.WriteLine($"b3: {345.22363462,20} {0.78486103508,20}")

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
        Console.WriteLine($"b1: {solution(0),20} {s(0),20}")
        Console.WriteLine($"b2: {solution(1),20} {s(1),20}")
        Console.WriteLine($"b3: {solution(2),20} {s(2),20}")

    End Sub

    ' This is our nonlinear curve implementation. For details, see
    ' http:'www.itl.nist.gov/div898/strd/nls/data/mgh10.shtml
    ' You must inherit from NonlinearCurve:
    Public Class MyCurve : Inherits NonlinearCurve
        ' Call the base constructor with the number of
        ' parameters.
        Public Sub New()
            MyBase.New(3)
            ' It is convenient to set common starting values
            ' for the curve parameters in the constructor:
            Me.Parameters(0) = 0.2
            Me.Parameters(1) = 40000
            Me.Parameters(2) = 2500
        End Sub

        ' The ValueAt method evaluates the function:
        Public Overrides Function ValueAt(x As Double) As Double
            Return Parameters(0) * Math.Exp(Parameters(1) / (x + Parameters(2)))
        End Function

        ' The SlopeAt method evaluates the derivative:
        Public Overrides Function SlopeAt(x As Double) As Double
            Return Parameters(0) * Parameters(1) * Math.Exp(Parameters(1) / (x + Parameters(2))) _
                 / Math.Pow(x + Parameters(2), 2)
        End Function

        ' The FillPartialDerivatives evaluates the partial derivatives
        ' with respect to the curve parameters, and returns
        ' the result in a vector. If you don't supply this method,
        ' a numerical approximation is used.
        Public Overrides Sub FillPartialDerivatives(x As Double, f As DenseVector(Of Double))
            Dim exp As Double = Math.Exp(Parameters(1) / (x + Parameters(2)))
            f(0) = exp
            f(1) = Parameters(0) * exp / (x + Parameters(2))
            f(2) = -Parameters(0) * Parameters(1) * exp / Math.Pow(x + Parameters(2), 2)
        End Sub

    End Class

End Module
