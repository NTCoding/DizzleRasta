using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DizzleRasta.Web.Handlers;
using FubuValidation;
using OpenRasta;
using OpenRasta.Hosting.AspNet;
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

			// TODO pluggable
			if (!ShouldValidate(input)) return true;

			var model = input.Binder.BuildObject().Instance;

			var validationResult = validator.Validate(model);

			if (validationResult.IsValid()) return true;

			context.PipelineData.Add("Validation", validationResult.ToValidationErrors());

			context.OperationResult = new OperationResult.BadRequest()
			{
				ResponseResource = ExecuteGetActionFor(model)
			};

			return false;
		}

		private object ExecuteGetActionFor(object model)
		{
			var viewModelType = GetTypeThatInherits(model.GetType());

			var handlerType = context.PipelineData.HandlerType;

			var getAction = handlerType
				.GetMethods()
				.Single(m => m.ReturnType == viewModelType);

			var handler = context.PipelineData.SelectedHandlers.Single();

			return getAction.Invoke(handler, null);
		}

		private bool ShouldValidate(InputMember input)
		{
			return input != null
				   && input.Member.Type.Name.ToLower().EndsWith("inputmodel")
			       && context.Request.HttpMethod.ToLower() == "post";
		}

		private Type GetTypeThatInherits(Type type)
		{
			return Assembly
				.GetAssembly(type)
				.GetTypes()
				.Where(t => type.IsAssignableFrom(t))
				.Where(t => t != type)
				.Single();
		}
	}
}