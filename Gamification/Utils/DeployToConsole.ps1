$extensions = @()

$extensions += 'SVNExtension'
$extensions += 'LanguageExtension'

$basepath = '.\..\{0}\bin\Debug'
$deployPath = '..\Gamification\bin\Debug'

$extensions | foreach{
    Get-ChildItem -Path ($basepath -f $_) -Exclude "*.config" | foreach{
        Copy-Item -Path $_.FullName -Destination $deployPath -Force 
        Copy-Item -Path $_.FullName -Destination "$deployPath\extensions" -Force 
    }
}