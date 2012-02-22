<%@ Page Language="C#" Inherits="OpenRasta.Codecs.WebForms.ResourceView<IEnumerable<Track>>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tracks</title>
</head>
<body>
   <h1>Random Tracks</h1>
   
   <h2>Queries</h2>
   <form action="/tracks" method="post">
		<p>
			<label>Price Min:</label> <input type="text" name="PriceMin" /> 
			<label>Price Max: </label> <input type="text" name="PriceMax" />
			<input type="submit" value="Search by price" />
		</p>
   </form>
   <br />
   <hr />
   <% foreach (var track in Resource) {  %>

	<div style="border-bottom: solid 2px red;">
		<p>
			<b><%: track.Title  %> (<%: track.Version %>)</b>
		</p>
		<p>
			<%: track.Price %>
		</p>
		<br />
	</div>

   <% } %>
</body>
</html>
