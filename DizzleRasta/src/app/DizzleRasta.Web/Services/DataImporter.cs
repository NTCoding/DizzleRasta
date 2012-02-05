using System.Collections.Generic;
using DizzleRasta.Web.Resources;

namespace DizzleRasta.Web.Services
{
	public class DataImporter
	{
		private ApiQuerier api = new ApiQuerier();

		public void ImportUpto100ArtistsPerLetter()
		{
			var artists = new List<Artist>();

			for (char c = 'A'; c <= 'Z'; c++)
			{
				artists.AddRange(api.GetArtistsByName(c.ToString(), 100));
			} 
		}
	}
}