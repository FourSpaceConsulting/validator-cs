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
using Fourspace.Validator.Builders;
using Fourspace.Validator.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Validator.Test.Validator
{
    [TestFixture]
    public class ValidatorBuilderTest
    {
        private class TestObject
        {
            public string Item1 { get; set; }
            public int Item2 { get; set; }
            public IList<decimal?> Item3 { get; set; }
            public int Item4 { get; set; }
            public int Item5 { get; set; }
        }

        [Test]
        public void TestPropertyBuilderValidator()
        {
            var validator = ValidatorBuilder.Builder<TestObject>()
                .Append("Random",new EqualityValidator<TestObject, int>(o => o.Item2, o => o.Item4, true))
                .BeginGroup("Item1")
                    .Append(o => o.Item1, NotWhitespaceString.NotNullInstance, new StringLength(i => i<5))
                    .Append(new EqualityValidator<TestObject>(new TestObject(), true))
                    .Append(new EqualityValidator<TestObject, int>(o => o.Item2, o => o.Item4, true))
                .EndGroup()
                .Append("Item2", o => o.Item2, new RangeValidator<int>(0,2))
                .Append("Item3", o => o.Item3, new CollectionValidator<decimal?>(new RangeValidator<decimal?>(0, 2, false)))
                .Append("Item4", o => o.Item4, new EqualityValidator<int>(6,true))
                .Build();
            var result = validator.Validate("Test",new TestObject() { Item1 = "HELLO ME", Item2 = -1, Item3 = new List<decimal?>() { 1m, 2.5m, null } });
            Console.WriteLine(result);
            Assert.AreEqual(false, result.Valid);
        }
    }
}
