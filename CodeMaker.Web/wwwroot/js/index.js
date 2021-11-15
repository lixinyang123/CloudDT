function initEditor() {
    var editor = monaco.editor.create(document.querySelector('#editor'), {
        value: 'console.log(123);',
        language: 'javascript',
        theme: 'vs-dark',
        automaticLayout: true,
        scrollBeyondLastLine: false
    });
}