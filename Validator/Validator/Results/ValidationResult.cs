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
using System;
using System.Collections.Generic;
using System.Text;

namespace Fourspace.Validator
{
    ///<inheritdoc/>
    public class ValidationResult : IValidationResult
    {
        public ValidationResult(bool valid, string itemId, string message)
        {
            ItemId = itemId;
            Valid = valid;
            Messages = Immutable.ReadOnlyList(message);
        }

        public ValidationResult(bool valid, string itemId, IEnumerable<string> messages, IEnumerable<IValidationResult> nestedResults)
        {
            ItemId = itemId;
            Valid = valid;
            Messages = Immutable.ReadOnlyList(messages);
            NestedResults = Immutable.ReadOnlyList(nestedResults);
        }

        public ValidationResult(bool valid, string itemId, IReadOnlyList<string> messages, IReadOnlyList<IValidationResult> nestedResults)
        {
            ItemId = itemId;
            Valid = valid;
            Messages = messages;
            NestedResults = nestedResults;
        }

        ///<inheritdoc/>
        public bool Valid { get; }

        ///<inheritdoc/>
        public string ItemId { get; set; }

        ///<inheritdoc/>
        public IReadOnlyList<string> Messages { get; }

        ///<inheritdoc/>
        public IReadOnlyList<IValidationResult> NestedResults { get; }

        public override string ToString()
        {
            var b = new StringBuilder("Validation Item (")
                .Append(ItemId)
                .Append(") ")
                .Append(Valid ? "[Valid]" : "[Invalid]")
                .Append(" {")
                .Append(string.Join(",", Messages ?? Immutable.ReadOnlyList<string>()))
                .Append(" }");
            if (CollectionUtil.IsNotNullOrEmpty(NestedResults))
            {
                b.Append(" Nested (").Append(NestedResults.Count).Append(") [")
                .Append(Environment.NewLine)
                .Append(string.Join(Environment.NewLine, NestedResults ?? Immutable.ReadOnlyList<IValidationResult>()))
                .Append(Environment.NewLine)
                .Append("]");
            }
            return b.ToString();
        }

    }
}
