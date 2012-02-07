<%@ Page Language="C#" Inherits="OpenRasta.Codecs.WebForms.ResourceView<Release>"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Random Releases</title>
</head>
<body>
	<h1>Release</h1>

	<div style="border-bottom: solid 2px red;">
		<img height="150" width="150" src="<%: Resource.ImageUrl %>" />
		<p>
			<b><%: Resource.Title  %> (<%: Resource.Version %>)</b>
		</p>
		<p>
			<%: Resource.Type %>
		</p>
		<br />
	</div>

</body>
</html>
