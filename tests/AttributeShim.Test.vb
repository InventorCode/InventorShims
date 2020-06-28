Imports InventorShims
Imports Inventor
AddVbFile "src/AttributeShim.vb"

'Public Module Test
Sub Main()

Dim doc As Inventor.Document = ThisApplication.ActiveDocument

' Get the selected entity.
Dim entity As Object = doc.SelectSet.Item(1)

Dim attributeSetExistAnswer As Boolean
attributeSetExistAnswer = AttributeShim.DoesAttributeSetExist(entity, "GrandTest")

AttributeShim.SetAttribute(entity, "GrandTest","testing", 2345)

AttributeShim.SetAttribute(entity, "GrandTest","testing", 123)

MsgBox("DoesAttributeSetExist: " & attributeSetExistAnswer & vbLf & _
    		"DoesAttributeSetExist after creation?: " & AttributeShim.DoesAttributeSetExist(entity, "GrandTest") & vbLf & _
    		"Does attribute Exist after creation? " & AttributeShim.DoesAttributeExist(entity, "GrandTest", "testing") & vbLf & _
           "GetAttribute: " & AttributeShim.GetAttribute(entity, "GrandTest", "testing")) 

AttributeShim.RemoveAttribute(entity, "GrandTest","testing")
AttributeShim.RemoveAttributeSet(entity, "GrandTest")

msgbox("Does attribute exist after removal? : " & AttributeShim.DoesAttributeExist(entity, "GrandTest", "testing") & vbLf & _
		"Does attributeSet exist after removal? : " & AttributeShim.DoesAttributeSetExist(entity, "GrandTest"))


End Sub
'End Module