# <img src="Aether GUI/GUI-Frontend-Avalonia/GUI/Assets/aet.ico" alt="[logo]" width="48"/> Virtual Desktop Assistant for Windows

## Features

- TODO app
- Search the internet
- Open apps

## Downloads

Download from [releases](https://github.com/MVP-Team/Project-Aether/releases).

## Development

- IDE: Visual Studio 2022
- Language: C# 9.0
- SDK: .NET 6

## Use Case

Open a app with Aether.

```bash
Aether open Chrome
```

## Prerequisites

Before you begin building the application, you must have the following prerequisites installed on your system:

- [Node.js](https://nodejs.org/dist/v18.13.0/node-v18.13.0-x64.msi)

## Building

If you wish to build the Virtual Desktop Assistant yourself, follow these steps:

### Step 1

Install the X64 version of [.NET 6.0 (or higher) SDK](https://dotnet.microsoft.com/download/dotnet/6.0).

### Step 2

Clone the repository.

```bash
git clone https://github.com/MVP-Team/Project-Aether.git
```

### Step 3

To build Aether, open a command prompt inside the project directory. Then type the following command:

```
dotnet build -c Release -o build
``` 
the built files will be found in the newly created build directory.

### Step 4

To build Electron App you need to:
run the
```bash
npm i
```
command
after that, run the app using 
```bash
npm start
```
