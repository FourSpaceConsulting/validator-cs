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
using Fourspace.Toolbox.Service;
using System;

namespace Fourspace.Validator.Validators
{
    /// <summary>
    /// Validate that a string value is time span.
    /// </summary>
    public class IsDate : BaseValidator<string>
    {
        private readonly IDateTimeSerializer dateTimeSerializer;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dateTimeSerializer">date format</param>
        /// <param name="passNull">Pass validation if value is null.</param>
        public IsDate(IDateTimeSerializer dateTimeSerializer, bool passNull = true, string messageTemplate = DefaultValidationMessage) : base(passNull, messageTemplate)
        {
            this.dateTimeSerializer = dateTimeSerializer;
        }

        protected override bool IsValidationFailure(string value)
        {
            DateTime result;
            return !dateTimeSerializer.ValidStringToDate(value, out result);
        }
    }
}
