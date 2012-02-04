using DizzleRasta.Web.Resources;

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