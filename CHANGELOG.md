## [Unreleased](https://github.com/InventorCode/InventorShims/releases/tag/2.40) (2022-01-??)

### Added

- PropertyShim extension methods now work on the following document types:
    - PartDocument
    - AssemblyDocument
    - DrawingDocument
    - PresentationDocument

- ParameterShim extension methods now work with the following document types:
    - PartDocument
    - AssemblyDocument
    - DrawingDocument

- Extension methods for use with LINQ and fluent design.

#### New general extension methods:

|extension method|on object|returns|
|---|---|---|
|IsContentCenter|Document, PartDocument, AssemblyDocument, DrawingDocument, PresentationDocument|bool|
|IsCustomContentCenter|Document, PartDocument, AssemblyDocument, DrawingDocument, PresentationDocument|bool|

#### New IEnumerable Providers

|extension method|on object|returns|
|---|---|---|
|EnumerateDocuments|SelectSet|IEnumerable\<Document>|
|EnumerateDocuments|IEnumerable\<DocumentDescriptor>|IEnumerable\<Document>|
|EnumerateAllReferencedDocuments|Document|IEnumerable\<Document>|
|EnumerateAllReferencedDocuments|Document, PartDocument, AssemblyDocument, DrawingDocument, PresentationDocument|IEnumerable\<Document>|
|EnumerateReferencedDocuments|Document, PartDocument, AssemblyDocument, DrawingDocument, PresentationDocument|IEnumerable\<Document>|
|EnumerateReferencingDocuments|Document, PartDocument, AssemblyDocument, DrawingDocument, PresentationDocument|IEnumerable\<Document>|
|EnumerateReferencedDocumentDescriptors|Document, PartDocument, AssemblyDocument, DrawingDocument, PresentationDocument|IEnumerable\<DocumentDescriptors>|
|EnumerateAllLeafOccurrencesDocumentDescriptors|AssemblyDocument|IEnumerable\<DocumentDescriptors>|
|EnumerateParameters|Document, PartDocument, AssemblyDocument, DrawingDocument|IEnumerable\<Parameter>|

#### New IEnumerable Filters

|extension method|on object|returns|
|---|---|---|
|AssemblyDocuments|IEnumerable\<Document>|IEnumerable\<AssemblyDocument>|
|DrawingDocuments|IEnumerable\<Document>|IEnumerable\<DrawingDocument>|
|PresentationDocuments|IEnumerable\<Document>|IEnumerable\<PresentationDocument>|
|PartDocuments|IEnumerable\<Document>|IEnumerable\<PartDocument>|
|RemoveAssemblyDocuments|IEnumerable\<Document>|IEnumerable\<Document>|
|RemoveDrawingDocuments|IEnumerable\<Document>|IEnumerable\<Document>|
|RemovePresentationDocuments|IEnumerable\<Document>|IEnumerable\<Document>|
|RemovePartDocuments|IEnumerable\<Document>|IEnumerable\<Document>|
|RemoveNonNativeDocuments|IEnumerable\<Document>|IEnumerable\<Document>|

### Changed
- .Net Framework version bumped to 4.8
## [1.3.1](https://github.com/InventorCode/InventorShims/releases/tag/1.3.1) (2021-09-03)

### Added
- ExternalRuleDirectories object now implements the IDisposable interface, allowing the using keyword.
- ExternalRuleDirectories documentation.
- Updated Nuke Build system.

### Changed
- Integration test project "InventorShims.tests" now uses the NUnit test framework.
    - Refactored some duplicate tests into TestCases. 
    - Refactored Inventor document creation routines to TestUtilities.cs.
- Nuke build updated to Nuke 5.3.0
    - Nuget packages for build project updated.
- Nuke build targets updated/created:
    - Nuke Compile
    - Nuke Pack
    - Nuke Push
    - Nuke BuildDocumentation
    - Nuke PublishGitHubRelease
- InventorShims.manual tests changed to InventorShims.ilogic.tests.  Removed VS project associated with this.

### Fixed
- ExternalRuleDirectories.Add was not adding entires properly.
- ExternalRuleDirectories.Remove was not removing entires properly.

### Removed
- InventorShims-vb project has been removed.
- Errant NewtonSoft.Json package reference

## [1.2.0](https://github.com/InventorCode/InventorShims/releases/tag/1.2.0) (2021-08-15)

### Added
- ExternalRuleDirectories object allows you to manipulate the iLogic Addin's list of External Rules Directories in a more straightforward manner.

### Fixed
- Added conventional commits keywords to gitversion configuration.


## [1.1.1](https://github.com/InventorCode/InventorShims/releases/tag/v1.1.1) (2021-02-11)

### Chore

* remove ILMerge nuget package

### Docs

* rebuild
* remove InventorShims-vb


## [v1.1.0](https://github.com/InventorCode/InventorShims/releases/tag/v1.1.0) (2021-02-11)

### Chore

* update nuget config, changelog, versions
* add nuspec
* add logo for nuget package
* Removed InventorShims-vs from builds
* renamed InventorShims-cs to InventorShims

### Docs

* added initial changelog
* removed reference to pre-release state.
* Updated API docs link in readme
* Added samples to ParameterShim, added more to the class description.
* Added ParameterShim and PathShim samples.

### Feat

* added git-chglog config
* ParameterIsWritable string signature added
* ParameterIsWritable now acts as an extension method

### Style

* remove whitespace

### Test

* Added ParameterIsWritable unit tests.

## [v1.0.0](https://github.com/InventorCode/InventorShims/releases/tag/v1.0.0) (2021-01-17)

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

* Added debug statements to GetDocumentsFromSelectSet()
* GetDocumentFromObject.tests updated
* ApplicationShim tests now wait for Inventor processes to finish.

### Tests

* modified for newest PathShim.cs

## [v0.1.0](https://github.com/InventorCode/InventorShims/releases/tag/v0.1.0) (2020-12-25)

