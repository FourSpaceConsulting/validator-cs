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

namespace Fourspace.Validator.Validators
{
    /// <summary>
    /// </summary>
    public abstract class BaseValidator<V> : IValidator<V>
    {
        protected const string DefaultValidationMessage = "Value '{0}' is invalid";
        protected string messageTemplate;
        protected readonly bool passNull;

        public BaseValidator(bool passNull, string messageTemplate)
        {
            if (messageTemplate == null) throw new ArgumentNullException(nameof(messageTemplate));
            this.passNull = passNull;
            this.messageTemplate = messageTemplate;
        }

        public IValidationResult Validate(string itemId, V value)
        {
            if (value == null && passNull) return PassValidationResult.Instance;
            if (IsValidationFailure(value))
            {
                return new ValidationResult(false, itemId, string.Format(messageTemplate, value));
            }
            return PassValidationResult.Instance;
        }

        protected abstract bool IsValidationFailure(V value);
    }
}
