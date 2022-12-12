<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MessageBoxExample.aspx.cs" Inherits="Windows_ASP.NET.Windows.MessageBoxExample" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Пример всплывающего окна</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous">
</head>
<body class="text-center m-5">
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js" integrity="sha384-oBqDVmMz9ATKxIep9tiCxS/Z9fNfEXiDAYTujMAeBAsjFuCZSmKbSSUnQlmh/jp3" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.min.js" integrity="sha384-cuYeSxntonz0PPNlHhBs68uyIAVpIIOZZ5JqeqvYYIcEL727kskC66kF92t6Xl2V" crossorigin="anonymous"></script>
    <script src="../Scripts/String.js"></script>
    <script src="../Scripts/MessageBox.js"></script>
    <%--Простая форма--%>
    <section>
        <div id="containerSimple">
        </div>
        <button id="btnSimpleWindow" type="button" class="btn btn-primary m-1 w-50">
            Показать простую форму
        </button>
        <script>
            var messageBox = new MessageBox("Простая форма", "Текст сообщения", []);
            messageBox.Insert("btnSimpleWindow", "containerSimple", "simpleWindow");
        </script>
    </section>
    <%--Форма с несколькими кнопками--%>
    <section>
        <div id="containerMultiBtn">
        </div>
        <button id="btnMultiBtn" type="button" class="btn btn-primary m-1 w-50">
            Показать форму с несколькими кнопками
        </button>
        <script>
            var messageBox = new MessageBox("Форма с несколькими кнопками", "Текст сообщения", [
                new MessageBoxButton("Да", "btn btn-primary", false),
                new MessageBoxButton("Нет", "btn btn-danger", false),
                new MessageBoxButton("Закрыть","btn btn-secondary",true)
            ]);
            messageBox.Insert("btnMultiBtn", "containerMultiBtn", "multiBtnWindow");
        </script>
    </section>

</body>
</html>
