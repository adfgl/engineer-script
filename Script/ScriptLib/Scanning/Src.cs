using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptLib.Scanning
{
    public class Src(string source)
    {
        readonly string _content = source ?? string.Empty;
        readonly Dictionary<SrcSpan, string> _cache = new Dictionary<SrcSpan, string>();

        public string Content => _content;
        public int Length => _content.Length;

        public ReadOnlySpan<char> Slice(SrcSpan span)
        {
            int start = span.start;
            int len = span.length;
            int max = _content.Length;

            if ((uint)start >= (uint)max)
                return ReadOnlySpan<char>.Empty;
            if (start + len > max)
                len = max - start;
            if (len <= 0)
                return ReadOnlySpan<char>.Empty;

            return _content.AsSpan(start, len);
        }

        public string GetString(SrcSpan span)
        {
            if (_cache.TryGetValue(span, out var cached))
                return cached;

            ReadOnlySpan<char> slice = Slice(span);
            if (slice.IsEmpty)
            {
                return string.Empty;
            }

            string result = new string(slice);
            _cache[span] = result;
            return result;
        }

        public override string ToString() => _content;
    }
}
