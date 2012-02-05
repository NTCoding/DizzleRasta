using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using DizzleRasta.Web.Resources;

namespace DizzleRasta.Web.Services
{
	public class ApiQuerier 
	{
		private WebClient client;
		private readonly SevenDigitalResponseParser responseParser = new SevenDigitalResponseParser();

		private const string OAuthQueryStringComponent = "oauth_consumer_key=7dwvjchqsj";
		private const string ReleaseQueryUrl = "http://api.7digital.com/1.2/release/bydate";
		private const string ArtistQueryUrl = "http://api.7digital.com/1.2/artist/browse";
		private const string TrackQueryUrl = "http://api.7digital.com/1.2/release/tracks";

		private WebClient Client
		{
			get { return client ?? (client = new WebClient()); }
		}
		
		public IEnumerable<Artist> GetArtistsByName(string letter, int pageSize = 10)
		{
			var url = BuildUrl(ArtistQueryUrl, new[] { "letter=" + letter }, pageSize: pageSize);

			var response = QueryApi(url);

			return responseParser.ParseArtistsFrom(response);
		}

		public IEnumerable<Release> GetReleases(string @from, string to, int page, int pageSize)
		{
			var url = BuildUrl(ReleaseQueryUrl, new[] {"fromDate=" + from, "toDate=" + to, "page=" + page}, pageSize);

			var response = QueryApi(url);

			return responseParser.ParseReleasesFrom(response);
		}

		public IEnumerable<Track> GetTracks(string releaseId)
		{
			var url = BuildUrl(TrackQueryUrl, new[] {"releaseId=" + releaseId});

			var response = QueryApi(url);

			return responseParser.ParseTracksFrom(response);
		}

		// TODO - separate class with the urls?
		private Uri BuildUrl(string baseUrl, string[] queryStringComponents = null, int pageSize = 10)
		{
			string querystring = BuildQueryString(queryStringComponents, pageSize);

			return new Uri(baseUrl + querystring);
		}

		private string BuildQueryString(string[] queryStringComponents, int limit)
		{
			string fullQueryString = "?";
			if (queryStringComponents != null)
			{
				fullQueryString += string.Join("&", queryStringComponents);
			}
			if (limit > 0)
			{
				fullQueryString += "&pageSize=" + limit;
			}

			return fullQueryString + "&" + OAuthQueryStringComponent;
		}

		private string QueryApi(Uri url)
		{
			var request = WebRequest.Create(url);

			var response = request.GetResponse();

			using (var reader = new StreamReader(response.GetResponseStream()))
			{
				return reader.ReadToEnd();
			}
		}


		
	}
}