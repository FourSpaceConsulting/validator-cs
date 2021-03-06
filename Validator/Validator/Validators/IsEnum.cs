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
using System;

namespace Fourspace.Validator.Validators
{
    /// <summary>
    /// Validate that a string value is a valid enumeration.
    /// </summary>
    /// <typeparam name="T">Enumerated type to validate against</typeparam>
    public class IsEnum<T> : BaseValidator<string>
        where T : struct, IConvertible
    {
        /// <summary>
        /// Constructor. Throws ArgumentException if parameterised type is not an Enum.
        /// </summary>
        /// <param name="passNull">Pass validation if value is null.</param>
        public IsEnum(bool passNull = true, string messageTemplate = DefaultValidationMessage) : base(passNull, messageTemplate)
        {
            if (!typeof(T).IsEnum) throw new ArgumentException("Parameter " + typeof(T) + " for IsEnum validator must be an enumerated type.");
        }

        protected override bool IsValidationFailure(string value)
        {
            T enumValue;
            return !Enum.TryParse(value, out enumValue);
        }
    }
}
