using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DizzleRasta.Web.Infrastructure;
using DizzleRasta.Web.Resources;
using Raven.Client;
using Raven.Client.Linq;

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

		public void ImportReleases(IDocumentSession session)
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

		public void ImportPopularTracks(IDocumentSession session)
		{
			var tracks = new List<Track>();

			var ids = GetReleaseIds(session);

			var tasks = new List<Task<IEnumerable<Track>>>();
			for (int i = 0; i < ids.Count(); i++)
			{
				var id = ids.ElementAt(i).ToString();
				var ta = new Task<IEnumerable<Track>>(() => api.GetTracks(id));
				tasks.Add(ta);
				ta.Start();
			}

			Task.WaitAll(tasks.ToArray());

			tasks.ForEach(x => tracks.AddRange(x.Result)); ;

			tracks.ForEach(session.Store);
		}

		private IEnumerable<int> GetReleaseIds(IDocumentSession session)
		{
			var t = session.Query<Release>().ToList();
			return t.Select(r => r.Id);
		}
	}
}