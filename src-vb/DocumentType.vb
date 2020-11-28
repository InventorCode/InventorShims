Imports System.Runtime.CompilerServices

Public Module DocumentType

    <Extension()>
    Public Function IsPart(document As Inventor.Document) As Boolean

        If document.DocumentType = Inventor.DocumentTypeEnum.kPartDocumentObject Then
            Return True
        End If

        Return False
    End Function

    <Extension()>
    Public Function IsAssembly(document As Inventor.Document) As Boolean

        If document.DocumentType = Inventor.DocumentTypeEnum.kAssemblyDocumentObject Then
            Return True
        End If

        Return False
    End Function

    <Extension()>
    Public Function IsDrawing(document As Inventor.Document) As Boolean

        If document.DocumentType = Inventor.DocumentTypeEnum.kDrawingDocumentObject Then
            Return True
        End If

        Return False
    End Function

    <Extension()>
    Public Function IsPresentation(document As Inventor.Document) As Boolean

        If document.DocumentType = Inventor.DocumentTypeEnum.kPresentationDocumentObject Then
            Return True
        End If

        Return False
    End Function

    <Extension()>
    Public Function IsForeignModel(document As Inventor.Document) As Boolean

        If document.DocumentType = Inventor.DocumentTypeEnum.kForeignModelDocumentObject Then
            Return True
        End If

        Return False
    End Function

    <Extension()>
    Public Function IsNest(document As Inventor.Document) As Boolean

        If document.DocumentType = Inventor.DocumentTypeEnum.kNestingDocument Then
            Return True
        End If

        Return False
    End Function

    <Extension()>
    Public Function IsSAT(document As Inventor.Document) As Boolean

        If document.DocumentType = Inventor.DocumentTypeEnum.kSATFileDocumentObject Then
            Return True
        End If


        Return False
    End Function

    <Extension()>
    Public Function IsUnknown(document As Inventor.Document) As Boolean

        If document.DocumentType = Inventor.DocumentTypeEnum.kUnknownDocumentObject Then
            Return True
        End If

        Return False
    End Function


End Module