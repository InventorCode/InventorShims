---
layout: default
title: Document[Type] Extension Methods
nav_order: 
---

# Document[Type] Extension Methods

## DocumentType Functions

A group of extension functions for the `Inventor.Document` object; each returns a boolean value indicating if the supplied document is of that particular type. The functions are listed in a table below.

| Function|Return Type|
|-
| IsAssembly|Boolean|
| IsDrawing|Boolean|
| IsPart|Boolean|
| IsPresentation|Boolean|
| IsForeignModel|Boolean|
| IsNest|Boolean|
| IsSAT|Boolean|
| IsUnknown|Boolean|


#### Syntax:

    Document.IsPart

#### Usage:

Show a dialogue returning the answer to "Is the provided document an assembly?":

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    msgbox(oDoc.IsAssembly)



