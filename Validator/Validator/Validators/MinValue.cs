﻿/*
MIT License

Copyright (c) 2017 Richard Steward

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
using System.Collections.Generic;

namespace Fourspace.Validator.Validators
{
    /// <summary>
    /// Validate that a string value is numeric.
    /// </summary>
    public class MinValue<V> : BaseValidator<V>
    {
        private const string DefaultMessage = "Value '{0}' must be greater than '{1}'";
        private readonly V minValue;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="passNull">Pass validation if value is null.</param>
        public MinValue(V minValue, bool passNull = true, string messageTemplate = DefaultMessage) : base(passNull,messageTemplate)
        {
            this.minValue = minValue;
        }

        protected override bool IsValidationFailure(V value)
        {
            return Comparer<V>.Default.Compare(value, minValue) < 0;
        }
    }
}
