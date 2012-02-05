using System;
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

		public void Import3000Releases(IDocumentSession session)
		{
			var releases = new List<Release>();

			for (int i = 0; i < 10; i++)
			{
				var from = DateTime.Now.AddYears(-2).ToString("yyyyMMdd");
				var to = DateTime.Now.ToString("yyyyMMdd");
				var page = i + 1;
				releases.AddRange(api.GetReleases(from, to, page, pageSize: 500));
			}

			releases.ForEach(session.Store);
		}

	}
}