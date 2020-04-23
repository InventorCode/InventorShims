AddVbFile "PropertyShim.vb"

'Run the PropertyShim functions from inventor's ilogic environment.
Sub Main()

    MsgBox(PropertyShim.GetProperty(ThisApplication.ActiveDocument, "Title"))

End Sub