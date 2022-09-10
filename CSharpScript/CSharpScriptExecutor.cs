using System.Reflection;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.VisualStudio.Text.Editor;
using MonoDevelop.Core;
using MonoDevelop.Core.Execution;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui;
using CSharpScript = Microsoft.CodeAnalysis.CSharp.Scripting.CSharpScript;

namespace VisualStudio.Mac.Scripting
{
    internal sealed partial class CSharpScriptExecutor
    {
        private const string ScriptFolder = "vsmacscripts";
        private Dictionary<int, Script<object>> _scripts = new();
        private ScriptOptions _scriptOptions = null;

        OutputProgressMonitor console = IdeServices.ProgressMonitorManager.GetOutputProgressMonitor("Scripting Output", Stock.Console, bringToFront: true, allowMonitorReuse: false);
        public async Task ExecuteAsync(ITextView textView, int scriptNumber, bool createEachTime)
        {
            try
            {
                Script<object> script;
                if (!TryGetScript(scriptNumber, out script))
                    return;

                var globals = new CSharpScriptGlobals(textView, Log);
                var scriptState = await script.RunAsync(globals, LogException);
            }
            catch (CompilationErrorException ex)
            {
                if (_scripts.ContainsKey(scriptNumber))
                    _scripts.Remove(scriptNumber);

                console.ErrorLog.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                LoggingService.LogError(ex.ToString());
                console.ErrorLog.WriteLine(ex.ToString());
            }
        }

        void Log(object message)
        {
            console.Log.WriteLine($"LOG [{DateTime.Now:u}]: {message}");
        }

        bool LogException(Exception ex)
        {
            console.ErrorLog.WriteLine(ex.ToString());
            return true;
        }

        static ScriptOptions GetScriptOptions(string scriptPath)
        {
            var ssr = ScriptSourceResolver.Default.WithBaseDirectory(scriptPath);
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            var searchPaths = new string[]
            {
                baseDirectory,
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
            };

            var smr = ScriptMetadataResolver.Default
                .WithBaseDirectory(scriptPath)
                .WithSearchPaths(searchPaths);

            var asm = new Assembly[]
            {
                typeof(MonoDevelop.Core.Runtime).Assembly,
                typeof(MonoDevelop.Ide.DesktopService).Assembly,
            };

            var so = ScriptOptions.Default
                  .WithSourceResolver(ssr)
                  .WithMetadataResolver(smr)
                  .WithEmitDebugInformation(true)
                  .WithReferences(asm);

            return so;
        }

        bool TryGetScript(int scriptNumber, out Script<object> script)
        {
            string scriptPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            scriptPath = Path.Combine(scriptPath, ScriptFolder);

            var scriptName = Directory.EnumerateFiles(scriptPath, $"{scriptNumber}*");

            if (!scriptName.Any())
            {
                console.ErrorLog.WriteLine($"Script file {scriptNumber} not found");
                script = null;
                return false;
            }

            string scriptFilePath = Path.Combine(scriptPath, scriptName.First());
            console.Log.WriteLine($"Running {scriptFilePath}");
            _scriptOptions ??= GetScriptOptions(scriptPath);

            script = CSharpScript.Create(File.ReadAllText(scriptFilePath), _scriptOptions, typeof(CSharpScriptGlobals));
            _scripts[scriptNumber] = script;
            return true;
        }
    }
}
