<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InventoryPage.aspx.cs" Inherits="InventoryManagement.InventoryPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" method="post">
        <h1>Inventory Management Page</h1>
        <ul id="liItems" runat="server">
        </ul>
        <div id="resultMessage" runat="server"></div>
        Add a new item
        <div>Item:<input type="text" name="newItemName" /></div>
        <div>Quantity:<input type="number" name="newItemQuantity" /></div>
        <div>Price Per Item:<input type="number" name="newItemPrice" /></div>
        <input type="submit" name="Add" value="Add" />
    </form>
    <a href="Default.aspx">Home Page</a>
</body>
</html>
