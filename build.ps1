Task Default -depends Tests

Task Build {
  Exec { msbuild /m:4 /property:Configuration=Release Logos.Utility.sln }
}

Task Tests -depends Build {
  Exec { tools\NUnit\nunit-console.exe /nologo /framework=4.0 tests\Logos.Utility.nunit }
}
