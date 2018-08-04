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
using Fourspace.Validator.Results;
using System.Collections.Generic;

namespace Fourspace.Validator.Validators
{
    /// <summary>
    /// Validates a collection of objects using an aggregated validator.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    /// <typeparam name="C"></typeparam>
    public class CollectionValidator<V> : IValidator<IEnumerable<V>>
    {
        private readonly IValidator<V> validator;
        private readonly bool testAllValues;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="validator">Validator for collection.</param>
        /// <param name="testAllValues"></param>
        public CollectionValidator(IValidator<V> validator, bool testAllValues = true)
        {
            this.validator = validator;
            this.testAllValues = testAllValues;
        }

        public IValidationResult Validate(string itemId, IEnumerable<V> values)
        {
            if (values == null) return PassValidationResult.Instance;
            List<IValidationResult> results = new List<IValidationResult>();
            foreach (var value in values)
            {
                var result = validator.Validate(itemId, value);
                if (!result.Valid)
                {
                    results.Add(result);
                    if (!testAllValues) break;
                }
            }
            return ResultUtil.MergeFailedResultsAsNested(itemId, results);
        }
    }
}
