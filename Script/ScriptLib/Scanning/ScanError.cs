using System;

namespace ScriptLib.Scanning
{
    public enum ScanError
    {
        None = 0,
        UnterminatedString,
        InvalidEscape,
        UnknownCharacter,
        UnterminatedComment
    }
}
