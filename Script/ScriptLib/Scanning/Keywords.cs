using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptLib.Scanning
{
    public static class Keywords
    {
        static readonly Dictionary<string, Keyword> _keywords = new Dictionary<string, Keyword>()
        {
            { "true", Keyword.True },
            { "false", Keyword.False },
            { "let", Keyword.Let },
            { "as", Keyword.As },
        };

        public static IReadOnlyDictionary<string, Keyword> Source => _keywords;
    }
}
