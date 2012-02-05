using System;
using System.Collections.Generic;
using System.Linq;
using DizzleRasta.Web.Resources;
using Raven.Client;

namespace DizzleRasta.Web.Handlers
{
	public class ReleasesHandler
	{
		private readonly IDocumentSession session;

		public ReleasesHandler(IDocumentSession session)
		{
			this.session = session;
		}

		public object Get()
		{
			return session
				.Query<Release>()
				.Customize(q => q.RandomOrdering())
				.Take(50);
		}
	}
	
}