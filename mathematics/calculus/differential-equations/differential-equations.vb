'=====================================================================
'
'  File: differential-equations.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

Imports System
Imports Numerics.NET.Calculus.OrdinaryDifferentialEquations
' We'll also need the basic math types:
Imports Numerics.NET

' <summary>
' Illustrates integrating systems of ordinary differential equations
' (ODE's) using classes in the
' Numerics.NET.Calculus.OrdinaryDifferentialEquations
' namespace of Numerics.NET.
Module DifferentialEquations

    Sub Main()

        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' The ClassicRungeKuttaIntegrator class implements the
        ' well-known 4th order fixed step Runge-Kutta method.
        Dim rk4 As New ClassicRungeKuttaIntegrator()

        ' The differential equation is expressed in terms of a
        ' DifferentialFunction delegate. This is a function that
        ' takes a double (time value) and two Vectors (y value and
        ' return value)  as arguments.
        '
        ' The Lorentz function below defines the differential function
        ' for the Lorentz attractor.
        rk4.DifferentialFunction = AddressOf Lorentz

        ' To perform the computations, we need to set the initial time...
        rk4.InitialTime = 0.0
        ' and the initial value.
        rk4.InitialValue = Vector.Create(1.0, 0.0, 0.0)
        ' The Runge-Kutta integrator also requires a step size:
        rk4.InitialStepsize = 0.1

        Console.WriteLine("Classic 4th order Runge-Kutta")
        For i As Integer = 0 To 5
            Dim t As Double = 0.2 * i
            ' The Integrate method performs the integration.
            ' It takes as many steps as necessary up to
            ' the specified time and returns the result:
            Dim y = rk4.Integrate(t)
            ' The IterationsNeeded always shows the number of steps
            ' needed to arrive at the final time.
            Console.WriteLine("{0:F2}: {1,20:F14} ({2} steps)", t, y, rk4.IterationsNeeded)
        Next

        ' The RungeKuttaFehlbergIntegrator class implements a variable-step
        ' Runge-Kutta method. This method chooses the stepsize, and so
        ' is generally more reliable.
        Dim rkf45 As New RungeKuttaFehlbergIntegrator()

        rkf45.InitialTime = 0.0
        rkf45.InitialValue = Vector.Create(1.0, 0.0, 0.0)
        rkf45.DifferentialFunction = AddressOf Lorentz
        rkf45.AbsoluteTolerance = 0.00000001

        Console.WriteLine("Classic 4/5th order Runge-Kutta-Fehlberg")
        For i As Integer = 0 To 5
            Dim t As Double = 0.2 * i
            Dim y = rkf45.Integrate(t)
            Console.WriteLine("{0:F2}: {1,20:F14} ({2} steps)", t, y, rkf45.IterationsNeeded)
        Next

        ' The CVODE integrator, part of the SUNDIALS suite of ODE solvers,
        ' is the most advanced of the ODE integrators.
        Dim cvode As New CvodeIntegrator()

        cvode.InitialTime = 0.0
        cvode.InitialValue = Vector.Create(1.0, 0.0, 0.0)
        cvode.DifferentialFunction = AddressOf Lorentz
        cvode.AbsoluteTolerance = 0.00000001

        Console.WriteLine("CVODE (multistep Adams-Moulton)")
        For i As Integer = 0 To 5
            Dim t As Double = 0.2 * i
            Dim y = cvode.Integrate(t)
            Console.WriteLine("{0:F2}: {1,20:F14} ({2} steps)", t, y, cvode.IterationsNeeded)
        Next

        '
        ' Other properties and methods
        '

        ' The IntegrateSingleStep method takes just a single step
        ' in the direction of the specified final time:
        cvode.IntegrateSingleStep(2.0)
        ' The CurrentTime property returns the corresponding time value.
        Console.WriteLine($"t after single step: {cvode.CurrentTime}")
        ' CurrentValue returns the corresponding value:
        Console.WriteLine($"Value at this t: {cvode.CurrentValue:F14}")
        ' The IntegrateMultipleSteps method performs the integration
        ' until at least the final time, and returns the last
        ' value that was computed:
        cvode.IntegrateMultipleSteps(2.0)
        Console.WriteLine($"t after multiple steps: {cvode.CurrentTime}")

        '
        ' Stiff systems
        '

        Console.WriteLine(Environment.NewLine + "Stiff systems")

        ' The CVODE integrator is the only ODE integrator that can
        ' handle stiff problems well. The following example uses
        ' an equation for the size of a flame. The smaller
        ' the initial size, the more stiff the equation is.

        ' We compare the performance of the variable step Runge-Kutta method
        ' and the CVODE integrator:

        Dim delta As Double = 0.0001
        Dim tFinal As Double = 2 / delta

        rkf45 = New RungeKuttaFehlbergIntegrator()
        rkf45.InitialTime = 0.0
        rkf45.InitialValue = Vector.Create(delta)
        rkf45.DifferentialFunction = AddressOf Flame

        Console.WriteLine("Classic 4/5th order Runge-Kutta-Fehlberg")
        For i As Integer = 0 To 10
            Dim t As Double = i * 0.1 * tFinal
            Dim y = rkf45.Integrate(t)
            Console.WriteLine("{0:F2}: {1,20:F14} ({2} steps)", t, y, rkf45.IterationsNeeded)
        Next

        ' The CVODE integrator will use a special (implicit) method
        ' for stiff problems. To select this method, pass
        ' EdeKind.Stiff as an argument to the constructor.
        cvode = New CvodeIntegrator(OdeKind.Stiff)
        cvode.InitialTime = 0.0
        cvode.InitialValue = Vector.Create(delta)
        cvode.DifferentialFunction = AddressOf Flame
        ' When solving stiff problems, a Jacobian for the system
        ' must be supplied. See below for the definition.
        cvode.SetDenseJacobian(AddressOf FlameJacobian)

        ' Notice how the CVODE integrator takes a lot fewer steps,
        ' and is also more accurate than the variable-step
        ' Runge-Kutta method.
        Console.WriteLine("CVODE (Stiff - Backward Differentiation Formula)")
        For i As Integer = 0 To 10
            Dim t As Double = i * 0.1 * tFinal
            Dim y = cvode.Integrate(t)
            Console.WriteLine("{0:F2}: {1,20:F14} ({2} steps)", t, y, cvode.IterationsNeeded)
        Next

    End Sub

    ' <summary>
    ' Represents the differential function for the Lorentz attractor.
    ' </summary>
    ' <param name="t">The time value.</param>
    ' <param name="y">The current value.</param>
    ' <param name="dy">On output, the first derivatives.</param>
    ' <returns>A reference to <paramref name="dy"/>.</returns>
    ' <remarks><paramref name="dy"/> may be <see langword="nothing"/>
    ' on input.</remarks>
    Function Lorentz(t As Double, y As Vector(Of Double), dy As Vector(Of Double)) As Vector(Of Double)
        If (dy = Nothing) Then
            dy = Vector.Create(Of Double)(3)
        End If

        Const sigma = 10.0
        Const beta = 8.0 / 3.0
        Const rho = 28.0

        dy(0) = sigma * (y(1) - y(0))
        dy(1) = y(0) * (rho - y(2)) - y(1)
        dy(2) = y(0) * y(1) - beta * y(2)

        Return dy
    End Function

    ' <summary>
    ' Represents the differential function for the flame expansion
    ' problem.
    ' </summary>
    Function Flame(t As Double, y As Vector(Of Double), dy As Vector(Of Double)) As Vector(Of Double)
        If (dy = Nothing) Then
            dy = Vector.Create(Of Double)(1)
        End If

        dy(0) = y(0) * y(0) * (1 - y(0))

        Return dy
    End Function

    ' <summary>
    ' Represents the Jacobian of the differential function
    ' for the flame expansion problem.
    ' </summary>
    ' <param name="t">The time value.</param>
    ' <param name="y">The current value.</param>
    ' <param name="dy">The current value of the first derivatives.</param>
    ' <param name="J">A Matrix that, on output, contains the value
    ' of the Jacobian.</param>
    ' <returns>A reference to <paramref name="J"/>.</returns>
    ' <remarks>The Jacobian is the matrix of partial derivatives of each
    ' equation in the system with respect to each variable in the system.
    ' <paramref name="J"/> may be <see langword="nothing"/>
    ' on input.</remarks>
    Function FlameJacobian(t As Double, y As Vector(Of Double),
dy As Vector(Of Double), J As Matrix(Of Double)) As Matrix(Of Double)

        If (J = Nothing) Then
            J = Matrix.Create(Of Double)(1, 1)
        End If

        J(0, 0) = (2 - 3 * y(0)) * y(0)

        Return J
    End Function

End Module
