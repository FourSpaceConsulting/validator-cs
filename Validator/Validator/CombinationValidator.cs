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
using Fourspace.Validator.Results;
using System.Collections.Generic;

namespace Fourspace.Validator
{
    public class CombinationValidator<T> : IValidator<T>
    {
        private readonly IReadOnlyList<IValidator<T>> validators;
        private readonly bool mergeAsNested; 

        public CombinationValidator(IEnumerable<IValidator<T>> validators, bool mergeAsNested)
        {
            this.validators = Immutable.ReadOnlyList(validators);
            this.mergeAsNested = mergeAsNested;
        }

        public IValidationResult Validate(string itemId, T value)
        {
            List<IValidationResult> results = new List<IValidationResult>();
            foreach (var validator in validators)
            {
                results.Add(validator.Validate(itemId, value));
            }
            return 
                mergeAsNested ?
                ResultUtil.MergeFailedResultsAsNested(itemId, results) :
                ResultUtil.MergeFailedMessages(itemId, results);
        }
    }
}
