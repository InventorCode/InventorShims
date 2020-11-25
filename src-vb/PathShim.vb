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
        path = TrimTrailingSlash(path)

        Dim delimPos As Integer = path.LastIndexOf(delimiter)
        If (delimPos <= 0) Then
            Return Nothing
        End If

        Return Left(path, delimPos + 1)

    End Function

    Shared Function IsLibraryPath(ByVal path As String, ByRef inventorApp As Inventor.Application) As Boolean

        Dim designprojectmanager As DesignProjectManager = inventorApp.DesignProjectManager
        Dim designproject As Inventor.DesignProject = designprojectmanager.ActiveDesignProject
        Dim librarypaths As ProjectPaths = designproject.LibraryPaths
        Dim librarypath As ProjectPath

        For Each librarypath In librarypaths
            If path.Contains(librarypath.Path) Then
                Return True
            End If
        Next

        Return False
    End Function

    Shared Function IsContentCenterPath(ByVal path As String, ByRef inventorApp As Inventor.Application) As Boolean

        Dim designprojectmanager As DesignProjectManager = inventorApp.DesignProjectManager
        Dim designproject As Inventor.DesignProject = designprojectmanager.ActiveDesignProject
        Dim useProjectCCPath As Boolean = designproject.ContentCenterPathOverridden
        Dim ccpath As String

        If useProjectCCPath Then
            ccpath = designproject.ContentCenterPath
        Else
            ccpath = inventorApp.FileOptions.ContentCenterPath
        End If

        ccpath = TrimTrailingSlash(ccpath)

        If path.Contains(ccpath) Then
            Return True
        End If

        Return False
    End Function
    Shared Function TrimTrailingSlash(ByVal path As String) As String

        If path.EndsWith("\") Then
            path = Left(path, path.LastIndexOf("\"))
        End If

        Return path

    End Function

End Class