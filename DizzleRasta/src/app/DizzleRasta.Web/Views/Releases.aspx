<%@ Page Language="C#" Inherits="OpenRasta.Codecs.WebForms.ResourceView<IEnumerable<Release>>"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Random Releases</title>
</head>
<body>
	<h1>Releases</h1>
	
	<h2>Queries</h2>
	<form action="/releases" method="post">

		<p>
			<label>Search term: </label>
			<input type="text" name="SearchTerm" />
			<input type="submit" value="search" />
		</p>
		<p>
			<input type="submit" name="GetSingles" value="Show all singles" />
		</p>
		

	</form>
	<br />
	<hr />

	<% foreach (var release in Resource) {  %>

	<div style="border-bottom: solid 2px red;">
		<img height="150" width="150" src="<%: release.ImageUrl %>" />
		<p>
			<b><%: release.Title  %> (<%: release.Version %>)</b>
		</p>
		<p>
			<%: release.Type %>
		</p>
		<p>
			<form action="/releases" method="post">
				<input type="hidden" name="Artist" value="<%: release.ArtistId %>" />
				<input type="submit" value="Get all releases for this artist" />
			</form>
		</p>
		<br />
	</div>

	<% } %>
</body>
</html>
