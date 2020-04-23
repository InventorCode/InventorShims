
AddVbFile "IpropertyShim.vb"
AddVbFile "TestCode/SpeedTest.vb"
AddVbFile "TestCode/Timer.vb"


'Module SpeedTestMain '<CollectorPrepend>'</CollectorPrepend>
    Public Sub Main ()
        
        Dim test As SpeedTest = New SpeedTest()

        test.AddTest("GetPropsInfo", AddressOf TestOne)
        test.AddTest("TryCatch", AddressOf TestTwo)
        test.Iterations = 1000000
        test.RunTests()
        test.ShowResultsInDialog()

    End Sub

    Public Sub TestOne()
        IpropertyShim.GetProperty(InventorApp.Application.ActiveDocument, "Sheet")
    End Sub

    Public Sub TestTwo()
        IpropertyShim.GetProperty2(InventorApp.Application.ActiveDocument, "Sheet")
    End Sub

'End Module '<CollectorPrepend>'</CollectorPrepend>

''' <summary>
''' This object is a shared class pointing to the Inventor.Application object.
''' </summary>
Public Class InventorApp
    Public Shared Property Application As Inventor.Application = GetObject(, "Inventor.Application")
End Class
