namespace ScriptLib.Scanning
{
    public readonly struct SrcPos(int line, int column)
    {
        public readonly int line = line, column = column;

        public override string ToString()
        {
            return $"Ln: {line + 1} Ch: {column + 1}";
        }
    }
}
