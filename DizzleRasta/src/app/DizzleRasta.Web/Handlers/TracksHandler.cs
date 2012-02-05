using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DizzleRasta.Web.Resources;

namespace DizzleRasta.Web.Handlers
{
	public class TracksHandler
	{
		private TracksRetriever retriever = new TracksRetriever();
       
	    public object Get()
		{
			return retriever.GetAllTracks();
		}
	}

	public class TracksRetriever
	{
	    public IEnumerable<Track> GetAllTracks()
		{
			for (int i = 0; i < 10; i++)
			{
				yield return new Track
				{
					Id       = 100 + i,
					Title    = "Track " + i,
					Version  = "1.0",
					ArtistId = 1,
					Price    = 1.50m
				};
			}
		}
	}
}