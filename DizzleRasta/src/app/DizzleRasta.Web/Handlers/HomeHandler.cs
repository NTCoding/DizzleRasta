using DizzleRasta.Web.Resources;
using DizzleRasta.Web.Services;
using Raven.Client;

namespace DizzleRasta.Web.Handlers
{
	public class HomeHandler
	{
		private IDocumentSession session;

		public HomeHandler(IDocumentSession session)
		{
			this.session = session;
		}

		public object Get()
		{
			//var importer = new DataImporter();
			//importer.ImportUpto100ArtistsPerLetter(session);
			//importer.ImportReleases(session);
			//importer.ImportPopularTracks(session);
			//session.SaveChanges();

			return new Home {Title = "7Dizzle"};
		}
	}
}