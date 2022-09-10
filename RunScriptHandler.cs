using Microsoft.VisualStudio.Text.Editor;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;

namespace VisualStudio.Mac.Scripting
{
    class RunScriptHandler : CommandHandler
    {
        static readonly CSharpScriptExecutor executor = new();
        protected override void Update(CommandInfo info)
        {
            var doc = IdeApp.Workbench.ActiveDocument;
            info.Visible = doc != null && doc.GetContent<ITextView>() != null;
        }

        protected async Task RunScript(int scriptNumber)
        {
            var doc = IdeApp.Workbench.ActiveDocument;
            var textView = doc.GetContent<ITextView>();
            await executor.ExecuteAsync(textView, scriptNumber, true);
        }
    }

    class RunScriptHandler0 : RunScriptHandler
    {
        protected async override void Run(object dataItem)
        {
            await base.RunScript(0);
        }
    }

    class RunScriptHandler1 : RunScriptHandler
    {
        protected async override void Run(object dataItem)
        {
            await base.RunScript(1);
        }
    }

    class RunScriptHandler2 : RunScriptHandler
    {
        protected async override void Run(object dataItem)
        {
            await base.RunScript(2);
        }
    }

    class RunScriptHandler3 : RunScriptHandler
    {
        protected async override void Run(object dataItem)
        {
            await base.RunScript(3);
        }
    }

    class RunScriptHandler4 : RunScriptHandler
    {
        protected async override void Run(object dataItem)
        {
            await base.RunScript(4);
        }
    }

    class RunScriptHandler5 : RunScriptHandler
    {
        protected async override void Run(object dataItem)
        {
            await base.RunScript(5);
        }
    }

    class RunScriptHandler6 : RunScriptHandler
    {
        protected async override void Run(object dataItem)
        {
            await base.RunScript(6);
        }
    }

    class RunScriptHandler7 : RunScriptHandler
    {
        protected async override void Run(object dataItem)
        {
            await base.RunScript(7);
        }
    }

    class RunScriptHandler8 : RunScriptHandler
    {
        protected async override void Run(object dataItem)
        {
            await base.RunScript(8);
        }
    }

    class RunScriptHandler9 : RunScriptHandler
    {
        protected async override void Run(object dataItem)
        {
            await base.RunScript(9);
        }
    }
}
