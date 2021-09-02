'</Collector>
Public Class Timer

    Private _stopwatch As Stopwatch = New Stopwatch()
    Private _timespan As TimeSpan
    Private _totalTime As String
    Private _message As String = ""
    Private _result As String = "Speed test has not been run."

    Public Sub New()
    End Sub

    Public Sub New(value As String)
        me._message = value
    End Sub

    Public Sub StartTimer()
        _stopwatch.Start()
    End Sub

    Public Sub StopTimer()
        _stopwatch.Stop()
        _timespan = _stopwatch.Elapsed
        _result = String.Format("{0:00}.{1:000} seconds", (_timespan.Hours * 3600) + (_timespan.Minutes*60) + _timespan.Seconds, _timespan.Milliseconds)
    End Sub

    Public Function GetResults() As String
        Return me._message & " Total Time: " & _result
    End Function

    Public Function GetMessage() As String
        Return me._message
    End Function
End Class