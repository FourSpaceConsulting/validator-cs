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

namespace Fourspace.Validator.Results
{
    public class ResultUtil
    {
        public static IValidationResult MergeFailedMessages(string itemId, IEnumerable<IValidationResult> results)
        {
            if (CollectionUtil.IsNotNullOrEmpty(results))
            {
                bool allPassed = true;
                List<string> messages = new List<string>();
                List<IValidationResult> nested = new List<IValidationResult>();
                foreach (var result in results)
                {
                    if (!result.Valid)
                    {
                        allPassed = false;
                        if (CollectionUtil.IsNotNullOrEmpty(result.Messages))
                            messages.AddRange(result.Messages);
                        if (CollectionUtil.IsNotNullOrEmpty(result.NestedResults))
                            nested.AddRange(result.NestedResults);
                    }
                }
                return allPassed ? (IValidationResult)PassValidationResult.Instance : new ValidationResult(false, itemId, messages, nested);
            }
            return PassValidationResult.Instance;
        }

        public static IValidationResult MergeFailedResultsAsNested(string itemId, IEnumerable<IValidationResult> results)
        {
            if (CollectionUtil.IsNotNullOrEmpty(results))
            {
                bool allPassed = true;
                List<IValidationResult> nested = new List<IValidationResult>();
                foreach (var result in results)
                {
                    if (!result.Valid)
                    {
                        allPassed = false;
                        nested.Add(result);
                    }
                }
                return allPassed ? (IValidationResult)PassValidationResult.Instance : new ValidationResult(false, itemId, Immutable.ReadOnlyList<string>(), nested);
            }
            return PassValidationResult.Instance;
        }

    }
}
