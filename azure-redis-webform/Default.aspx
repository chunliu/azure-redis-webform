<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="azure_redis_webform.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Redis SessionState Provider</title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Redis SessionState Provider</h2>
        <p>
            This value is from Redis SessionState Provider. This session starts at: <%= Session["redis"] %>
        </p>
        <p>
            Current time: <%= DateTime.Now.ToString() %>
        </p>
        <p>
            <div>
                Redis cache key: <asp:Label ID="keyLabel" runat="server" />
            </div>
            <div>
                Redis cache value: <asp:Label ID="valueLabel" runat="server" />
            </div>
        </p>
    </form>
</body>
</html>
