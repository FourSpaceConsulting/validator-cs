/*
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
using System.Collections.Generic;

namespace Fourspace.Validator.Validators
{
    /// <summary>
    /// Validates a value being within a range
    /// </summary>
    public class RangeValidator<T> : IValidator<T>
    {
        private const string DefaultMessage = "Value '{0}' is outside valid range [{1},{2}]";
        private readonly string messageTemplate;
        private readonly bool passNull;
        // validator values
        private readonly T minimum;
        private readonly T maximum;

        /// <summary>
        /// Value is considered invalid if value is:
        /// strictly less than minimum
        /// strictly greater than maximum
        /// </summary>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        /// <param name="passNull"></param>
        /// <param name="messageTemplate"></param>
        public RangeValidator(T minimum, T maximum, bool passNull = true, string messageTemplate = DefaultMessage)
        {
            if (messageTemplate == null) throw new ArgumentNullException(nameof(messageTemplate));
            //
            this.minimum = minimum;
            this.maximum = maximum;
            this.messageTemplate = messageTemplate;
            this.passNull = passNull;
        }

        public IValidationResult Validate(string itemId, T value)
        {
            if (value == null && passNull) return PassValidationResult.Instance;
            if (isOutsideRange(value))
            {
                return new ValidationResult(false, itemId, string.Format(messageTemplate, value, minimum, maximum));
            }
            return PassValidationResult.Instance;
        }

        private bool isOutsideRange(T value)
        {
            if (value == null) return true;
            return Comparer<T>.Default.Compare(value, minimum) < 0 || Comparer<T>.Default.Compare(value, maximum) > 0;
        }
    }
}
