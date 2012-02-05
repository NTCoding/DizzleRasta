using Raven.Client;
using Raven.Client.Embedded;

namespace DizzleRasta.Web.Infrastructure
{
	public class DocumentStoreHolder
	{
		private static IDocumentStore documentStore;

		public static IDocumentStore DocumentStore
		{
			get { return (documentStore ?? (documentStore = CreateDocumentStore())); }
		}

		private static IDocumentStore CreateDocumentStore()
		{
			var store = new EmbeddableDocumentStore
			{
				DataDirectory = "@App_Data\\Raven",
				UseEmbeddedHttpServer = true,
			}.Initialize();

			store.Conventions.MaxNumberOfRequestsPerSession = 500;

			return store;
		}
	}
}