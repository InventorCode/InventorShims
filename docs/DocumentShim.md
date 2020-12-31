---
layout: default
title: DocumentShim
nav_order: 
---

# DocumentShim Extension Methods

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

This is an alternate form of this style of extension method. This alternate will work on similar methods listed throughout this document:

    DocumentShim.IsPart(Document document)

#### Usage:

Show a dialogue returning the answer to "Is the provided document an assembly?":

    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    msgbox(oDoc.IsAssembly)


## ZoomExtents

Zooms extents in a drawing, part, or assembly.
#### Syntax:
    document.ZoomExtents()

## OrbitToIsoFrontRightTop

Orbits around the part/assembly to show the view from a front isometric angle on the view cube

#### Syntax:
    document.OrbitToIsoFrontRightTop()

## Screenshot

Takes a screenshot of the document.

#### Syntax:
    document.ScreenShot(string locationToSaveImage, bool setWhiteBg = false, bool orbitToIso = false)

## SaveSilently

Saves the document without notifying the user.

#### Syntax:
    document.SaveSilently()


## SaveWithFileDialog

Saves, but shows the user a file dialog so they can pick where to save the document

#### Syntax:
    document.SaveWithFileDialog()


## ReturnSpecificDocumentObject

Returns a Document Object subtype if a subtype exists.  If not, a generic Inventor.Document is returned.  One caveat, objects that are created with this will not be able to use extension methods due to a limitation in .net.  You can instead use their customary method/function form instead.  E.g. When using late-bound objects extension methods of this form do not work...

    var tt = doc2.GetPropertyValue("Author");

But when using the below form those same methods do work...

    var tt = PropertyShim.GetPropertyValue(doc2, "Author");

#### Syntax:
    document.ReturnSpecificDocumentObject()

or

    ReturnSpecificDocumentObject(document)
