'=====================================================================
'
'  File: nonlinear-systems.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

' The optimization classes reside in the
' Numerics.NET.Optimization namespace.
Imports Numerics.NET.EquationSolvers
' Vectors and Function delegates reside in the Numerics.NET
' namespace.
Imports Numerics.NET
Imports Numerics.NET.Algorithms

    ' Illustrates solving systems of non-linear equations using
    ' classes in the Numerics.NET.EquationSolvers namespace
    ' of Numerics.NET.
    Module NonlinearSystems

        Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        '
        ' Target function
        '

        ' The function we are trying to solve can be provided
        ' on one of two ways. The first is as an array of
        ' Func(Of Vector, Double) delegates. See the end of this
        ' sample for definitions of the methods that are referenced here.
        Dim f As Func(Of Vector(Of Double), Double)() = {AddressOf f1, AddressOf f2}
        ' We can also supply the Jacobian, which is the matrix of partial
        ' derivatives. We do so by providing the gradient of each target
        ' function as a Func(Of Vector, Vector, Vector) delegate.
        '
        ' The Func(Of Vector, Vector, Vector) takes a second argument:
        ' the vector that is to hold the return value. This avoids unnecessary
        ' creation of new Vector instances.
        Dim df As Func(Of Vector(Of Double), Vector(Of Double), Vector(Of Double))() =
                {AddressOf df1, AddressOf df2}

        ' The initial values are supplied as a vector:
        Dim initialGuess = Vector.Create(0.5, 0.5)

        '
        ' Newton-Raphson Method
        '

        ' The Newton-Raphson method is implemented by
        ' the NewtonRaphsonSystemSolver class.
        Dim solver As New NewtonRaphsonSystemSolver(f, df, initialGuess)

        ' and call the Solve method to obtain the solution:
        Dim solution = solver.Solve()

        Console.WriteLine("N-dimensional Newton-Raphson Solver:")
        Console.WriteLine("exp(x)*cos(y) - x^2 + y^2 = 0")
        Console.WriteLine("exp(x)*sin(y) - 2xy = 0")
        Console.WriteLine($"  Initial guess: {initialGuess:F2}")
        ' The Status property indicates
        ' the result of running the algorithm.
        Console.WriteLine($"  Status: {solver.Status}")
        ' The result is also available through the
        ' Result property.
        Console.WriteLine($"  Solution: {solver.Result}")
        Console.WriteLine($"  Function value: {solver.ValueTest.Error}")
        ' You can find out the estimated error of the result
        ' through the EstimatedError property:
        Console.WriteLine($"  Estimated error: {solver.EstimatedError}")
        Console.WriteLine($"  # iterations: {solver.IterationsNeeded}")
        Console.WriteLine($"  # evaluations: {solver.EvaluationsNeeded}")

        ' You can use Automatic Differentiation to compute the Jacobian.
        ' To do so, set the target functions using the SetSymbolicTargetFunctions
        ' method:
        solver = New NewtonRaphsonSystemSolver()
        solver.InitialGuess = initialGuess

        solver.SetSymbolicTargetFunctions(
            Function(x) Math.Exp(x(0)) * Math.Cos(x(1)) - x(0) * x(0) + x(1) * x(1),
            Function(x) Math.Exp(x(0)) * Math.Sin(x(1)) - 2 * x(0) * x(1))

        solution = solver.Solve()
        Console.WriteLine("Using Automatic Differentiation:")
        Console.WriteLine($"  Solution: {solver.Result}")
        Console.WriteLine($"  Function value: {solver.ValueTest.Error}")
        Console.WriteLine($"  # iterations: {solver.IterationsNeeded}")

        ' When you don't have the Jacobian for the target functions
        ' and you don't use Automatic Differentiation, the equation solver
        ' will use a numerical approximation.

        '
        ' Controlling the process
        '
        Console.WriteLine("Same with modified parameters:")
        ' You can set the maximum # of iterations:
        ' If the solution cannot be found in time, the
        ' Status will return a value of
        ' IterationStatus.IterationLimitExceeded
        solver.MaxIterations = 10

        ' The ValueTest property returns the convergence
        ' test based on the function value. We can set
        ' its tolerance property:
        solver.ValueTest.Tolerance = 0.0000000001
        ' Its Norm property determines how the error
        ' is calculated. Here, we choose the maximum
        ' of the function values:
        solver.ValueTest.Norm = VectorConvergenceNorm.Maximum

        ' The SolutionTest property returns the test
        ' on the change in location of the solution.
        solver.SolutionTest.Tolerance = 0.0000000001
        ' You can specify how convergence is to be tested
        ' through the ConvergenceCriterion property:
        solver.SolutionTest.ConvergenceCriterion = ConvergenceCriterion.WithinRelativeTolerance

        solver.InitialGuess = initialGuess
        solution = solver.Solve()
        Console.WriteLine($"  Status: {solver.Status}")
        Console.WriteLine($"  Solution: {solver.Result}")
        ' The estimated error will be less than 5e-14
        Console.WriteLine($"  Estimated error: {solver.SolutionTest.Error}")
        Console.WriteLine($"  # iterations: {solver.IterationsNeeded}")
        Console.WriteLine($"  # evaluations: {solver.EvaluationsNeeded}")

        '
        ' Powell's dogleg method
        '

        ' The dogleg method is more robust than Newton's method.
        ' It will converge often when Newton's method fails.
        Dim dogleg As New DoglegSystemSolver(f, df, initialGuess)

        ' Unique to the dogleg method is the TrustRegionRadius property.
        ' Any step of the algorithm is not larger than this value.
        ' It is adjusted at each iteration.
        dogleg.TrustRegionRadius = 0.5

        ' Call the Solve method to obtain the solution:
        solution = dogleg.Solve()

        Console.WriteLine("Powell's Dogleg Solver:")
        Console.WriteLine($"  Initial guess: {initialGuess:F2}")
        Console.WriteLine($"  Status: {dogleg.Status}")
        Console.WriteLine($"  Solution: {dogleg.Result}")
        Console.WriteLine($"  Estimated error: {dogleg.EstimatedError}")
        Console.WriteLine($"  # iterations: {dogleg.IterationsNeeded}")
        Console.WriteLine($"  # evaluations: {dogleg.EvaluationsNeeded}")

        ' The dogleg method can work without derivatives. Care is taken
        ' to keep the number of evaluations down to a minimum.
        dogleg.JacobianFunction = Nothing
        ' Call the Solve method to obtain the solution:
        solution = dogleg.Solve()

        Console.WriteLine("Powell's Dogleg Solver (no derivatives):")
        Console.WriteLine($"  Initial guess: {initialGuess:F2}")
        Console.WriteLine($"  Status: {dogleg.Status}")
        Console.WriteLine($"  Solution: {dogleg.Result}")
        Console.WriteLine($"  Estimated error: {dogleg.EstimatedError}")
        Console.WriteLine($"  # iterations: {dogleg.IterationsNeeded}")
        Console.WriteLine($"  # evaluations: {dogleg.EvaluationsNeeded}")

    End Sub

    ' First set of functions.
    Function f1(x As Vector(Of Double)) As Double
        Return Math.Exp(x(0)) * Math.Cos(x(1)) - x(0) * x(0) + x(1) * x(1)
    End Function

    Function f2(x As Vector(Of Double)) As Double
        Return Math.Exp(x(0)) * Math.Sin(x(1)) - 2 * x(0) * x(1)
    End Function

    ' Gradient of the first set of functions.
    Function df1(x As Vector(Of Double), df As Vector(Of Double)) As Vector(Of Double)
        df(0) = Math.Exp(x(0)) * Math.Cos(x(1)) - 2 * x(0)
        df(1) = -Math.Exp(x(0)) * Math.Sin(x(1)) + 2 * x(1)
        Return df
    End Function

    Function df2(x As Vector(Of Double), df As Vector(Of Double)) As Vector(Of Double)
        df(0) = Math.Exp(x(0)) * Math.Sin(x(1)) - 2 * x(1)
        df(1) = Math.Exp(x(0)) * Math.Cos(x(1)) - 2 * x(0)
        Return df
    End Function

End Module
