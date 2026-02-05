namespace ScriptLib.Scanning
{
    public enum OperatorKind
    {
        None = 0,
        Plus,
        Minus,
        Multiply,
        Divide,
        Pow,

        Assign,

        Equal,
        NotEqual,
        Less,
        LessEqual,
        Greater,
        GreaterEqual,

        And,
        Or,
        Not,
    }
}
