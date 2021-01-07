### Getting Started

First off, you'll want to get or build a copy of the InventorShims.dll file, or (or advanced users) extract whatever source code you want.  There are several ways to do this:

1. The simplest method is to visit the [Releases page](https://github.com/InventorCode/InventorShims/releases), and download the compiled InventorShims.dll file to your computer and reference it in your Inventor iLogic or add-in code.  This will allow you to access the entirety of the InventorShims API.
2. By cloning this repo and building the dll yourself.
3. By installing the (forthcoming) nuget package for use in Visual Studio.
4. By copying out the methods, classes, or modules that are useful and including those in your own vb or C# code-base.
5. By using the code in this package as reference material and examples to build your own solution.


### How to Use in iLogic

We'll look at the most simple (#1) method below, coding in iLogic.

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

- View the documentation pages for the API.  You can include just a portion of the API at a time if you want.  For example, if you want to add an include to the iProperty set of functions, your include statement would look like `Imports InventorShims.iProperty`


* For add-in development, add the InventorShims.dll as a reference to your project.
* Visit the [InventorShims API Documentation](https://inventorcode.github.io/InventorShims/) for a more detailed guide.
