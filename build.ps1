Task Default -depends Coverity, SourceIndex

Task Build {
  Exec { msbuild /m:4 /property:Configuration=Release Logos.Utility.sln }
}

Task Tests -depends Build {
  mkdir build -force
  Exec { tools\NUnit\nunit-console.exe /nologo /framework=4.0 /xml=build\Logos.Utility.NUnit.xml tests\Logos.Utility.nunit }
}

Task Coverity -depends Tests {
  mkdir build\coverity -force
  & "C:\Program Files\Coverity\Coverity Static Analysis\bin\cov-analyze-cs.exe" `--all `--max-mem 8192 `--dir build\coverity src\Logos.Utility\bin\Release\Logos.Utility.dll
  if ($LastExitCode -ne 0) {
    throw "Error executing cov-analyze-cs.exe"
  }
  $headSha = & "C:\Program Files (x86)\Git\bin\git.exe" rev-parse HEAD
  & "C:\Program Files\Coverity\Coverity Static Analysis\bin\cov-commit-defects.exe" `--host coverity `--dataport 9090 `--stream Logos.Utility `--user logosutilityreporter `--password password `--dir build\coverity `--version $headSha `--strip-path $(get-location).Path
  if ($LastExitCode -ne 0) {
    throw "Error executing cov-commit-defects.exe"
  }
}

Task SourceIndex -depends Tests {
  $headSha = & "C:\Program Files (x86)\Git\bin\git.exe" rev-parse HEAD
  Exec { tools\SourceIndex\github-sourceindexer.ps1 -symbolsFolder src\Logos.Utility\bin\Release -userId LogosBible -repository Logos.Utility -branch $headSha -sourcesRoot ${pwd} -dbgToolsPath "C:\Program Files (x86)\Windows Kits\8.0\Debuggers\x86" -gitHubUrl "https://raw.github.com" -serverIsRaw -verbose }
}

Task NuGetPack -depends SourceIndex {
  Exec { nuget pack src\Logos.Utility\Logos.Utility.csproj -Prop Configuration=Release -Symbols }
}
