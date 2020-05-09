AddVbFile "PropertyShim.vb"

'Run the PropertyShim functions from inventor's ilogic environment.
Sub Main()

    MsgBox(PropertyShim.GetProperty(ThisApplication.ActiveDocument, "Title"))

    MsgBox(PropertyShim.GetProperty(ThisApplication.ActiveDocument, "Inventor Summary Information", "Title"))

    MsgBox(PropertyShim.GetProperty(ThisApplication.ActiveDocument, "Inventor Summary Information", "Failure"))

End Sub