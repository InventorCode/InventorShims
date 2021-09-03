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

