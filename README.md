# InventorShims

## About

This is an unofficial collection of extension APIs for Autodesk Inventor.  The goal is to present developers and iLogic users with the following:

* Additional API functionality that is not included with Inventor.  E.g. a set of simple regex functions that help the developer parse string object more effectively.
* Improved functionality of stock API objects/methods.  This may included simplified access to objects, more advanced management of objects, or safer code with better error handling outcomes.  E.g. an example is an iproperty access method that will not throw an exception when a non-present property is accessed.

The minimum supported version of Inventor will be 2020.

## Documentation

API Documentation can be found at [InventorShims API Docs](https://inventorcode.github.io/InventorShims/).  The source files for these documents are located in [/docs/](https://github.com/InventorCode/InventorShims/tree/master/docs).

## How to Use

This package may be utilized in one of the following ways...

1. By downloading the compiled InventorShims.dll file to your computer and including it into your Inventor iLogic code.  This will allow you to access the entirety of the InventorShims API.  Insert the following code in your iLogic rule header:

```VB
AddReference "C:\Path\To\File\InventorShims.dll"
Imports InventorShims
```

2. By cloning this repo and building the dll yourself.
3. By copying out the methods, classes, or modules that are useful and including those in your own vb or C# code-base. This will result in a smaller code footprint.
4. By using the code in this package as reference material and examples to build your own solution.

## Languages

At this time there are two languages in this repo: vb.net; and c#.  Each resides in it's own project within the Visual Studio solution.  There is still development on the integration of these two projects into a single dll resource file.

## Contributions

Contributions of any type are welcome!  If you want to help...

* Please drop into issues and see if there are any outstanding ones that you would like to tackle.
* Add any issues you want and we can discuss.
* Fork the code and add some functionality!

If you want to contribute, the process proceeds as follows:

1. fork the repo
2. create a new feature branch in your forked repo
3. make changes
4. commit your work
5. then issue a pull request back to this repo (upstream) for a code review.

## Branch Guidance

*Master* will be the main repository branch; this branch will contain the most up-to-date code.  All short-lived branches will be merged back into *Master* after a code review by repo maintainers.

The following branch names are suggested for short-lived branches...

* feature/xx
* bugfix/xx
* issue/xx
* username/xx

## Dependencies for Build

* Visual Studio Compatible IDE
* .Net 4.7 (Inventor 2020 support)
* ILMerge nuget package
* Inventor 2020 SDK installation

## Version Numbers

Versioning will follow [Sematic Versioning](https://semver.org/).  The version numbers will follow the format `MAJOR.MINOR.PATCH` where:

* MAJOR - incompatible API changes
* MINOR - added functionality in a backwards compatible manner
* PATCH - backwards compatible bug fixes

## License

This code is under an MIT license.
