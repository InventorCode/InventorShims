### Contributions

Contributions of any type are welcome!  If you want to help...

* Please drop into [issues](https://github.com/InventorCode/InventorShims/issues) and see if there are any outstanding ones that you would like to tackle.
* Add any issues you want and we can discuss.
* Fork the code and add some functionality!

If you want to contribute, the process proceeds as follows:

1. fork the repo
2. create a new feature branch in your forked repo
3. make changes
4. commit your work
5. then issue a pull request back to this repo (upstream) for a code review.


### Branch Guidance

*Master* will be the main repository branch; this branch will contain the most up-to-date code.  All short-lived branches will be merged back into *Master* after a code review by repo maintainers.

The following branch names are suggested for short-lived branches...

* feature/xx
* bugfix/xx
* issue/xx
* username/xx

### Dependencies for Build

* Visual Studio Compatible IDE
* .Net 4.7 (for Inventor 2020 support)
* Inventor 2021 SDK installation (minimum)

### Build

Builds may be run by opening powershell or cmd in the repo folder and entering the following commands:

* `Nuke`, or
* `Nuke compile`

### Version Numbers

Versioning will follow [Sematic Versioning](https://semver.org/) once version 1.0.0 is released. The version numbers will follow the format `MAJOR.MINOR.PATCH` where:

* MAJOR - incompatible API changes
* MINOR - added functionality in a backwards compatible manner
* PATCH - backwards compatible bug fixes

### Commit Guidelines

[Conventional Commits](https://www.conventionalcommits.org/en) will be used as the commit message guidelines.  Following these simple guidelines will ensure commit message consistency that simplifies changelog generation and version number management.  Contributors are encouraged to visit the page and read the tutorial and spec.  Pull requests are expected to follow this convention.

A simple, breezy tutorial follows... commit messages should be in the form "KEYWORD: message".  Commit may have multiple lines. Try to keep each commit to a single keyword.  This keyword is one of the following:

- build
- chore
- ci
- docs
- feat
- fix
- refactor
- revert
- style
- test

Breaking changes in the codebase are denoted by an exclamation mark after a keyword, such as:

- feat!
- fix!
- etc...

A footer ```BREAKING CHANGE:``` should also be included in the commit message for human readability, but is not strictly required.

### License

This code is under an MIT license.