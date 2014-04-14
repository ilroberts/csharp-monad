﻿////////////////////////////////////////////////////////////////////////////////////////
// The MIT License (MIT)
// 
// Copyright (c) 2014 Paul Louth
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monad.Parsec
{
    public class ParserResult<A>
    {
        public readonly IEnumerable<Tuple<A, IEnumerable<ParserChar>>> Value;
        public readonly IEnumerable<ParserError> Errors;

        public ParserResult(IEnumerable<Tuple<A, IEnumerable<ParserChar>>> value)
        {
            Value = value;
        }

        public ParserResult(IEnumerable<ParserError> errors)
        {
            Value = new Tuple<A, IEnumerable<ParserChar>>[0];
            Errors = errors;
        }

        public bool IsFaulted
        {
            get
            {
                return !Value.HasHead();
            }
        }
    }

    public static class ParserResult
    {
        public static ParserResult<A> Fail<A>(IEnumerable<ParserError> errors)
        {
            return new ParserResult<A>(errors);
        }

        public static ParserResult<A> Fail<A>(ParserError error)
        {
            return new ParserResult<A>(error.Cons());
        }

        public static ParserResult<A> Fail<A>(string expected, IEnumerable<ParserChar> input,string message = "")
        {
            return new ParserResult<A>(new ParserError(expected,input,message).Cons());
        }

        public static ParserResult<A> Success<A>(this IEnumerable<Tuple<A, IEnumerable<ParserChar>>> self)
        {
            return new ParserResult<A>(self);
        }
    }
}
