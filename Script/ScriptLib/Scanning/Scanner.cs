using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptLib.Scanning
{
    public sealed class Scanner
    {
        public const int LOOKAHEAD_SIZE = 10;
        readonly Token[] _lookaheadBuffer = new Token[LOOKAHEAD_SIZE];

        int _bufferStart = 0;
        int _bufferCount = 0;

        TokenReader _reader = new TokenReader();
        Token _previous = new Token();

        public Src Source
        {
            get => _reader.Source;
            set => _reader.Source = value;
        }

        public Token Previous => _previous;

        public void Reset()
        {
            _reader.Reset();
            _bufferStart = 0;
            _bufferCount = 0;
        }

        public IEnumerable<Token> ReadAll()
        {
            while (true)
            {
                Token t = _reader.Read();
                yield return t;

                if (t.type == TokenKind.EndOfFile)
                    yield break;
            }
        }

        public Token Consume()
        {
            Token token;
            if (_bufferCount > 0)
            {
                token = _lookaheadBuffer[_bufferStart++];
                if (_bufferStart == LOOKAHEAD_SIZE)
                {
                    _bufferStart = 0;
                }
                _bufferCount--;

                _previous = token;
                return token;
            }
            else
            {
                token = _reader.Read();
            }
            _previous = token;
            return token;
        }

        public Token Peek(int offset = 0)
        {
            while (_bufferCount <= offset)
            {
                if (_bufferCount >= LOOKAHEAD_SIZE)
                {
                    throw new InvalidOperationException("Lookahead buffer size exceeded.");
                }

                int insertIndex = _bufferStart + _bufferCount;
                if (insertIndex >= LOOKAHEAD_SIZE)
                {
                    insertIndex -= LOOKAHEAD_SIZE;
                }

                Token token = _reader.Read();
                _lookaheadBuffer[insertIndex] = token;
                _bufferCount++;

                if (token.type == TokenKind.EndOfFile)
                {
                    break;
                }
            }

            int peekIndex = _bufferStart + offset;
            if (peekIndex >= LOOKAHEAD_SIZE)
            {
                peekIndex -= LOOKAHEAD_SIZE;
            }
            return _lookaheadBuffer[peekIndex];
        }
    }
}
