AddReference "InventorShims.dll"
Imports InventorShims.DocumentShim
Imports InventorShims.PropertyShim

' #######################################
' ###   Change Selected Part iProps   ###
' #######################################
'
' Changes the value of selected component "Title" iproperties. Works in a drawing or assembly file.
' This sample showcases the GetDocumentsFromSelectSet(), IsAssembly(), IsDrawing(), and SetPropertyValue() methods.

Sub Main()

	Dim oApp As Inventor.Application = ThisApplication
    Dim oDoc As Document = ThisDoc.Document
    Dim documentList As List(of Inventor.Document) = new List(of Document)

	Select Case true

		Case oDoc.IsAssembly, oDoc.IsDrawing
			'change the selected documents...

			Dim oSSet As SelectSet = oApp.ActiveDocument.SelectSet 'get the current selection set

			'if nothing is selected... select something!
			If oSSet.count = 0 Then
				oSSet.Select(oApp.CommandManager.Pick(SelectionFilterEnum.kAllEntitiesFilter, "Select thing(s)..."));
			End If

			'if there are selected objects, get their associated documents...
			documentList = GetDocumentsFromSelectSet(oSSet)

			For Each i In documentList
				ChangeIProperty(i)
			Next

		Case Else
			MsgBox ("This tool is not compatible with this type of file. Exiting...")
			Exit Sub

	End Select

End Sub


Sub ChangeIProperty(oDoc)
	SetPropertyValue(oDoc, "Title", "Some Cool Title!")
End Sub