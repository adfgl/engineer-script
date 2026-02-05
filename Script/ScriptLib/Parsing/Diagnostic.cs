using ScriptLib.Scanning;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptLib.Parsing
{
    public readonly struct Diagnostic
    {
        public readonly Severity severity;
        public readonly string message;
        public readonly SrcPos position;
        public readonly SrcSpan span;
        public readonly Src source;

        public Diagnostic(Src source, Severity severity, string message, SrcPos position, SrcSpan span)
        {
            this.source = source;
            this.severity = severity;
            this.message = message;
            this.position = position;
            this.span = span;
        }

        public override string ToString()
            => $"{severity}: {message} at {position} '{source.GetString(span)}'";
    }
}
