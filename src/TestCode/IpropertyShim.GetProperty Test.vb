AddVbFile "IpropertyShim.vb"

'Run the ipropertyShim functions from inventor's ilogic environment.
Sub Main()

    Dim doc As Inventor.Document = ThisApplication.ActiveDocument
    MsgBox(IpropertyShim.GetProperty(doc, "Title"))

End Sub