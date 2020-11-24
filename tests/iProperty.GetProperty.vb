'Imports InventorShims
Imports Inventor
AddVbFile "src-vb/iProperty.vb"

'Run the PropertyShim functions from inventor's ilogic environment.
'Public Module Test
    Sub Main()

    MsgBox(iProperty.GetProperty(ThisApplication.ActiveDocument, "Title"))

    MsgBox(iProperty.GetProperty(ThisApplication.ActiveDocument, "Inventor Summary Information", "Title"))

    MsgBox(iProperty.GetProperty(ThisApplication.ActiveDocument, "Inventor Summary Information", "Failure"))

End Sub
'End Module
