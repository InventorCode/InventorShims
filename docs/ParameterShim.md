---
layout: default
title: ParameterShim
nav_order: 
---

# ParameterShim

## Methods

### GetParameterValue

Returns the specified document parameter's value as a string. If the parameter is not found, an empty string is returned.

#### Syntax:

    document.GetParameterValue(string parameterName)

This is an alternate form of this style of extension method. This alternate will work on similar methods listed throughout this document:

    GetParameterValue(Document document, string parameterName)

#### Usage:

Get the value of a parameter:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    msgbox(oDoc.GetParameterValue("MyParameter"))


### SetParameterValue

Set the specified document parameter's value.  If the parameter already exists it will set the value.  If the name does not exist, it will add the parameter to the document. There are several versions of this method that will create different types of parameters.  Numeric, text, and boolean signatures are supported.  If the parameter already exists, an optional clobberFlag parameter = true will restrict modification of the parameter's value.

#### Syntax:

Numeric signature:

    document.SetParameterValue(string parameterName, string parameterValue, string units, optional bool clobberFlag)

Text signature:

    document.SetParameterValue(string parameterName, string parameterValue, optional bool clobberFlag)

Boolean signature:

    document.SetParameterValue(string parameterName, bool parameterValue, optional bool clobberFlag)

#### Usage:

Set value for a numeric parameter:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    oDoc.SetParameterValue("MyNumericParameter", "20.000", "in")

Set value for a text parameter:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    oDoc.SetParameterValue("MyTextParameter", "Here we go!")

Set value for a boolean parameter:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    oDoc.SetParameterValue("MyBooleanParameter", True)


### RemoveParameter

Removes the specified document parameter.  If the parameter is a reference, derived, or table parameter they will not be removed.   Parameters that are in use in the file will not be removed.

#### Syntax:

    document.RemoveParameter(string parameterName)

#### Usage:

Remove a parameter:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    oDoc.RemoveParameter("MyParameter")    


### GetParameter

A simple function that returns a Parameter (if it exists) from the provided document.  If the parameter does not exist, a null Parameter object is returned.

#### Syntax:
    document.GetParameter(string parameterName)

#### Usage:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    Dim oParameter As Inventor.Parameter

    oParameter = oDoc.GetParameter("MyParameter")


### GetParameters

A simple function that returns a Parameters collection for the provided document.

#### Syntax:

    document.GetParameters

#### Usage:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    Dim oParams As Inventor.Parameters

    oParams = oDoc.GetParameters


### ParameterIsWritable

A simple function that returns a boolean value indicating if the provided parameter is writable by the user.

#### Syntax:

    ParameterIsWritable(Parameter parameter)

#### Usage:

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    Dim oParameter As Inventor.Parameter = oDoc.GetParameter("MyParameter")

    If (ParameterShim.ParameterIsWritable == true) Then
        'do something with the parameter
    End If
        
