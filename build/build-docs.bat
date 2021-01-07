REM Requires docfx installed to work...

docfx ../docfx/docfx.json
PowerShell.exe -Command "& './replace-code-tags.ps1'"