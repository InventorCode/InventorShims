AddReference "InventorShims.dll"
Imports InventorShims.PropertyShims
'Imports InventorShims.PropertyShims
'Run this test from inventor's ilogic environment.

	Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    oDoc.SetProperty("Title", "Stuffffff")
    oDoc.SetProperty("Things", "Stuffffff2")
    oDoc.SetProperty("Custom Prop Set", "Things", "Stufzzzzzz")

    MessageBox.Show(oDoc.GetProperty("Title"),"Stuffffff")
    MessageBox.Show(oDoc.GetProperty("Things"),"Stuffffff2")
    MessageBox.Show(oDoc.GetProperty("Custom Prop Set", "Things"), "Stufzzzzzz")

	MsgBox("Does title exist? (yes): " & oDoc.PropertyExists("Title"))
	MsgBox("Does things exist? (yes): " & oDoc.PropertyExists("Things"))
	MsgBox("Does custom things exist? (yes): " & oDoc.PropertyExists("Custom Prop Set", "Things"))


	oDoc.SetProperty("Title", "Title")
    oDoc.RemoveProperty("Things")
    oDoc.RemoveProperty("Custom Prop Set", "Things")

	MsgBox("Does title exist? (yes): " & oDoc.PropertyExists("Title"))
	MsgBox("Does things exist? (no): " & oDoc.PropertyExists("Things"))
	MsgBox("Does custom things exist? (no): " & oDoc.PropertyExists("Custom Prop Set", "Things"))
