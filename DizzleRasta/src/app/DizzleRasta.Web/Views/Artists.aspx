<%@ Page Language="C#" Inherits="OpenRasta.Codecs.WebForms.ResourceView<IEnumerable<Artist>>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Artists</title>
</head>
<body>
	<h1>Artists</h1>
	<b>Here are a random selection of artists</b>
   <div>
		<% foreach (var artist in Resource)
		{ %>
		
		<div style="border-bottom: solid 2px red;">
			<img height="150" width="150" src="<%: artist.ImageUrl %>" />
			<p>
				<b><%: artist.Name %></b>
			</p>
			<br />
		</div>

	 <% } %>
   </div>

</body>
</html>
