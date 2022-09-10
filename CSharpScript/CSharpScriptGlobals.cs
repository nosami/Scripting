using Microsoft.VisualStudio.Text.Editor;

namespace VisualStudio.Mac.Scripting
{
    public class CSharpScriptGlobals
    {
        public ITextView TextView { get; }
        public Action<object> Log { get; }

        public CSharpScriptGlobals(ITextView textView, Action<object> logger)
        {
            TextView = textView;
            Log = logger;
        }
    }
}
