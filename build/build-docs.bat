REM Requires docfx installed to work...

docfx ../docfx/docfx.json --force -t default,../docfx/templates/inventor-shims 
PowerShell.exe -Command "& './replace-code-tags.ps1'"