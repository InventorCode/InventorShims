'Imports InventorShims
Imports Inventor
AddVbFile "src-vb/Attribute.vb"

'Public Module Test
Sub Main()

Dim doc As Inventor.Document = ThisApplication.ActiveDocument

' Get the selected entity.
Dim entity As Object = doc.SelectSet.Item(1)

Dim attributeSetExistAnswer As Boolean
	attributeSetExistAnswer = InventorShims.Attribute.DoesAttributeSetExist(entity, "GrandTest")

	Attribute.SetAttribute(entity, "GrandTest", "testing", 2345)

	InventorShims.Attribute.SetAttribute(entity, "GrandTest", "testing", 123)

	MsgBox("DoesAttributeSetExist: " & attributeSetExistAnswer & vbLf &
			"DoesAttributeSetExist after creation?: " & InventorShims.Attribute.DoesAttributeSetExist(entity, "GrandTest") & vbLf &
			"Does attribute Exist after creation? " & InventorShims.Attribute.DoesAttributeExist(entity, "GrandTest", "testing") & vbLf &
			"GetAttribute: " & InventorShims.Attribute.GetAttribute(entity, "GrandTest", "testing"))

	InventorShims.Attribute.RemoveAttribute(entity, "GrandTest", "testing")
	InventorShims.Attribute.RemoveAttributeSet(entity, "GrandTest")

	MsgBox("Does attribute exist after removal? : " & InventorShims.Attribute.DoesAttributeExist(entity, "GrandTest", "testing") & vbLf &
		"Does attributeSet exist after removal? : " & InventorShims.Attribute.DoesAttributeSetExist(entity, "GrandTest"))


End Sub
'End Module