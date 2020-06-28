Imports Inventor

Public Class AttributeShim

    Shared Sub SetAttribute(obj As Object, attributeSet As String, attribute As String, value As String)
        SetAttributeEngine(obj, attributeSet, attribute, value, Inventor.ValueTypeEnum.kStringType)
    End Sub

    Shared Sub SetAttribute(obj As Object, attributeSet As String, attribute As String, value As Integer)
        SetAttributeEngine(obj, attributeSet, attribute, value, Inventor.ValueTypeEnum.kIntegerType)
    End Sub

    Shared Sub SetAttribute(obj As Object, attributeSet As String, attribute As String, value As Double)
        SetAttributeEngine(obj, attributeSet, attribute, value, Inventor.ValueTypeEnum.kDoubleType)
    End Sub

    Shared Sub SetAttribute(obj As Object, attributeSet As String, attribute As String, value As Byte())
        SetAttributeEngine(obj, attributeSet, attribute, value, Inventor.ValueTypeEnum.kByteArrayType)
    End Sub

    Shared Sub SetAttributeEngine(obj As Object, attributeSet As String, attribute As String, value As Object, kind As Inventor.ValueTypeEnum)

        If IsAttributeCapable(obj) = False Then
            Throw New SystemException("The selected object is not attribute-capable.")
        End If

        Dim oAttributeSets As Inventor.AttributeSets
        Dim oAttributeSet As Inventor.AttributeSet

        oAttributeSet = CreateAttributeSet(obj, attributeSet)

        If DoesAttributeExist(obj, attributeSet, attribute) Then
            oAttributeSet.Item(attribute).value = value
        Else
            oAttributeSet.Add(attribute, kind, value)
        End If


    End Sub

    Shared Function CreateAttributeSet(obj as Object, attributeSet As String) As Inventor.AttributeSet
        
        If IsAttributeCapable(obj) = False Then
            Throw New SystemException("The selected object is not attribute-capable.")
        End If

        If DoesAttributeSetExist(obj, attributeSet) Then
            Return obj.AttributeSets.Item(attributeSet)
        
        Else 
            
            Dim oAttributeSets As Inventor.AttributeSets = obj.AttributeSets
            Return oAttributeSets.Add(attributeSet)
        End If

    End Function

    Shared Function GetAttribute(obj As Object, attributeSet As String, attribute As Object) As Object

        If DoesAttributeExist(obj, attributeSet, attribute) Then
            Dim oAttributeSets As Inventor.AttributeSets = obj.AttributeSets
            Dim oAttributeSet  As Inventor.AttributeSet = oAttributeSets.Item(attributeSet)
            Return oAttributeSet.Item(attribute).Value
        Else
            Return ""
        End If

    End Function

    Shared Sub RemoveAttribute(obj As Object, attributeSet As String, attribute As Object)

        If DoesAttributeExist(obj, attributeSet, attribute) Then
            Dim oAttributeSets As Inventor.AttributeSets = obj.AttributeSets
            Dim oAttributeSet  As Inventor.AttributeSet = oAttributeSets.Item(attributeSet)
            oAttributeSet.Item(attribute).Delete()
        End If

    End Sub

    Shared Sub RemoveAttributeSet(obj As Object, attributeSet As String)

        If DoesAttributeSetExist(obj, attributeSet) Then
            Dim oAttributeSets As Inventor.AttributeSets = obj.AttributeSets
            oAttributeSets.Item(attributeSet).Delete()
        End If

    End Sub


    Shared Function DoesAttributeSetExist(obj As Object, attributeSet As String) As Boolean

        If IsAttributeCapable(obj) = False Then
            Return False
        End If

        Dim oAttributeSets As Inventor.AttributeSets= obj.AttributeSets
        Return oAttributeSets.NameIsUsed(attributeSet)

    End Function

    Shared Function DoesAttributeExist(obj As Object, attributeSet As String, attribute As String) As Boolean

        If IsAttributeCapable(obj) = False Then
            Return False
        End If

        If DoesAttributeSetExist(obj, attributeSet) = False
            Return False
        End If

        Dim oAttributeSets As Inventor.AttributeSets= obj.AttributeSets
        Dim oAttributeSet As Inventor.AttributeSet = oAttributeSets.Item(attributeSet)
        Return oAttributeSet.NameIsUsed(attribute)

    End Function

    Shared Function IsAttributeCapable(obj As Object) As Boolean

        Dim oAttributeSets As Inventor.AttributeSets

        Try 
            oAttributeSets = obj.AttributeSets
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function


End Class