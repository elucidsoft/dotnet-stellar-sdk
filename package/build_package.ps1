$scriptpath = $MyInvocation.MyCommand.Path
$dir = Split-Path $scriptpath

Push-Location $dir

./nuget pack ./dotnet-stellar-sdk.nuspec