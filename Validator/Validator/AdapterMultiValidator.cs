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
using System;
using System.Collections.Generic;

namespace Fourspace.Validator
{
    public class AdapterMultiValidator<T, V> : IValidator<T>
    {
        private readonly string itemId;
        private readonly Func<T, V> adapter;
        private readonly IEnumerable<IValidator<V>> validators;

        public AdapterMultiValidator(string itemId, Func<T, V> adapter, IEnumerable<IValidator<V>> validators)
        {
            this.itemId = itemId;
            this.adapter = adapter;
            this.validators = validators;
        }

        public IValidationResult Validate(string ignore, T value)
        {
            List<IValidationResult> results = new List<IValidationResult>();
            V adapted = adapter(value);
            foreach (var validator in validators)
            {
                results.Add(validator.Validate(itemId, adapted));
            }
            return ResultUtil.MergeFailedMessages(itemId, results);
        }
    }
}
