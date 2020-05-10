Imports InventorShims
Imports Inventor
AddVbFile "src/PropertyShim.vb"

'Run the PropertyShim functions from inventor's ilogic environment.
'Public Module Test
    Sub Main()

        MsgBox(PropertyShim.GetProperty(ThisApplication.ActiveDocument, "Title"))

        MsgBox(PropertyShim.GetProperty(ThisApplication.ActiveDocument, "Inventor Summary Information", "Title"))

        MsgBox(PropertyShim.GetProperty(ThisApplication.ActiveDocument, "Inventor Summary Information", "Failure"))

    End Sub
'End Module
