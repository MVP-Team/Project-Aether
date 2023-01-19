# <img src="Aether GUI/GUI-Frontend-Avalonia/GUI/Assets/aet.ico" alt="[logo]" width="48"/> Virtual Desktop Assistant only for Windows

## Features

- TODO app
- Search in the internet
- Open apps
- Translate sentences
- Make jokes

## Downloads

Download from [releases](https://github.com/MVP-Team/Project-Aether/releases).

## Development

- IDE: Visual Studio 2022
- Language: C# 9.0
- SDK: .NET 6

## Use Case

How to search the web with Aether.

```bash
Search the tallest building in the world
```

## Prerequisites

Before you begin building the application, the following prerequisites must be installed on your system:

- [Node.js](https://nodejs.org/dist/v18.13.0/node-v18.13.0-x64.msi)

## Building

If you wish to build the Virtual Desktop Assistant on Windows yourself, follow these steps:

### Step 1

Install the X64 version of [.NET 6.0 (or higher) SDK](https://dotnet.microsoft.com/download/dotnet/6.0).

### Step 2

Clone the repository.

```bash
git clone https://github.com/MVP-Team/Project-Aether.git
```

### Step 3

In order to run Aether, open a command prompt inside the project directory. Then type the following commands:

```
cd Aether-Console\bin\Debug\net6.0

dotnet Aether-Console.dll
``` 
Afterwards you are free to use the application.

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

## Commands that you can use in Aether:

```bash
Search [your request]

Translate [Your sentence] from [From language] into [Wished Language]

Translate [Your sentence] into [Wished Language]

Open [Your Application]

Close

Random Joke
```
