---
layout: default
title: ParameterShim
nav_order: 
---

# ParameterShim

## Methods

### GetParameter

Returns the specified document parameter's value as a string. If the parameter is not found, an empty string is returned.

#### Syntax:

    document.GetParameter(string parameterName)

#### Usage:

Get the value of a parameter:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    msgbox(oDoc.GetParameter("MyParameter"))


### SetParameter

Set the specified document parameter's value.  If the parameter already exists it will set the value.  If the name does not exist, it will add the parameter to the document. There are several versions of this method that will create different types of parameters.  Numeric, text, and boolean signatures are supported.  If the parameter already exists, an optional clobberFlag boolean will not modify the parameter's value.

#### Syntax:

Numeric signature:
    document.SetParameter(string parameterName, string parameterValue, string units, optional bool clobberFlag)

Text signature:
    document.SetParameter(string parameterName, string parameterValue, optional bool clobberFlag)

Boolean signature:
    document.SetParameter(string parameterName, bool parameterValue, optional bool clobberFlag)

#### Usage:

Set value for a numeric parameter:
    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    oDoc.SetParameter("MyNumericParameter", "20.000", "in")

Set value for a text parameter:
    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    oDoc.SetParameter("MyTextParameter", "Here we go!")

Set value for a boolean parameter:
    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    oDoc.SetParameter("MyBooleanParameter", True)


### RemoveParameter

Removes the specified document parameter.  If the parameter is a reference, derived, or table parameter they will not be removed.   Parameters that are in use in the file will not be removed.

#### Syntax:

    document.RemoveParameter(string parameterName)

#### Usage:

Remove a parameter:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    oDoc.RemoveParameter("MyParameter")    


### GetParameters

A simple function that returns a Parameters collection for the provided document.

#### Syntax:
    document.GetParameters

#### Usage:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    Dim oParams As Inventor.Parameters

    oParams = oDoc.GetParameters