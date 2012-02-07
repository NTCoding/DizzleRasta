using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DizzleRasta.Web.Resources;
using Raven.Client;

namespace DizzleRasta.Web.Handlers
{
	public class TracksHandler
	{
		private readonly IDocumentSession session;

		public TracksHandler(IDocumentSession session)
		{
			this.session = session;
		}

		public object Get()
		{
			return session
				.Query<Track>()
				.Customize(x => x.RandomOrdering())
				.Take(50);
		}
	}
}