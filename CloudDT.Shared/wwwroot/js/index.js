function getTemplete(lang) {
    switch (lang) {
        case "javascript":
            return `console.log("Hello Node.js");`;

        case "python":
            return `print("Hello Python")`;

        case "csharp":
            return `Console.WriteLine("Hello .NET");`;

        default:
            return "";
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

async function runCodeSnippets(containerId, lang, code) {

    console.log(containerId);
    console.log(lang);
    console.log(code);

    let formData = new FormData();
    formData.append("code", code);

    let res = await fetch(`https://cloudshell.conchbrain.club/forward/${containerId}/4314/${lang}`, {
        method: "post",
        body: formData
    });
}