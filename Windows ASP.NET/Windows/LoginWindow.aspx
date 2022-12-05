<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginWindow.aspx.cs" Inherits="Windows_ASP.NET.Forms.LoginWindow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>LoginWindow</title>
    <meta name="description" content="YOUR_DESCRIPTION" />
    <meta name="keywords" content="YOUR_KEYWORDS" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous">
    <link href="../Styles/LoginWindow.css" rel="stylesheet" />
</head>

<body class="text-center">
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js" integrity="sha384-oBqDVmMz9ATKxIep9tiCxS/Z9fNfEXiDAYTujMAeBAsjFuCZSmKbSSUnQlmh/jp3" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.min.js" integrity="sha384-cuYeSxntonz0PPNlHhBs68uyIAVpIIOZZ5JqeqvYYIcEL727kskC66kF92t6Xl2V" crossorigin="anonymous"></script>
  
    <main class="form-signin w-100 m-auto">
        <form id="loginForm" runat="server">
            <%--<img />--%>
            <svg xmlns="http://www.w3.org/2000/svg" width="50" height="50" fill="currentColor" class="bi bi-door-open" viewBox="0 0 16 16">
                <path d="M8.5 10c-.276 0-.5-.448-.5-1s.224-1 .5-1 .5.448.5 1-.224 1-.5 1z" />
                <path d="M10.828.122A.5.5 0 0 1 11 .5V1h.5A1.5 1.5 0 0 1 13 2.5V15h1.5a.5.5 0 0 1 0 1h-13a.5.5 0 0 1 0-1H3V1.5a.5.5 0 0 1 .43-.495l7-1a.5.5 0 0 1 .398.117zM11.5 2H11v13h1V2.5a.5.5 0 0 0-.5-.5zM4 1.934V15h6V1.077l-6 .857z" />
            </svg>
            <h1 class="h3 mb-3 fw-normal">Please sign in</h1>
            <div class="form-floating">
                <asp:TextBox ID="txbLogin" type="email" class="form-control" placeholder="name@example.com" runat="server"></asp:TextBox>
                <label for="txbLogin">Email address</label>
            </div>
            <div class="form-floating">
                <asp:TextBox ID="txbPassword" type="password" class="form-control" placeholder="Password" runat="server"></asp:TextBox>
                <label for="txbPassword">Password</label>
            </div>
            <div class="checkbox mb-3">
                <label>
                    <input type="checkbox" value="remember-me" />
                    Remember me   
                </label>
            </div>
            <asp:Button ID="btnLogin" type="submit" class="w-100 btn btn-lg btn-primary" Text="Sign in" OnClick="btnLogin_Click" runat="server"></asp:Button>
            <p class="mt-5 mb-3 text-muted">© <%= DateTime.Now.ToString("yyyy") %></p>
        </form>
    </main>
</body>
</html>
