Imports Inventor

'For types that aren't handled by the script already, could it loop recursively through the parents until it runs into a ComponentDefinition or Document object?
'jordanrobot: Sure.  That's a good idea to try to find a parent.  I didn't know if there was a more efficient way to do this, or a more elegant way to do this whole thing.  It should probably be an extension method though.
'pockybum522: I have some code for getcompdef, given criteria
'jordanrobot: Or make a hash table of types that will work with particular method to extract a document.  then lookup the provided type in the table and send it to that method
'nannerdw: I don't know if this would be useful https://medium.com/@obaranovskyi/code-refactoring-polymorphism-instead-of-the-switch-and-other-conditionals-84826b36d00e
'nannerdw: @jordanrobot The ActiveDocument can be an assembly, while the ActiveEditDocument can be a part
'nannerdw: Also, the object being passed to the script might not exist in either document
'nannerdw: Also, if you have a ComponentOccurrenceProxy, and you check "Is ComponentOccurrence", it will return true
'nannerdw: @pockybum522 @jordanrobot extension methods don't seem to work on late-bound objects either
'nannerdw: https://devblogs.microsoft.com/vbteam/extension-methods-and-late-binding-extension-methods-part-4/
'nannerdw: I think ObjectShim.GetDocFromObject should just be a long switch statement, without the active document type checks
'nannerdw: Especially because when you're editing an assembly in place, you can select components that are in either the ActiveDocument or the ActiveEditDocument

Public Class ObjectShim

    ''' <summary>
    ''' Returns a document object (if able) for a provided object
    ''' </summary>
    ''' <param name="obj">provided Object</param>
    ''' <returns>Document</returns>
    Public Function GetDocFromObject(ByRef obj As Object) As Document

        Dim app As Inventor.Application = GetObject(, "Inventor.Application")
        Dim oCCdef As Document
        Dim componentOccurrence As ComponentOccurrence
        Dim drawingDocument As DrawingDocument
        Dim view As DrawingView
        Dim debugVar As Boolean = False

        If debugVar Then MsgBox("temp.type = " & obj.Type)

        Dim doc As Document = app.ActiveDocument
        'Determine the activeDocument type, lets us check which type of objects to test for...
        Select Case doc.DocumentType 'what kind of document?


            Case DocumentTypeEnum.kDesignElementDocumentObject, DocumentTypeEnum.kForeignModelDocumentObject, DocumentTypeEnum.kNoDocument, DocumentTypeEnum.kPresentationDocumentObject, DocumentTypeEnum.kSATFileDocumentObject, DocumentTypeEnum.kUnknownDocumentObject
                If debugVar Then MsgBox("Strange DocumentObjectType detected.  Aborting.")
                If debugVar Then MsgBox("This tool is not compatible with this type of file.")
                Return Nothing
                Exit Select

            Case DocumentTypeEnum.kPartDocumentObject
                If debugVar Then MsgBox("Part document found, passing this document object on.")
                Return doc

        'AssemblyDocument
            Case DocumentTypeEnum.kAssemblyDocumentObject
                If debugVar Then MsgBox("Assembly file found.  Processing...")
                If debugVar Then MsgBox("temp.type = " & obj.Type)

                Select Case obj.type

                '###   In Assembly Document [kAssemblyDocumentObject]   ###
                'kComponentOccurrenceObject or kComponentOccurrenceProxyObject (component occurence?)
                'Case 67113776, 67113888
                    Case ObjectTypeEnum.kComponentOccurrenceObject, ObjectTypeEnum.kComponentOccurrenceProxyObject
                        If debugVar Then MsgBox("Component Occurence [Proxy] Object detected, document type is: " & obj.Definition.Document.Type)
                        Return obj.Definition.Document

                    Case Else
                        Return Nothing

                End Select 'temp.type

        'Drawing Document
            Case DocumentTypeEnum.kDrawingDocumentObject
                'recast as a DrawingDocumentObject so we can get access the specific properties and methods
                drawingDocument = doc
                If debugVar Then MsgBox("Drawing detected, processing...")

                Select Case obj.type
                'Drawing View, Section View, Detail View
                    Case ObjectTypeEnum.kDrawingViewObject, 117463296, 117474304
                        If debugVar Then MsgBox("Drawing view object found...")
                        Return obj.ReferencedFile.ReferencedDocument


                'Generic Object
                    Case ObjectTypeEnum.kGenericObject
                        If debugVar Then MsgBox("Generic object found...")
                        Try
                            'try to get single document from selected part
                            Call drawingDocument.ProcessViewSelection(obj, view, oCCdef)
                            Return oCCdef
                        Catch
                            'if this doesn't work, try to get the compoinent occurence instead, and then get the document from that
                            Try
                                If debugVar Then MsgBox("    There was an error at 'oDrawDoc.ProcessViewSelection(temp, oView, oCCdef)'")
                                Call drawingDocument.ProcessViewSelection(obj, view, componentOccurrence)
                                Return componentOccurrence.Definition.Document
                            Catch
                                If debugVar Then MsgBox("    There was an error at 'Set oCCdef = oCompOcc.Definition.Document'")
                                Return Nothing
                            End Try
                        End Try


                'Drawing Curve Segment
                    Case ObjectTypeEnum.kDrawingCurveSegmentObject 'drawing curve
                        If debugVar Then MsgBox("Drawing curve segment found...")
                        'try to set the drawing curve object to point at the containingOccurrence object.
                        'Edge Objects and Edge Proxy Objects                        
                        Try
                            Return obj.Parent.ModelGeometry.ContainingOccurrence.Definition.Document
                        Catch
                            Try
                                Return obj.Parent.ModelGeometry.Parent.ComponentDefinition.Document
                            Catch
                                If debugVar Then MsgBox("    There was an error at 'Set oCCdef = temp.Parent.ModelGeometry.ContainingOccurrence.Definition.Document'")
                                Return Nothing
                            End Try
                        End Try


                'Parts List
                    Case ObjectTypeEnum.kPartsListObject '117444096
                        If debugVar Then MsgBox("Parts list object found...")
                        Return obj.ReferencedFile.ReferencedDocument


                'PartsListRow
                    Case ObjectTypeEnum.kPartsListRowObject '117445120
                        If debugVar Then MsgBox("Parts list row object found...")
                        Return obj.ReferencedFiles.Item(1).ReferencedDocument

                    Case Else
                        Return Nothing

                End Select 'temp.type

            Case Else
                Return Nothing

        End Select 'ThisApplication.ActiveDocument.DocumentType
    End Function
End Class