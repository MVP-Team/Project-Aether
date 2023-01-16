const { kMaxLength } = require('buffer')
const { app, BrowserWindow, autoUpdater } = require('electron')
const { maxHeaderSize } = require('http')
const path = require('path')

function createWindow () {
const win = new BrowserWindow({
    width: 300,
    height: 800,
    titleBarStyle: 'hidden',
    webPreferences: {
      preload: path.join(__dirname, 'preload.js')
    },
    resizable: false
  });
win.loadFile('index.html');
}

app.whenReady().then(() => {
  createWindow()

  app.on('activate', () => {
    if (BrowserWindow.getAllWindows().length === 0) {
      createWindow()
    }
  })
})

app.on('window-all-closed', () => {
  if (process.platform !== 'darwin') {
    app.quit()
  }
})

