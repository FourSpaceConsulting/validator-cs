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
    /// Validates equality of value against a constant
    /// </summary>
    /// <typeparam name="V"></typeparam>
    public class EqualityValidator<V> : IValidator<V>
    {
        private const string DefaultMessage = "Value '{0}' must equal '{1}'";
        private readonly string messageTemplate;
        private readonly bool passNull;

        private readonly bool areEqual;
        private readonly V target;

        /// <summary>
        /// </summary>
        /// <param name="getter">Gets the value from the validation data.</param>
        /// <param name="passNull">Pass validation if value is null.</param>
        public EqualityValidator(V target, bool areEqual = true, bool passNull = true, string messageTemplate = DefaultMessage)
        {
            this.target = target;
            this.areEqual = areEqual;
            this.passNull = passNull;
            this.messageTemplate = messageTemplate;
        }

        public IValidationResult Validate(string itemId, V value)
        {
            if (value == null && passNull) return PassValidationResult.Instance;
            if (areEqual != EqualityComparer<V>.Default.Equals(value, target))
            {
                return new ValidationResult(false, itemId, string.Format(messageTemplate, value, target));
            }
            return PassValidationResult.Instance;
        }
    }


    /// <summary>
    /// Validates equality of two items derived from input value.
    /// </summary>
    /// <typeparam name="V"></typeparam>
    /// <typeparam name="T"></typeparam>
    public class EqualityValidator<V,T> : IValidator<V>
    {
        private const string DefaultMessage = "Value '{0}' must equal '{1}'";
        private readonly string messageTemplate;
        private readonly bool passNull;

        private readonly bool areEqual;
        private readonly Func<V, T> getFirst;
        private readonly Func<V, T> getSecond;

        /// <summary>
        /// </summary>
        /// <param name="getter">Gets the value from the validation data.</param>
        /// <param name="passNull">Pass validation if value is null.</param>
        public EqualityValidator(Func<V, T> getFirst, Func<V, T> getSecond, bool areEqual = true, bool passNull = true, string messageTemplate = DefaultMessage)
        {
            this.getFirst = getFirst;
            this.getSecond = getSecond;
            this.areEqual = areEqual;
            this.passNull = passNull;
            this.messageTemplate = messageTemplate;
        }

        public IValidationResult Validate(string itemId, V value)
        {
            if (value == null && passNull) return PassValidationResult.Instance;
            var first = getFirst(value);
            var second = getSecond(value);
            if (areEqual != EqualityComparer<T>.Default.Equals(first, second))
            {
                return new ValidationResult(false, itemId, string.Format(messageTemplate, first, second));
            }
            return PassValidationResult.Instance;
        }
    }
}
