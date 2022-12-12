window.onload = function () {
    //var messageBox = new MessageBox("my title", "some text", []);
    //messageBox.Insert("exampleBtn", "exampleContainer", "exampleWindow");
}

class MessageBoxButton {
    constructor(title, cssClass, isClosing, onClickFunction) {
        this.id = "btnMsgBox" + title.replace(" ", "");
        this.title = title;
        this.cssClass = cssClass;
        this.isClosing = isClosing;
        if (onClickFunction)
            this.onClickFunction = onClickFunction;
        else
            //TODO: подключить string format
            //"Для кнопки '" + this.id + "' не назначено действие"  
            this.onClickFunction = "console.log('Для кнопки {0} не назначено действие);".format(this.id);;
    }
    Create() {
        var btn = document.createElement("button");
        btn.id = this.id;
        btn.setAttribute("type", "button");
        btn.setAttribute("class", this.cssClass);
        btn.setAttribute("onclick", this.onClickFunction);
        if (this.isClosing)
            btn.setAttribute("data-bs-dismiss", "modal");
        btn.innerHTML = this.title;
        return btn;
    }
}

class MessageBox {
    constructor(title, message, buttonsList) {
        this.title = title;
        this.message = message;
        this.buttonsList = buttonsList;
    }
    Create(idWindow, buttonsList) {
        //#region Modal
        var divModal = document.createElement("div");
        divModal.id = idWindow;
        divModal.setAttribute("class", "modal fade");
        //#region Modal dialog
        var divModalDialog = document.createElement("div");
        divModalDialog.setAttribute("class", "modal-dialog");
        //#region Modal content
        var divModalContent = document.createElement("div");
        divModalContent.setAttribute("class", "modal-content");

        //#region Modal header
        var divModalHeader = document.createElement("div");
        divModalHeader.setAttribute("class", "modal-header");
        var h1Title = document.createElement("h1");
        h1Title.setAttribute("class", "modal-title fs-5");
        h1Title.innerHTML = this.title;
        var btnClose = document.createElement("button");
        btnClose.setAttribute("class", "btn-close");
        btnClose.setAttribute("data-bs-dismiss", "modal");
        divModalHeader.appendChild(h1Title);
        divModalHeader.appendChild(btnClose);
        //#endregion

        //#region Modal body
        var divModalBody = document.createElement("div");
        divModalBody.setAttribute("class", "modal-body");
        var pContent = document.createElement("p");
        //pContent.setAttribute("class", "");
        pContent.innerHTML = this.message;
        divModalBody.appendChild(pContent);
        //#endregion

        //#region Modal footer
        var divModalFooter = document.createElement("div");
        divModalFooter.setAttribute("class", "modal-footer");
        if (this.buttonsList == undefined || this.buttonsList.length == 0) {
            var closeBtn = new MessageBoxButton("Закрыть", "btn btn-secondary", true);
            divModalFooter.appendChild(closeBtn.Create());
        }
        else {
            for (var i = 0; i < this.buttonsList.length; i++) {
                if (!(this.buttonsList[i] instanceof MessageBoxButton)) {
                    console.error("Кнопка должна быть класса MessageBoxButton");
                }
                else {
                    divModalFooter.appendChild(this.buttonsList[i].Create());
                }
            }
        }
        //#endregion

        //#endregion
        //#endregion
        //#endregion

        //#region Добавление элементов на форму
        divModalContent.appendChild(divModalHeader);
        divModalContent.appendChild(divModalBody);
        divModalContent.appendChild(divModalFooter);
        divModalDialog.appendChild(divModalContent);
        divModal.appendChild(divModalDialog);
        //#endregion

        return divModal;
    }

    Insert(idBtn, idContainer, idWindow) {
        var btn = document.getElementById(idBtn);
        var container = document.getElementById(idContainer);
        if (btn && container) {
            var window = this.Create(idWindow);
            container.appendChild(window);
            btn.setAttribute("data-bs-toggle", "modal");
            btn.setAttribute("data-bs-target", "#" + idWindow);
        }
        else {
            if (!btn)
                console.error("Не найдена управляющая кнопка для MessageBox!");
            if (!container)
                console.error("Не найден контейнер для вставки MessageBox!");
        }

    }
}