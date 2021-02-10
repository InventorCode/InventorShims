
<a name="v1.0.0"></a>
## [v1.0.0](https://github.com/InventorCode/InventorShims/compare/v0.1.0...v1.0.0) (2021-01-17)

### Build

* v1.0.0

### Docs

* new build
* Added Conventional Commits as the commit format guideline.
* added --force to docfx rebuild.  Added new template reference for icons
* added logo and favicon
* rebuilt with manual fix for "view source" error
* Added an ilogic rule sample
* tentative fix for "View Source" in docfx not working.

### Feat

* AttributeShim methods are now extension methods.

### Fix

* fixed typoe in sample.  Added commented out msgbox call.
* GetDocumentFromObject for kDrawingDurveSegmentObject

### Refactor

* added error handling to SaveSilently() method
* PathShim moved to C#
* added todo for ittermittent bug in test

### Test

* Added debug statments to GetDocumentsFromSelectSet()
* GetDocumentFromObject.tests updated
* ApplicationShim tests now wait for Inventor processes to finish.

### Tests

* modified for newest PathShim.cs


<a name="v0.1.0"></a>
## v0.1.0 (2020-12-25)

