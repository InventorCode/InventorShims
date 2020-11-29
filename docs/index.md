---
layout: default
title: Home
nav_order: 1
---

# InventorShims Documentation

## About

A collection of extension APIs for Autodesk Inventor.  In case you've zoomed by the project page, you can find the github repo at [InventorShims](https://github.com/InventorCode/InventorShims).

## How to Get

---
*Please note: InventorShims is currently a work in progress, and a release is forthcoming within the next few weeks.*
---

Visit the [releases page](https://github.com/InventorCode/InventorShims/releases) and download the latest InventorShims release.

## How to Install:

There are a variety of ways to make use of this API library.  The simplest is to load the InventorShims.dll into Inventor and start using the API in your iLogic code.  The more complicated is to clone the repo and extract code and fuctions as you need into your own solution.  We'll look at the typical (most simple) use case below.

## How to Use in iLogic

Here's a step by step guide to get this working with iLogic.

1. Place the InventorShims.dll somewhere your Inventor application can access it.  You may need to add this location to your External Rules Directory.
2. Place the following in your iLogic rule header:

```VB
AddReference "InventorShims.dll"
Imports InventorShims
```

3. If the path to the InventorShims.dll is not in your external rules direcotries, you can still find it, you just have to manually point to the dll file...

```VB
AddReference "C:\Path\To\The\InventorShims.dll"
Imports InventorShims
```

4. And that's it!  Now you can access the API from your iLogic code.  Call functions such as:

```VB
PathShim.IsLibraryPath("C:\This\Is\A\Test", ThisApplication)
```

5. View the documentation pages for the API on the left.  You can include just only a portion of the API at a time.  For example, if you want to add an include to the iProperty set of functions, you're include statement would look like `Imports InventorShims.iProperty`


