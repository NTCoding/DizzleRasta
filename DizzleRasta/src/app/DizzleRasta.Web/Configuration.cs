using System;
using System.Collections.Generic;
using DizzleRasta.Web.Handlers;
using DizzleRasta.Web.Infrastructure;
using DizzleRasta.Web.Resources;
using OpenRasta.Configuration;
using OpenRasta.DI;
using Raven.Client;

namespace DizzleRasta.Web
{
	public class Configuration : IConfigurationSource
	{
		public void Configure()
		{
			// TODO - this is where Fubu would use conventions

			using (OpenRastaConfiguration.Manual)
			{
				ResourceSpace.Has
					.ResourcesOfType<Home>()
					.AtUri("/home")
					.HandledBy<HomeHandler>()
					.RenderedByAspx("~/Views/HomeView.aspx");

				ResourceSpace.Has
					.ResourcesOfType<IEnumerable<Artist>>()
					.AtUri("/artists")
					.HandledBy<ArtistsHandler>()
					.RenderedByAspx("~/Views/Artists.aspx");

				ResourceSpace.Has
					.ResourcesOfType<IEnumerable<Release>>()
					.AtUri("/releases")
					.HandledBy<ReleasesHandler>()
					.RenderedByAspx("~/Views/Releases.aspx");

				ResourceSpace.Has
					.ResourcesOfType<IEnumerable<Track>>()
					.AtUri("/tracks")
					.HandledBy<TracksHandler>()
					.RenderedByAspx("~/Views/Tracks.aspx");

				ResourceSpace.Uses.Resolver.AddDependencyInstance(
					typeof (IDocumentSession),DocumentStoreHolder.DocumentStore.OpenSession(),DependencyLifetime.PerRequest);

			}
		}
	}
}