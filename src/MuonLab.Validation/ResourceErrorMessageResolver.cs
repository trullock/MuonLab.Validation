﻿using System.Globalization;
using System.Resources;

namespace MuonLab.Validation
{
	public sealed class ResourceErrorMessageResolver : IErrorMessageResolver
	{
		readonly ResourceManager resourceManager;
		readonly CultureInfo defaultCulture;

		public ResourceErrorMessageResolver()
		{
			this.resourceManager = new ResourceManager("MuonLab.Validation.ErrorMessages", this.GetType().Assembly);
			this.defaultCulture = CultureInfo.CreateSpecificCulture("en");
		}

		public string GetErrorMessage(ErrorDescriptior error, CultureInfo culture)
		{
			// TODO: merge {val} and {argX}s

			var message = this.resourceManager.GetString(error.Key, culture);
			if (!string.IsNullOrEmpty(message))
				return message;

			message = this.resourceManager.GetString(error.Key, this.defaultCulture);
			if (!string.IsNullOrEmpty(message))
				return message;

			return error.Key;
		}
	}
}