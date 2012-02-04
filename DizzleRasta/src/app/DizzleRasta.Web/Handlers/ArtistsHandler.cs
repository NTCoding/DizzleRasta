using System;
using System.Collections.Generic;
using DizzleRasta.Web.Resources;

namespace DizzleRasta.Web.Handlers
{
	public class ArtistsHandler
	{
		private ArtistRetriever retriever = new ArtistRetriever();

		public object Get()
		{
			return retriever.GetAllArtists();
		}
	}

	public class ArtistRetriever
	{
		public IEnumerable<Artist> GetAllArtists()
		{
			for (int i = 0; i < 10; i++)
			{
				yield return new Artist
				             	{
									Id       = 100 + 1,
									Name     = "Artist " + i,
									ImageUrl = "http://www.electricpig.co.uk/wp-content/uploads/2008/09/7digital1.jpg"
				             	};
			}
		}
	}
}