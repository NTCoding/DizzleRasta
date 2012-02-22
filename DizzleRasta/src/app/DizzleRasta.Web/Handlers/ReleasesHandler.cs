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
			if (model.IsForSearch)
			{
				return GetNamesLike(model.SearchTerm);
			}

			if (model.IsForSingles)
			{
				return GetSingles();
			}

			return Get();
		}

		private IEnumerable<Release> GetSingles()
		{
			return session
				.Advanced
				.LuceneQuery<Release>()
				.Where("Type:\"Single\"");
		}

		private IEnumerable<Release> GetNamesLike(string searchTerm)
		{
			return session
				.Advanced
				.LuceneQuery<Release>()
				.Where("Title: " + searchTerm + "~");
		}
	}

	public class ReleasesQueryModel
	{
		public string SearchTerm { get; set; }

		public bool IsForSearch
		{
			get { return !String.IsNullOrWhiteSpace(SearchTerm); }
		}

		public string GetSingles { get; set; }
		
		public bool IsForSingles
		{
			get { return !String.IsNullOrWhiteSpace(GetSingles); }
		}
	}
}