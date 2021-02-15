cd ..
$version = gitversion /output json /showvariable MajorMinorPatch
git-chglog --next-tag $version -o CHANGELOG.md
