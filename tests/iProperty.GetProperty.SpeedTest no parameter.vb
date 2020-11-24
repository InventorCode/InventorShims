AddVbFile "src-vb/iProperty.vb"
AddVbFile "tests/SpeedTest.vb"
AddVbFile "tests/Timer.vb"

'This is a speed test between several methods to access all iproperties within a propertyset.
'   Results: accessing all properties in a propertyset with GetPropertyInfo is faster than
'           iterating through all properties within that propertyset.

'Module SpeedTestMain '<CollectorPrepend>'</CollectorPrepend>
    Public Sub Main ()
        
        Dim test As SpeedTest = New SpeedTest()

    test.AddTest("iProperty.GetProperty", AddressOf Test1)
    'test.AddTest("Inventor API", AddressOf Test2)

    test.Iterations = 100
        test.RunTests()
        test.ShowResultsInDialog()

    End Sub


    Public Sub Test1()
        Dim _doc As Inventor.Document = ThisApplication.ActiveDocument
        Dim value As String
    value = iProperty.GetProperty(_doc, "Part Numberz")

End Sub

    Public Sub Test2()
        Dim _doc As Inventor.Document = ThisApplication.ActiveDocument
        Dim propertySet As Inventor.PropertySet = _doc.PropertySets.Item("Design Tracking Properties")
        Dim value As String
        value = propertySet.Item("Part Number").Value

    End Sub

'    End Module '<CollectorPrepend>'</CollectorPrepend>
