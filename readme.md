# MuonKit Validation

A fluent validation library for .NET

## Overview

Express your valiation rules in a clear, concise manner:

### Simple rules

	Ensure(x => x.SomeProperty.MeetsSomeRule());


### Conditionals statements


	When(x => x.SomeProperty.MeetsSomeRule(), () => {

		Ensure(x => x.OtherProperty.MeetsSomeRule());

	});


When using conditionals, should the `when` clause fail, no error messages are set for the `when` clause.


### AND Condtitionals statements


	Ensure(x => x.SomeProperty.MeetsSomeRule()).And(() => {

		Ensure(x => x.OtherProperty.MeetsSomeRule());

	});


This case is useful when you have two rules for a property, except that if the first rule fails, the 2nd rule is not run.
An example usage of this would be checking an email address. You want to check that it is not empty, and that its a valid email. You wouldnt want two error messages in the "empty" case.



## Complete Example


	public class TestViewModel
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
	}



	public class TestViewModelValidator : Validator<TestViewModel>
	{
		protected override void Rules()
		{
			Ensure(x => x.Email.IsNotNullOrEmpty()).And(()=>
				Ensure(x => x.Email.IsAValidEmailAddress()));

			Ensure(x => x.Password.IsNotNullOrEmpty()).And(() => 
				Ensure(x => x.ConfirmPassword.IsEqualTo(x.Password)));
		}
	}
