﻿#region Copyright (C) 2012 MPExtended
// Copyright (C) 2012 MPExtended Developers, http://mpextended.github.com/
// 
// MPExtended is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 2 of the License, or
// (at your option) any later version.
// 
// MPExtended is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with MPExtended. If not, see <http://www.gnu.org/licenses/>.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPExtended.Libraries.Service.Shared.Filters
{
    internal class Tokenizer
    {
        private List<string> tokens;
        private string data;

        private int pos;
        private string value;

        public Tokenizer(string data)
        {
            this.data = data;
        }

        public List<string> GetTokens()
        {
            return tokens;
        }

        public List<string> Tokenize()
        {
            tokens = new List<string>();
            value = String.Empty;
            char valueQuoteCharacter = '\0';
            bool valueIsQuoted = false;

            pos = -1;
            while(true)
            {
                ResetValue();
                while (++pos < data.Length && ExpectToken(Tokens.IsFieldName))
                    value += data[pos];
                if (String.IsNullOrWhiteSpace(value))
                    ParseError(true, "field name");
                tokens.Add(value);

                ResetValue();
                while (++pos < data.Length && ExpectToken(Tokens.IsOperator))
                    value += data[pos];
                if (String.IsNullOrWhiteSpace(value))
                    ParseError(true, "operator");
                tokens.Add(value.Trim());

                ResetValue();
                valueIsQuoted = Tokens.IsQuote(GetNextCharacter("value"));
                if (valueIsQuoted)
                    valueQuoteCharacter = data[pos];
                else
                    value += data[pos];

                while (++pos < data.Length)
                {
                    if (Tokens.IsEscapeCharacter(data[pos]))
                        value += GetNextCharacter("any character");
                    else if ((valueIsQuoted && data[pos] == valueQuoteCharacter) || (!valueIsQuoted && NotExpectToken(Tokens.IsConjunction)))
                        break;
                    else
                        value += data[pos];
                }
                if (valueIsQuoted ? String.IsNullOrEmpty(value) : String.IsNullOrWhiteSpace(value))
                    ParseError(true, "value");
                if (valueIsQuoted && pos >= data.Length) // if the value is quoted, we want a closing quote
                    ParseError(valueQuoteCharacter.ToString());
                tokens.Add(valueIsQuoted ? value : value.Trim());

                ResetValue();
                if (++pos < data.Length)
                {
                    if (Tokens.IsConjunction(data[pos]))
                        tokens.Add(data[pos].ToString());
                    else
                        ParseError("conjunction");
                }
                else
                    return tokens;
            }
        }

        private void ResetValue()
        {
            while (++pos < data.Length && ExpectToken(Tokens.IsWhitespace)) ;
            value = String.Empty;
        }

        private bool ExpectToken(Func<char, bool> check)
        {
            bool result = check(data[pos]);
            if (!result)
                pos--;
            return result;
        }

        private bool NotExpectToken(Func<char, bool> check)
        {
            bool result = check(data[pos]);
            if (result)
                pos--;
            return result;
        }

        private char GetNextCharacter(string expectedValue)
        {
            if (++pos >= data.Length)
                throw new ParseException("Tokenizer: Unexpected end of string, expected {0} instead.", expectedValue);
            return data[pos];
        }

        private void ParseError(string expectedValue)
        {
            string got = pos >= data.Length ? "end of string" : String.Format("'{0}'", data[pos]);
            throw new ParseException("Tokenizer: Unexpected {0}, expected {1} instead.", got, expectedValue);
        }

        private void ParseError(bool onNextChar, string expectedValue)
        {
            if (onNextChar)
                pos++;
            ParseError(expectedValue);
        }
    }
}
