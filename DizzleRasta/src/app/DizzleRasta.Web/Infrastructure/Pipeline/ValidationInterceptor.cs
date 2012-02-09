using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DizzleRasta.Web.Handlers;
using FubuValidation;
using OpenRasta.OperationModel;
using OpenRasta.OperationModel.Interceptors;
using OpenRasta.Pipeline;
using OpenRasta.Web;

namespace DizzleRasta.Web.Infrastructure.Pipeline
{
	public class ValidationInterceptor : OperationInterceptor
	{
		private readonly IValidator validator;
		private readonly ICommunicationContext context;

		public ValidationInterceptor(IValidator validator, ICommunicationContext context)
		{
			this.validator = validator;
			this.context = context;
		}

		public override bool BeforeExecute(IOperation operation)
		{
			var input = operation.Inputs.FirstOrDefault();

			if (input == null) return true;

			if (!GetModelTypesName(input).EndsWith("inputmodel")) return true;

			var model = input.Binder.BuildObject().Instance;

			var validationResult = validator.Validate(model);

			if (validationResult.IsValid()) return true;

			context.PipelineData.Add("Validation", validationResult.ToValidationErrors());

			context.OperationResult = new OperationResult.BadRequest
			{
				ResponseResource = Activator.CreateInstance(GetClassThatInherits(model.GetType()))
			};

			return false;
		}

		private Type GetClassThatInherits(Type type)
		{
			return Assembly
				.GetAssembly(type)
				.GetTypes()
				.Where(t => type.IsAssignableFrom(t))
				.Where(t => t != type)
				.Single();
		}

		private string GetModelTypesName(InputMember input)
		{
			return input.Member.Type.Name.ToLower();
		}
	}
}