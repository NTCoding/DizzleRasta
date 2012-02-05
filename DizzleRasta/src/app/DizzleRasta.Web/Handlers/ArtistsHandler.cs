using System;
using System.Collections.Generic;
using DizzleRasta.Web.Resources;
using DizzleRasta.Web.Services;

namespace DizzleRasta.Web.Handlers
{
	public class ArtistsHandler
	{
		private ApiQuerier api = new ApiQuerier();

		public object Get()
		{
			return api.GetArtistsByName("n", 50);
		}
	}
	
}