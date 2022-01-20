AddReference "InventorShims.dll"
Imports InventorShims.DocumentShim
Imports InventorShims.PropertyShim
Imports System.Linq

' #################################################################
' ###   Change Selected Part iProps w/ IEnumerable collection   ###
' #################################################################
'
' Changes the value of selected component "Title" iproperties. Works in a drawing or assembly file.
' This sample showcases chained IEnumerable methods w/ a fluent syntax.

Sub Main()

	Dim oApp As Inventor.Application = ThisApplication
    Dim oDoc As Document = ThisDoc.Document

	Dim oSSet As SelectSet = oApp.ActiveDocument.SelectSet 'get the current selection set

	'if nothing is selected... select something!
	If oSSet.count = 0 Then
		oSSet.Select(oApp.CommandManager.Pick(SelectionFilterEnum.kAllEntitiesFilter, "Select thing(s)..."))
	End If

	'if there are selected objects, enumerate through their associated documents and filter...
	oSSet.EnumerateDocuments() _
		.Where(Function(d) d.IsAssembly()) _
		.Where(Function(d) Not d.IsContentCenter()) _
		.Where(Function(d) d.IsModifiable()) _
		.Distinct() _
		.ToList() _
		.ForEach(Sub(d) SetPropertyValue(d, "Title", "Some Cool Title!"))

End Sub