<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="InventoryManagement.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" method="post" action="Cart.aspx">
        <h1>Store Page</h1>
        <ul id="liItems" runat="server">
        </ul>
        <input type="submit" id="submitButton" runat="server" value="Continue to cart" />
    </form>
    <a href="InventoryPage.aspx">Inventory Management</a>
</body>
</html>
