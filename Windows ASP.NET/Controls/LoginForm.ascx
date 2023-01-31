<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginForm.ascx.cs" Inherits="Windows_ASP.NET.Controls.LoginForm" %>
<style>
    .form-signin {
        max-width: 330px;
        padding: 15px;
    }

        .form-signin input[type="email"] {
            margin-bottom: -1px;
            border-bottom-right-radius: 0;
            border-bottom-left-radius: 0;
        }

        .form-signin input[type="password"] {
            margin-bottom: 10px;
            border-top-left-radius: 0;
            border-top-right-radius: 0;
        }
</style>
<script>
    // Get the visitor identifier when you need it.
    function FingerPrintGetId() {
        // Initialize the agent at application startup.
        const fpPromise = import("https://openfpcdn.io/fingerprintjs/v3").then(
            (FingerprintJS) => FingerprintJS.load()
        );
        fpPromise
            .then((fp) => fp.get())
            .then((result) => {
                // This is the visitor identifier:
                const visitorId = result.visitorId;
                console.log("User fingerprint: " + visitorId);
                var hiddenField = document.getElementById("hfFingerprint");
                if (hiddenField) {
                    hiddenField.value = visitorId;
                }
                else {
                    console.error("Нет скрытого поля для вставки visitorId");
                }
            });
    }

    FingerPrintGetId();
</script>
<div class="text-center">
    <main class="form-signin w-100 m-auto">
        <form id="loginForm" runat="server">
            <asp:HiddenField ID="hfFingerprint" ClientIDMode="Static" runat="server" />
            <svg xmlns="http://www.w3.org/2000/svg" width="50" height="50" fill="currentColor" class="bi bi-door-open" viewBox="0 0 16 16">
                <path d="M8.5 10c-.276 0-.5-.448-.5-1s.224-1 .5-1 .5.448.5 1-.224 1-.5 1z" />
                <path d="M10.828.122A.5.5 0 0 1 11 .5V1h.5A1.5 1.5 0 0 1 13 2.5V15h1.5a.5.5 0 0 1 0 1h-13a.5.5 0 0 1 0-1H3V1.5a.5.5 0 0 1 .43-.495l7-1a.5.5 0 0 1 .398.117zM11.5 2H11v13h1V2.5a.5.5 0 0 0-.5-.5zM4 1.934V15h6V1.077l-6 .857z" />
            </svg>
            <h1 class="h3 mb-3 fw-normal">
                <asp:Label ID="lblTitle" AssociatedControlID="loginForm" runat="server"> Please sign in</asp:Label>
            </h1>
            <div class="form-floating">
                <asp:TextBox ID="txbLogin" type="email" class="form-control" placeholder="name@example.com" runat="server" OnTextChanged="txbLogin_TextChanged" AutoPostBack="false"></asp:TextBox>
                <asp:Label ID="lblLogin" AssociatedControlID="loginForm" for="txbLogin" runat="server">Email address</asp:Label>
                <label id="lblLoginMain"></label>
            </div>
            <div class="form-floating">
                <asp:TextBox ID="txbPassword" type="password" class="form-control" placeholder="Password" runat="server" OnTextChanged="txbPassword_TextChanged" AutoPostBack="false"></asp:TextBox>
                <asp:Label ID="lblPassword" AssociatedControlID="loginForm" for="txbPassword" runat="server">Password</asp:Label>
            </div>
            <asp:Label ID="lblError" class="fs-6 fw-light text-danger" AssociatedControlID="loginForm" runat="server">
                Ошибка авторизации
            </asp:Label>
            <div class="checkbox mb-3">
                <asp:Label ID="lblRemeber" AssociatedControlID="loginForm" runat="server">
                    <asp:CheckBox type="checkbox" value="remember-me" ID="chbRemember" OnCheckedChanged="chbRemember_CheckedChanged" runat="server" AutoPostBack="false" />
                    Remember me   
                </asp:Label>
            </div>
            <asp:Button ID="btnLogin" type="submit" class="w-100 btn btn-lg btn-primary" Text="Sign in" OnClick="btnLogin_Click" runat="server"></asp:Button>
            <p class="mt-5 mb-3 text-muted">© <%= DateTime.Now.ToString("yyyy") %></p>
        </form>
    </main>
</div>
