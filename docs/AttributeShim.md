---
layout: default
title: AttributeShim
nav_order: 
---

# AttributeShim

## Methods

### SetAttribute

Creates or modifies an attribute in the provided object.  If the AttributeSet does not exist, it will be created.  The attribute value can be of the type String, Integer, Double, or ByteArray.

#### Syntaxes:

    SetAttribute(obj As Object, attributeSet As String, attribute As String, value As String)

    SetAttribute(obj As Object, attributeSet As String, attribute As String, value As Integer)

    SetAttribute(obj As Object, attributeSet As String, attribute As String, value As Double)

    SetAttribute(obj As Object, attributeSet As String, attribute As String, value As Byte())

    SetAttribute(obj As Object, attributeSet As String, attribute As String, value As Boolean)


#### Usage:

Set the attribute:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    SetAttribute(oDoc, "OutAttributeSet", "NameOfOurAttribute", "The attribute value!")



### GetAttribute

Returns the specified attribute within the supplied object.  If the attribute does not exist, it will return an empty string.

#### Syntax:

    GetAttribute(obj As Object, attributeSet As String, attribute As Object)

#### Usage:

Set value for built-in property:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    GetAttribute(oDoc, "OurAttributeSet", "NameOfOurAttribute")    


### RemoveAttribute

Removes an attribute from an AttributeSet if the Attribute exists.

#### Syntax:
    RemoveAttribute(obj As Object, attributeSet As String, attribute As Object)

#### Usage:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    RemoveAttribute(oDoc, "OurAttributeSet", "NameOfOurAttribute")


### CreateAttributeSet

Creates an AttributeSet in an object if the AttributeSet does not already exist.

#### Syntax:
    CreateAttributeSet(obj as Object, attributeSet As String)

#### Usage:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    CreateAttributeSet(oDoc, "AnotherAttributeSet")


### RemoveAttributeSet

Removes an attribute from an AttributeSet if the Attribute exists.

#### Syntax:
    RemoveAttributeSet(obj As Object, attributeSet As String)

#### Usage:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    RemoveAttributeSet(oDoc, "OurAttributeSet")


### DoesAttributeExist

Returns a boolean value indicating if the specified Attribute exists.

#### Syntax:
    DoesAttributeExist(obj As Object, attributeSet As String, attribute As String) As Boolean

#### Usage:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    If DoesAttributeExist(oDoc, "OurAttributeSet", "testAttribute") Then
        'true
    End If

### DoesAttributeSetExist

Returns a boolean value indicating if the specified AttributeSet exists.

#### Syntax:
    DoesAttributeSetExist(obj As Object, attributeSet As String) As Boolean

#### Usage:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    If DoesAttributeSetExist(oDoc, "OurAttributeSet") Then
        'true
    End If




### IsObjectAttributeCapable

Returns a boolean value indicating if the specified object has Attribute capability.

#### Syntax:
    IsObjectAttributeCapable(obj As Object) As Boolean

#### Usage:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    If IsObjectAttributeCapable(oDoc) Then
        'true
    End If