<%@ Page Language="C#" Inherits="OpenRasta.Codecs.WebForms.ResourceView<ReleaseCreateModel>"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Release</title>
</head>
<body>
    <h1>Add Release</h1>

	<% using(scope(Xhtml.Form(new ReleaseInputModel()).Method("POST"))) { %>
	  
	  <p>
	     <b>Artist: </b>
		 <%= Xhtml.Select<ReleaseInputModel>(r => r.ArtistId, Resource.Artists) %>
	  </p>

	  <p>
			<b>Title: </b>
			<%= Xhtml.TextBox<ReleaseInputModel>(r => r.Title) %>
	  </p>

	  <p>
			<b>Type: </b>
			<%= Xhtml.TextBox<ReleaseInputModel>(r => r.Type) %>
	  </p>

	  <p>
			<b>Version: </b>
			<%= Xhtml.TextBox<ReleaseInputModel>(r => r.Version) %>
	  </p>

	  <p>
			<b>Image Url: </b>
			<%= Xhtml.TextBox<ReleaseInputModel>(r => r.ImageUrl) %>
	  </p>
	  

	  <input type="submit" value="Create that mofo" />

	<% } %>

</body>
</html>
