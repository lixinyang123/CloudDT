const { app, BrowserWindow } = require('electron');

process.env['ELECTRON_DISABLE_SECURITY_WARNINGS'] = 'true';

function createWindow () {
    const mainWindow = new BrowserWindow({
        width: 1000,
        height: 700
    });

    mainWindow.loadFile('wwwroot/index.html');

    // Open the DevTools.
    mainWindow.webContents.openDevTools();
}

app.whenReady().then(() => {
    createWindow();

    app.on('activate', () => {
        if (BrowserWindow.getAllWindows().length === 0) 
            createWindow();
    });
})

app.on('window-all-closed', () => {
    if (process.platform !== 'darwin') 
        app.quit();
})