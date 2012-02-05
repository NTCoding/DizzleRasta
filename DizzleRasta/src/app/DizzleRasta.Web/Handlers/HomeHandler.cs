using DizzleRasta.Web.Resources;
using DizzleRasta.Web.Services;
using Raven.Client;

namespace DizzleRasta.Web.Handlers
{
	public class HomeHandler
	{
		private readonly IDocumentSession sesion;

		public HomeHandler(IDocumentSession sesion)
		{
			this.sesion = sesion;
		}

		public object Get()
		{
			return new Home {Title = "7Dizzle is the Shizzle"};
		}
	}
}