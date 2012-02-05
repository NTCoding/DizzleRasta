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

		//public IEnumerable<Release> ParseReleases(string releasesString)
		//{
		//    var doc = XDocument.Parse(releasesString);

		//    var releaseNodes = doc.Descendants("release");

		//    return releaseNodes.Select(ParseRelease);
		//}

		//private Release ParseRelease(XElement releaseElement)
		//{
		//    var id               = TryGetAttribute("id", releaseElement);
		//    var type             = TryGetValue("type", releaseElement);
		//    var version          = TryGetValue("version", releaseElement);
		//    var barcode          = TryGetValue("barcode", releaseElement);
		//    var artist           = ParseArtistId(releaseElement);
		//    var title            = TryGetValue("title", releaseElement);
		//    var year             = int.Parse(TryGetValue("year", releaseElement));
		//    var explicitContent  = bool.Parse(TryGetValue("explicitContent", releaseElement));
		//    var imageUrl         = TryGetValue("image", releaseElement);

		//    return new Release { ArtistId = int.Parse(artist), Id = int.Parse(id), ImageUrl = imageUrl, Title = title, Type = type };
		//}

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

		//private Tag ParseTag(XElement tagElement)
		//{
		//    string id = TryGetAttribute("id", tagElement);
		//    string text = TryGetValue("text", tagElement);
		//    string url = TryGetValue("url", tagElement);
		//    int count;

		//    bool countValid = int.TryParse(TryGetValue("count", tagElement), out count);

		//    return IsTagParseSuccessful(id, text, url, countValid)
		//               ? new Tag(id, text, url, count)
		//               : null;
		//}

		private bool IsTagParseSuccessful(string id, string text, string url, bool countValid)
		{
			return !string.IsNullOrWhiteSpace(id)
				   && !string.IsNullOrWhiteSpace(text)
				   && !string.IsNullOrWhiteSpace(url)
				   && countValid;
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