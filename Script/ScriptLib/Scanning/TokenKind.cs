namespace ScriptLib.Scanning
{
    public enum TokenKind
    {
        Undefined,

        EndOfFile,
        LineBreak,
        TabBreak,

        Error,
        Keyword,
        Operator,

        LiteralNumeric,
        LiteralIdentifier,

        OpenParen,
        CloseParen,

        OpenSquare,
        CloseSquare,

        OpenCurly,
        CloseCurly,
    }
}
