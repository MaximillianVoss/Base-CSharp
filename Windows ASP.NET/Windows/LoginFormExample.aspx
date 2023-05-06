<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginFormExample.aspx.cs" Inherits="Windows_ASP.NET.Windows.LgoinFormExample" %>

<%@ Register Src="~/Controls/LoginForm.ascx" TagPrefix="uc" TagName="LoginForm" %>
<%@ Register Src="~/Controls/Bootstrap.ascx" TagPrefix="uc" TagName="Bootstrap" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <uc:Bootstrap runat="server" id="Bootstrap" />
    <uc:LoginForm runat="server" id="LoginForm" />
</body>
</html>
