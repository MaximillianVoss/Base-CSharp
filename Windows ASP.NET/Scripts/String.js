// Пример форматирования
// "{0} is dead, but {1} is alive! {0} {2}".format("ASP", "ASP.NET")
function AddStringFormat() {
    //Проверим, не содержит ли класс String реализацию format
    if (!String.prototype.format) {
        String.prototype.format = function () {
            var args = arguments;
            return this.replace(/{(\d+)}/g, function (match, number) {
                return typeof args[number] != 'undefined'
                    ? args[number]
                    : match
                    ;
            });
        };
    }
}
AddStringFormat();

