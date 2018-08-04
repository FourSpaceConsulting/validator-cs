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

namespace Fourspace.Validator.Builders
{
    public class SubBuilder<T> : CombinedValidatorBuilder<T>
    {
        public SubBuilder(CombinedValidatorBuilder<T> parentValidatorBuilder, string itemId, bool mergeAsNested) : base(mergeAsNested)
        {
            this.ParentValidatorBuilder = parentValidatorBuilder;
            this.ItemId = itemId;
        }

        public CombinedValidatorBuilder<T> ParentValidatorBuilder { get; }
        public string ItemId { get; }
    }

    public static class ValidatorBuilder
    {
        public static CombinedValidatorBuilder<T> Builder<T>()
        {
            return new CombinedValidatorBuilder<T>(true);
        }

        #region top level builder
        public static CombinedValidatorBuilder<T> Append<T>(this CombinedValidatorBuilder<T> b, IValidator<T> validator)
        {
            b.Add(validator);
            return b;
        }

        public static CombinedValidatorBuilder<T> Append<T>(this CombinedValidatorBuilder<T> b, string itemId, IValidator<T> validator)
        {
            b.Add(new ItemIdAdapterValidator<T>(itemId, validator));
            return b;
        }

        public static CombinedValidatorBuilder<T> Append<T,P>(this CombinedValidatorBuilder<T> b, string itemId, Func<T,P> propertyGetter, IValidator<P> validator)
        {
            b.Add(CreateAdapter(itemId, propertyGetter, validator));
            return b;
        }
        #endregion

        #region Group Builder
        public static SubBuilder<T> BeginGroup<T>(this CombinedValidatorBuilder<T> b, string itemId)
        {
            return new SubBuilder<T>(b, itemId, false);
        }

        public static CombinedValidatorBuilder<T> EndGroup<T>(this SubBuilder<T> b)
        {
            b.ParentValidatorBuilder.Add(new ItemIdAdapterValidator<T>(b.ItemId, b.Build()));
            return b.ParentValidatorBuilder;
        }

        public static SubBuilder<T> Append<T, P>(this SubBuilder<T> b, Func<T, P> propertyGetter, IValidator<P> validator)
        {
            b.Add(CreateAdapter(b.ItemId, propertyGetter, validator));
            return b;
        }

        public static SubBuilder<T> Append<T, P>(this SubBuilder<T> b, Func<T, P> propertyGetter, params IValidator<P>[] validators)
        {
            b.Add(CreateAdapter(b.ItemId, propertyGetter, validators));
            return b;
        }

        public static SubBuilder<T> Append<T>(this SubBuilder<T> b, IValidator<T> validator)
        {
            b.Add(validator);
            return b;
        }
        #endregion

        #region adapter
        public static IValidator<T> CreateAdapter<T, P>(string itemId, Func<T, P> propertyGetter, IValidator<P> validator)
        {
            return new AdapterValidator<T, P>(itemId, propertyGetter, validator);
        }

        public static IValidator<T> CreateAdapter<T, P>(string itemId, Func<T, P> propertyGetter, params IValidator<P>[] validators)
        {
            return new AdapterMultiValidator<T, P>(itemId, propertyGetter, validators);
        }
        #endregion

    }
}
