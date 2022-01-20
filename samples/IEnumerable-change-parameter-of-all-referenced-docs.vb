AddReference "InventorShims.dll"
Imports InventorShims.DocumentShim
Imports InventorShims.ParameterShim
Imports System.Linq

' #####################################################
' ###   Change All Referenced Document Parameters   ###
' #####################################################
'
' Changes the value of almost all referenced files' parameter "param1".
' This sample showcases chained IEnumerable methods w/ a fluent syntax.

Sub Main()
	
	Dim oApp As Inventor.Application = ThisApplication
    Dim oDoc As Document = ThisDoc.Document

    'enumerate through every referenced document in the current file and filter...
    oDoc.EnumerateAllReferencedDocuments() _
        .RemoveNonNativeDocuments() _
        .RemovePresentationDocuments() _
        .Where(Function(d) d.IsModifiable) _
		.ToList() _
		.ForEach(Sub(d) SetParameterValue(d, "testing", "16", "cm", True))
	
End Sub
