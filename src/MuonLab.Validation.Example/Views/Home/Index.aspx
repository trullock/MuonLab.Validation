<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<MuonLab.Validation.Example.ViewModels.TestViewModel>" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Index</title>
</head>
<body>
    <div>
        <form action="" method="post">
		
			<fieldset>
				<label>Email</label>
				<%: Html.TextBoxFor(x => x.Email) %>
				<%: Html.ValidationMessageFor(x => x.Email) %>
			</fieldset>

			<fieldset>
				<label>Password</label>
				<%: Html.PasswordFor(x => x.Password)%>
				<%: Html.ValidationMessageFor(x => x.Password)%>
			</fieldset>
			<fieldset>
				<label>Confirm Password</label>
				<%: Html.PasswordFor(x => x.ConfirmPassword)%>
				<%: Html.ValidationMessageFor(x => x.ConfirmPassword)%>
			</fieldset>
			<input type="submit" value="Test me!" />
		</form>
    </div>
</body>
</html>
