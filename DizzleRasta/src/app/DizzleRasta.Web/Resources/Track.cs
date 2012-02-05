using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DizzleRasta.Web.Resources
{
	public class Track
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Version { get; set; }

		public int ArtistId { get; set; }

		public decimal Price { get; set; }
	}
}