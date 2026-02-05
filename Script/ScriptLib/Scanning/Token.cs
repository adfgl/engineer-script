namespace ScriptLib.Scanning
{
    public readonly struct Token(Src src, TokenKind type, SrcPos pos, SrcSpan span)
    {
        public readonly Src src = src;
        public readonly TokenKind type = type;
        public readonly SrcPos pos = pos;
        public readonly SrcSpan span = span;

        public override string ToString()
        {
            return src.GetString(span);
        }

        public string AsString()
        {
            return $"{pos} '{src.GetString(span)}' [{type}]";
        }
    }
}
