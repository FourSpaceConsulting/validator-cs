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
namespace Fourspace.Validator.Validators
{
    /// <summary>
    /// Validate that a string value is numeric.
    /// </summary>
    public class MaxPrecision : BaseValidator<decimal?>
    {
        private const string DefaultMessage = "Value '{0}' exceeds maximum precision";
        private readonly int precision;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="passNull">Pass validation if value is null.</param>
        public MaxPrecision(int precision, bool passNull = true, string messageTemplate = DefaultMessage) : base(passNull,messageTemplate)
        {
            this.precision = precision;
        }

        protected override bool IsValidationFailure(decimal? value)
        {
            return value == null || !decimal.Equals(value.Value, decimal.Round(value.Value, precision));
        }
    }
}
