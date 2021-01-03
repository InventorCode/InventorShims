'This is a speed test between several methods to access propertysets and properties.
'   Results: accessing properties By ID is slightly faster than accessing by name.
'   Results: accessing propertysets by name is almost 4x faster than accessing by the unique id.

'Module SpeedTestMain '<CollectorPrepend>'</CollectorPrepend>
    Public Sub Main ()
        
        Dim test As SpeedTest = New SpeedTest()

        test.AddTest("PropByName", AddressOf AccessPropByName)
        test.AddTest("PropByID", AddressOf AccessPropByID)
        test.AddTest("SetByName", AddressOf AccessPropertySetByName)
        test.AddTest("SetByUnique", AddressOf AccessPropertySetByUniqueID)

    test.Iterations = 100
    test.RunTests()
        test.ShowResultsInDialog()

    End Sub

    Public Sub AccessPropByName()
        Dim _doc As Inventor.Document = ThisApplication.ActiveDocument
        Dim propertySet As Inventor.PropertySet = _doc.PropertySets.Item("Design Tracking Properties")
        Dim propValue As Inventor.Property = propertySet.Item("Part Number")
    End Sub

    Public Sub AccessPropByID()
        Dim _doc As Inventor.Document = ThisApplication.ActiveDocument
        Dim propertySet As Inventor.PropertySet = _doc.PropertySets.Item("Design Tracking Properties")
        Dim propValue As Inventor.Property = propertySet.ItemByPropId(5)
    End Sub

    Public Sub AccessPropertySetByName()
        Dim _doc As Inventor.Document = ThisApplication.ActiveDocument
        Dim propertySet As Inventor.PropertySet = _doc.PropertySets.Item("Design Tracking Properties")
    End Sub

    Public Sub AccessPropertySetByUniqueID()
        Dim _doc As Inventor.Document = ThisApplication.ActiveDocument
        Dim propertySet As Inventor.PropertySet = _doc.PropertySets.Item("32853F0F-3444-11D1-9E93-0060B03C1CA6")
    End Sub

'End Module '<CollectorPrepend>'</CollectorPrepend>

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

