cd Python
IF NOT EXIST "python399.exe" (call Aether_Setup.bat)
cd ..
cd Aether_Fusion\my-electron-app
call npm i
npm start
cd ..
cd bin\Debug\net7.0
dotnet Aether_Fusion.dll
pause