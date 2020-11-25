Imports Inventor

Public Class PathShim

    Shared Function UpOneLevel(ByVal path As String, Optional delimiter As String = "\") As String

        'fix paths with incorrect backslashes
        If delimiter = "\" Then path = Replace(path, "/", "\")

        'no slashes?  not a path...
        If Not path.Contains(delimiter) Then
            Return Nothing
        End If

        'Catch paths which end with a delimiter, such as C:\Work\Stuff\
        'Clean up so that they look like C:\Work\Stuff
        If path.EndsWith(delimiter) Then
            path = Left(path, path.LastIndexOf(delimiter))
        End If


        Dim delimPos As Integer = path.LastIndexOf(delimiter)
        If (delimPos <= 0) Then
            Return Nothing
        End If

        Return Left(path, delimPos + 1)

    End Function

End Class