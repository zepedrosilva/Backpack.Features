# run tests
Get-ChildItem -Path .\ -Filter Fixie.Console.exe -Recurse | % {
    & $_.FullName .\Backpack.Features.Tests\bin\Release\Backpack.Features.Tests.dll --NUnitXml TestResults.xml
}

# upload results to AppVeyor
$wc = New-Object 'System.Net.WebClient'
$wc.UploadFile("https://ci.appveyor.com/api/testresults/nunit/$($env:APPVEYOR_JOB_ID)", (Resolve-Path .\TestResults.xml))