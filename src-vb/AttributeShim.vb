Imports Inventor


Public Class AttributeShim

    Shared Sub SetAttributeValue(obj As Object, attributeSet As String, attribute As String, value As String)
        SetAttributeValueEngine(obj, attributeSet, attribute, value, Inventor.ValueTypeEnum.kStringType)
    End Sub

    Shared Sub SetAttributeValue(obj As Object, attributeSet As String, attribute As String, value As Integer)
        SetAttributeValueEngine(obj, attributeSet, attribute, value, Inventor.ValueTypeEnum.kIntegerType)
    End Sub

    Shared Sub SetAttributeValue(obj As Object, attributeSet As String, attribute As String, value As Boolean)
        SetAttributeValueEngine(obj, attributeSet, attribute, value, Inventor.ValueTypeEnum.kBooleanType)
    End Sub

    Shared Sub SetAttributeValue(obj As Object, attributeSet As String, attribute As String, value As Double)
        SetAttributeValueEngine(obj, attributeSet, attribute, value, Inventor.ValueTypeEnum.kDoubleType)
    End Sub

    Shared Sub SetAttributeValue(obj As Object, attributeSet As String, attribute As String, value As Byte())
        SetAttributeValueEngine(obj, attributeSet, attribute, value, Inventor.ValueTypeEnum.kByteArrayType)
    End Sub

    Shared Sub SetAttributeValueEngine(obj As Object, attributeSet As String, attribute As String, value As Object, kind As Inventor.ValueTypeEnum)

        If ObjectIsAttributeCapable(obj) = False Then
            Throw New SystemException("The selected object is not attribute-capable.")
        End If

        Dim oAttributeSets As Inventor.AttributeSets
        Dim oAttributeSet As Inventor.AttributeSet

        oAttributeSet = CreateAttributeSet(obj, attributeSet)

        If AttributeExists(obj, attributeSet, attribute) Then
            oAttributeSet.Item(attribute).Value = value
        Else
            oAttributeSet.Add(attribute, kind, value)
        End If


    End Sub

    Shared Function CreateAttributeSet(obj As Object, attributeSet As String) As Inventor.AttributeSet

        If ObjectIsAttributeCapable(obj) = False Then
            Throw New SystemException("The selected object is not attribute-capable.")
        End If

        If AttributeSetExists(obj, attributeSet) Then
            Return obj.AttributeSets.Item(attributeSet)

        Else

            Dim oAttributeSets As Inventor.AttributeSets = obj.AttributeSets
            Return oAttributeSets.Add(attributeSet)
        End If

    End Function

    Shared Function GetAttributeValue(obj As Object, attributeSet As String, attribute As Object) As Object

        If AttributeExists(obj, attributeSet, attribute) Then
            Dim oAttributeSets As Inventor.AttributeSets = obj.AttributeSets
            Dim oAttributeSet As Inventor.AttributeSet = oAttributeSets.Item(attributeSet)
            Return oAttributeSet.Item(attribute).Value
        Else
            Return ""
        End If

    End Function

    Shared Sub RemoveAttribute(obj As Object, attributeSet As String, attribute As Object)

        If AttributeExists(obj, attributeSet, attribute) Then
            Dim oAttributeSets As Inventor.AttributeSets = obj.AttributeSets
            Dim oAttributeSet As Inventor.AttributeSet = oAttributeSets.Item(attributeSet)
            oAttributeSet.Item(attribute).Delete()
        End If

    End Sub

    Shared Sub RemoveAttributeSet(obj As Object, attributeSet As String)

        If AttributeSetExists(obj, attributeSet) Then
            Dim oAttributeSets As Inventor.AttributeSets = obj.AttributeSets
            oAttributeSets.Item(attributeSet).Delete()
        End If

    End Sub


    Shared Function AttributeSetExists(obj As Object, attributeSet As String) As Boolean

        If ObjectIsAttributeCapable(obj) = False Then
            Return False
        End If

        Dim oAttributeSets As Inventor.AttributeSets = obj.AttributeSets
        Return oAttributeSets.NameIsUsed(attributeSet)

    End Function

    Shared Function AttributeExists(obj As Object, attributeSet As String, attribute As String) As Boolean

        If ObjectIsAttributeCapable(obj) = False Then
            Return False
        End If

        If AttributeSetExists(obj, attributeSet) = False Then
            Return False
        End If

        Dim oAttributeSets As Inventor.AttributeSets = obj.AttributeSets
        Dim oAttributeSet As Inventor.AttributeSet = oAttributeSets.Item(attributeSet)
        Return oAttributeSet.NameIsUsed(attribute)

    End Function

    Shared Function ObjectIsAttributeCapable(obj As Object) As Boolean

        Dim oAttributeSets As Inventor.AttributeSets

        Try
            oAttributeSets = obj.AttributeSets
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function


End Class