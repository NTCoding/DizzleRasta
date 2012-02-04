using System;
using System.Collections.Generic;
using DizzleRasta.Web.Resources;

namespace DizzleRasta.Web.Handlers
{
	public class ReleasesHandler
	{
		private ReleaseRetriever retriever = new ReleaseRetriever();

		public object Get()
		{
			return retriever.GetAllReleases();
		}
	}

	public class ReleaseRetriever
	{
		public IEnumerable<Release> GetAllReleases()
		{
			for (int i = 0; i < 10; i++)
			{
				yield return new Release
				             	{
									Id       = 100 + i,
									Title    = "Release " + i,
									Version  = 1,
									Type     = "single",
									ArtistId = 101,
									ImageUrl = "http://www.electricpig.co.uk/wp-content/uploads/2008/09/7digital1.jpg",
				             	};
			}
		}
	}
}