using System;
using System.Collections.Generic;
using System.Linq;
using DizzleRasta.Web.Resources;
using OpenRasta.Web;
using Raven.Client;

namespace DizzleRasta.Web.Handlers
{
	public class ReleaseHandler
	{
		private readonly IDocumentSession session;

		public ReleaseHandler(IDocumentSession session)
		{
			this.session = session;
		}

		[HttpOperation(HttpMethod.GET)]
		public ReleaseCreateModel Add()
		{
			return new ReleaseCreateModel
			            	{
								Artists = session
								.Query<Artist>()
								.ToDictionary(a => a.Name, a => a.Id)
			            	};
		}

		[HttpOperation(HttpMethod.POST)]
		public OperationResult Add(ReleaseInputModel model)
		{
			var release = new Release
			              	{
			              		ArtistId = model.ArtistId,
			              		ImageUrl = model.ImageUrl,
			              		Title    = model.Title,
			              		Type     = model.Type,
			              		Version  = model.Version
			              	};

			session.Store(release);
			
			session.SaveChanges(); // TODO - fix session management and this goes

			return new OperationResult.OK(new {Resource = release});
		}

	}

	public class ReleaseCreateModel : ReleaseInputModel
	{
		public Dictionary<string, int> Artists { get; set; }
	}

	public class ReleaseInputModel
	{
		public int ArtistId { get; set; }

		public int Version { get; set; }

		public string Type { get; set; }

		public string Title { get; set; }

		public string ImageUrl { get; set; }
	}
}