# InventorShims

## About

This is an unofficial collection of extension APIs for Autodesk Inventor.  The goal is to present developers with the following:

* Additional API functionality that is not included with Inventor.  E.g. a set of simple regex functions that help the developer parse string object more effectively.
* Improved functionality of stock API objects/methods.  This may included simplified access to objects, more advanced management of objects, or safer code with better error handling outcomes.  E.g. an example is an iproperty access method that will not throw an exception when a non-present property is accessed.

The minimum supported version of Inventor will be 2020.

## How to Use

This package may be best utilized in one of the following ways...

1. By including the compiled .dll file into your project references.  This will allow you to access the entirety of the InventorShims package.
2. By copying out the methods, classes, or modules that are useful and including those in your own code-base. This will result in a smaller code footprint, but may be less convenient.
3. By using the code in this package as reference material and examples.

## Languages

At this time there are two languages in this repo: vb.net; and c#.  Each resides in it's own project within the Visual Studio solution.  There is still development on the integration of these two projects into a single dll resource file.

## Documentation

API Documentation can be found at the following location (API Docs)[https://inventorcode.github.io/InventorShims/].  The .md files for these documents are located in the /docs/ folder.

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

## License

This code is under an MIT license.
