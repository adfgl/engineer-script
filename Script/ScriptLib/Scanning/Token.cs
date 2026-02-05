namespace ScriptLib.Scanning
{
    public readonly struct Token
    {
        readonly int group;
        public readonly Src src;
        public readonly TokenKind type;
        public readonly SrcPos pos;
        public readonly SrcSpan span;

        public Token(Src src, TokenKind type, int group, SrcPos pos, SrcSpan span)
        {
            this.src = src;
            this.type = type;
            this.group = group;
            this.pos = pos;
            this.span = span;
        }

        public Token(Src src, TokenKind type, SrcPos pos, SrcSpan span)
            : this(src, type, 0, pos, span) { }

        public static explicit operator Keyword(Token v)
        {
            if (v.type != TokenKind.Keyword)
            {
                throw new InvalidCastException();
            }
            return (Keyword)v.group;
        }

        public static explicit operator OperatorKind(Token v)
        {
            if (v.type != TokenKind.Operator)
            {
                throw new InvalidCastException();
            }
            return (OperatorKind)v.group;
        }

        public static explicit operator ScanError(Token v)
        {
            if (v.type != TokenKind.Error)
            {
                throw new InvalidCastException();
            }
            return (ScanError)v.group;
        }

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
