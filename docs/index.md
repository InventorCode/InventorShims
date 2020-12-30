---
layout: default
title: Home
nav_order: 1
---

# InventorShims Documentation

## About

A collection of extension APIs for Autodesk Inventor.  In case you've zoomed by the project page, you can find the github repo at [InventorShims](https://github.com/InventorCode/InventorShims).

## How to Get

*Please note: InventorShims is currently a work in progress, and a release is forthcoming within the next few weeks.*

Visit the [releases page](https://github.com/InventorCode/InventorShims/releases) and download the latest InventorShims release.

## How to Install

There are a variety of ways to make use of this API library.  The simplest is to load the InventorShims.dll into Inventor and start using the API in your iLogic code.  The more complicated is to clone the repo and extract code and fuctions as you need into your own solution.  We'll look at the typical (most simple) use case below.

## How to Use in iLogic

Here's a step by step guide to get this working with iLogic.

- Place the InventorShims.dll somewhere your Inventor application can access it.  You may need to add this location to Inventor's External Rules Directory.
- Place the following in your iLogic rule's header:

    ```VB
    AddReference "InventorShims.dll"
    Imports InventorShims
    ```

- If the path to the InventorShims.dll is not in one of Inventor's external rules directories, you can still reference it by providing the full path to the dll file...

    ```VB
    AddReference "C:\Path\To\The\InventorShims.dll"
    Imports InventorShims
    ```

- And that's it!  Now you can access the API from your iLogic code.  Call functions such as:

    ```VB
    PathShim.IsLibraryPath("C:\This\Is\A\Test", ThisApplication)
    ```

- View the documentation pages for the API on the left.  You can include just only a portion of the API at a time.  For example, if you want to add an include to the iProperty set of functions, you're include statement would look like `Imports InventorShims.iProperty`


