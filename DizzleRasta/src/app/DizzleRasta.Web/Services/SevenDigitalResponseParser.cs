using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using DizzleRasta.Web.Resources;

namespace DizzleRasta.Web.Services
{
	public class SevenDigitalResponseParser 
	{
		//public IEnumerable<Tag> ParseTag(string tagsString)
		//{
		//    var doc = XDocument.Parse(tagsString);

		//    var tagNodes = doc.Descendants("tag");

		//    return tagNodes.Select(ParseTag)
		//        .Where(t => t != null);
		//}

		public IEnumerable<Release> ParseReleasesFrom(string response)
		{
			var xml = XDocument.Parse(response);

			var releases = xml.Descendants("release");

			return releases.Select(ParseRelease);
		}

		private Release ParseRelease(XElement releaseElement)
		{
			var id              = TryGetAttribute("id", releaseElement);
			var type            = TryGetValue("type", releaseElement);
			var version         = TryGetValue("version", releaseElement);
			var barcode         = TryGetValue("barcode", releaseElement);
			var artistId        = ParseArtist(releaseElement).Id;
			var title           = TryGetValue("title", releaseElement);
			var year            = int.Parse(TryGetValue("year", releaseElement));
			var explicitContent = bool.Parse(TryGetValue("explicitContent", releaseElement));
			var imageUrl        = TryGetValue("image", releaseElement);

			return new Release { ArtistId = artistId, Id = int.Parse(id), ImageUrl = imageUrl, Title = title, Type = type };
		}

		public IEnumerable<Artist> ParseArtistsFrom(string response)
		{
			var xml = XDocument.Parse(response);

			var artists = xml.Descendants("artist");

			return artists.Select(ParseArtist);
		}

		private Artist ParseArtist(XElement element)
		{
			var id = TryGetAttribute("id", element);
			var name = TryGetValue("name", element);
			var imageUrl = TryGetValue("image", element);

			return new Artist {Id = int.Parse(id), ImageUrl = imageUrl, Name = name};
		}

		private string TryGetAttribute(string attributeName, XElement element)
		{
			var attribute = element.Attribute(attributeName);

			return attribute != null
					   ? attribute.Value
					   : "";
		}

		private string TryGetValue(string elementName, XElement element)
		{
			var child = element.Element(elementName);

			return child != null
					   ? child.Value
					   : "";
		}


		
	}
}