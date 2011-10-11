using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml;
using MuonLab.Commons.StructureMap.Validation;
using StructureMap;
using StructureMap.TypeRules;

namespace MuonLab.Validation.Reports
{
	public class AssemblyAnalyser
	{
		private Assembly assembly;
		private XmlDocument document;

		public XmlDocument Analyse(Assembly assembly)
		{
			if (assembly == null)
				throw new ArgumentException("Assembly cannot be null");

			this.assembly = assembly;

			configureStructuremap();

			this.document = createXmlDocument();

			var validatorTypes = findValidators();

			createValidationReport(validatorTypes);
			return this.document;
		}

		private void createValidationReport(IEnumerable<DtoValidator> validators)
		{
			var groupedValidators = validators.GroupBy(v => v.ValidatedType);

			foreach (var groupedValidator in groupedValidators)
			{
				var xmlFor = this.document.CreateElement("for");
				document.DocumentElement.AppendChild(xmlFor);

				xmlFor.SetAttribute("Name", groupedValidator.Key.Name);
				xmlFor.SetAttribute("AssemblyQualifiedName", groupedValidator.Key.AssemblyQualifiedName);

				foreach (var validatorType in groupedValidator)
				{
					object validator = null;

					try
					{
						validator = ObjectFactory.GetInstance(validatorType.ValidatorType);
					}
					catch
					{
						// bloody buggy structuremap
					}

					if (validator != null)
					{
						var rules = validatorType.ValidatorType.GetProperty("ValidationRules").GetValue(validator, null) as IEnumerable;

						var enumerable = rules.Cast<IValidationRule>();

						xmlFor.AppendChild(analyseValidator(groupedValidator.Key, validator, enumerable));
					}
					else
					{
						// todo: automocker ftw
					}
				}
			}
		}

		private XmlDocument createXmlDocument()
		{
			this.document = new XmlDocument();
			var root = this.document.CreateElement("assembly");
			root.SetAttribute("Name", assembly.FullName);
			this.document.AppendChild(root);

			return this.document;
		}

		private IEnumerable<DtoValidator> findValidators()
		{
			return this.assembly.GetTypes()
				.Select(t => new { Type = t, InterfaceTypes = t.FindInterfacesThatClose(typeof (IValidator<>)) })
				.Where(t => t.InterfaceTypes.Any())
				.SelectMany(ts => ts.InterfaceTypes.Select(t => new DtoValidator(ts.Type, t)))
				.ToArray();
		}

		private XmlElement analyseValidator(Type validatedType, object validator, IEnumerable<IValidationRule> rules)
		{
			var xmlValidator = this.document.CreateElement("validator");
			xmlValidator.SetAttribute("Name", validator.GetType().Name);
			xmlValidator.SetAttribute("AssemblyQualifiedName", validator.GetType().AssemblyQualifiedName);

			var xmlProperties = this.document.CreateElement("properties");
			xmlValidator.AppendChild(xmlProperties);

			analyseRules(validatedType, rules, xmlProperties);

			return xmlValidator;
		}

		private void analyseRules(Type validatedType, IEnumerable<IValidationRule> rules, XmlNode xmlProperties)
		{
			var grouped = rules.GroupBy(vr => (vr.Condition.Arguments[0] as MemberExpression).Member.Name);
			foreach(var propertyGroup in grouped)
			{
				var xmlProperty = this.document.CreateElement("property");
				xmlProperties.AppendChild(xmlProperty);

				var xmlPropertyName = this.document.CreateElement("name");
				xmlProperty.AppendChild(xmlPropertyName);
				xmlPropertyName.InnerText = propertyGroup.Key;

				foreach (var rule in propertyGroup)
				{
					var xmlRule = this.document.CreateElement("rule");
					xmlProperty.AppendChild(xmlRule);

					var xmlCondition = this.document.CreateElement("condition");
					xmlRule.AppendChild(xmlCondition);
					xmlCondition.InnerText = rule.Condition.Method.Name.ToEnglish();

					var xmlArguments = this.document.CreateElement("arguments");
					xmlRule.AppendChild(xmlArguments);

					var parameters = rule.Condition.Method.GetParameters();

					for (int i = 1; i < parameters.Length; i++)
					{
						var argument = rule.Condition.Arguments[i];

						var xmlArgument = this.document.CreateElement("argument");
						xmlArguments.AppendChild(xmlArgument);

						if (argument is MemberExpression)
						{
							var memberExpression = (argument as MemberExpression);

							if(memberExpression.Member.MemberType == MemberTypes.Field)
							{
								var fieldInfo = memberExpression.Member as FieldInfo;

								if (typeof(IValidator).IsAssignableFrom(fieldInfo.FieldType))
								{
									xmlArgument.InnerText = "IValidator<" + fieldInfo.FieldType.GetGenericArguments()[0].Name + ">";
									continue;
								}
							}

							if (memberExpression.Member.DeclaringType == validatedType)
								xmlArgument.InnerText = memberExpression.Member.Name;
							else
								xmlArgument.InnerText = parameters[i].Name + ": " + memberExpression;
						}
						else if (argument is ConstantExpression || argument is NewExpression)
							xmlArgument.InnerText = parameters[i].Name + ": " + argument;
						else
							xmlArgument.InnerText = parameters[i].Name + ": " + argument;
					}
				}
			}
		}

		private void configureStructuremap()
		{
			ObjectFactory.Initialize(x =>
				{
					x.Scan(s =>
						{
							s.Assembly(this.assembly);
							s.With(new ValidatorRegistrationConvention());
						});

					x.For(typeof (IValidator<>))
						.Use(typeof (EmptyValidator<>));
				});
		}
	}
}