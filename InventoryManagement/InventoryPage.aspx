﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InventoryPage.aspx.cs" Inherits="InventoryManagement.InventoryPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" method="post">
        <h1>Inventory Management Page</h1>
        <ul id="listOfItems" runat="server">
        </ul>
        Add a new item
        <div>Item:<input type="text" name="newItemName" /></div>
        <div>Quantity:<input type="number" name="newItemQuantity" /></div>
        <input type="submit" value="Add" />
    </form>
    <a href="Default.aspx">Home Page</a>
</body>
</html>
