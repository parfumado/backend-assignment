# Backend / Fullstack take home assignment Parfumado

This take home assignment has been ellaborated as a take home assignment during the interview process of a Full-Stack or Backend position. If you're applying for a full stack position, please refer to the repo in: https://github.com/parfumado/frontend-assignment

For backend assignment, don't forget to attach an example postman collection to your application (please document it so we can find it).

## Goal of the assignment
 This assignment will evaluate your general knowledge on implementing a simple username and password login mechanism. But making it run is not the most important aspect of this assignment. This assignment is meant to evaluate how do you think and work around problems and solutions. Please comment in the code your intentions and choices. This will not only help us understand what you're trying to do, but it will also help you keep track of what you want to demonstrate.

Most of the boiler-plate code is already setup, although you will find boiler-plate related TODO's. Please ensure you implement all project's TODO commented sections.

The project is split in pseudo microservices which are automatically loaded into one monolythic runtime application. You should consider best practices when implementing. And also refactor portions that you identify that do not adopt some of these best practices.

There is a mocked persistence layer, but you're free to implement and inject a persistence layer that will write to a real database, or a file, if you prefer. As long as you provide instructions on how to use it, it's fine.

## Expected delivered features:

All of the below should be focused on functionality and not quality of the UI (of course, we won't complain if it's pretty) if you're also delivering the frontend project.

1 - Sign-in via username/password
2 - Sign-in via token (simulating a reset password link)
3 - Update user details (and/or password)
4 - Get user info (to show user on home page)
5 - Sign-up
6 - Sign out

The expected outcome of this assignment should be at least compiling code, that can be tested (either via postman or via the frontend assignment). A live version, acessible through a secure url is a big plus.

## How to deliver.

1 - Fork the repositories privately
2 - Write the code
3 - Share with git hub organization "parfumado"
4 - Notify us via e-mail describing what you delivered.

Good luck and success! Will try to get back to you as soon as possible.

# Technical background

## Pre-Requisites

- [VS Code](https://code.visualstudio.com/download)
- [dotnet 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)

### Recommended VSCode extensions

- [C#](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
- [C# Extensions](https://marketplace.visualstudio.com/items?itemName=jchannon.csharpextensions)
- [C# XML Documentation](https://marketplace.visualstudio.com/items?itemName=jchannon.csharpextensions)
- [Docker](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-docker)

## Develop, Build and Run
All commands below assume you're on the root folder of the project.

### *Initialize settings as environment variables*

> Powershell
```
setx PLATFORM_api-settings__StorefrontToken=12345
${Env:PLATFORM_api-settings__StorefrontToken}="12345"
```

> Bash
```
export PLATFORM_api-settings__StorefrontToken="%!sv4JBM6%4w7Ert"
```

### Add a new Web API project (Web Server)

`dotnet new webapi -o some-service --language "C#"`

### Add a new class library project

`dotnet new classlib -o some-service --language "C#"`

### Add a reference from one project to another

`dotnet add some-service reference other-service`

## Building and Running

### Building
> Debug Build 

`dotnet build api -c Debug`

> Release Build

`dotnet build api -c Release`

### Running (also builds)
You can use the already configured launch and debug setting of VSCode (press F5). Alternative you can also start the project through the CLI:

> Run in Debug mode

`dotnet run --project api -c Debug`

> Run in Release mode

`dotnet run --project api -c Release`

> Attach Debugger

![Attach Debugger](doc-assets/attach.jpg?raw=true "Attach Debugger")

## Publishing

### Publish to local environment (/bin/Debug or /bin/Release)
> Debug binaries

`dotnet publish api -c Debug`

> Release binaries

`dotnet publish api -c Release`

### Publish to local Docker image
`docker build -t platform-backend -f api/Dockerfile .`