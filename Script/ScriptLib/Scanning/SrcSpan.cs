namespace ScriptLib.Scanning
{
    public readonly struct SrcSpan(int start, int length)
    {
        public readonly int start = start, length = length;

        public override string ToString()
            => $"{start}..{start + length}";
    }
}
