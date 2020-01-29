# Based on https://stackoverflow.com/a/49019179/25702
<#
.SYNOPSIS
    Runs test projects (all target platforms) and uploads results to AppVeyor
#>
param (
    [string]
    $Config = "Debug"
)

$failed = $false

# Find each test project and run tests and upload results to AppVeyor
Get-ChildItem .\**\*.csproj -Recurse | 
    Where-Object { $_.Name.StartsWith("Tests") } |
    Where-Object { $_.BaseName -ne "TestsExplicit" } |
    ForEach-Object { 

        # Run dotnet test on the project and output the results in mstest format (also works for other frameworks like nunit)
        & dotnet test $_.FullName --configuration $Config --no-build --no-restore --logger "trx;LogFilePrefix=test-results" 

        $failed = $failed -or $LASTEXITCODE

        $trxFiles = Get-ChildItem "$($_.Directory)\TestResults\*.trx"
        # if on build server upload results to AppVeyor
        if ("${ENV:APPVEYOR_JOB_ID}" -ne "") {
            $wc = New-Object 'System.Net.WebClient'

            $trxFiles | ForEach-Object {
                $wc.UploadFile("https://ci.appveyor.com/api/testresults/mstest/$($env:APPVEYOR_JOB_ID)", (Resolve-Path $_.FullName))
            }
        }

        # don't leave the test results lying around
        $trxFiles | Remove-Item -ErrorAction SilentlyContinue
}

if ($failed) {
    exit 1
}