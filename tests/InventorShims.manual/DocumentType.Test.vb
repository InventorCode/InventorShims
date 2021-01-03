'Imports InventorShims
Imports Inventor
AddVbFile "src-vb/DocumentType.vb"

'Run the PropertyShim functions from inventor's ilogic environment.
'Public Module Test
Sub Main()

    MsgBox("IsAssembly: " & DocumentType.IsAssembly(ThisApplication.ActiveDocument) & vbLf &
            "IsDrawing: " & DocumentType.IsDrawing(ThisApplication.ActiveDocument) & vbLf &
            "IsForeignModel: " & DocumentType.IsForeignModel(ThisApplication.ActiveDocument) & vbLf &
            "IsNest: " & DocumentType.IsNest(ThisApplication.ActiveDocument) & vbLf &
            "IsPart: " & DocumentType.IsPart(ThisApplication.ActiveDocument) & vbLf &
            "IsPresentation: " & DocumentType.IsPresentation(ThisApplication.ActiveDocument) & vbLf &
            "IsSAT: " & DocumentType.IsSAT(ThisApplication.ActiveDocument) & vbLf &
            "IsUnknown: " & DocumentType.IsUnknown(ThisApplication.ActiveDocument))

End Sub
'End Module
