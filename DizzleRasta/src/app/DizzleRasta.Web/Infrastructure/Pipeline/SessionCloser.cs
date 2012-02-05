using System;
using OpenRasta.Pipeline;
using OpenRasta.Web;
using Raven.Client;

namespace DizzleRasta.Web.Infrastructure.Pipeline
{
	public class SessionCloser : IPipelineContributor
	{
		private readonly IDocumentSession session;

		public SessionCloser(IDocumentSession session)
		{
			this.session = session;
		}

		public void Initialize(IPipeline pipelineRunner)
		{
			// TODO - fix issue with container first
			//pipelineRunner.Notify(CloseDbSession).Before<KnownStages.IEnd>();
		}

		private PipelineContinuation CloseDbSession(ICommunicationContext arg)
		{
			session.SaveChanges();

			return PipelineContinuation.Continue;
		}
	}
}