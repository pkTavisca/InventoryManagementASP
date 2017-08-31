<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="InventoryManagement.Cart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h3>Your cart</h3>
        <ul id="cartItems" runat="server"></ul>
        <input type="submit" name="checkout" value="Checkout" />
    </form>
</body>
</html>
