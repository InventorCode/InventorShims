---
layout: default
title: PropertyShim Class
nav_order: 
---

# PropertyShim Class

## Methods

### GetProperty

(Short form signature) returns the specified document property's value as an object.  Tries to get the built-in properties first, then resorts to User-Defined Iproperties, and then searches through totally custom property groups.  If the property is not found, an empty string is returned.

(Long form signature) returns the specified property's value (from the specified property set) as an object.

#### Syntax:

    document.GetProperty(string propertyName)

    document.GetProperty(string propertySetName, string propertyName)

#### Usage:

Get the property (short form):

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    msgbox(oDoc.GetProperty("Title"))  

Get the property from a specific property set:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    msgbox(oDoc.GetProperty("Property Set", Title"))  


### SetProperty

Set the specified document property's value.  If the iproperty name exist it will set the value.  If the name does not exist, it will add the property with the value you have specified in the "User Defined Properties" property set.  The long signature function must be used to specify custom property groups.

#### Syntax:

    document.SetProperty(string propertyName)

    document.SetProperty(string PropertySetName, string propertyName)

#### Usage:

Set value for built-in property:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    oDoc.SetProperty("Title", "Custom File Title!")    

Set value for custom property:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    oDoc.SetProperty("CustomProperty", "Value Here!")    

Set value for a property in a specific property set:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    oDoc.SetProperty("Property Group", CustomProperty", "Value Here!")    


### RemoveProperty

Removes the specified document property.  If the property is native, it will only delete the iproperty's value.

#### Syntax:

    document.RemoveProperty(string propertyName)

    document.RemoveProperty(string PropertySetName, string propertyName)

#### Usage:

Set value for built-in property:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    oDoc.RemoveProperty("Title")    

Set value for custom property:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    oDoc.RemoveProperty("CustomPropertySet", "Custom Property Name") 


### PropertySetExists

A simple function that returns true/false if the specified property set exists in the document.

#### Syntax:
    document.PropertySetExists(string propertySetName)

    document.PropertyExists(string propertySetName, string propertyName)

#### Usage:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    If oDoc.PropertySetExists("User Defined Properties") Then
        'True!
    Else
        'False!
    End If


### PropertyExists

A simple function that returns true/false if the specified property exists in the document.

#### Syntax:

    document.PropertyExists(string propertyName)
    document.PropertyExists(string propertySetName, string propertyName)

#### Usage:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    If oDoc.PropertyExists("Custom Iproperty") Then
        'True!
    Else
        'False!
    End If

### IsPropertyNative

A simple static function that returns true/false if the specified property is one of Inventor's built-in iProperties.

#### Syntax:

IsPropertyNative(string name)
#### Usage:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    If IsPropertyNative("Custom Iproperty") Then
        'True!
    Else
        'False!
    End If

