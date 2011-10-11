using System;

namespace MuonLab.Validation.Reports
{
	public class DtoValidator
	{
		public Type ValidatorType { get; set; }
		public Type ValidatorInterfaceType { get; set; }
		public Type ValidatedType { get; set; }

		public DtoValidator(Type validator, Type validatorInterface)
		{
			this.ValidatorType = validator;
			this.ValidatorInterfaceType = validatorInterface;
			this.ValidatedType = validatorInterface.GetGenericArguments()[0];
		}
	}
}