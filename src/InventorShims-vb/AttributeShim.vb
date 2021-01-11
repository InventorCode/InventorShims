Imports Inventor


Public Class AttributeShim_Old

    ''' <summary>
    ''' Sets the value of a specified attribute in the provided object. The attribute is specified by it's
    ''' name. If no such attribute exists, one is created. If the containing object is not attribute
    ''' capable, the method will throw a system exception.  This signature accepts a string value.
    ''' </summary>
    ''' <param name="obj">Object</param>
    ''' <param name="attributeSet">AttributeSet name as a string</param>
    ''' <param name="attribute">Attribute name as a string</param>
    ''' <param name="value">Attribute value as a string</param>
    Shared Sub SetAttributeValue(obj As Object, attributeSet As String, attribute As String, value As String)
        SetAttributeValueEngine(obj, attributeSet, attribute, value, Inventor.ValueTypeEnum.kStringType)
    End Sub

    ''' <summary>
    ''' Sets the value of a specified attribute in the provided object. The attribute is specified by it's
    ''' name. If no such attribute exists, one is created. If the containing object is not attribute
    ''' capable, the method will throw a system exception.  This signature accepts an integer.
    ''' </summary>
    ''' <param name="obj">Object</param>
    ''' <param name="attributeSet">AttributeSet name as a string</param>
    ''' <param name="attribute">Attribute name as a string</param>
    ''' <param name="value">Attribute value as an integer</param>
    Shared Sub SetAttributeValue(obj As Object, attributeSet As String, attribute As String, value As Integer)
        SetAttributeValueEngine(obj, attributeSet, attribute, value, Inventor.ValueTypeEnum.kIntegerType)
    End Sub

    ''' <summary>
    ''' Sets the value of a specified attribute in the provided object. The attribute is specified by it's
    ''' name. If no such attribute exists, one is created. If the containing object is not attribute
    ''' capable, the method will throw a system exception.  This signature accepts a System.Boolean
    ''' value.  Note that the Inventor.Parameter object does not utilize a true boolean value, but instead a
    ''' ValueTypeEnum.kBooleanType which is actually an integer value of 0 or 1.
    ''' </summary>
    ''' <param name="obj">Object</param>
    ''' <param name="attributeSet">AttributeSet name as a string</param>
    ''' <param name="attribute">Attribute name as a string</param>
    ''' <param name="value">Attribute value as a Boolean</param>
    Shared Sub SetAttributeValue(obj As Object, attributeSet As String, attribute As String, value As Boolean)
        Dim i As Integer
        If (value = True) Then
            i = 1
        Else
            i = 0
        End If
        SetAttributeValueEngine(obj, attributeSet, attribute, value, Inventor.ValueTypeEnum.kBooleanType)
    End Sub

    ''' <summary>
    ''' Sets the value of a specified attribute in the provided object. The attribute is specified by it's
    ''' name. If no such attribute exists, one is created. If the containing object is not attribute
    ''' capable, the method will throw a system exception.  This signature accepts a double value.
    ''' </summary>
    ''' <param name="obj">Object</param>
    ''' <param name="attributeSet">AttributeSet name as a string</param>
    ''' <param name="attribute">Attribute name as a string</param>
    ''' <param name="value">Attribute value as a double</param>
    Shared Sub SetAttributeValue(obj As Object, attributeSet As String, attribute As String, value As Double)
        SetAttributeValueEngine(obj, attributeSet, attribute, value, Inventor.ValueTypeEnum.kDoubleType)
    End Sub

    '    Shared Sub SetAttributeValue(obj As Object, attributeSet As String, attribute As String, value As Byte())
    '        SetAttributeValueEngine(obj, attributeSet, attribute, value, Inventor.ValueTypeEnum.kByteArrayType)
    '    End Sub

    Shared Sub SetAttributeValueEngine(obj As Object, attributeSet As String, attribute As String, value As Object, valueType As Inventor.ValueTypeEnum)

        If ObjectIsAttributeCapable(obj) = False Then
            Throw New SystemException("The selected object is not attribute-capable.")
        End If

        Dim oAttributeSets As Inventor.AttributeSets
        Dim oAttributeSet As Inventor.AttributeSet

        oAttributeSet = CreateAttributeSet(obj, attributeSet)

        If AttributeExists(obj, attributeSet, attribute) Then
            oAttributeSet.Item(attribute).Value = value
        Else
            oAttributeSet.Add(attribute, valueType, value)
        End If


    End Sub

    ''' <summary>
    ''' This static function will create an Attribute Set for the provided object if one with that name
    ''' does not already exist.  The newly created AttributeSet object is returned.  If the object is not
    ''' attribute capable, the function will throw a system exception.
    ''' </summary>
    ''' <param name="obj">Object</param>
    ''' <param name="attributeSet">AttributeSet name as a string</param>
    ''' <returns></returns>
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

    ''' <summary>
    ''' This static function will return an Attribute's value for the provided object and Attribute name.
    ''' If the specified attribute does not exist, an empty string is returned. If the object is not
    ''' attribute capable, the function will throw a system exception.
    ''' </summary>
    ''' <param name="obj">Object</param>
    ''' <param name="attributeSet">AttributeSet name as a string</param>
    ''' <param name="attribute">Attribute name as a string</param>
    ''' <returns></returns>
    Shared Function GetAttributeValue(obj As Object, attributeSet As String, attribute As Object) As Object

        If AttributeExists(obj, attributeSet, attribute) Then
            Dim oAttributeSets As Inventor.AttributeSets = obj.AttributeSets
            Dim oAttributeSet As Inventor.AttributeSet = oAttributeSets.Item(attributeSet)
            Return oAttributeSet.Item(attribute).Value
        Else
            Return ""
        End If

    End Function

    ''' <summary>
    ''' This static method will remove a specified Attribute from the provided Object if one exists.
    '''  If the object is not attribute capable, the function will throw a system exception.
    ''' </summary>
    ''' <param name="obj">Object</param>
    ''' <param name="attributeSet">AttributeSet name as a string</param>
    ''' <param name="attribute">Attribute name as a string</param>
    Shared Sub RemoveAttribute(obj As Object, attributeSet As String, attribute As Object)

        If AttributeExists(obj, attributeSet, attribute) Then
            Dim oAttributeSets As Inventor.AttributeSets = obj.AttributeSets
            Dim oAttributeSet As Inventor.AttributeSet = oAttributeSets.Item(attributeSet)
            oAttributeSet.Item(attribute).Delete()
        End If

    End Sub

    ''' <summary>
    ''' This static method will remove a specified AttributeSet from the provided Object if one exists.
    '''  If the object is not attribute capable, the function will throw a system exception.
    ''' </summary>
    ''' <param name="obj">Object</param>
    ''' <param name="attributeSet">AttributeSet name as a string</param>
    Shared Sub RemoveAttributeSet(obj As Object, attributeSet As String)

        If AttributeSetExists(obj, attributeSet) Then
            Dim oAttributeSets As Inventor.AttributeSets = obj.AttributeSets
            oAttributeSets.Item(attributeSet).Delete()
        End If

    End Sub

    ''' <summary>
    ''' This static function will return boolean value indicating if the specified AttributeSet
    ''' exists in the provided object. If the object is not attribute capable, the function
    ''' will throw a system exception.
    ''' </summary>
    ''' <param name="obj">Object</param>
    ''' <param name="attributeSet">AttributeSet name as a string</param>
    Shared Function AttributeSetExists(obj As Object, attributeSet As String) As Boolean

        If ObjectIsAttributeCapable(obj) = False Then
            Return False
        End If

        Dim oAttributeSets As Inventor.AttributeSets = obj.AttributeSets
        Return oAttributeSets.NameIsUsed(attributeSet)

    End Function

    ''' <summary>
    ''' This static function will return boolean value indicating if the specified Attribute
    ''' exists in the provided object. If the object is not attribute capable, the function
    ''' will throw a system exception.
    ''' </summary>
    ''' <param name="obj">Object</param>
    ''' <param name="attributeSet">AttributeSet name as a string</param>
    ''' <param name="attribute">Attribute name as a string</param>
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

    ''' <summary>
    ''' This static function will return boolean value indicating if the specified Object
    ''' is Attribute capable.
    ''' </summary>
    ''' <param name="obj">Object</param>
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