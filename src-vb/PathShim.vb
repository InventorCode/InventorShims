Imports Inventor

Public Class PathShim

    Shared Function UpOneLevel(ByVal path As String) As String

        Dim delimiter As Char

        If path.Contains(IO.Path.DirectorySeparatorChar) Then
            delimiter = IO.Path.DirectorySeparatorChar
        ElseIf path.Contains(IO.Path.AltDirectorySeparatorChar) Then
            delimiter = IO.Path.AltDirectorySeparatorChar
        Else
            'no slashes?  not a path...
            Return Nothing
        End If


        'Catch paths which end with a delimiter, such as C:\Work\Stuff\
        'Clean up so that they look like C:\Work\Stuff
        path = TrimEndingDirectorySeparator(path)

        Dim delimPos As Integer = path.LastIndexOf(delimiter)
        If (delimPos = 0) Then
            Return Nothing
        ElseIf (delimPos < 0) Then
            Return path
        End If

        Return Left(path, delimPos + 1)

    End Function

    Shared Function IsLibraryPath(ByVal path As String, ByRef inventorApp As Inventor.Application) As Boolean

        If (String.IsNullOrEmpty(path)) Then Return False

        Dim designproject As Inventor.DesignProject = inventorApp.DesignProjectManager.ActiveDesignProject
        Dim librarypaths As ProjectPaths = designproject.LibraryPaths
        Dim librarypath As ProjectPath

        For Each librarypath In librarypaths
            If path.Contains(TrimEndingDirectorySeparator(librarypath.Path)) Then
                Return True
            End If
        Next

        Return False
    End Function

    Shared Function IsContentCenterPath(ByVal path As String, ByRef inventorApp As Inventor.Application) As Boolean

        If (String.IsNullOrEmpty(path)) Then Return False

        Dim designProject As Inventor.DesignProject = inventorApp.DesignProjectManager.ActiveDesignProject
        Dim useProjectCCPath As Boolean = designProject.ContentCenterPathOverridden
        Dim ccpath As String

        If useProjectCCPath Then
            ccpath = designProject.ContentCenterPath
        Else
            ccpath = inventorApp.FileOptions.ContentCenterPath
        End If

        ccpath = TrimEndingDirectorySeparator(ccpath)

        If path.Contains(ccpath) Then
            Return True
        End If

        Return False
    End Function
    Shared Function TrimEndingDirectorySeparator(ByVal path As String) As String

        If path.EndsWith(IO.Path.DirectorySeparatorChar) Then
            path = Left(path, path.LastIndexOf(IO.Path.DirectorySeparatorChar))
        ElseIf path.EndsWith(IO.Path.AltDirectorySeparatorChar) Then
            path = Left(path, path.LastIndexOf(IO.Path.AltDirectorySeparatorChar))
        End If

        Return path

    End Function

End Class