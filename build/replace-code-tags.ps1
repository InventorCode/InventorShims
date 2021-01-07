#Convert html tags...
$projectRoot = (get-item $PSScriptRoot ).parent.FullName
$docsFiles = Get-ChildItem "$projectRoot\docs\" *.html -rec
foreach ($file in $docsFiles)
{
    (Get-Content $file.PSPath) |
    Foreach-Object { $_ -replace "<pre><code></code></pre>", "<br>" } |
    Set-Content $file.PSPath
}