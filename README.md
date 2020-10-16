# `validator-cs`
> C# Validation library

## Features

- Flexible and comprehensive validator classes for object validation
- Fluent builder to easily create validators 


## Usage

`Validator Builder`

```cs

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
            // Results in 
			// Validation Item (Test) [Invalid] { } Nested (5) [
			// 	Validation Item (Random) [Invalid] {Value '-1' must equal '0' }
			// 	Validation Item (Item1) [Invalid] {Value has invalid length '8',Value 'Validator.Test.Validator.ValidatorBuilderTest+TestObject' must equal 'Validator.Test.Validator.ValidatorBuilderTest+TestObject',Value '-1' must equal '0' }
			// 	Validation Item (Item2) [Invalid] {Value '-1' is outside valid range [0,2] }
			// 	Validation Item (Item3) [Invalid] { } Nested (2) [
			// 		Validation Item (Item3) [Invalid] {Value '2.5' is outside valid range [0,2] }
			// 		Validation Item (Item3) [Invalid] {Value '' is outside valid range [0,2] }
			// 		]
			// 	Validation Item (Item4) [Invalid] {Value '0' must equal '6' }
			// 	]			
			

```

## Credits

> Author - Richard Steward

## License

[MIT](LICENSE)