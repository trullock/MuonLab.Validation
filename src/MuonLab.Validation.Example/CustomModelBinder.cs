using System.Globalization;
using System.Web.Mvc;
using MuonLab.Validation.Example.ViewModels;

namespace MuonLab.Validation.Example
{
	public class CustomModelBinder : DefaultModelBinder
	{
		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			// Bind normally
			var model = base.BindModel(controllerContext, bindingContext);

			if (model == null)
				return null;

			// get a validator for the viewmodel
			var validator = GetValidator(model);
			if (validator == null)
				return model;

			// validate!
			var validationReport = validator.Validate(model);

			// valid?
			if (!validationReport.IsValid)
			{
				var violationPropertyNameResolver = new MvcViolationPropertyNameResolver();
				var errorMessageResolver = new ResourceErrorMessageResolver();

				foreach (var violation in validationReport.Violations)
				{
					// add errors to modelstate
					bindingContext.ModelState.AddModelError(violationPropertyNameResolver.ResolvePropertyName(violation), errorMessageResolver.GetErrorMessage(violation.Error.Key, new CultureInfo("de")));
				}
			}

			return model;
		}

		static IValidator GetValidator(object model)
		{
			// get yourself an IValidator<T> from your IoC container here.
			// I've hardcoded this for demo purposes.

			if(model.GetType() == typeof(TestViewModel))
				return new TestViewModelValidator();

			return null;
		}
	}
}