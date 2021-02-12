# InventorShims

## About

This is an unofficial collection of extension APIs for Autodesk Inventor.  The goal is to present developers and iLogic users with the following:

* Code that promises to be routinely and consistently useful,
* Additional API functionality that is not included in Inventor's public APIs, or
* Improved functionality of stock API objects/methods.  This may include:
  * simplified access to API objects, functions, and values,
  * functions designed to reduce boilerplate code,
  * more advanced management of API objects, and
  * better error handling outcomes.  E.g. an example is an iproperty access method that will not throw an exception when a non-present property is accessed.

The minimum supported version of Inventor will be 2020.

## Documentation

Find the detailed API Documentation at [InventorShims API Docs](https://inventorcode.github.io/InventorShims/api/InventorShims.html).  This documentation is built using DocFX; the documentation source is at [/docfx/](https://github.com/InventorCode/InventorShims/tree/master/docfx).

For general documentation see the [Quick-Start Guide](https://inventorcode.github.io/InventorShims/articles/quick-start.html).

## Getting Started

First off, you'll want to download or build a copy of the InventorShims.dll file. There are several options available:

1. Include the [InventorShims nuget package](https://www.nuget.org/packages/InventorShims/) in your .net project.
2. Visit the [Releases page](https://github.com/InventorCode/InventorShims/releases) and downld the compiled InventorShims.dll and referenceit in your Inventor iLogic or add-in code.  This will allow you to access the entirety of the InventorShims API.
3. Clone this repo and building the dll yourself.
4. Copy out the methods, classes, or modules that are useful and including those in your own code.
5. Use the code in this package as reference material or examples.

## How to Use

* For iLogic, add the following code in your iLogic rule header:

    ```VB
    AddReference "C:\Path\To\File\InventorShims.dll"
    Imports InventorShims
    
    'Add your code here, include InventorShims API functions as you need...
    ```
* For add-in development, add the InventorShims.dll as a reference to your project.
* Visit the [InventorShims API Documentation](https://inventorcode.github.io/InventorShims/) for a more detailed guide.

## License

This code is under an MIT license.
