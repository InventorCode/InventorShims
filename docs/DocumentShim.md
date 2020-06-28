---
layout: default
title: DocumentShim Class
nav_order: 
---

# DocumentShim Class

## Methods

### DocumentType Functions

A series of functions that return true or false depending on the document type of the supplied document.  The functions are listed in a table below.

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

    IsAssembly(doc As Inventor.Document)

#### Usage:

Show a dialogue returning the answer to "Is the provided document an assembly?":

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    msgbox(IsAssembly(oDoc))



