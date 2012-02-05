using System.Collections.Generic;
using DizzleRasta.Web.Infrastructure;
using DizzleRasta.Web.Resources;
using Raven.Client;

namespace DizzleRasta.Web.Services
{
	public class DataImporter
	{
		private ApiQuerier api = new ApiQuerier();

		public void ImportUpto100ArtistsPerLetter(IDocumentSession session)
		{
			var artists = new List<Artist>();

			for (char c = 'A'; c <= 'Z'; c++)
			{
				artists.AddRange(api.GetArtistsByName(c.ToString(), 100));
			}

			artists.ForEach(session.Store);
		}
	}
}