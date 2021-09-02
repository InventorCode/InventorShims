'https://github.com/jordanrobot/SpeedTest	[master@2b1aee2]
'</Collector>
Public Class SpeedTest

    Public Delegate Sub CodeToExecute()
    Public Property Iterations As Integer = 1
    Private _results As String = ""
    Private _timers As Dictionary(Of Timer, CodeToExecute) = New Dictionary(Of Timer,CodeToExecute)

    Public Sub New()
    End Sub

    Public Sub AddTest(message As String, ByVal code As CodeToExecute)
        
        _timers.Add(New Timer(message), code)

    End Sub

    Public Sub RunTests()

        For Each timer As KeyValuePair(Of Timer, CodeToExecute) in _timers

           GC.WaitForPendingFinalizers
           GC.Collect

           timer.Key.StartTimer()
           For i = 0 To Iterations
               timer.Value.Invoke()
           Next i
           timer.Key.StopTimer()

           AppendResults(timer.Key.GetResults)
       Next timer
    End Sub

    Public Sub ShowResultsInDialog()
        System.Windows.Forms.Messagebox.Show(_results)
    End Sub

    Public Function GetResults() As String
        Return _results
    End Function

    Private Sub AppendResults(value As String)
        _results = _results & vbLf & value
    End Sub

End Class
