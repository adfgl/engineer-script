using ScriptLib.Scanning;

namespace ScriptLib.Parsing
{
    public sealed class Diagnostics
    {
        readonly List<Diagnostic> _items = new();

        public IReadOnlyList<Diagnostic> Items => _items;

        public void Report(Src source, Severity severity, string message, SrcPos pos, SrcSpan span)
        {
            _items.Add(new Diagnostic(source, severity, message, pos, span));
        }

        public void Report(Severity severity, string message, Token token)
        {
            _items.Add(new Diagnostic(token.src, severity, message, token.pos, token.span));
        }

        public bool HasErrors
        {
            get
            {
                return _items.Any(d => d.severity == Severity.Error);
            }
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}
