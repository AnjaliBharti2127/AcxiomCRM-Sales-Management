$ErrorActionPreference = 'Stop'
Set-Location $PSScriptRoot
function Check { if ($LASTEXITCODE -ne 0) { throw "Command failed: $LASTEXITCODE" } }
dotnet --info; Check
dotnet restore AcxiomCRM/AcxiomCRM.csproj; Check
dotnet tool restore; Check
dotnet build AcxiomCRM/AcxiomCRM.csproj; Check
dotnet dev-certs https --trust; Check
if (-not (Test-Path AcxiomCRM/Migrations)) { dotnet tool run dotnet-ef migrations add InitialCreate --project AcxiomCRM --startup-project AcxiomCRM; Check }
dotnet tool run dotnet-ef database update --project AcxiomCRM --startup-project AcxiomCRM; Check
$secret = Read-Host 'Choose a local demo password (8+ chars, upper/lower, number, symbol)' -AsSecureString
$plain = [System.Net.NetworkCredential]::new('', $secret).Password
try { dotnet user-secrets set 'Demo:Password' $plain --project AcxiomCRM; Check } finally { Remove-Variable plain }
dotnet user-secrets set 'Demo:Enabled' 'true' --project AcxiomCRM; Check
dotnet run --project AcxiomCRM
