using System;
using System.Collections.Generic;
using DizzleRasta.Web.Handlers;
using DizzleRasta.Web.Infrastructure;
using DizzleRasta.Web.Infrastructure.Pipeline;
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
					.ResourcesOfType<ReleaseCreateModel>()
					.AtUri("/releases/add")
					.HandledBy<ReleaseHandler>()
					.RenderedByAspx("~/Views/AddRelease.aspx");

				// TODO - don't like all the manual configurations - look for a conventional way (a la Fubu)

				ResourceSpace.Has
					.ResourcesOfType<ReleaseInputModel>()
					.AtUri("/releases/add")
					.HandledBy<ReleaseHandler>();

				ResourceSpace.Has
					.ResourcesOfType<IEnumerable<Track>>()
					.AtUri("/tracks")
					.HandledBy<TracksHandler>()
					.RenderedByAspx("~/Views/Tracks.aspx");

				

				// TODO - fork and fix or use different container?
				//ResourceSpace.Uses.Resolver.AddDependencyInstance<IDocumentSession>(
				//    DocumentStoreHolder.DocumentStore.OpenSession(),DependencyLifetime.PerRequest);

				ResourceSpace.Uses.Resolver.AddDependencyInstance<IDocumentSession>(DocumentStoreHolder.DocumentStore.OpenSession());

				ResourceSpace.Uses.PipelineContributor<SessionCloser>();

			}
		}
	}
}