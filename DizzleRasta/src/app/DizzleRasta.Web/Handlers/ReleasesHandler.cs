using System;
using System.Collections.Generic;
using System.Linq;
using DizzleRasta.Web.Resources;
using OpenRasta.Web;
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

		public IEnumerable<Release> Get()
		{
			return session
				.Query<Release>()
				.Customize(q => q.RandomOrdering())
				.Take(50);
		}

		public IEnumerable<Release> Post(ReleasesQueryModel model)
		{
			return session
				.Advanced
				.LuceneQuery<Release>()
				.Where("Title: " + model.SearchTerm + "~");
		}
	}

	public class ReleasesQueryModel
	{
		public string SearchTerm { get; set; }
	}
}