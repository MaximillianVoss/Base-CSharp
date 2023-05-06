<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegisterForm.ascx.cs" Inherits="Windows_ASP.NET.Controls.RegisterForm" %>
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
    //#region Получение и сохранение отпечатка пользователя
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
    //#endregion

    //#region Отправка email и отпечатка на указанный URL
    function sendRegisterRequest() {
        const email = document.getElementById("txbLogin").value;
        const fingerprint = document.getElementById("hfFingerprint").value;

        const url = 'https://localhost:44391/api/Attempts?email=${email}&idVisitor=${fingerprint}';
        fetch(url, {
            method: "PUT"
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error("Network response was not ok");
                }
                // handle successful response
            })
            .catch(error => {
                console.error("There was a problem with the fetch operation:", error);
                // handle error
            });
    }
    //#endregion
</script>
<div class="text-center">
    <main class="form-signin w-100 m-auto">
        <form id="loginForm" runat="server">
            <asp:HiddenField ID="hfFingerprint" ClientIDMode="Static" runat="server" />
            <svg xmlns="http://www.w3.org/2000/svg" width="50" height="50" fill="currentColor" class="bi bi-person-plus-fill" viewBox="0 0 16 16">
                <path d="M1 14s-1 0-1-1 1-4 6-4 6 3 6 4-1 1-1 1H1zm5-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6z" />
                <path fill-rule="evenodd" d="M13.5 5a.5.5 0 0 1 .5.5V7h1.5a.5.5 0 0 1 0 1H14v1.5a.5.5 0 0 1-1 0V8h-1.5a.5.5 0 0 1 0-1H13V5.5a.5.5 0 0 1 .5-.5z" />
            </svg>
            <h1 class="h3 mb-3 fw-normal">
                <asp:Label ID="lblTitle" AssociatedControlID="loginForm" runat="server"> Please sign up</asp:Label>
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
            <div class="row m-2">
                <asp:Button ID="btnRegister" type="submit" class="w-100 btn btn-lg btn-primary" Text="Register" OnClick="btnRegister_Click" OnClientClick="sendRegisterRequest" runat="server"></asp:Button>
            </div>
            <p class="mt-5 mb-3 text-muted">© <%= DateTime.Now.ToString("yyyy") %></p>
        </form>
    </main>
</div>
