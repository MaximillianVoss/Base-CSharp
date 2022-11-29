<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginWindow.aspx.cs" Inherits="Windows_ASP.NET.Forms.LoginWindow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>LoginWindow</title>
    <meta name="description" content="YOUR_DESCRIPTION" />
    <meta name="keywords" content="YOUR_KEYWORDS" />
</head>

<!--#region Styles  -->
<!-- CSS only -->
<link
    href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.2/dist/css/bootstrap.min.css"
    rel="stylesheet"
    integrity="sha384-Zenh87qX5JnK2Jl0vWa8Ck2rdkQ2Bzep5IDxbcnCeuOxjzrPF/et3URy9Bv1WTRi"
    crossorigin="anonymous" />
<!-- <link rel="stylesheet" type="text/css" href="yourCss.css" /> -->

<!--#endregion -->

<!--#region Scripts  -->

<!-- JavaScript Bundle with Popper -->
<script
    src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.2/dist/js/bootstrap.bundle.min.js"
    integrity="sha384-OERcA2EqjJCMA+/3y+gxIOqMEjwtxJY7qPCqsdltbNJuaOe923+mo//f6V8Qbsw3"
    crossorigin="anonymous"></script>
<!-- <script src="your.js"></script> -->

<%--https://getbootstrap.com/docs/5.2/examples/sign-in/--%>

<body class="text-center">
    <form id="loginForm" class="container col-4" runat="server">
        <%--<img />--%>
        <h1 class="h3 mb-3 fw-normal">Please sign in</h1>
        <div class="mb-3">
            <label for="exampleFormControlInput1" class="form-label">Email address</label>
            <input type="email" class="form-control" id="exampleFormControlInput1" placeholder="name@example.com">
        </div
        <div class="mb-3">
            <label for="exampleFormControlInput1" class="form-label">Email address</label>
            <input type="email" class="form-control" id="exampleFormControlInput1" placeholder="name@example.com">
        </div>

        <div class="form-floating">
            <input type="email" class="form-control" id="floatingInput" placeholder="name@example.com">
            <label for="floatingInput">Email address</label>
        </div>
        <div class="checkbox mb-3">
            <label>
                <input type="checkbox" value="remember-me">
                Remember me
     
            </label>
        </div>
    </form>
</body>
</html>
