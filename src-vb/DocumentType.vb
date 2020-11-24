'Imports System.Runtime.CompilerServices

Public Class DocumentType

    Shared Function IsPart(document As Inventor.Document) As Boolean

        If document.DocumentType = Inventor.DocumentTypeEnum.kPartDocumentObject Then
            Return True
        End If

        Return False
    End Function

    Shared Function IsAssembly(document As Inventor.Document) As Boolean

        If document.DocumentType = Inventor.DocumentTypeEnum.kAssemblyDocumentObject Then
            Return True
        End If

        Return False
    End Function

    Shared Function IsDrawing(document As Inventor.Document) As Boolean

        If document.DocumentType = Inventor.DocumentTypeEnum.kDrawingDocumentObject Then
            Return True
        End If

        Return False
    End Function

    Shared Function IsPresentation(document As Inventor.Document) As Boolean

        If document.DocumentType = Inventor.DocumentTypeEnum.kPresentationDocumentObject Then
            Return True
        End If

        Return False
    End Function

    Shared Function IsForeignModel(document As Inventor.Document) As Boolean

        If document.DocumentType = Inventor.DocumentTypeEnum.kForeignModelDocumentObject Then
            Return True
        End If

        Return False
    End Function

    Shared Function IsNest(document As Inventor.Document) As Boolean
        '        Try
        '        If document.DocumentType = Inventor.DocumentTypeEnum.kNestingDocument
        '            Return True
        '        End If
        '        Catch
        '            End Try
        Return False
    End Function

    Shared Function IsSAT(document As Inventor.Document) As Boolean

        If document.DocumentType = Inventor.DocumentTypeEnum.kSATFileDocumentObject Then
            Return True
        End If


        Return False
    End Function

    Shared Function IsUnknown(document As Inventor.Document) As Boolean

        If document.DocumentType = Inventor.DocumentTypeEnum.kUnknownDocumentObject Then
            Return True
        End If

        Return False
    End Function


End Class
