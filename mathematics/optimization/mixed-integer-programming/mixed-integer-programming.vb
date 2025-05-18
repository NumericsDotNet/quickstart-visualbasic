'=====================================================================
'
'  File: mixed-integer-programming.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

' The linear programming classes reside in their own namespace.
Imports Numerics.NET.Optimization
' Vectors and matrices are in the Numerics.NET namespace
Imports Numerics.NET
Imports Numerics.NET.LinearAlgebra

    Module MixedIntegerProgramming

        ' Illustrates solving mixed integer programming problems
        ' using the classes in the Numerics.NET.Optimization
        ' namespace of Numerics.NET.
        Sub Main()
        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' In this QuickStart sample, we'll use the Mixed Integer
        ' programming capabilities to solve Sudoku puzzles.
        ' The rules of Sudoku will be4 expressed in terms of
        ' linear constraints on binary variables.

        ' First, create an empty linear program.
        Dim lp As New LinearProgram()

        ' Create an array of binary variables that indicate whether
        ' the cell at a specific row and column contain a specific digit.
        ' - The first index corresponds to the row.
        ' - The second index corresponds to the column.
        ' - The third index corresponds to the digit.
        Dim variables(8, 8, 8) As LinearProgramVariable

        ' Create a binary variable for each digit in each row and column.
        ' The AddBinaryVariable method creates a variable that can have values of 0 or 1.
        For row As Integer = 0 To 8
            For column As Integer = 0 To 8
                For digit As Integer = 0 To 8
                    Dim name As String = String.Format("x{0}{1}{2}", row, column, digit)
                    variables(row, column, digit) = lp.AddBinaryVariable(name, 0.0)
                Next
            Next
        Next

        ' To add integer variables, you can use the AddIntegerVariable method.
        ' To add real variables, you can use the AddVariable method.

        ' Now add constraints that represent the rules of Sudoku.

        ' There are 4 rules in Sudoku. They are all of the kind
        ' where only one of a certain set of combinations
        ' of (row, column, digit) can occur at the same time.
        ' We can express this by stating that the sum of the corresponding
        ' binary variables must be one.

        ' AddConstraints is a helper function defined below.
        ' For each combination of the first two arguments,
        ' it builds a constraint by iterating over the third argument.

        ' Rule 1: each posiion contains exactly one digit
        AddConstraints(lp, Function(row, column, digit) variables(row, column, digit))
        ' Rule 2: each digit appears once in each row
        AddConstraints(lp, Function(row, digit, column) variables(row, column, digit))
        ' Rule 3: each digit appears once in each column
        AddConstraints(lp, Function(column, digit, row) variables(row, column, digit))
        ' Rule 4: each digit appears exactly once in each block
        AddConstraints(lp, Function(block, digit, index) _
                variables(3 * (block Mod 3) + (index Mod 3), 3 * (block \ 3) + (index \ 3), digit))

        ' We represent the board with a 9x9 sparse matrix.
        ' The nonzero entries correspond to the numbers
        ' already on the board.

        ' Let's see if we can solve "the world's hardest Sudoku" puzzle:
        ' http:'www.mirror.co.uk/fun-games/sudoku/2010/08/19/world-s-hardest-sudoku-can-you-solve-dr-arto-inkala-s-puzzle-115875-22496946/
        Dim rows() = {0, 0, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 8, 8}
        Dim columns() = {2, 3, 0, 7, 1, 4, 6, 0, 5, 6, 1, 4, 8, 2, 3, 7, 1, 3, 8, 2, 7, 5, 6}
        Dim digits() As Double = {5, 3, 8, 2, 7, 1, 5, 4, 5, 3, 1, 7, 6, 3, 2, 8, 6, 5, 9, 4, 3, 9, 7}
        Dim board = Matrix.CreateSparse(9, 9, rows, columns, digits)

        ' Now fix the variables for the for the digits that are already on the board.
        ' We do this by setting the lower bound equal to the upper bound:
        For Each triplet In board.NonzeroElements
            variables(triplet.Row, triplet.Column, CInt(triplet.Value) - 1).LowerBound = 1.0
        Next

        ' Solve the linear program.
        Dim solution = lp.Solve()

        ' Scan the variables and print the digit if the value is 1.
        For row As Integer = 0 To 8
            For column As Integer = 0 To 8
                Dim theDigit As Integer = 0
                For digit As Integer = 0 To 8
                    If (variables(row, column, digit).Value = 1.0) Then
                        theDigit = digit + 1
                        Exit For
                    End If
                Next
                Console.Write(If(theDigit > 0, theDigit.ToString(), "."))
            Next
            Console.WriteLine()
        Next


    End Sub

    ' Helper function that creates a constraint:
    Dim coefficients() As Double = {1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0}
    Sub AddConstraints(lp As LinearProgram, variable As Func(Of Integer, Integer, Integer, LinearProgramVariable))
        For i As Integer = 0 To 8
            For j As Integer = 0 To 8
                Dim variables(8) As LinearProgramVariable
                For k As Integer = 0 To 8
                    variables(k) = variable(i, j, k)
                Next
                lp.AddLinearConstraint(variables, coefficients, ConstraintType.Equal, 1.0)
            Next
        Next
    End Sub

End Module
