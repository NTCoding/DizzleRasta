using System;
using System.Collections.Generic;
using System.Linq;
using DizzleRasta.Web.Resources;
using DizzleRasta.Web.Services;
using Raven.Client;

namespace DizzleRasta.Web.Handlers
{
	public class ArtistsHandler
	{
		private IDocumentSession session;

		public ArtistsHandler(IDocumentSession session)
		{
			this.session = session;
		}

		public object Get()
		{
			return session
				.Query<Artist>()
				.Customize(obj => obj.RandomOrdering())
				.Take(25);
		}
	}
	
}