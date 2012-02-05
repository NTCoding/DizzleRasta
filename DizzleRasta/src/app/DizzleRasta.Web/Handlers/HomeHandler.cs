using DizzleRasta.Web.Resources;
using DizzleRasta.Web.Services;

namespace DizzleRasta.Web.Handlers
{
	public class HomeHandler
	{
		public object Get()
		{
			return new Home {Title = "7Dizzle is the Shizzle"};
		}
	}
}