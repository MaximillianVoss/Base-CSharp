<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterFormExapmle.aspx.cs" Inherits="Windows_ASP.NET.Windows.RegisterFormExapmle" %>

<%@ Register Src="~/Controls/RegisterForm.ascx" TagPrefix="uc" TagName="RegisterForm" %>
<%@ Register Src="~/Controls/Bootstrap.ascx" TagPrefix="uc" TagName="Bootstrap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <uc:Bootstrap runat="server" ID="Bootstrap" />
    <uc:RegisterForm runat="server" ID="RegisterForm" />
</body>
</html>
