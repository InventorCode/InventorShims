Imports InventorShims
Imports Inventor
AddVbFile "src-vb/Attribute.vb"

'Public Module Test
Sub Main()

	Dim doc As Inventor.Document = ThisApplication.ActiveDocument

	' Get the selected entity.

	Dim attributeSetExistAnswer As Boolean
	attributeSetExistAnswer = Attribute.DoesAttributeSetExist(doc, "GrandTest")

	Attribute.SetAttribute(doc, "GrandTest", "testing", 2345)

	Attribute.SetAttribute(doc, "GrandTest", "testing", 123)

	MsgBox("DoesAttributeSetExist: " & attributeSetExistAnswer & vbLf &
			"DoesAttributeSetExist after creation?: " & Attribute.DoesAttributeSetExist(doc, "GrandTest") & vbLf &
			"Does attribute Exist after creation? " & Attribute.DoesAttributeExist(doc, "GrandTest", "testing") & vbLf &
			"GetAttribute: " & Attribute.GetAttribute(doc, "GrandTest", "testing"))

	Attribute.RemoveAttribute(doc, "GrandTest", "testing")
	Attribute.RemoveAttributeSet(doc, "GrandTest")

	MsgBox("Does attribute exist after removal? : " & Attribute.DoesAttributeExist(doc, "GrandTest", "testing") & vbLf &
			"Does attributeSet exist after removal? : " & Attribute.DoesAttributeSetExist(doc, "GrandTest"))


End Sub
'End Module
