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

		public object Post(TracksQueryModel model)
		{
			if (model.IsForPriceSearch)
			{
				return GetTracksForPriceRange(model.PriceMin.Value, model.PriceMax.Value);
			}

			return Get();
		}

		private object GetTracksForPriceRange(decimal priceMin, decimal priceMax)
		{
			return session
				.Advanced
				.LuceneQuery<Track>()
				.Where("Price: [" + priceMin + " TO " + priceMax + "]");
		}
	}

	public class TracksQueryModel
	{
		public bool IsForPriceSearch
		{
			get { return PriceMin.HasValue && PriceMax.HasValue; }
		}

		public decimal? PriceMin { get; set; }

		public decimal? PriceMax { get; set; }
	}
}