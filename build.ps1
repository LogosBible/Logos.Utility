Task Default -depends SourceIndex

Task Build {
  Exec { msbuild /m:4 /property:Configuration=Release Logos.Utility.sln }
}

Task Tests -depends Build {
  mkdir build -force
  Exec { tools\NUnit\nunit-console.exe /nologo /framework=4.0 /xml=build\Logos.Utility.NUnit.xml tests\Logos.Utility.nunit }
}

Task SourceIndex -depends Tests {
  $headSha = & "C:\Program Files (x86)\Git\bin\git.exe" rev-parse HEAD
  Exec { tools\SourceIndex\github-sourceindexer.ps1 -symbolsFolder src\Logos.Utility\bin\Release -userId LogosBible -repository Logos.Utility -branch $headSha -sourcesRoot ${pwd} -dbgToolsPath "C:\Program Files (x86)\Windows Kits\8.0\Debuggers\x86" -gitHubUrl "https://raw.github.com" -serverIsRaw -verbose }
}

Task NuGetPack -depends SourceIndex {
  Exec { nuget pack src\Logos.Utility\Logos.Utility.csproj -Prop Configuration=Release -Symbols }
}
