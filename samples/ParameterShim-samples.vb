AddReference "InventorShims.dll"
Imports InventorShims.ParameterShim

' #################################
' ###   ParameterShim Samples   ###
' #################################
'

Sub Main()

    Dim oDoc As Document = ThisDoc.Document

	'If the parameter doesn't exist, it will be created...
	SetParameterValue(oDoc, "testing", "16", "cm")

	'If the clobberFlag bool is set to true, an existing parameter will not be overritten...
	SetParameterValue(oDoc, "testing", "16", "cm", true)

	'Create a boolean parameter
	SetParameterValue(oDoc, "booleanParam", true)

	'Create a text parameter
	SetParameterValue(oDoc, "booleanParam", "A really cool string!")


	'Get a parameters object
	Dim oParams As Parameters = GetParameters(oDoc)

	'Get a parameter object
	Dim oParam As Parameter = GetParameter(oDoc, "testing")

	'If this object does not exist, it will return null
	Dim oNullParam As Parameter = GetParameter(oDoc, "nonExistantParameter")
	If (oNullParam Is Nothing) Then
		'will return true!
	End If

	'Return the value
	Msgbox(GetParameterValue(oDoc, "testing"))

	'Test if the parameter is writable by the user
	
	If ParameterIsWritable(oParam) Then
		'will return true!
	End If

	'Remove a parameter
	RemoveParameter(oDoc, "testing")


End Sub
