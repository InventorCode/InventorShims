---
layout: default
title: PropertyShim Class
nav_order: 
---

# PropertyShim Class

## Methods

GetProperty (function)
: Return the specified document property's value as an object.  Tries to get the built-in properties first, then resorts to User-Defined Iproperties, and then searches through totally custom property groups.

Syntax: `GetProperty(doc As Inventor.Document, propertyName As String)`

Usage: 

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    Dim value As String
    value = GetProperty(oDoc, "Title")    

SetProperty
: Set the specified document property's value.  If the ipropery name exist it will set the value.  If the name does not exist, it will add the property with the value you have specified.  Completely custom property groups are not utilized in this signature.

Usage: 

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    Dim value As String
    value = SetProperty(oDoc, "Title", "Custom File Title!")    
