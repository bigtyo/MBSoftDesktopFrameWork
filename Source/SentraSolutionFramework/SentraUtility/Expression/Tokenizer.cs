using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace SentraUtility.Expression
{
    internal enum eTokenType
    {
        none,
        end_of_expr,
        operator_plus,
        operator_minus,
        operator_mul,
        operator_div,
        open_parenthesis,
        comma,
        dot,
        close_parenthesis,
        operator_ne,
        operator_gt,
        operator_ge,
        operator_eq,
        operator_le,
        operator_lt,
        operator_and,
        operator_or,
        operator_not,

        value_identifier,
        value_true,
        value_false,
        value_number,
        value_string,
        value_date
    }

    [DebuggerNonUserCode]
    internal class Tokenizer
    {
        string _String;
        int _Len, _Pos;
        char _CurrentChar;

        public eTokenType TokenType;
        public StringBuilder Value = new StringBuilder();

        internal Tokenizer()
        {
            _Pos = -1;
        }

        internal void Start(string str)
        {
            _String = str;
            _Len = str.Length;
            _Pos = 0;
            NextChar();
        }

        private void NextChar()
        {
            _CurrentChar = _Pos < _Len ?
                _String[_Pos++] : '\x0';
        }
        internal void NextToken()
        {
            Value.Length = 0;
            TokenType = eTokenType.none;
            do
            {
                switch (_CurrentChar)
                {
                    case '\x0':
                        TokenType = eTokenType.end_of_expr;
                        break;
                    case '-':
                        NextChar();
                        TokenType = eTokenType.operator_minus;
                        break;
                    case '+':
                        NextChar();
                        TokenType = eTokenType.operator_plus;
                        break;
                    case '*':
                        NextChar();
                        TokenType = eTokenType.operator_mul;
                        break;
                    case '/':
                        NextChar();
                        TokenType = eTokenType.operator_div;
                        break;
                    case '(':
                        NextChar();
                        TokenType = eTokenType.open_parenthesis;
                        break;
                    case ')':
                        NextChar();
                        TokenType = eTokenType.close_parenthesis;
                        break;
                    case '<':
                        NextChar();
                        switch (_CurrentChar)
                        {
                            case '=':
                                NextChar();
                                TokenType = eTokenType.operator_le;
                                break;
                            case '>':
                                NextChar();
                                TokenType = eTokenType.operator_ne;
                                break;
                            default:
                                TokenType = eTokenType.operator_lt;
                                break;
                        }
                        break;
                    case '>':
                        NextChar();
                        if (_CurrentChar == '=')
                        {
                            NextChar();
                            TokenType = eTokenType.operator_ge;
                        }
                        else
                            TokenType = eTokenType.operator_gt;
                        break;
                    case ',':
                        NextChar();
                        TokenType = eTokenType.comma;
                        break;
                    case '=':
                        NextChar();
                        TokenType = eTokenType.operator_eq;
                        break;
                    case '.':
                        NextChar();
                        TokenType = eTokenType.dot;
                        break;
                    case '"':
                        ParseText();
                        break;
                    case '#':
                        ParseDate();
                        break;
                    case ' ':
                        break;
                    default:
                        if (_CurrentChar >= '0' && _CurrentChar <= '9')
                            ParseNumber();
                        else
                            ParseIdentifier();
                        break;
                }
                if (TokenType != eTokenType.none) break;
                NextChar();
            } while (true);
        }

        private void ParseIdentifier()
        {
            while (_CurrentChar >= '0' && _CurrentChar <= '9' ||
                _CurrentChar >= 'a' && _CurrentChar <= 'z' ||
                _CurrentChar >= 'A' && _CurrentChar <= 'Z' ||
                _CurrentChar == '_')
            {
                Value.Append(_CurrentChar);
                NextChar();
            }
            switch (Value.ToString().ToLower())
            {
                case "and":
                    TokenType = eTokenType.operator_and;
                    break;
                case "or":
                    TokenType = eTokenType.operator_or;
                    break;
                case "true":
                case "yes":
                    TokenType = eTokenType.value_true;
                    break;
                case "false":
                case "no":
                    TokenType = eTokenType.value_false;
                    break;
                default:
                    TokenType = eTokenType.value_identifier;
                    break;
            }
        }
        private void ParseDate()
        {
            NextChar();
            while (_CurrentChar >= '0' && _CurrentChar <= '9' ||
                _CurrentChar == ':' || _CurrentChar == ' ' ||
                _CurrentChar == '/' || _CurrentChar == 'A' || _CurrentChar == 'P' ||
                _CurrentChar == 'M')
            {
                Value.Append(_CurrentChar);
                NextChar();
            }
            if (_CurrentChar != '#')
                throw new ApplicationException("Error Tipe Data Date !");
            NextChar();
            TokenType = eTokenType.value_date;
        }
        private void ParseText()
        {
            NextChar();
            while (_CurrentChar != '\x0')
            {
                if (_CurrentChar == '"')
                {
                    NextChar();
                    if (_CurrentChar != '"')
                        break;
                }
                Value.Append(_CurrentChar);
                NextChar();
            }
            TokenType = eTokenType.value_string;
        }
        private void ParseNumber()
        {
            while (_CurrentChar >= '0' && _CurrentChar <= '9')
            {
                Value.Append(_CurrentChar);
                NextChar();
            }
            if (_CurrentChar == '.')
            {
                Value.Append('.');
                NextChar();
                while (_CurrentChar >= '0' && _CurrentChar <= '9')
                {
                    Value.Append(_CurrentChar);
                    NextChar();
                }
            }
            TokenType = eTokenType.value_number;
        }
    }
}
