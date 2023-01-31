<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginWindow.aspx.cs" Inherits="Windows_ASP.NET.Forms.LoginWindow" %>

<%@ Register Src="~/Controls/LoginForm.ascx" TagPrefix="uc1" TagName="LoginForm" %>
<%@ Register Src="~/Controls/Bootstrap.ascx" TagPrefix="uc1" TagName="Bootstrap" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>LoginWindow</title>
    <meta name="description" content="YOUR_DESCRIPTION" />
    <meta name="keywords" content="YOUR_KEYWORDS" />
    <link href="../Styles/LoginWindow.css" rel="stylesheet" />
</head>

<body class="text-center">

    <uc1:Bootstrap runat="server" id="Bootstrap"/>
    <uc1:LoginForm runat="server" id="LoginForm" OnLoad="LoginForm_Load"/>
</body>
</html>
