const { app, BrowserWindow } = require('electron');
const liveServer = require("live-server");

process.env['ELECTRON_DISABLE_SECURITY_WARNINGS'] = 'true';

function createWindow () {
    const mainWindow = new BrowserWindow({
        width: 1200,
        height: 800
    });

    mainWindow.loadURL("http://localhost:8435");

    // Open the DevTools.
    // mainWindow.webContents.openDevTools();
}

liveServer.start({
    port: 8435,
    host: "0.0.0.0",
    root: "./publish/wwwroot",
    open: false,
    file: "index.html",
});

app.whenReady().then(() => {
    createWindow();

    app.on('activate', () => {
        if (BrowserWindow.getAllWindows().length === 0) 
            createWindow();
    });
});

app.on('window-all-closed', () => {
    if (process.platform !== 'darwin') 
        app.quit();
});