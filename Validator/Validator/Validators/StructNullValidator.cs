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
    /// Validate that a nullable is not null.
    /// </summary>
    public class StructNullValidator<T> : IValidator<T?>
        where T : struct
    {
        public static readonly StructNullValidator<T> NotNullInstance = new StructNullValidator<T>(true);
        public static readonly StructNullValidator<T> IsNullInstance = new StructNullValidator<T>(false);

        private const string DefaultNullMessage = "Value must be empty";
        private const string DefaultNotNullMessage = "Value must not be empty";
        private readonly string messageTemplate;
        private readonly bool nullIsInvalid;

        public StructNullValidator(bool nullIsInvalid, string messageTemplate = null)
        {
            this.nullIsInvalid = nullIsInvalid;
            this.messageTemplate = messageTemplate == null ? nullIsInvalid ? DefaultNotNullMessage : DefaultNullMessage : messageTemplate;
        }

        public IValidationResult Validate(string itemId, T? value)
        {
            if (nullIsInvalid == (value != null)) return PassValidationResult.Instance;
            return new ValidationResult(false, itemId, string.Format(messageTemplate, value));
        }
    }
}
