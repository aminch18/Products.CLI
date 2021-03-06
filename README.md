# Products.CLI

## Technologies used
For this uses a number of open source projects to work properly:
- [net6] - Target framework
- [YamlDotNet] - Library used to work with yml content.
- [Newtonsoft.Json] - Library used to work with json content.
- [FluentValidation] - Library used to make better validations.
- [xunit] - Library used to testing.
- [Moq] - Library used to mock dependencies.
- [FluentAssertions] - Library used to make better readable unit tests.

## Installation
This tool requires the installation of .net6 framework.

Install the tool as global application in your personal computer.
```sh
dotnet tool install --global Import.Tool.Ingestion --version 0.0.1
```

Instal the tool as local application on your local repository.
```sh
dotnet new tool-manifest
dotnet tool install --local Import.Tool.Ingestion --version 0.0.1
```

## Run tests locally
This is the command in order to run the tests locally.

```sh
dotnet test "Products.Cli.sln" -c Release
```

## Where to find the code.
- .github\workflows folder have got the pipelines:
    - integration.yml => restore, build and run tests for ubunto, windows and macOS latest versions.
    - publish.yml => Build and tests, pack and publish the tool on nuget.org, create a release on github and cleanup the artifact.

- src folder have got all the resources related with the implementation:
- test folder have got all the unit tests related with the implementation of src folder.
- database folder has got the README.md with solutions.
