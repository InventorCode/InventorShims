Imports InventorShims
Imports Inventor
AddVbFile "src/DocumentShim.vb"

'Run the PropertyShim functions from inventor's ilogic environment.
'Public Module Test
Sub Main()

MsgBox("IsAssembly: " & DocumentShim.IsAssembly(ThisApplication.ActiveDocument) & vbLf & _
            "IsDrawing: " & DocumentShim.IsDrawing(ThisApplication.ActiveDocument) & vbLf & _
            "IsForeignModel: " & DocumentShim.IsForeignModel(ThisApplication.ActiveDocument) & vbLf & _
            "IsNest: " & DocumentShim.IsNest(ThisApplication.ActiveDocument) & vbLf & _
            "IsPart: " & DocumentShim.IsPart(ThisApplication.ActiveDocument) & vbLf & _    
            "IsPresentation: " & DocumentShim.IsPresentation(ThisApplication.ActiveDocument) & vbLf & _
            "IsSAT: " & DocumentShim.IsSAT(ThisApplication.ActiveDocument) & vbLf & _
            "IsUnknown: " & DocumentShim.IsUnknown(ThisApplication.ActiveDocument))

End Sub
'End Module
