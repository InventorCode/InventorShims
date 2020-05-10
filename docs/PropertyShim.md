---
layout: default
title: PropertyShim Class
nav_order: 
---

# PropertyShim Class

## Methods

### GetProperty (function)

(Short form signature) returns the specified document property's value as an object.  Tries to get the built-in properties first, then resorts to User-Defined Iproperties, and then searches through totally custom property groups.  If the property is not found, an empty string is returned.

(Long form signature) returns the specified property's value (from the specified property set) as an object.

#### Syntax:

    GetProperty(doc As Inventor.Document, propertyName As String)

    GetProperty(doc As Inventor.Document, propertySetName As String, propertyName As String)

#### Usage:

Get the property (short form):

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    msgbox(GetProperty(oDoc, "Title"))  

Get the property from a specific property set:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    msgbox(GetProperty(oDoc, "Property Set", Title"))  


### SetProperty

Set the specified document property's value.  If the ipropery name exist it will set the value.  If the name does not exist, it will add the property with the value you have specified in the "User Defined Properties" property set.  The long signature function must be used to specify custom property groups.

#### Syntax:

    SetProperty(doc As Inventor.Document, propertyName As String)

    SetProperty(doc As Inventor.Document, PropertySetName As String, propertyName As String)

#### Usage:

Set value for built-in property:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    SetProperty(oDoc, "Title", "Custom File Title!")    

Set value for custom property:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    SetProperty(oDoc, "CustomProperty", "Value Here!")    

Set value for a property in a specific property set:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    SetProperty(oDoc, "Property Group", CustomProperty", "Value Here!")    


### PropertySetExists

A simple function that returns true/false if the specified property set exists in the document.

#### Syntax:
    PropertySetExists(doc As Inventor.Document, propertySetName As String)

#### Usage:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    If PropertySetExists(oDoc, "User Defined Properties") Then
        'True!
    Else
        'False!
    End If