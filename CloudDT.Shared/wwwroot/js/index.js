function getTemplete(lang) {
    switch (lang) {
        case "javascript":
            return `console.log("Hello Node.js");`;

        case "python":
            return `print("Hello Python")`;

        case "csharp":
            return `Console.WriteLine("Hello .NET");`;

        default:
            return `
                         ________________________________________________
                        -                                                -
                       |    _________________________________________     |
                       |   |                                         |    |
                       |   |  C:/ > Hello CloudDT_                   |    |
                       |   |                                         |    |
                       |   |                                         |    |
                       |   |                                         |    |
                       |   |                                         |    |
                       |   |                                         |    |
                       |   |                                         |    |
                       |   |                                         |    |
                       |   |                                         |    |
                       |   |                                         |    |
                       |   |                                         |    |
                       |   |                                         |    |
                       |   |_________________________________________|    |
                       |                                                  |
                        -________________________________________________-
                               -___________________________________-
                            ___________________________________________
                         _-'    .-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.  ---  -_
                      _-'.-.-. .---.-.-.-.-.-.-.-.-.-.-.-.-.-.-.--.  .-.-.--_
                   _-'.-.-.-. .---.-.-.-.-.-.-.-.-.-.-.-.-.-.-.--__-. .-.-.-.--_
                _-'.-.-.-.-. .-----.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-----. .-.-.-.-.--_
             _-'.-.-.-.-.-. .---.-. .-------------------------. .-.---. .---.-.-.-.--_
            :-------------------------------------------------------------------------:
            '--._.-------------------------------------------------------------._.---'`
    }
}

var editor, worker;

function initEditor(lang, code) {
    document.querySelector("#editor-container").innerHTML = `<div id="editor"></div>`;

    if (!code)
        code = getTemplete(lang);

    editor = monaco.editor.create(document.querySelector('#editor'), {
        value: code,
        language: lang,
        theme: 'vs-dark',
        automaticLayout: true,
        scrollBeyondLastLine: false
    });
}

function openFind() {
    editor.getAction("actions.find").run();
}

function getCode() {
    return editor.getValue();
}

function dalayContainer(id) {
    worker = new Worker("./js/worker.js");
    worker.postMessage(id);
}