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
			var d = new DataImporter();
			d.ImportUpto100ArtistsPerLetter(session);
			session.SaveChanges();

			return new Home {Title = "7Dizzle is the Shizzle"};
		}
	}
}