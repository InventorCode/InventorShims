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

API Documentation can be found at [InventorShims API Docs](https://inventorcode.github.io/InventorShims/).  Documentation is built with DocFX; the documentation source is located at [/docfx/](https://github.com/InventorCode/InventorShims/tree/master/docfx).

## Getting Started

First off, you'll want to get or build a copy of the InventorShims.dll file, or (or advanced users) extract whatever source code you want.  There are several ways to do this:

1. By visiting the [Releases page](https://github.com/InventorCode/InventorShims/releases), downloading the compiled InventorShims.dll file to your computer, and referencing it in your Inventor iLogic or add-in code.  This will allow you to access the entirety of the InventorShims API.
2. By cloning this repo and building the dll yourself.
3. By installing the (forthcoming) nuget package for use in Visual Studio.
4. By copying out the methods, classes, or modules that are useful and including those in your own vb or C# code-base.
5. By using the code in this package as reference material and examples to build your own solution.

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
