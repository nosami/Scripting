using System;
using System.Xml.Linq;
using Mono.Addins;
using Mono.Addins.Description;


[assembly: Addin(
"Scripting",
    Namespace = "Scripting",
    Version = VersionInfo.Version
)]

[assembly: AddinName("Scripting")]
[assembly: AddinCategory("IDE extensions")]
[assembly: AddinUrl("https://whatever")] //
[assembly: AddinDescription("Scripting extension for VSMac")]
[assembly: AddinAuthor("Jason Imison")]

public static class VersionInfo
{ 
    public const string Version = "0.0.0.1";
}
