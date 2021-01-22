AddReference "InventorShims.dll"
Imports InventorShims.PathShim

' ############################
' ###   PathShim Samples   ###
' ############################
'

Sub Main()


	'Test if the string is a Content Center Path...
    If IsContentCenterPath("C:/Testing/", ThisApplication) Then 
        'do something
    End If

	'Test if the string is a Library Path...
    If IsLibraryPath("C:/Testing/", ThisApplication) Then 
        'do something
    End If

    'Trim the ending directory / or \
    Dim test As String = TrimEndingDirectorySeperator("C:/Testing/Stuff/")
    'returns "C:/Testing/Stuff"

    'Trim the ending directory / or \
    Dim test As String = UpOneLevel("C:/Testing/Stuff/")
    'returns "C:/Testing/"

End Sub