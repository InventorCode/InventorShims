---
layout: default
title: PathShim Class
nav_order: 
---

# PathShim Class

## Methods

### UpOneLevel Function

A simple parser that takes in a file or directory path as a string and returns the directory location one level up.  E.G.

#### Syntax:

    UpOneLevel(path As String) As String


#### Usage:

    PathShim.UpOneLevel("C:\This\Is\A\Test")

will return the following
`"C:\This\Is\A"`

### IsContentCenterPath Function

Tis function takes a file or directory path as a string and will determine if that path is a Content Center path.  This function will return a boolean value. 

#### Syntax:

    IsContentCenterPath(path As String, inventorApp As Inventor.Application) As Boolean


### Usage:

    PathShim.IsContentCenterPath("C:\This\Is\A\Test", ThisApplication)

or...

    PathShim.IsContentCenterPath(ThisDocument.FullFileName, ThisApplication)

### IsLibraryPath Function

This function takes a file or directory path as a string and will determine if that path is a Library path.  This function will return a boolean value. 

#### Syntax:

IsLibraryPath(path As String, inventorApp As Inventor.Application) As Boolean

### Usage:

    PathShim.IsLibraryPath("C:\This\Is\A\Test", ThisApplication)

or...

    PathShim.IsLibraryPath(ThisDocument.FullFileName, ThisApplication)

### TrimEndingDirectorySeparator Function

This function trims the last directory separator character (if it exists) from a string.

#### Syntax:

    TrimEndingDirectorySeparator(ByVal path As String)
