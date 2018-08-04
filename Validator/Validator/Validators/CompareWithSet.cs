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
using Fourspace.Toolbox.Util;
using System.Collections.Generic;

namespace Fourspace.Validator.Validators
{
    /// <summary>
    /// Compare with set creators
    /// </summary>
    public static class CompareWithSet
    {
        public static CompareWithSet<U> InSet<U>(ICollection<U> col, bool passNull = true, string messageTemplate = null)
        {
            return messageTemplate == null ? new CompareWithSet<U>(CollectionUtil.AsSet(col), true, passNull) : new CompareWithSet<U>(CollectionUtil.AsSet(col), true, passNull, messageTemplate);
        }
        public static CompareWithSet<U> NotInSet<U>(ICollection<U> col, bool passNull = true, string messageTemplate = null)
        {
            return messageTemplate == null ? new CompareWithSet<U>(CollectionUtil.AsSet(col), false, passNull) : new CompareWithSet<U>(CollectionUtil.AsSet(col), false, passNull, messageTemplate);
        }
    }

    /// <summary>
    /// Validate that a value is either contained, or not contained in a specific set.
    /// </summary>
    /// <typeparam name="T">Type to validate against</typeparam>
    public class CompareWithSet<T> : BaseValidator<T>
    {
        private readonly ISet<T> values;
        private readonly bool inSet;

        /// <summary>
        /// Construct with a set of values.
        /// </summary>
        /// <param name="set">Set of values to validate against.</param>
        /// <param name="isInSet">Whether validation is true for presence or non-presence in set.</param>
        /// <param name="passNull">Pass validation if value is null.</param>
        public CompareWithSet(ISet<T> set, bool isInSet, bool passNull = true, string messageTemplate = DefaultValidationMessage) : base(passNull, messageTemplate)
        {
            values = set == null ? Immutable.Set<T>(): set;
            this.inSet = isInSet;
        }

        protected override bool IsValidationFailure(T value)
        {
            return inSet != values.Contains(value);
        }
    }
}
