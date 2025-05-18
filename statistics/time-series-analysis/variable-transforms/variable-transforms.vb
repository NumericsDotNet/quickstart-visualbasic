'=====================================================================
'
'  File: variable-transforms.vb
'
'---------------------------------------------------------------------
'
'  This file is part of the Numerics.NET Code Samples.
'
'  Copyright (c) 2004-2025 ExoAnalytics Inc. All rights reserved.
'
'=====================================================================

Option Infer On

Imports Numerics.NET.Data.Text
Imports Numerics.NET.DataAnalysis
Imports Numerics.NET
Imports Numerics.NET.Statistics
Imports Numerics.NET.Statistics.TimeSeriesAnalysis

    ' Illustrates various kinds of transformations of numerical variables
    ' by showing how to compute several financial indicators.
    Module VariableTransforms

        Sub Main()

        ' The license is verified at runtime. We're using
        ' a 30 day trial key here. For more information, see
        '     https://numerics.net/trial-key
        Numerics.NET.License.Verify("your-trial-key-here")

        ' We load the data into a data frame with a DateTime row index
        Dim timeSeries = DelimitedTextFile.ReadDataFrame(Of DateTime)(
                "..\..\..\..\..\Data\MicrosoftStock.csv", "Date")

        Dim open = timeSeries("Open").As(Of Double)
        Dim close = timeSeries("Close").As(Of Double)
        Dim high = timeSeries("High").As(Of Double)
        Dim low = timeSeries("Low").As(Of Double)
        Dim volume = timeSeries("Volume").As(Of Double)

        '
        ' Arithmetic operations
        '

        ' The NumericalVariable class defines the standard
        ' arithmetic operators. Operands can be either
        ' numerical variables or constants.

        ' The Typical Price (TP) is the average of the day's high, low and close:
        Dim TP = (high + low + close) / 3.0

        ' Exponentiation is available through the ElementwisePow method,
        ' with special cases for square roots and reciprocals:
        Dim inverseVolume = volume.Reciprocal

        '
        ' Simple transformations
        '

        ' The Transforms property of a numerical variable gives access
        ' to a large number of transformations.

        ' The GetLag method returns a variable whose observations
        ' are moved ahead by the specified amount:
        Dim close1 = close.Lag(1)
        ' You can get cumulative sums and products:
        Dim cumVolume = volume.CumulativeSum

        '
        ' Indicators of change
        '

        ' You can get the absolute change, percent change,
        ' or (exponential) growth rate of a variable. The optional
        ' parameter is the number of periods to go back.
        ' The default is 1.
        Dim closeChange = close.Change(10)

        ' You can extrapolate the change to a longer number of periods.
        ' The additional argument is the number of large periods.
        Dim monthyChange = close.ExtrapolatedChange(10, 20)

        '
        ' Moving averages
        '

        ' You can get simple, exponential, and weighted moving averages.
        Dim MA20 = close.MovingAverage(20)

        ' Weighted moving averages can use either a fixed array or vector
        ' to specify the weight. The weights are automatically normalized.
        Dim weights As Double() = {1.0, 2.0, 3.0}
        Dim WMA3 = close.WeightedMovingAverage(weights)
        ' You can also specify another variable for the weights.
        ' In this case, the corresponding observations are used.
        ' For example, to obtain the volume weighted average
        ' of the close price over a 14 day period, you can write:
        Dim VWA14 = close.WeightedMovingAverage(14, volume)

        ' Other statistics, such as maximum, minimum and standard
        ' deviation are also available.

        '
        ' Misc. transforms
        '

        ' The Box-Cox transform is often used to reduce the effects
        ' of non-normality of a variable. It takes one parameter,
        ' which must be between 0 and 1.
        Dim bcVolume = volume.BoxCoxTransform(0.4)

        '
        ' Creating more complicated indicators
        '

        ' All these transformations can be combined to create
        ' more complicated transformations. We give some examples
        ' of common Technical Analysis indicators.

        ' The Accumulation Distribution is a leading indicator of price movements.
        ' It is used in many other indicators.
        ' The formula uses only arithmetic operations:
        Dim AD = Vector.ElementwiseMultiply(
                Vector.ElementwiseDivide(close - open, high - low),
                volume)

        ' The Chaikin oscillator is used to monitor the flow of money into
        ' and out of a market.  It is the difference between a 3 day and a 10 day
        ' moving average of the Accumulation Distribution.
        ' We use the GetExponentialMovingAverage method for this purpose.
        Dim CO = AD.ExponentialMovingAverage(3) - AD.ExponentialMovingAverage(10)

        ' Bollinger bands provide an envelope around the price that indicates
        ' whether the current price level is relatively high or low.
        ' It uses a 20 day simple average as a central line:
        Dim TPMA20 = TP.MovingAverage(20)
        ' The actual bands are at 2 standard deviations (over the same period)
        ' from the central line. We have to pass the moving average
        ' over the same period as the second parameter.
        Dim SD20 = TP.MovingStandardDeviation(20, TPMA20)
        Dim BOLU = MA20 + 2 * SD20
        Dim BOLD = MA20 - 2 * SD20

        ' The Relative Strength Index is an index that compares
        ' the average price gain to the average loss.
        ' The GetPositiveToNegativeIndex method performs this
        ' calculation in one operation. The first argument is the period.
        ' The second argument is the variable that determines
        ' if an observation counts towards the plus or the minus side.
        Dim change = close.Change(1)
        Dim RSI = change.PositiveToNegativeIndex(14, change)

        ' Finally, let's print some of our results:
        Dim index As Integer = timeSeries.RowIndex.Lookup(New DateTime(2002, 9, 17))
        Console.WriteLine("Data for September 17, 2002:")
        Console.WriteLine($"Acumulation Distribution (in millions): {AD(index) / 1000000:F2}")
        Console.WriteLine($"Chaikin Oscillator (in millions): {CO(index) / 1000000:F2}")
        Console.WriteLine($"Bollinger Band (Upper): {BOLU(index):F2}")
        Console.WriteLine($"Bollinger Band (Central): {TPMA20(index):F2}")
        Console.WriteLine($"Bollinger Band (Lower): {BOLD(index):F2}")
        Console.WriteLine($"Relative Strength Index: {RSI(index):F2}")

    End Sub

End Module
