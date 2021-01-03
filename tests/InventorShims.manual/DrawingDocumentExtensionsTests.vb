AddReference "InventorShims.dll"
Imports InventorShims.DrawingDocumentExtensions	

Dim oDoc As Inventor.DrawingDocument = ThisApplication.ActiveDocument
oDoc.SaveWithFileDialog
