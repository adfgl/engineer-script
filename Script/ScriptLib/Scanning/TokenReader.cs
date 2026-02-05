using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ScriptLib.Scanning
{
    public sealed class TokenReader
    {
        const char EOF = '\0';
        int _pos, _line, _col;
        string _src = string.Empty;
        int _len;
        Src _source = default!;

        public Src Source
        {
            get => _source;
            set
            {
                _source = value ?? throw new ArgumentNullException(nameof(value));
                _src = _source.Content ?? string.Empty;
                _len = _src.Length;
                Reset();
            }
        }

        public int Cursor => _pos;
        public SrcPos Position => new SrcPos(_line, _col);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
        {
            _pos = _line = _col = 0;
        }

        public void SetCursor(int pos)
        {
            if ((uint)pos > (uint)_len)
                throw new ArgumentOutOfRangeException(nameof(pos));

            _pos = pos;
            RecalculateLineCol();
        }

        public void SetPosition(int line, int col)
        {
            _line = line;
            _col = col;
            RecalculateCursor();
        }

        public Token Read()
        {
            SkipWhiteSpace();

            char ch = Peek();
            if (ch == EOF) return EndOfFile();
            if (ch == '\r' || ch == '\n') return LineBreak();
            if (ch == '\t') return TabBreak();
            if (IsIdentStart(ch)) return IdentifierOrKeyword();
            if (IsDigit(ch) || ch == '.' && IsDigit(Peek(1))) return Number();
            return OperatorOrPunct();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void Advance()
        {
            if (_pos < _len)
            {
                if (_src[_pos] == '\n')
                {
                    _line++;
                    _col = 0;
                }
                else
                {
                    _col++;
                }
                _pos++;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        char Peek(int offset = 0)
        {
            int i = _pos + offset;
            return (uint)i < (uint)_len ? _src[i] : EOF;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void SkipWhiteSpace()
        {
            while (true)
            {
                char ch = Peek();
                if (ch == ' ') Advance();
                else break;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool IsDigit(char c) => char.IsDigit(c);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool IsIdentStart(char c) => char.IsLetter(c) || c == '_';

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool IsIdentPart(char c) => char.IsLetterOrDigit(c) || c == '_';

        Token EndOfFile()
        {
            return new Token(Source, TokenKind.EndOfFile, Position, new SrcSpan(_pos, 0));
        }

        Token LineBreak()
        {
            int start = _pos;
            char c = Peek(0);
            char n = Peek(1);

            // Handle CRLF or LFCR as one logical line break
            if ((c == '\r' && n == '\n') || (c == '\n' && n == '\r'))
            {
                Advance();
                Advance();
                return new Token(Source, TokenKind.LineBreak, Position, new SrcSpan(start, 2));
            }

            Advance();
            return new Token(Source, TokenKind.LineBreak, Position, new SrcSpan(start, 1));
        }

        Token TabBreak()
        {
            var tkn = new Token(Source, TokenKind.TabBreak, Position, new SrcSpan(_pos, 1));
            Advance(); // treat as a single column step
            return tkn;
        }

        Token ReadString(TokenKind type, char quote, bool allowEscape)
        {
            Advance(); // consume opening quote

            SrcPos startPos = Position;
            int start = _pos;

            bool terminated = false;
            ScanError error = ScanError.None;

            while (true)
            {
                char ch = Peek();

                if (ch == EOF || (!allowEscape && (ch == '\n' || ch == '\r')))
                {
                    error = ScanError.UnterminatedString;
                    break;
                }

                if (ch == quote)
                {
                    Advance(); // consume closing
                    terminated = true;
                    break;
                }

                if (allowEscape && ch == '\\')
                {
                    Advance(); // consume '\'

                    char esc = Peek();
                    if (esc == EOF)
                    {
                        error = ScanError.UnterminatedString;
                        break;
                    }

                    switch (esc)
                    {
                        case 'n':
                        case 'r':
                        case 't':
                        case '\\':
                        case '"':
                        case '\'':
                            Advance();
                            break;

                        case 'x': // \xFF
                            Advance();
                            SkipHex(2);
                            break;

                        case 'u': // \u1234
                            Advance();
                            SkipHex(4);
                            break;

                        default:
                            error = ScanError.InvalidEscape;
                            Advance();
                            break;
                    }
                    continue;
                }

                Advance();
            }

            int length = _pos - start - 1;
            SrcSpan span = new SrcSpan(start, length);

            if (!terminated)
            {
                return new Token(Source, TokenKind.Error,
                    (int)(error == ScanError.None ? ScanError.UnterminatedString : error),
                    startPos, span);
            }
            return new Token(Source, type, startPos, span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void SkipHex(int digits)
        {
            for (int i = 0; i < digits; i++)
            {
                char c = Peek();
                bool isHex = c >= '0' && c <= '9' ||
                             c >= 'a' && c <= 'f' ||
                             c >= 'A' && c <= 'F';
                if (!isHex) break;
                Advance();
            }
        }

        public static Keyword GetKeyword(string lexeme)
        {
            if (Keywords.Source.TryGetValue(lexeme, out Keyword result))
            {
                return result;
            }
            return Keyword.None;
        }

        Token IdentifierOrKeyword()
        {
            SrcPos pos = Position;
            int start = _pos;
            if (!IsIdentStart(Peek()))
            {
                throw new Exception("Invalid identifier start");
            }

            Advance();
            while (IsIdentPart(Peek())) Advance();
            SrcSpan span = new SrcSpan(start, _pos - start);

            Token id = new Token(Source, TokenKind.LiteralIdentifier, pos, span);
            Keyword keyword = GetKeyword(id.ToString());
            if (keyword == Keyword.None) return id;
            return new Token(Source, TokenKind.Keyword, (int)keyword, pos, span);
        }

        Token Number()
        {
            SrcPos pos = Position;
            int start = _pos;

            if (Peek() == '.') Advance();
            else while (char.IsDigit(Peek())) Advance();

            if (Peek() == '.') { Advance(); while (IsDigit(Peek())) Advance(); }

            if (Peek() is 'e' or 'E')
            {
                Advance();
                if (Peek() is '+' or '-') Advance();
                while (IsDigit(Peek())) Advance();
            }

            SrcSpan span = new SrcSpan(start, _pos - start);
            return new Token(Source, TokenKind.LiteralNumeric, pos, span);
        }

        T MultiSym<T>(T op1, params (char, T)[] expect)
        {
            Advance();
            for (int i = 0; i < expect.Length; i++)
            {
                (char ch, T op) = expect[i];
                if (Peek() == ch)
                {
                    Advance();
                    return op;
                }
            }
            return op1;
        }

        Token OperatorOrPunct()
        {
            SrcPos pos = Position;
            int start = _pos;

            TokenKind type;
            switch (Peek())
            {
                case '(':
                    type = TokenKind.OpenParen;
                    Advance();
                    break;

                case '[':
                    type = TokenKind.OpenSquare;
                    Advance();
                    break;

                case '{':
                    type = TokenKind.OpenCurly;
                    Advance();
                    break;

                case ')':
                    type = TokenKind.CloseParen;
                    Advance();
                    break;

                case ']':
                    type = TokenKind.CloseSquare;
                    Advance();
                    break;

                case '}':
                    type = TokenKind.CloseCurly;
                    Advance();
                    break;

                default:
                    type = TokenKind.Undefined;
                    break;
            }

            if (type != TokenKind.Undefined)
            {
                return new Token(Source, type, pos, new SrcSpan(start, _pos - start));
            }

            OperatorKind op;
            switch (Peek())
            {
                case '+':
                    op = MultiSym(OperatorKind.Plus);
                    break;

                case '-':
                    op = MultiSym(OperatorKind.Minus);
                    break;

                case '*':
                    op = MultiSym(OperatorKind.Multiply);
                    break;

                case '/':
                    op = MultiSym(OperatorKind.Divide);
                    break;

                case '=':
                    op = MultiSym(OperatorKind.Assign,
                        ('=', OperatorKind.Equal));
                    break;

                case '!':
                    op = MultiSym(OperatorKind.Not,
                        ('=', OperatorKind.NotEqual));
                    break;

                case '<':
                    op = MultiSym(OperatorKind.Less,
                        ('=', OperatorKind.LessEqual));
                    break;

                case '>':
                    op = MultiSym(OperatorKind.Greater,
                        ('=', OperatorKind.GreaterEqual));
                    break;

                case '|':
                    op = MultiSym(OperatorKind.None,
                             ('|', OperatorKind.Or));
                    break;

                case '&':
                    op = MultiSym(OperatorKind.None,
                             ('&', OperatorKind.And));
                    break;

                case '^':
                    op = MultiSym(OperatorKind.Pow);
                    break;

                default:
                    op = OperatorKind.None;
                    break;
            }

            if (op != OperatorKind.None)
            {
                return new Token(Source, TokenKind.Operator, (int)op, pos, new SrcSpan(start, _pos - start));
            }

            Advance();
            return new Token(Source, TokenKind.Undefined, pos, new SrcSpan(start, _pos - start));
        }


        void RecalculateLineCol()
        {
            _line = 0;
            _col = 0;

            for (int i = 0; i < _pos && i < _len; i++)
            {
                char c = _src[i];
                if (c == '\n')
                {
                    _line++;
                    _col = 0;
                }
                else
                {
                    _col++;
                }
            }
        }

        void RecalculateCursor()
        {
            int pos = 0;
            int line = 0;
            int col = 0;

            while (pos < _len && line < _line)
            {
                if (_src[pos++] == '\n')
                    line++;
            }

            while (pos < _len && col < _col)
            {
                if (_src[pos] == '\n')
                    break;
                pos++;
                col++;
            }

            _pos = pos;
        }
    }
}
