# Scripting
An extension that makes it easy to run csx files as VSMac extensions

Running the Scripting project should launch a new instance of VSMac with the addin loaded.

If this doesn't work the first time, try running

```sh
dotnet msbuild *.csproj /t:InstallAddin
```

This will generate the mpack file in the bin folder as well as attempt to load it into VSMac.

# Usage
Drop csx files in ~/vsmacscripts and name them 0-script1.csx 1-myscript.csx etc
and run them from inside vsmac with ctrl+shift+0 and ctrl+shift+1

There are 2 global objects available - `TextView` and `Log`
