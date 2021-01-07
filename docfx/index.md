# InventorShims Documentation

### About

InventorShims is an unofficial collection of extension APIs for Autodesk Inventor.  In case you've zoomed by the project page, you can find it here: [InventorShims](https://github.com/InventorCode/InventorShims).

The following are the stated goals of this API, and will be used to determine what is included in the code-base:

* Functionality that promises to be routinely and consistently useful,
* Additional API functionality that is not included in Inventor's public APIs, or
* Improved functionality of stock API objects/methods.  This may include:
  * simplified access to API objects, functions, and values,
  * functions designed to reduce boilerplate code,
  * improved management of stock API objects or behavior, or
  * better error handling outcomes (fail gracefully).
  
The minimum supported version of Inventor will be 2020.

### Get Started

You can visit the other docs on this site to learn more about the API, how to use it, and how to report issues and bugs.  To get started immediately, visit the [Quick Start](articles/quick-start.md) page to learn how start using InventorShims in just a few minutes.

### Download

*Please note: InventorShims is currently in pre-release state. As initial development continues, the API structure may be in flux slightly until 1.0.0 is released.  Major architectural changes should be finished, but things may still shift around.  Luckily deployment of an updated version takes less than a minute.*

Visit the [releases page](https://github.com/InventorCode/InventorShims/releases) and download the latest InventorShims release.

### How to Install

There are a variety of ways to make use of this API library.  The simplest is to download the dll and load it in iLogic by including the following in your iLogic rule's header...  

    AddReference "InventorShims.dll"
    Imports InventorShims

Visit the [Quick-Start](articles/quick-start.md) guide for some more in-depth code samples and information on how to get started.